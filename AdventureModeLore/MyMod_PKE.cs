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
			PKEMeter.Logic.PKEText meterTextFunc = PKEMeter.PKEMeterAPI.GetMeterText();
			PKEMeter.Logic.PKEGauge gaugeFunc = PKEMeter.PKEMeterAPI.GetGauge();
			int timer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				(float b, float g, float y, float r) existingGauge = gaugeFunc?.Invoke( plr, pos )
					?? (0f, 0f, 0f, 0f);

				if( timer-- <= 0 ) {
					timer = 10;
					AMLMod.LastGaugedExpeditionPercent = AMLMod.GaugeExpeditionsNear( pos ) ?? 0f;
				}

				existingGauge.g = AMLMod.LastGaugedExpeditionPercent;	// Green channel

				return existingGauge;
			} );

			PKEMeter.PKEMeterAPI.SetMeterText( ( plr, pos, gauges ) => {
				(string text, Color color) oldText = meterTextFunc?.Invoke( plr, pos, gauges )
					?? ("", Color.Transparent);

				if( !string.IsNullOrEmpty(oldText.text) ) {
					return oldText;
				}

				Color color = Color.Transparent;
				string myText = "";

				if( gauges.r > 0.75f ) {
					color = Color.Red;
					myText = "WARNING - CLASS V+ PKE-EMITTING ENTITIES AT LARGE";
				} else if( gauges.y > 0.75f ) {
					color = Color.Yellow;
					if( Main.rand.NextFloat() < 0.95f ) {
						myText = "CLASS VI TRANSDIM ELEVATED ORGANIC";
					} else {
						myText = "CLASS IX ULDTRADIM POST-MORTAL DEITY";
					}
				} else if( gauges.g > 0.75f ) {
					color = Color.Lime;
					myText = "CLASS III ECTOPLASM AGGREGATE VESSEL";
				} else if( gauges.b > 0.75f ) {
					color = Color.Blue;
					myText = "CLASS II ETHEREAL GEOFORM";
				}

				color = color * (0.5f + (Main.rand.NextFloat() * 0.5f));

				return (myText, color);
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