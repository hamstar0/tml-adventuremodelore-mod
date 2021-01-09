using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Maps;
using AdventureModeLore.Logic;


namespace AdventureModeLore {
	partial class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			LoreLogic.Initialize();

			if( AMLConfig.Instance.DebugModeFailedExpeditionsReveal ) {
				this.LoadFailedExpeditionMapMarkers();
			}
		}

		private void LoadFailedExpeditionMapMarkers() {
			var myworld = ModContent.GetInstance<AMLWorld>();

			int i = 0;
			foreach( (int x, int y) exped in myworld.FailedExpeditions ) {
				if( MapMarkers.TryGetFullScreenMapMarker( "AMLExpedition_" + i, out _ ) ) {
					continue;
				}

				MapMarkers.AddFullScreenMapMarker(
					tileX: exped.x,
					tileY: exped.y,
					id: "AMLExpedition_" + i,
					icon: Main.itemTexture[ ItemID.Skull ],
					scale: 1f
				);

				i++;
			}
		}
	}
}
