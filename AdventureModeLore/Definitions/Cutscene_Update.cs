using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal void Update_Internal() {
			// If the cutscene says so, continue to next scene
			if( !this.Update() ) {
				if( !this.CanAdvanceCurrentScene() ) {
					this.AdvanceScene( true );
				}
				return;
			}

			// If the current scene has ended, continue to next scene
			if( this.CurrentScene.Update_Internal(this) ) {
				if( !this.CanAdvanceCurrentScene() ) {
					this.AdvanceScene( true );
				}
			}
		}


		////

		/// <summary>`false` ends the current scene, thus triggering the next (or else ending the cutscene).</summary>
		/// <returns></returns>
		protected abstract bool Update();
	}
}
