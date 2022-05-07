using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using LostExpeditions;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static void InitializePKE() {
			PKEMeter.Logic.PKEGaugesGetter gaugeFunc = PKEMeter.PKEMeterAPI.GetGauge();

			float lastGaugedExpeditionProximityPercent = 0;
			int gaugeTimer = 0;

			PKEMeter.PKEMeterAPI.SetGauge( ( plr, pos ) => {
				PKEMeter.Logic.PKEGaugeValues existingGauge = gaugeFunc?.Invoke( plr, pos )
					?? new PKEMeter.Logic.PKEGaugeValues( 0f, 0f, 0f, 0f);

				if( gaugeTimer-- <= 0 ) {
					gaugeTimer = 10;
					lastGaugedExpeditionProximityPercent = LostExpeditionsAPI.GaugeUnexploredExpeditionsNear(pos, out _)
						?? 0f;
				}

				existingGauge.GreenSeenPercent = lastGaugedExpeditionProximityPercent;  // Green channel
				existingGauge.GreenRealPercent = existingGauge.GreenSeenPercent;

				return existingGauge;
			} );

			PKEMeter.PKEMeterAPI.SetMeterText( "AMLoreLostExpeditions", ( plr, pos, gauges ) =>
				new PKEMeter.Logic.PKETextMessage(
					message: "CLASS III ECTOPLASM CONCENTRATE VESSEL",
					color: Color.Lime * (0.5f + (Main.rand.NextFloat() * 0.5f)),
					priority: gauges.GreenRealPercent
				)
			);

			PKEMeter.PKEMeterAPI.SetPKEGreenTooltip( () => "CONCENTRATES" );
		}
	}
}