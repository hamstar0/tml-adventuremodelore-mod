using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Cutscenes.Intro {
	public abstract partial class Cutscene {
		protected virtual void UpdateForWorld() { }

		protected virtual void UpdateForPlayer( AMLPlayer myplayer ) { }

		////

		internal void UpdateForWorld_Internal() {
			this.UpdateForWorld();

			if( !this.Scenes[this.CurrentScene].UpdateOnWorld_Internal() ) {
				return;
			}

			this.CurrentScene++;
			if( this.CurrentScene < (this.Scenes.Length - 1) ) {
				this.CurrentScene++;
				AMLCutsceneNetData.SendToClients( -1, this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}

		internal void UpdateForPlayer_Internal( AMLPlayer myplayer ) {
			this.UpdateForPlayer( myplayer );

			if( Main.netMode != NetmodeID.Server ) {
				if( myplayer.player.whoAmI == Main.myPlayer ) {
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

			if( this.CurrentScene < (this.Scenes.Length - 1) ) {
				this.CurrentScene++;
				AMLCutsceneNetData.Broadcast( this, this.CurrentScene );
			} else {
				CutsceneManager.Instance.EndCutscene( this.UniqueId, true );
			}
		}
	}
}
