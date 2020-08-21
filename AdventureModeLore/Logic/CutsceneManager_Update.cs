using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Capture;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		internal void Update_Internal() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				this.UpdateActivations_Host_Internal();

				foreach( Cutscene cutscene in this.CutscenePerPlayer.Values ) {
					cutscene.Update_Internal();
				}
			} else {
				Cutscene cutscene = this.GetCurrentCutscene_Player( Main.LocalPlayer );
				cutscene.Update_Internal();
			}
		}

		private void UpdateActivations_Host_Internal() {
			int playerCount = Main.player.Length;

			foreach( Cutscene cutscene in this.CutscenePerPlayer.Values ) {
				for( int i=0; i<playerCount; i++ ) {
					Player plr = Main.player[i];
					if( plr?.active != true ) {
						continue;
					}

					if( !this.TryBeginCutscene(cutscene.UniqueId, plr, null, true, out string result) ) {
						LogHelpers.LogOnce( "Tried to begin cutscene: "+result );
					}

					if( Main.netMode == NetmodeID.SinglePlayer ) {
						break;
					}
				}
			}
		}


		////////////////

		internal void Update_Player_Internal( AMLPlayer myplayer ) {
			Cutscene cutscene = this.GetCurrentCutscene_Player( myplayer.player );
			if( cutscene == null ) {
				return;
			}

			myplayer.player.immune = true;
			myplayer.player.immuneTime = 2;

			if( Main.netMode != NetmodeID.Server ) {
				if( myplayer.player.whoAmI == Main.myPlayer ) {
					//Main.mapFullscreen = false;
					//Main.mapEnabled = false;
					Main.mapStyle = 0;
					CaptureManager.Instance.Active = false;
				}
			}
		}
	}
}
