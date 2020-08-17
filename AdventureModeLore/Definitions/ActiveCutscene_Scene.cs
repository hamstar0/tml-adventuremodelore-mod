using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class ActiveCutscene {
		internal void SetCurrentScene_Player( Player player, int sceneIdx ) {
			if( this.CurrentSceneIdx == sceneIdx ) {
				return;
			}

			Scene prevScene = this.Parent.Scenes[this.CurrentSceneIdx];
			prevScene.End_Player_Internal( this.Parent, player );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Parent.Scenes[this.CurrentSceneIdx];
			currScene.Begin_Player_Internal( this.Parent, player );
		}

		internal void SetCurrentScene_World( int sceneIdx ) {
			if( this.CurrentSceneIdx == sceneIdx ) {
				return;
			}

			Scene prevScene = this.Parent.Scenes[this.CurrentSceneIdx];
			prevScene.End_World_Internal( this.Parent );

			this.CurrentSceneIdx = sceneIdx;

			Scene currScene = this.Parent.Scenes[this.CurrentSceneIdx];
			currScene.Begin_World_Internal( this.Parent );
		}


		////////////////

		public bool CanAdvanceCurrentScene_Any() {
			Scene scene = this.Parent.Scenes[this.CurrentSceneIdx];

			return ( scene.DefersToHostForSync && Main.netMode == NetmodeID.Server )
				|| ( !scene.DefersToHostForSync && Main.netMode == NetmodeID.MultiplayerClient );
		}

		public bool AdvanceScene_Any( bool sync ) {
			Player playingFor = Main.player[this.PlayingForWho];
			if( playingFor == null ) {
				return false;
			}

			this.CurrentSceneIdx++;

			if( this.CurrentSceneIdx < this.Parent.Scenes.Length ) {
				if( sync ) {
					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						AMLCutsceneNetData.Broadcast( playingFor, this.Parent, this.CurrentSceneIdx );
					} else if( Main.netMode == NetmodeID.Server ) {
						AMLCutsceneNetData.SendToClients( playingFor, - 1, this.Parent, this.CurrentSceneIdx );
					}
				}
			} else {
				CutsceneManager.Instance.EndCutscene_Any( this.Parent.UniqueId, sync );
			}

			return true;
		}
	}
}
