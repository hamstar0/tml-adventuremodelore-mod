using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal void SetCurrentSceneForPlayer( Player player, int sceneIdx ) {
			Scene prevScene = this.Scenes[this.CurrentSceneIdx];
			prevScene.EndForPlayer_Internal( this, player );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Scenes[this.CurrentSceneIdx];
			currScene.BeginOnPlayer_Internal( this, player );
		}

		internal void SetCurrentSceneForWorld( int sceneIdx ) {
			Scene prevScene = this.Scenes[ this.CurrentSceneIdx ];
			prevScene.EndForWorld_Internal( this );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Scenes[ this.CurrentSceneIdx ];
			currScene.BeginOnWorld_Internal( this );
		}
		

		////////////////

		private void AdvanceSceneForWorld() {
			this.CurrentSceneIdx++;

			if( this.CurrentSceneIdx < this.Scenes.Length ) {
				AMLCutsceneNetData.SendToClients( -1, this, this.CurrentSceneIdx );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}

		private void AdvanceSceneForPlayer() {
			this.CurrentSceneIdx++;

			if( this.CurrentSceneIdx < this.Scenes.Length ) {
				AMLCutsceneNetData.Broadcast( this, this.CurrentSceneIdx );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}
	}
}
