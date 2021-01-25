using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static void InitializePKE() {
			PKEMeter.Logic.PKEGaugesGetter gaugeFunc = PKEMeter.PKEMeterAPI.GetGauge();

			float lastGaugedExpeditionPercent = 0;
			int gaugeTimer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				PKEMeter.Logic.PKEGaugeValues existingGauge = gaugeFunc?.Invoke( plr, pos )
					?? new PKEMeter.Logic.PKEGaugeValues( 0f, 0f, 0f, 0f);

				if( gaugeTimer-- <= 0 ) {
					gaugeTimer = 10;
					lastGaugedExpeditionPercent = AMLMod.GaugeExpeditionsNear( pos ) ?? 0f;
				}

				existingGauge.GreenPercent = lastGaugedExpeditionPercent;	// Green channel

				return existingGauge;
			} );

			PKEMeter.PKEMeterAPI.SetMeterText( "AMLoreFailedExpeditions", ( plr, pos, gauges ) =>
				new PKEMeter.Logic.PKETextMessage(
					message: "CLASS III ECTOPLASM CONCENTRATE VESSEL",
					color: Color.Lime * (0.5f + (Main.rand.NextFloat() * 0.5f)),
					priority: gauges.GreenPercent
				)
			);

			PKEMeter.PKEMeterAPI.SetPKEGreenTooltip( () => "CONCENTRATES" );
		}

		////

		public static float? GaugeExpeditionsNear( Vector2 worldPos ) {
			var config = AMLConfig.Instance;
			int maxTileRange = config.Get<int>( nameof( config.FailedExpeditionPKEDetectionTileRangeMax ) );
			if( maxTileRange <= 0 ) {
				return null;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			float nearestExpDist = float.MaxValue;

			foreach( (int x, int y) in myworld.FailedExpeditions ) {
				var expPos = new Vector2( x * 16, y * 16 );
				float dist = (expPos - worldPos).Length();

				if( (expPos - worldPos).Length() < nearestExpDist ) {
					nearestExpDist = dist;
				}
			}

			if( nearestExpDist >= float.MaxValue ) {
				return null;
			}

			float distPerc = nearestExpDist / ((float)maxTileRange * 16f); // within 256 tiles default
			float closePerc = Math.Max( 1f - distPerc, 0f );

			return closePerc;
		}
	}
}