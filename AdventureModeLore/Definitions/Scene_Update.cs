using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool Update_Local_Internal( Cutscene parent ) {
			if( base.Update_Local_Internal(parent) ) {
				return true;
			}
			return this.Update_Local( (T)parent );
		}

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool Update_World_Internal( Cutscene parent ) {
			if( base.Update_World_Internal( parent ) ) {
				return true;
			}
			return this.Update_World( (T)parent );
		}

		////

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool Update_Local( T parent );

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool Update_World( T parent );
	}
}
