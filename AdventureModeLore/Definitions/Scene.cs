using System;
using Terraria;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T, U, V> : Scene
				where T : Cutscene
				where U : MovieSet
				where V : AMLCutsceneNetData {
		public abstract SceneID UniqueId { get; }

		////

		protected U Set;



		////////////////

		protected Scene( bool defersToHostForSync, U set ) : base( defersToHostForSync ) {
			this.Set = set;
		}


		////////////////

		internal sealed override void BeginScene_Internal( Cutscene parent ) {
			base.BeginScene_Internal( parent );
			this.OnBegin( (T)parent );
		}

		////

		protected virtual void OnBegin( T parent ) { }


		////////////////

		internal sealed override void EndScene_Internal( Cutscene parent ) {
			base.EndScene_Internal( parent );
			this.OnEnd( (T)parent );
		}

		////

		protected virtual void OnEnd( T parent ) { }
	}
}
