using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.UI;
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
		}

		public override void Unload() {
			AMLMod.Instance = null;
		}


		////////////////
		
		public override void PostUpdateInput() {
			if( Main.gameMenu ) { return; }
			if( AMLConfig.Instance.DebugModeFreeMove ) { return; }

			Cutscene nowCutscene = CutsceneManager.Instance?.GetCurrentPlayerCutscene( Main.LocalPlayer );
			if( nowCutscene?.IsSiezingControls() != true ) {
				return;
			}

			IDictionary<string, bool> keys = PlayerInput.Triggers.Current.KeyStatus;

			foreach( string key in keys.Keys.ToArray() ) {
				bool on = keys[key];

				nowCutscene.SiezeControl( key, ref on );
				keys[key] = on;
			}
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.gameMenu ) { return; }
			if( AMLConfig.Instance.DebugModeFreeMove ) { return; }

			Cutscene nowCutscene = CutsceneManager.Instance?.GetCurrentPlayerCutscene( Main.LocalPlayer );
			if( nowCutscene == null ) {
				return;
			}

			foreach( GameInterfaceLayer layer in layers ) {
				if( !nowCutscene.AllowInterfaceLayer(layer) ) {
					layer.Active = false;
				}
			}
		}
	}
}