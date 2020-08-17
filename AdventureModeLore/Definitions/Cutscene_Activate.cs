using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Logic;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public virtual bool CanBegin( Player playsFor ) {
			var cutsceneMngr = CutsceneManager.Instance;

			if( this.ActiveInstances.ContainsKey( playsFor.whoAmI ) ) {
LogHelpers.LogOnce( "Fail 2a" );
				return false;
			}
			if( cutsceneMngr.HasCutscenePlayed_World(this.UniqueId) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			if( cutsceneMngr.HasCutscenePlayed_Player( this.UniqueId, playsFor ) ) {
				LogHelpers.LogOnce( "Fail 2b" );
				return false;
			}

			return true;
		}


		////////////////

		internal bool Begin_Internal( Player playsFor, int sceneIdx ) {
			this.ActiveInstances[playsFor.whoAmI] = this.Begin( playsFor, sceneIdx );

			if( !this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
				LogHelpers.Warn( "No active cutscene to engage player code for, playing for player "
					+playsFor.name+" ("+playsFor.whoAmI+")" );
				return false;
			}

			Scene scene = this.Scenes[ sceneIdx ];
			scene.Begin_Internal( this, playsFor );

			return true;
		}


		////////////////
		
		internal void End_Internal( Player playsFor ) {
			this.ActiveInstances[ playsFor.whoAmI ].OnEnd( playsFor );
			this.ActiveInstances.Remove( playsFor.whoAmI );
		}


		////////////////
		
		protected abstract ActiveCutscene Begin( Player playsFor, int sceneIdx );
	}
}
