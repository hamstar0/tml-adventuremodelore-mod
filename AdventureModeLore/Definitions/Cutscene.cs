using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public abstract CutsceneID UniqueId { get; }

		public abstract SceneID FirstSceneId { get; }

		////

		public int PlaysForWhom { get; private set; } = -1;

		////

		public Scene CurrentScene { get; protected set; } = null;



		////////////////

		protected Cutscene( Player playsFor ) {
			this.PlaysForWhom = playsFor?.whoAmI ?? -1;
		}

		////

		protected abstract Scene CreateScene( SceneID sceneId );

		protected abstract Scene CreateSceneFromNetwork( SceneID sceneId, AMLCutsceneNetData data );

		////

		public abstract AMLCutsceneNetData CreatePacketPayload( SceneID sceneId );


		////////////////
		
		public abstract bool CanBegin( out string result );

		////////////////

		internal void BeginCutscene_Internal( SceneID sceneId ) {
			this.CurrentScene = this.CreateScene( sceneId );
			this.CurrentScene.BeginScene_Internal( this );
		}

		internal void BeginCutsceneFromNetwork_Internal(
					SceneID sceneId,
					AMLCutsceneNetData data,
					Action<string> onSuccess,
					Action<string> onFail ) {
			this.CurrentScene = this.CreateSceneFromNetwork( sceneId, data );

			if( this.CurrentScene != null ) {
				this.CurrentScene.BeginScene_Internal( this );
				onSuccess( "Success." );
				return;
			}

			int retries = 0;
			Timers.SetTimer( 2, true, () => {
				this.CurrentScene = this.CreateSceneFromNetwork( sceneId, data );

				if( this.CurrentScene == null ) {
					if( retries++ < 1000 ) {
						return true;
					} else {
						onFail( "Timed out." );
						return false;
					}
				}

				this.CurrentScene.BeginScene_Internal( this );

				onSuccess( "Success." );
				return false;
			} );
		}


		////////////////

		internal void EndCutscene_Internal() {
			this.OnEnd();
		}

		////

		protected virtual void OnEnd() { }
	}
}
