using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		protected Scene( bool worldControlsSyncOnly, bool defersToHostForSync )
			: base( worldControlsSyncOnly, defersToHostForSync ) { }


		////////////////

		internal override void Begin_Player_Internal( Cutscene parent, Player player ) {
			base.Begin_Player_Internal( parent, player );
			this.OnBegin_Player( (T)parent, player );
		}

		internal sealed override void Begin_World_Internal( Cutscene parent ) {
			base.Begin_World_Internal( parent );
			this.OnBegin_World( (T)parent );
		}

		////

		protected virtual void OnBegin_Player( T parent, Player player ) { }

		protected virtual void OnBegin_World( T parent ) { }

		////////////////

		internal sealed override void End_World_Internal( Cutscene parent ) {
			base.End_World_Internal( parent );
			this.OnEnd_World( (T)parent );
		}
		
		internal sealed override void End_Player_Internal( Cutscene parent, Player player ) {
			base.End_Player_Internal( parent, player );
			this.OnEnd_Player( (T)parent, player );
		}

		////

		protected virtual void OnEnd_Player( T parent, Player player ) { }

		protected virtual void OnEnd_World( T parent ) { }
	}
}
