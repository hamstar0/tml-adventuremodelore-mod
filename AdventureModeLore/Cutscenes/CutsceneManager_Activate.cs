using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager : ILoadable {
		public bool BeginCutscene( CutsceneID cutsceneId, Player player, out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.CanBeginForPlayer(player) ) {
				result = "Player cannot play cutscene " + cutsceneId;
				return false;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			if( myworld.CurrentPlayingCutsceneForWorld != 0 ) {
				result = "Could not play cutscene " + cutsceneId + "; world already playing cutscene " + myworld.CurrentPlayingCutsceneForWorld;
				return false;
			}
			if( !cutscene.CanBeginForWorld() ) {
				result = "World cannot play cutscene " + cutsceneId;
				return false;
			}

			myworld.TriggeredCutsceneIDsForWorld.Add( cutsceneId );
			myworld.CurrentPlayingCutsceneForWorld = cutsceneId;

			cutscene.BeginForWorld_Internal( 0 );

			return this.BeginCutsceneForPlayer( cutsceneId, player, 0, out result );
		}


		internal bool BeginCutsceneForPlayer( CutsceneID cutsceneId, Player player, int sceneIdx, out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( cutscene.StartPosition == null ) {
				result = "No start position defined.";
				return false;
			}
			if( !cutscene.CanBeginForPlayer( player ) ) {
				result = "Player still cannot play cutscene " + cutsceneId;
				return false;
			}

			var myplayer = player.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDsForPlayer.Add( cutsceneId );
			myplayer.CurrentPlayingCutsceneForPlayer = cutsceneId;

			if( Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient ) {
				cutscene.BeginForPlayer_Internal( player, sceneIdx );
			} else if( Main.netMode == NetmodeID.Server ) {
				AMLCutsceneNetData.SendToClient( player.whoAmI, cutscene, sceneIdx );
			}

			result = "Success.";
			return true;
		}


		////////////////
		
		public void SetCutsceneScene( CutsceneID cutsceneId, int sceneIdx ) {
			this.Cutscenes[cutsceneId].SetCurrentScene( sceneIdx );
		}


		////////////////

		public void EndCutscene( CutsceneID cutsceneId, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			var myworld = ModContent.GetInstance<AMLWorld>();
			if( myworld.CurrentPlayingCutsceneForWorld == cutsceneId ) {
				myworld.CurrentPlayingCutsceneForWorld = 0;
				cutscene.OnEndForWorld_Internal();
			}

			for( int i=0; i<Main.player.Length; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true ) { continue; }

				var myplayer = player.GetModPlayer<AMLPlayer>();
				if( myplayer.CurrentPlayingCutsceneForPlayer == cutsceneId ) {
					myplayer.CurrentPlayingCutsceneForPlayer = 0;
					cutscene.OnEndForPlayer_Internal( myplayer );
				}
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( -1, cutscene, -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, -1 );
				}
			}
		}
	}
}
