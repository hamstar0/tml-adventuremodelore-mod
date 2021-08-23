using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore {
	partial class AMLPlayer : ModPlayer {
		private void DiscoverNearbyFEsIf() {
			(int tileX, int tileY) nearestFEPos;
			if( AMLWorld.GaugeExpeditionsNear(this.player.MountedCenter, out nearestFEPos) == null ) {
				return;
			}

			int minTileDist = 6;
			int minTileDistSqr = minTileDist * minTileDist;

			int xDiff = nearestFEPos.tileX - ((int)this.player.MountedCenter.X / 16);
			int yDiff = nearestFEPos.tileY - ((int)this.player.MountedCenter.Y / 16);
			int distSqr = (xDiff * xDiff) + (yDiff * yDiff);

			if( distSqr <= minTileDistSqr ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					Main.NewText( "You discovered a lost expedition!", Color.Lime );
				} else {
					Main.NewText( "Lost expedition discovered!", Color.Lime );
				}

				AMLWorld.RevealExpeditionAt( nearestFEPos.tileX, nearestFEPos.tileY );
			}
		}
	}
}
