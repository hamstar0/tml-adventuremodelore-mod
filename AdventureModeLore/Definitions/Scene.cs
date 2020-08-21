using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		public abstract SceneID UniqueId { get; }



		////////////////

		protected Scene( bool worldControlsSyncOnly, bool defersToHostForSync )
			: base( worldControlsSyncOnly, defersToHostForSync ) { }


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
