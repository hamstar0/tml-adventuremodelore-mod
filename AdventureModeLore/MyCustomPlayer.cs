using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsMaps.Services.Maps;
using AdventureModeLore.Lore;


namespace AdventureModeLore {
	partial class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				var lore = ModContent.GetInstance<LoreEvents>();

				lore.InitializeOnCurrentPlayerEnter();

				if( AMLConfig.Instance.DebugModeFailedExpeditionsReveal ) {
					this.LoadFailedExpeditionMapMarkers();
				}
			}
		}

		private void LoadFailedExpeditionMapMarkers() {
			var myworld = ModContent.GetInstance<AMLWorld>();

			int i = 0;
			foreach( (int x, int y) exped in myworld.FailedExpeditions ) {
				if( MapMarkers.TryGetFullScreenMapMarker( "AMLExpedition_" + i, out _ ) ) {
					continue;
				}

				MapMarkers.SetFullScreenMapMarker(
					id: "AMLExpedition_" + i,
					tileX: exped.x,
					tileY: exped.y,
					icon: Main.itemTexture[ ItemID.Skull ],
					scale: 1f
				);

				i++;
			}
		}
	}
}
