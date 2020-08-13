using System;
using Terraria;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		internal virtual bool UpdateOnLocal_Internal( Cutscene parent ) {
			//var animCam = CameraMover.Current;
			//if( animCam?.Name != this.SequenceName || !animCam.IsAnimating() || animCam.IsPaused ) {
			//	return false;
			//}
			return true;
		}

		internal virtual bool UpdateOnWorld_Internal( Cutscene parent ) {
			return true;
		}
	}
}
