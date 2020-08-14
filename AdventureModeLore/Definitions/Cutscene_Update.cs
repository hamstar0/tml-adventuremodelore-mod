using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		protected virtual bool UpdateForWorld() {
			return false;
		}

		protected virtual bool UpdateForPlayer( Player player ) {
			return false;
		}


		////

		internal void UpdateForWorld_Internal() {
			if( this.UpdateForWorld() ) {
				this.AdvanceSceneForWorld();
				return;
			}

			// Has scene ended?
			Scene scene = this.Scenes[ this.CurrentSceneIdx ];
			if( !scene.UpdateOnWorld_Internal(this) ) {
				return;
			}

			this.AdvanceSceneForWorld();
		}

		////

		internal void UpdateForPlayer_Internal( Player player ) {
			if( this.UpdateForPlayer(player) ) {
				this.AdvanceSceneForPlayer();
				return;
			}

			if( Main.netMode != NetmodeID.Server ) {
				if( player.whoAmI == Main.myPlayer ) {
					this.UpdateForLocal();
				}
			}
		}

		internal void UpdateForLocal() {
			Scene scene = this.Scenes[ this.CurrentSceneIdx ];

			// Has scene ended?
			if( !scene.UpdateOnLocal_Internal(this) ) {
				return;
			}
			if( scene.WorldControlsScenesOnly ) {
				return;
			}
			
			this.AdvanceSceneForPlayer();
		}
	}
}
