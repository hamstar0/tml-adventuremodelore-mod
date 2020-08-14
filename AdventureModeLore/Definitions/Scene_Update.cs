using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool UpdateOnLocal_Internal( Cutscene parent ) {
			if( base.UpdateOnLocal_Internal(parent) ) {
				return true;
			}
			return this.UpdateOnLocal( (T)parent );
		}

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool UpdateOnWorld_Internal( Cutscene parent ) {
			if( base.UpdateOnWorld_Internal( parent ) ) {
				return true;
			}
			return this.UpdateOnWorld( (T)parent );
		}

		////

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool UpdateOnLocal( T parent );

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool UpdateOnWorld( T parent );
	}
}
