using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public override void PostUpdateEverything() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				if( !Main.gameMenu ) {
					this.UpdateCutscenes_Internal();
				}
			}
		}

		////

		private void UpdateCutscenes_Internal() {
			if( ModContent.GetInstance<AMLWorld>()?.IsThisWorldAdventureMode != true ) {
				return;
			}

			CutsceneManager.Instance.Update_Host_Internal();
		}


		////////////////

		public override void PostUpdateInput() {
			if( Main.gameMenu ) { return; }
			if( AMLConfig.Instance.DebugModeFreeMove ) { return; }

			Cutscene nowCutscene = CutsceneManager.Instance?.GetCurrentCutscene_Player( Main.LocalPlayer );
			if( nowCutscene?.IsSiezingControls() != true ) {
				return;
			}

			IDictionary<string, bool> keys = PlayerInput.Triggers.Current.KeyStatus;

			foreach( string key in keys.Keys.ToArray() ) {
				bool on = keys[key];

				nowCutscene.SiezeControl_Internal( key, ref on );
				keys[key] = on;
			}
		}
	}
}