using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsMaps.Services.Maps;
using AdventureModeLore.Lore;
using LostExpeditions;

namespace AdventureModeLore {
	partial class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				var lore = ModContent.GetInstance<LoreEventManager>();

				lore.InitializeOnCurrentPlayerEnter();

				if( AMLConfig.Instance.DebugModeLostExpeditionsReveal ) {
					this.LoadLostExpeditionMapMarkers();
				}
			}
		}

		private void LoadLostExpeditionMapMarkers() {
			var myworld = ModContent.GetInstance<AMLWorld>();

			int i = 0;
			foreach( (int x, int y) tile in LostExpeditionsAPI.GetLostExpeditionLocations() ) {
				if( LostExpeditionsAPI.IsLostExpeditionFound(tile.x, tile.y) == true ) {
					continue;
				}

				if( MapMarkersAPI.TryGetFullScreenMapMarker("AMLExpedition_" + i, out _) ) {
					continue;
				}

				MapMarkersAPI.SetFullScreenMapMarker(
					id: "AMLExpedition_" + i,
					tileX: tile.x,
					tileY: tile.y,
					icon: Main.itemTexture[ ItemID.Skull ],
					scale: 1f
				);

				i++;
			}
		}
	}
}
