using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public abstract CutsceneID UniqueId { get; }

		////

		public int PlaysForWhom { get; private set; } = -1;

		////

		public Scene CurrentScene { get; protected set; } = null;



		////////////////

		protected Cutscene( Player playsFor ) {
			this.PlaysForWhom = playsFor?.whoAmI ?? -1;
		}

		////

		protected abstract Scene CreateInitialScene();

		protected abstract Scene CreateScene( SceneID sceneId );

		////

		public abstract AMLCutsceneNetData CreatePacketPayload( SceneID sceneId );


		////////////////
		
		public abstract bool CanBegin();

		////////////////

		internal void BeginCutscene_Internal() {
			this.CurrentScene = this.CreateInitialScene();
			this.CurrentScene.BeginScene_Internal( this );
		}


		////////////////

		internal void EndCutscene_Internal() {
			this.OnEnd();
		}

		////

		protected virtual void OnEnd() { }
	}
}
