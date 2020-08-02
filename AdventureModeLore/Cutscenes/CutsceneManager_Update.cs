using System;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager : ILoadable {
		internal void UpdateForWorld() {
			if( this.CurrentActiveCutscene == 0 ) {
				this.UpdateToActivate();
			} else {
				this.Cutscenes[this.CurrentActiveCutscene].UpdateForWorld();
			}
		}

		private void UpdateToActivate() {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( !this.CanBeginForWorld( cutscene.UniqueId ) ) {
					continue;
				}

				int plrMax = Main.player.Length;
				for( int i = 0; i < plrMax; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true ) { continue; }

					this.BeginCutscene( cutscene.UniqueId, plr );
					break;
				}
			}
		}


		////////////////

		internal void UpdateForPlayer( AMLPlayer myplayer ) {
			if( this.CurrentActiveCutscene != 0 ) {
				this.Cutscenes[this.CurrentActiveCutscene].UpdateForPlayer( myplayer );
			}
		}
	}
}
