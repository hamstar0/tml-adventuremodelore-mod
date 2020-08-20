using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using Terraria.ID;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal void Update_Internal() {
			int playerCount = Main.player.Length;
			for( int i = 0; i < playerCount; i++ ) {
				Player plr = Main.player[i];
				if( plr?.active != true ) {
					continue;
				}

				this.UpdateActiveInstances_Internal( plr );

				if( Main.netMode == NetmodeID.SinglePlayer ) {
					break;
				}
			}
		}


		internal void UpdateActiveInstances_Internal( Player playsFor ) {
			// If the cutscene says so, continue to next scene
			if( !this.Update() ) {
				if( !this.CanAdvanceCurrentScene() ) {
					this.AdvanceScene( true );
				}
				return;
			}

			// If the current scene has ended, continue to next scene
			if( this.CurrentScene.Update_Internal( this, playsFor ) ) {
				if( !this.CanAdvanceCurrentScene() ) {
					this.AdvanceScene( true );
				}
			}
		}


		////

		/// <summary></summary>
		/// <returns>`false` ends the current scene, thus triggering the next (or else ending the cutscene).</returns>
		protected abstract bool Update();
	}
}
