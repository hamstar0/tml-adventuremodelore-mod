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
	}
}
