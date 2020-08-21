﻿using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene<T> : Scene where T : Cutscene {
		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal sealed override bool Update_Internal( Cutscene parent ) {
			if( base.Update_Internal( parent ) ) {
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
