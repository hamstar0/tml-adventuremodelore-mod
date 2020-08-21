using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public int PlaysForWhom { get; }

		////

		public abstract CutsceneID UniqueId { get; }

		////

		public Scene CurrentScene { get; protected set; }



		////////////////

		protected Cutscene( Player playsFor ) {
			this.PlaysForWhom = playsFor.whoAmI;
		}

		protected abstract Scene CreateInitialScene();

		protected abstract Scene CreateScene( SceneID sceneId );

		////

		public abstract AMLCutsceneNetData CreatePacketPayload( SceneID sceneId );


		////////////////

		public abstract bool CanBegin();

		////////////////

		internal void Begin_Internal() {
			this.CurrentScene = this.CreateInitialScene();
			this.CurrentScene.Begin_Internal( this );
			this.OnBegin();
		}

		////

		protected virtual void OnBegin() { }


		////////////////

		internal void End_Internal() {
			this.OnEnd();
		}

		////

		protected virtual void OnEnd() { }
	}
}
