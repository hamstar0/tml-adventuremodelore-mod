using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Logic;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public virtual bool CanBegin_World( Player playsFor ) {
			var cutsceneMngr = CutsceneManager.Instance;

			if( this.ActiveInstances.ContainsKey( playsFor.whoAmI ) ) {
LogHelpers.LogOnce( "Fail 2a" );
				return false;
			}
			if( cutsceneMngr.HasCutscenePlayed_World(this.UniqueId) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			
			return true;
		}

		public virtual bool CanBegin_Player( Player playsFor ) {
			var cutsceneMngr = CutsceneManager.Instance;

			if( this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
LogHelpers.LogOnce("Fail 1b");
				return false;
			}
			if( cutsceneMngr.HasCutscenePlayed_Player(this.UniqueId, playsFor) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			
			return true;
		}


		////////////////

		internal bool Begin_World_Internal( Player playsFor, int sceneIdx ) {
			this.ActiveInstances[ playsFor.whoAmI ] = this.Begin_World( playsFor, sceneIdx );

			this.Scenes[ sceneIdx ].Begin_World_Internal( this );

			return true;
		}

		internal bool Begin_Player_Internal( Player playsFor, int sceneIdx ) {
			if( !this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
				LogHelpers.Warn( "No active cutscene to engage player code for, playing for player "
					+playsFor.name+" ("+playsFor.whoAmI+")" );
				return false;
			}

			Scene scene = this.Scenes[ sceneIdx ];
			scene.Begin_Player_Internal( this, playsFor );

			return true;
		}


		////////////////
		
		internal void End_World_Internal( Player playsFor ) {
			this.ActiveInstances[ playsFor.whoAmI ].OnEnd_World();
			this.ActiveInstances.Remove( playsFor.whoAmI );
		}

		internal void End_Player_Internal( Player player ) {
			ActiveCutscene actCut = this.ActiveInstances[ player.whoAmI ];
			if( actCut == null ) {
				throw new ModHelpersException( "No active cutsene for player "+player.name+" ("+player.whoAmI+")" );
			}

			actCut.CurrentSceneIdx = 0;
			actCut.OnEnd_Player( player );
		}


		////////////////

		protected abstract ActiveCutscene Begin_World( Player playsForWho, int sceneIdx );
	}
}
