﻿using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureModeLore {
	public partial class AMLConfig : ModConfig {
		public static AMLConfig Instance => ModContent.GetInstance<AMLConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeMiscInfo { get; set; } = false;

		public bool DebugModeLostExpeditionsReveal { get; set; } = false;

		////

		[Range(-1, 4048)]
		[DefaultValue( 256 )]
		public int LostExpeditionPKEDetectionTileRangeMax { get; set; } = 256;

		[Range(0, 4048)]
		[DefaultValue( 160 )]
		public int MinimumTileDistanceBetweenLostExpeditions { get; set; } = 160;



		////////////////

		public override ModConfig Clone() {
			var clone = (AMLConfig)this.MemberwiseClone();

			clone.CloneOverrides( this );

			return clone;
		}
	}
}
