using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public virtual bool CanBegin_World() {
			var myworld = ModContent.GetInstance<AMLWorld>();
			var cutsceneMngr = CutsceneManager.Instance;

			//if( myworld.CurrentPlayingCutscenes_World.Count > 0 ) {
			//	return false;
			//}	<- Multiple world cutscenes allowed?
			if( cutsceneMngr.IsCutsceneActivated_World(this.UniqueId) ) {
LogHelpers.LogOnce("Fail 2a");
				return false;
			}
			
			return true;
		}

		public virtual bool CanBegin_Player( Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myplayer.CurrentPlayingCutscene_Player != null ) {
LogHelpers.LogOnce("Fail 1b");
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivated_Player(this.UniqueId, player) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			
			return true;
		}


		////////////////

		internal bool Begin_World_Internal( int sceneIdx ) {
			//var myworld = ModContent.GetInstance<AMLWorld>();
			//if( myworld.CurrentPlayingCutscenes_World.Count > 0 ) {
			//	LogHelpers.Warn( "Cannot begin cutscene " + this.UniqueId + " (scene " + sceneIdx + ") while "
			//		+ string.Join(", ", myworld.CurrentPlayingCutscenes_World) + " is active." );
			//	return false;
			//}	<- Multiple world cutscenes allowed?

			this.CurrentSceneIdx = sceneIdx;
			this.Scenes[ sceneIdx ].Begin_World_Internal( this );

			this.ActiveForWorld = this.CreateActiveCutscene();
			this.ActiveForWorld.OnBegin_World( sceneIdx );

			return true;
		}

		internal void Begin_Player_Internal( Player player, int sceneIdx ) {
			this.CurrentSceneIdx = sceneIdx;

			Scene scene = this.Scenes[ sceneIdx ];
			scene.Begin_Player_Internal( this, player );
			
			this.ActiveForPlayer[ player.whoAmI ] = this.CreateActiveCutscene();
			this.ActiveForPlayer[ player.whoAmI ].OnBegin_Player( player, sceneIdx );
		}


		////////////////

		internal void OnEnd_World_Internal() {
			this.CurrentSceneIdx = 0;
			this.ActiveForWorld.OnEnd_World();
		}

		internal void OnEnd_Player_Internal( Player player ) {
			this.CurrentSceneIdx = 0;
			this.ActiveForPlayer[ player.whoAmI ]?.OnEnd_Player( player );
		}
	}
}
