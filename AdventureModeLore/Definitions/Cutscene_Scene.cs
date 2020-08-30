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
				|| ( !this.CurrentScene.DefersToHostForSync && Main.netMode != NetmodeID.Server );
		}


		////////////////

		internal void SetCurrentScene_NoSync( SceneID uid ) {
			this.CurrentScene.EndScene_Internal( this );
			this.CurrentScene = this.CreateScene( uid );
			this.CurrentScene.BeginScene_Internal( this );
		}

		////

		public void AdvanceScene( bool sync ) {
			SceneID nextUid = this.CurrentScene.GetNextSceneId();

			if( nextUid != null ) {
				this.CurrentScene = this.CreateScene( nextUid );

				if( sync ) {
					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						AMLCutsceneNetData.Broadcast( this, nextUid );
					} else if( Main.netMode == NetmodeID.Server ) {
						AMLCutsceneNetData.SendToClients( this, nextUid, -1 );
					}
				}
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, this.PlaysForWhom, sync );
			}
		}
	}
}
