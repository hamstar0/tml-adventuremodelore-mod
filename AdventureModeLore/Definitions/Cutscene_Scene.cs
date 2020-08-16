using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal void SetCurrentScene_Player( Player player, int sceneIdx ) {
			if( this.CurrentSceneIdx == sceneIdx ) {
				return;
			}

			Scene prevScene = this.Scenes[this.CurrentSceneIdx];
			prevScene.End_Player_Internal( this, player );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Scenes[this.CurrentSceneIdx];
			currScene.Begin_Player_Internal( this, player );
		}

		internal void SetCurrentScene_World( int sceneIdx ) {
			if( this.CurrentSceneIdx == sceneIdx ) {
				return;
			}

			Scene prevScene = this.Scenes[ this.CurrentSceneIdx ];
			prevScene.End_World_Internal( this );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Scenes[ this.CurrentSceneIdx ];
			currScene.Begin_World_Internal( this );
		}


		////////////////

		public bool CanAdvanceCurrentScene_Any() {
			Scene scene = this.Scenes[ this.CurrentSceneIdx ];
			return (scene.DefersToHostForSync && Main.netMode == NetmodeID.Server)
				|| (!scene.DefersToHostForSync && Main.netMode == NetmodeID.MultiplayerClient);
		}

		private void AdvanceScene_Any( bool sync ) {
			this.CurrentSceneIdx++;

			if( this.CurrentSceneIdx < this.Scenes.Length ) {
				if( sync ) {
					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						AMLCutsceneNetData.Broadcast( this, this.CurrentSceneIdx );
					} else if( Main.netMode == NetmodeID.Server ) {
						AMLCutsceneNetData.SendToClients( -1, this, this.CurrentSceneIdx );
					}
				}
			} else {
				CutsceneManager.Instance.EndCutscene_Any( this.UniqueId, sync );
			}
		}
	}
}
