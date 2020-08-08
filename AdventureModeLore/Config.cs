using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace AdventureModeLore {
	class AMLConfig : ModConfig {
		public static AMLConfig Instance => ModContent.GetInstance<AMLConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeFreeMove { get; set; } = false;
	}
}
