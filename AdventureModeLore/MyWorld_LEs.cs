using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		public static float? GaugeUnexploredExpeditionsNear(
					Vector2 worldPos,
					out (int tileX, int tileY) nearestFEPos ) {
			nearestFEPos = default;

			var config = AMLConfig.Instance;
			int maxTileRange = config.Get<int>( nameof( config.LostExpeditionPKEDetectionTileRangeMax ) );
			if( maxTileRange <= 0 ) {
				return null;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			float nearestExpDist = float.MaxValue;

			foreach( ((int x, int y) tile, bool found) in myworld.LostExpeditions ) {
				if( found ) {
					continue;
				}

				var expPos = new Vector2( tile.x * 16, tile.y * 16 );
				float dist = (expPos - worldPos).Length();

				if( (expPos - worldPos).Length() < nearestExpDist ) {
					nearestFEPos = tile;
					nearestExpDist = dist;
				}
			}

			if( nearestExpDist >= float.MaxValue ) {
				return null;
			}

			float distPerc = nearestExpDist / ( (float)maxTileRange * 16f ); // within 256 tiles default
			float closePerc = Math.Max( 1f - distPerc, 0f );

			return closePerc;
		}


		////////////////

		public static void RevealExpeditionAt( int tileX, int tileY ) {
			var myworld = ModContent.GetInstance<AMLWorld>();

			myworld.LostExpeditions[ (tileX, tileY) ] = true;
			//return myworld.LostExpeditions.Remove( (tileX, tileY) );
		}
	}
}
