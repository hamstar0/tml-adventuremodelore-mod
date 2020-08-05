using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
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
			if( Main.gameMenu ) { return; }

			var myplayer = Main.LocalPlayer.GetModPlayer<AMLPlayer>();

			if( myplayer.CurrentPlayingCutsceneForPlayer != 0 ) {
				foreach( string key in PlayerInput.Triggers.Current.KeyStatus.Keys.ToArray() ) {
					if( key == "Inventory" ) {
						continue;	// don't overdo it!
					}
					PlayerInput.Triggers.Current.KeyStatus[key] = false;
				}
			}
		}
	}
}