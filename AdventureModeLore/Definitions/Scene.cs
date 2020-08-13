using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		protected Scene( bool worldControlsSyncOnly ) : base( worldControlsSyncOnly ) {
		}


		////////////////

		internal sealed override void BeginOnPlayer_Internal( Cutscene parent, Player player ) {
			base.BeginOnPlayer_Internal( parent, player );
			this.OnBeginOnPlayer( (T)parent, player );
		}

		internal sealed override void BeginOnWorld_Internal( Cutscene parent ) {
			base.BeginOnWorld_Internal( parent );
			this.OnBeginOnWorld( (T)parent );
		}

		////

		protected virtual void OnBeginOnPlayer( T parent, Player player ) { }

		protected virtual void OnBeginOnWorld( T parent ) { }

		////////////////

		internal sealed override void EndForWorld_Internal( Cutscene parent ) {
			base.EndForWorld_Internal( parent );
			this.OnEndOnWorld( (T)parent );
		}
		
		internal sealed override void EndForPlayer_Internal( Cutscene parent, Player player ) {
			base.EndForPlayer_Internal( parent, player );
			this.OnEndOnPlayer( (T)parent, player );
		}

		////

		protected virtual void OnEndOnPlayer( T parent, Player player ) { }

		protected virtual void OnEndOnWorld( T parent ) { }
	}
}
