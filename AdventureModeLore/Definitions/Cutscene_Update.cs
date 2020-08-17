using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using Terraria.ID;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		internal void Update_Internal() {
			int playerCount = Main.player.Length;
			for( int i=0; i<playerCount; i++ ) {
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
			if( !this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
				return;
			}

			ActiveCutscene actCut = this.ActiveInstances[ playsFor.whoAmI ];

			// If the cutscene says so, continue to next scene
			if( !actCut.Update() ) {
				if( !actCut.CanAdvanceCurrentScene() ) {
					actCut.AdvanceScene( true );
				}
				return;
			}

			Scene scene = this.Scenes[ actCut.CurrentSceneIdx ];

			// If the current scene has ended, continue to next scene
			if( scene.Update_Internal(this, playsFor) ) {
				if( !actCut.CanAdvanceCurrentScene() ) {
					actCut.AdvanceScene( true );
				}
			}
		}
	}
}
