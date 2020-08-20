using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public Scene GetCurrentScene( Player playsFor ) {
			if( playsFor?.active != true ) {
				LogHelpers.Warn( "Inactive player #" + playsFor.whoAmI );
				return null;
			}

			ActiveCutscene currActCut = this.ActiveInstances.GetOrDefault( Main.myPlayer );
			if( currActCut == null ) {
				LogHelpers.Warn( "No active cutscene for player #" + playsFor.whoAmI );
				return null;
			}

			return this.Scenes[ currActCut.CurrentSceneIdx ];
		}


		////////////////

		internal bool SetScene_Internal( Player playsFor, int sceneIdx ) {
			if( !this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
				return false;
			}

			this.ActiveInstances[ playsFor.whoAmI ].SetCurrentScene( sceneIdx );
			return true;
		}
	}
}
