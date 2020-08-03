using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using AdventureModeLore.Cutscenes;


namespace AdventureModeLore {
	public class AMLMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremodelore-mod";


		////////////////

		public static AMLMod Instance { get; private set; }



		////////////////

		public AMLMod() {
			AMLMod.Instance = this;
			CutsceneManager.Instance = new CutsceneManager();
		}

		public override void Unload() {
			AMLMod.Instance = null;
			CutsceneManager.Instance = null;
		}


		////////////////

		public override void PostUpdateInput() {
			var cutsceneMngr = CutsceneManager.Instance;

			if( cutsceneMngr != null && cutsceneMngr.CurrentlyPlayingCutsceneID != 0 ) {
				foreach( string key in PlayerInput.Triggers.Current.KeyStatus.Keys.ToArray() ) {
					PlayerInput.Triggers.Current.KeyStatus[key] = false;
				}
			}
		}
	}
}