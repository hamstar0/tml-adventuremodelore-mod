using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		protected virtual bool Update_World() {
			return false;
		}

		protected virtual bool Update_Player( Player player ) {
			return false;
		}


		////

		internal void Update_WorldAndHost_Internal() {
			ActiveCutscene actCut = this.ActiveForWorld;
			if( actCut == null ) {
				throw new ModHelpersException( "No active cutscene" );
			}

			Scene scene = this.Scenes[ actCut.CurrentSceneIdx ];

			// If the cutscene code says so, continue to next scene
			if( this.Update_World() ) {
				if( !actCut.CanAdvanceCurrentScene_Any() ) {
					actCut.AdvanceScene_Any( true );
				}
				return;
			}

			// If the current scene has ended, continue to next scene
			if( scene.Update_World_Internal(this) ) {
				if( !actCut.CanAdvanceCurrentScene_Any() ) {
					actCut.AdvanceScene_Any( true );
				}
			}
		}

		////

		internal void Update_Player_Internal( Player player ) {
			ActiveCutscene actCut = this.ActiveForPlayer[ player.whoAmI ];
			if( actCut == null ) {
				throw new ModHelpersException( "No active cutscene for player "+player.name+" ("+player.whoAmI+")" );
			}

			if( !this.Update_Player(player) ) {
				// Only the local player can advance scenes (if allowed)
				if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
					if( !actCut.CanAdvanceCurrentScene_Any() ) {
						actCut.AdvanceScene_Any( true );
					}
				}
			}
		}
	}
}
