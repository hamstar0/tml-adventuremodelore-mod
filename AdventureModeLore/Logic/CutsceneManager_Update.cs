﻿using System;
using Terraria;
using Terraria.Graphics.Capture;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		internal void UpdateForWorld( AMLWorld myworld ) {
			if( myworld.CurrentPlayingCutsceneForWorld == null ) {
				this.UpdateForWorldToActivate();
			} else {
				this.Cutscenes[ myworld.CurrentPlayingCutsceneForWorld ].UpdateForWorld_Internal();
			}
		}

		private void UpdateForWorldToActivate() {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( !cutscene.CanBeginForWorld() ) {
					continue;
				}

				int plrMax = Main.player.Length;
				for( int i = 0; i < plrMax; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true ) { continue; }

					if( cutscene.CanBeginForPlayer( plr ) ) {
						if( !this.BeginCutscene( cutscene.UniqueId, plr, out string result) ) {
							LogHelpers.WarnOnce( result );
							continue;
						}
					}
					break;
				}
			}
		}


		////////////////

		internal void UpdateForPlayer( AMLPlayer myplayer ) {
			CutsceneID currCutsceneID = myplayer.CurrentPlayingCutsceneForPlayer;
			if( currCutsceneID == null ) {
				return;
			}

			myplayer.player.immune = true;
			myplayer.player.immuneTime = 2;

			//Main.mapFullscreen = false;
			//Main.mapEnabled = false;
			Main.mapStyle = 0;
			CaptureManager.Instance.Active = false;

			this.Cutscenes[ currCutsceneID ].UpdateForPlayer_Internal( myplayer.player );
		}
	}
}
