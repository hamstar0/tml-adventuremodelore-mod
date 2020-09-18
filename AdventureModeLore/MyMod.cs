using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremodelore-mod";


		////////////////

		public static AMLMod Instance { get; private set; }



		////////////////

		private int _CheckTimer = 0;



		////////////////

		public AMLMod() {
			AMLMod.Instance = this;
		}

		public override void Unload() {
			AMLMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			if( this._CheckTimer-- <= 0 ) {
				this._CheckTimer = 60;
			} else {
				return;
			}

			ObjectiveLogic.Run();
		}
	}
}