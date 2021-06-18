using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace AdventureModeLore {
	public partial class AMLConfig : ModConfig {
		public static AMLConfig Instance { get; internal set; }



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeFailedExpeditionsReveal { get; set; } = false;

		////

		[Range(-1, 4048)]
		[DefaultValue( 256 )]
		public int FailedExpeditionPKEDetectionTileRangeMax { get; set; } = 256;

		[Range(0, 4048)]
		[DefaultValue( 160 )]
		public int MinimumTileDistanceBetweenFailedExpeditions { get; set; } = 160;



		////////////////

		public override ModConfig Clone() {
			var clone = (AMLConfig)this.MemberwiseClone();

			clone.CloneOverrides( this );

			return clone;
		}
	}
}
