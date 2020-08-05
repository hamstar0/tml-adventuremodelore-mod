using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager {
		internal void UpdateForWorld( AMLWorld myworld ) {
			if( myworld.CurrentPlayingCutsceneForWorld == 0 ) {
				this.UpdateToActivate();
			} else {
				this.Cutscenes[ myworld.CurrentPlayingCutsceneForWorld ].UpdateForWorld();
			}
		}

		private void UpdateToActivate() {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( !cutscene.CanBeginForWorld() ) {
					continue;
				}

				int plrMax = Main.player.Length;
				for( int i = 0; i < plrMax; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true ) { continue; }

					if( cutscene.CanBeginForPlayer(plr) ) {
						this.BeginCutscene( cutscene.UniqueId, plr );
					}
					break;
				}
			}
		}


		////////////////

		internal void UpdateForPlayer( AMLPlayer myplayer ) {
			if( myplayer.CurrentPlayingCutsceneForPlayer != 0 ) {
				this.Cutscenes[ myplayer.CurrentPlayingCutsceneForPlayer ].UpdateForPlayer( myplayer );
			}
		}
	}
}
