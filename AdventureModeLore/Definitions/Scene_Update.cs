using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		internal bool UpdateOnLocal_Internal() {
			//var animCam = CameraMover.Current;
			//if( animCam?.Name != this.SequenceName || !animCam.IsAnimating() || animCam.IsPaused ) {
			//	return false;
			//}
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
