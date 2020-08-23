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

		internal override void Begin_Internal( Cutscene parent ) {
			base.Begin_Internal( parent );
			this.OnBegin( (T)parent );
		}

		////

		protected virtual void OnBegin( T parent ) { }


		////////////////

		internal sealed override void End_Internal( Cutscene parent ) {
			base.End_Internal( parent );
			this.OnEnd_Any( (T)parent );
		}

		////

		protected virtual void OnEnd_Any( T parent ) { }
	}
}
