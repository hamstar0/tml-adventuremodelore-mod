using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		private static float LastGaugedExpeditionPercent = 0f;



		////////////////

		public static void InitializePKE() {
			PKEMeter.PKEGauge gauge = PKEMeter.PKEMeterAPI.GetGauge();
			int timer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				(float b, float g, float y, float r) existingGauge = gauge?.Invoke( plr, pos )
					?? (0f, 0f, 0f, 0f);

				if( timer-- <= 0 ) {
					timer = 10;
					AMLMod.LastGaugedExpeditionPercent = AMLMod.GaugeExpeditionsNear( pos ) ?? 0f;
				}

				existingGauge.g = AMLMod.LastGaugedExpeditionPercent;	// Green channel

				return existingGauge;
			} );
		}

		////

		public static float? GaugeExpeditionsNear( Vector2 worldPos ) {
			var config = AMLConfig.Instance;
			int maxTileRange = config.Get<int>( nameof( config.FailedExpeditionPKEDetectionTileRangeMax ) );
			if( maxTileRange <= 0 ) {
				return null;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			(int x, int y)? nearestExp = null;
			float nearestExpDist = float.MaxValue;

			foreach( (int x, int y) in myworld.FailedExpeditions ) {
				var expPos = new Vector2( x * 16, y * 16 );

				if( (expPos - worldPos).Length() < nearestExpDist ) {
					nearestExp = (x, y);
				}
			}

			if( !nearestExp.HasValue ) {
				return null;
			}

			float distPerc = nearestExpDist / ((float)maxTileRange * 16f); // within 256 tiles default
			float closePerc = Math.Max( 1f - distPerc, 0f );

			return closePerc;
		}
	}
}