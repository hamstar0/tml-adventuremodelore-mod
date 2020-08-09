using System;
using Terraria;
using HamstarHelpers.Services.Camera;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		internal bool UpdateOnLocal_Internal() {
			if( AnimatedCamera.Instance.CurrentMoveSequence != this.SequenceName ) {
				return false;
			}
			return this.UpdateOnLocal();
		}

		internal bool UpdateOnWorld_Internal() {
			return this.UpdateOnWorld();
		}

		////

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool UpdateOnLocal();

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		protected abstract bool UpdateOnWorld();
	}
}
