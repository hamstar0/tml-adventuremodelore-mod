using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Cutscenes.Intro {
	public abstract partial class Cutscene {
		protected virtual void UpdateForWorld() { }

		protected virtual void UpdateForPlayer( Player player ) { }

		////

		internal void UpdateForWorld_Internal() {
			this.UpdateForWorld();

			if( !this.Scenes[this.CurrentScene].UpdateOnWorld_Internal() ) {
				return;
			}

			this.CurrentScene++;

			if( this.CurrentScene < this.Scenes.Length ) {
				AMLCutsceneNetData.SendToClients( -1, this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}

		internal void UpdateForPlayer_Internal( Player player ) {
			this.UpdateForPlayer( player );

			if( Main.netMode != NetmodeID.Server ) {
				if( player.whoAmI == Main.myPlayer ) {
					this.UpdateForLocal();
				}
			}
		}

		////

		internal void UpdateForLocal() {
			Scene scene = this.Scenes[ this.CurrentScene ];
			if( !scene.MustSync ) {
				return;
			}

			// Has scene ended?
			if( !scene.UpdateOnLocal_Internal() ) {
				return;
			}

			this.CurrentScene++;

			if( this.CurrentScene < this.Scenes.Length ) {
				AMLCutsceneNetData.Broadcast( this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}
	}
}
