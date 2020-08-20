using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal static T Create<T>( Player playsFor ) where T : Cutscene {
			return Activator.CreateInstance( typeof(T), new object[] { playsFor } ) as T;
		}



		////////////////

		protected int CurrentSceneIdx = 0;

		////

		public int PlaysForWhom { get; }

		////

		public abstract CutsceneID UniqueId { get; }

		protected Scene[] Scenes { get; }

		////

		public Scene CurrentScene => this.CurrentSceneIdx < this.Scenes.Length
			? this.Scenes[ this.CurrentSceneIdx ]
			: null;



		////////////////

		protected Cutscene( Player playsFor ) {
			this.PlaysForWhom = playsFor.whoAmI;
			this.Scenes = this.LoadScenes();

			this.CurrentScene.Begin_Internal( this, playsFor );
		}

		protected abstract Scene[] LoadScenes();

		////

		public abstract AMLCutsceneNetData CreatePacketPayload( int sceneIdx );


		////////////////

		internal void End_Internal() {
			this.OnEnd();
		}

		////

		protected virtual void OnEnd() { }
	}
}
