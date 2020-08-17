using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		protected abstract partial class ActiveCutscene {
			internal void SetCurrentScene( int sceneIdx ) {
				if( this.CurrentSceneIdx == sceneIdx ) {
					return;
				}

				Scene prevScene = this.Parent.Scenes[this.CurrentSceneIdx];
				Scene currScene = this.Parent.Scenes[sceneIdx];
				Player playsFor = Main.player[ this.PlaysForWhom ];

				prevScene.End_Internal( this.Parent, playsFor );

				this.CurrentSceneIdx = sceneIdx;

				currScene.Begin_Internal( this.Parent, playsFor );
			}


			////////////////

			public bool CanAdvanceCurrentScene() {
				Scene scene = this.Parent.Scenes[this.CurrentSceneIdx];

				return ( scene.DefersToHostForSync && Main.netMode == NetmodeID.Server )
					|| ( !scene.DefersToHostForSync && Main.netMode == NetmodeID.MultiplayerClient );
			}

			////

			public bool AdvanceScene( bool sync ) {
				Player playsFor = Main.player[ this.PlaysForWhom ];
				if( playsFor == null ) {
					return false;
				}

				this.CurrentSceneIdx++;

				if( this.CurrentSceneIdx < this.Parent.Scenes.Length ) {
					if( sync ) {
						if( Main.netMode == NetmodeID.MultiplayerClient ) {
							AMLCutsceneNetData.Broadcast( playsFor, this.Parent, this.CurrentSceneIdx );
						} else if( Main.netMode == NetmodeID.Server ) {
							AMLCutsceneNetData.SendToClients( playsFor, -1, this.Parent, this.CurrentSceneIdx );
						}
					}
				} else {
					CutsceneManager.Instance.EndCutscene( this.Parent.UniqueId, playsFor, sync );
				}

				return true;
			}
		}
	}
}
