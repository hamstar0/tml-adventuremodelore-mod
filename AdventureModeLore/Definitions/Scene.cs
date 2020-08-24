using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T, U>
				: Scene where T : Cutscene where U : MovieSet {
		public abstract SceneID UniqueId { get; }

		////

		protected U Set;



		////////////////

		protected Scene( bool worldControlsSyncOnly, bool defersToHostForSync, U set )
					: base( worldControlsSyncOnly, defersToHostForSync ) {
			this.Set = set;
		}


		////////////////

		internal override void BeginScene_Internal( Cutscene parent ) {
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
