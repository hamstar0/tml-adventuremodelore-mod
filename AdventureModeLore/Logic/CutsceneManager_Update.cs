using System;
using Terraria;
using Terraria.Graphics.Capture;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		internal void Update_WorldAndHost( AMLWorld myworld ) {
			this.UpdateActivations_WorldAndHost();

			if( myworld.CurrentPlayingCutscenes_World.Count >= 1 ) {
				foreach( CutsceneID uid in myworld.CurrentPlayingCutscenes_World ) {
					this.Cutscenes[ uid ].Update_WorldAndHost_Internal();
				}
			}
		}

		private void UpdateActivations_WorldAndHost() {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( !this.BeginCutscene_Host(cutscene.UniqueId, out string result) ) {
					LogHelpers.WarnOnce( result );
				}
			}
		}


		////////////////

		internal void Update_Player( AMLPlayer myplayer ) {
			CutsceneID uid = myplayer.CurrentPlayingCutscene_Player;
			if( uid == null ) {
				return;
			}

			myplayer.player.immune = true;
			myplayer.player.immuneTime = 2;

			//Main.mapFullscreen = false;
			//Main.mapEnabled = false;
			Main.mapStyle = 0;
			CaptureManager.Instance.Active = false;

			this.Cutscenes[ uid ].Update_Player_Internal( myplayer.player );
		}
	}
}
