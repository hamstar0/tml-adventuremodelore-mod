using Messages;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace AdventureModeLore.Tiles {
	public class FallenCyborgTile : ModTile {
		public override void SetDefaults() {
			Main.tileLighted[ this.Type ] = true;
			Main.tileFrameImportant[ this.Type ] = true;
			Main.tileObsidianKill[ this.Type ] = true;
			//Main.tileNoAttach[ this.Type ] = true;

			TileObjectData.newTile.CopyFrom( TileObjectData.Style2xX );
			TileObjectData.addTile( this.Type );

			ModTranslation name = this.CreateMapEntryName();
			name.SetDefault( "Fallen Cyborg" );

			this.AddMapEntry( new Color(160, 160, 180), name );

			this.dustType = 1;
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { this.Type };
		}


		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}


		////////////////
		
		public override void KillMultiTile( int x, int y, int frameX, int frameY ) {
			Mod riMod = ModLoader.GetMod( "RuinedItems" );
			if( riMod == null ) {
				return;
			}

			for( int i=0; i<3; i++ ) {
				Item.NewItem(
					X: x * 16,
					Y: y * 16,
					Height: 3 * 16,
					Width: 2 * 16,
					Type: riMod.ItemType( "MagitechScrapItem" )
				);
			}
		}


		////////////////

		public override void MouseOverFar( int i, int j ) {
			MessagesAPI.AddMessage(
				title: "Fallen Cyborgs",
				description: "These poor guys can sometimes be found simply lying around in the deep places of the"
					+" island, either in disrepair, or maybe simply out of juice. Without the means of repairing"
					+" their fine circuitry, or even simply giving them a jump, the precise purpose for their"
					+" presence on here cannot be directly discerned. Are they friendly? Are they here to combat"
					+" the plague? Hopefully time will tell!"
					+"\n \nIn the meantime, maybe a more pragmatic move would be to salvage their parts for your"
					+" own use. They are just machines after all, right...?",
				modOfOrigin: AMLMod.Instance,
				alertPlayer: true,
				isImportant: false,
				parentMessage: MessagesAPI.StoryLoreCategoryMsg,
				id: "AML_Lore_FallenCyborg"
			);
		}
	}
}
