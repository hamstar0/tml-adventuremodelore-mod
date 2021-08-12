using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		public static float? GaugeExpeditionsNear( Vector2 worldPos, out (int tileX, int tileY) nearestFEPos ) {
			nearestFEPos = default;

			var config = AMLConfig.Instance;
			int maxTileRange = config.Get<int>( nameof( config.AbandonedExpeditionPKEDetectionTileRangeMax ) );
			if( maxTileRange <= 0 ) {
				return null;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			float nearestExpDist = float.MaxValue;

			foreach( (int x, int y) in myworld.AbandonedExpeditions ) {
				var expPos = new Vector2( x * 16, y * 16 );
				float dist = (expPos - worldPos).Length();

				if( (expPos - worldPos).Length() < nearestExpDist ) {
					nearestFEPos = (x, y);
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

		public static bool RemoveExpeditionAt( int tileX, int tileY ) {
			var myworld = ModContent.GetInstance<AMLWorld>();

			return myworld.AbandonedExpeditions.Remove( (tileX, tileY) );
		}
	}
}
