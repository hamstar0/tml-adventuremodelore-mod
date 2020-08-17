using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
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

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.gameMenu ) { return; }
			if( AMLConfig.Instance.DebugModeFreeMove ) { return; }

			Cutscene nowCutscene = CutsceneManager.Instance?.GetCurrentCutscene_Player( Main.LocalPlayer );
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