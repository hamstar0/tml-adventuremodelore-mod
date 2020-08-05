using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Cutscenes;
using AdventureModeLore.Cutscenes.Intro;


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
			if( Main.gameMenu ) { return; }
			if( AMLConfig.Instance.DebugModeFreeMove ) { return; }

			Cutscene currCutscene = CutsceneManager.Instance?.GetCurrentPlayerCutscene( Main.LocalPlayer );
			if( currCutscene?.IsSiezingControls() != true ) {
				return;
			}

			IDictionary<string, bool> keys = PlayerInput.Triggers.Current.KeyStatus;

			foreach( string key in keys.Keys.ToArray() ) {
				bool on = keys[key];

				currCutscene.SiezeControl( key, ref on );
				keys[key] = on;
			}
		}
	}
}