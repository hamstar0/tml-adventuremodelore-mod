using Terraria;
using Terraria.ModLoader;


namespace AdventureModeLore {
	public class AMLMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremodelore-mod";


		////////////////

		public static AMLMod Instance { get; private set; }



		////////////////

		public AMLMod() {
			AMLMod.Instance = this;
		}


		public override void Unload() {
			AMLMod.Instance = null;
		}
	}
}