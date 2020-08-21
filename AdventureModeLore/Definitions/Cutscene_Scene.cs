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

		internal void SetCurrentScene_NoSync( SceneID uid ) {
			this.CurrentScene.End_Internal( this );
			this.CurrentScene = this.CreateScene( uid );
			this.CurrentScene.Begin_Internal( this );
		}

		////

		public bool AdvanceScene( bool sync ) {
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
				CutsceneManager.Instance.EndCutscene( this.UniqueId, Main.player[this.PlaysForWhom], sync );
			}

			return true;
		}
	}
}
