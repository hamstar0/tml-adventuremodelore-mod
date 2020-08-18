using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		protected Scene( bool worldControlsSyncOnly, bool defersToHostForSync )
			: base( worldControlsSyncOnly, defersToHostForSync ) { }


		////////////////

		internal override void Begin_Internal( Cutscene parent, Player playsFor ) {
			base.Begin_Internal( parent, playsFor );
			this.OnBegin( (T)parent, playsFor );
		}

		////

		protected virtual void OnBegin( T parent, Player playsFor ) { }

		////////////////

		internal sealed override void End_Internal( Cutscene parent, Player playsFor ) {
			base.End_Internal( parent, playsFor );
			this.OnEnd_Any( (T)parent, playsFor );
		}

		////

		protected virtual void OnEnd_Any( T parent, Player playsFor ) { }
	}
}
