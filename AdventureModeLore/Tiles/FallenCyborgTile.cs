using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Services.Timers;
using ModLibsUtilityContent.Tiles;
using Messages;
using PKEMeter;
using PKEMeter.Logic;


namespace AdventureModeLore.Tiles {
	public class MyFallenCyborgTile : ILoadable {
		public static void ShowMessage() {
			string id = "AML_Lore_FallenCyborg";
			string story = "These poor guys can sometimes be found simply lying around in the deep places of the"
					+ " island, either in disrepair, or maybe simply out of juice. Without the means of repairing"
					+ " their fine circuitry, or even simply giving them a jump, the precise purpose for their"
					+ " presence here cannot be directly discerned. Are they friendly? Are they here to combat"
					+ " evil? Hopefully time will tell!";
			string addenum = "In the meantime, maybe a more pragmatic move would be to salvage their parts for your"
					+ " own use. They are just machines after all... right?";

			MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
				MessagesAPI.AddMessage(
					title: "Fallen Cyborgs",
					description: story + "\n \n" + addenum,
					modOfOrigin: AMLMod.Instance,
					alertPlayer: MessagesAPI.IsUnread( id ),
					isImportant: false,
					parentMessage: MessagesAPI.StoryLoreCategoryMsg,
					id: id
				);
			} );
		}



		////////////////

		private bool IsHoveringCyborg = false;



		////////////////
		
		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() {
			var mytile = ModContent.GetInstance<FallenCyborgTile>();

			mytile.KillMultiTile_Hook = this.KillMultiTile;
			mytile.MouseOverFar_Hook = this.MouseOverFar;

			//

			PKEMeterAPI.SetScannable(
				name: "FallenCyborgTile",
				scannable: new PKEScannable(
					canScan: (x, y) => this.IsHoveringCyborg,
					onScanCompleteAction: MyFallenCyborgTile.ShowMessage
				),
				allowRepeat: false,
				runIfComplete: true
			);
		}


		////////////////

		private void KillMultiTile( int x, int y, int frameX, int frameY ) {
			for( int i = 0; i < 2; i++ ) {
				Item.NewItem(
					X: x * 16,
					Y: y * 16,
					Height: 3 * 16,
					Width: 2 * 16,
					Type: ModContent.ItemType<ModLibsUtilityContent.Items.MagitechScrapItem>()
				);
			}
		}


		private void MouseOverFar( int i, int j ) {
			this.IsHoveringCyborg = true;

			Timers.SetTimer( "CyborgTileHover", 3, false, () => {
				this.IsHoveringCyborg = false;

				return false;
			} );
		}
	}
}
