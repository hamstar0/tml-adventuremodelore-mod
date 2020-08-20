using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Logic;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public bool CanAdvanceCurrentScene() {
			return ( this.CurrentScene.DefersToHostForSync && Main.netMode == NetmodeID.Server )
				|| ( !this.CurrentScene.DefersToHostForSync && Main.netMode == NetmodeID.MultiplayerClient );
		}


		////////////////

		internal void SetCurrentScene_NoSync( int sceneIdx ) {
			Player playsFor = Main.player[ this.PlaysForWhom ];

			this.CurrentScene.End_Internal( this, playsFor );

			if( sceneIdx < this.Scenes.Length ) {
				this.CurrentSceneIdx = sceneIdx;
				this.CurrentScene?.Begin_Internal( this, playsFor );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, playsFor, false );
			}
		}

		////

		public bool AdvanceScene( bool sync ) {
			Player playsFor = Main.player[ this.PlaysForWhom ];
			if( playsFor == null ) {
				return false;
			}

			this.CurrentSceneIdx++;

			if( this.CurrentSceneIdx < this.Scenes.Length ) {
				if( sync ) {
					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						AMLCutsceneNetData.Broadcast( this, playsFor, this.CurrentSceneIdx );
					} else if( Main.netMode == NetmodeID.Server ) {
						AMLCutsceneNetData.SendToClients( this, playsFor, this.CurrentSceneIdx, -1 );
					}
				}
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, playsFor, sync );
			}

			return true;
		}
	}
}
