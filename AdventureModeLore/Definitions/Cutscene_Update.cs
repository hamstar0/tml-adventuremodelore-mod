using System;
using Terraria;
using Terraria.ID;
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
			Scene scene = this.Scenes[ this.CurrentSceneIdx ];

			// If the cutscene code says so, continue to next scene
			if( this.Update_World() ) {
				if( !this.CanAdvanceCurrentScene_Any() ) {
					this.AdvanceScene_Any( true );
				}
				return;
			}

			// If the current scene has ended, continue to next scene
			if( scene.Update_World_Internal(this) ) {
				if( !this.CanAdvanceCurrentScene_Any() ) {
					this.AdvanceScene_Any( true );
				}
			}
		}

		////

		internal void Update_Player_Internal( Player player ) {
			if( this.Update_Player(player) ) {
				if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
					if( !this.CanAdvanceCurrentScene_Any() ) {
						this.AdvanceScene_Any( true );
					}
				}
				return;
			}
		}
	}
}
