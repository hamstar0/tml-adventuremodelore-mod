using System;
using Terraria;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T, U, V> : Scene
				where T : Cutscene
				where U : MovieSet
				where V : AMLCutsceneNetData {
		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool UpdateScene_Internal( Cutscene parent ) {
			if( base.UpdateScene_Internal( parent ) ) {
				return true;
			}
			return this.Update( (T)parent );
		}


		////

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool Update( T parent );
	}
}
