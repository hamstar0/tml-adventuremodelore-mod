using System;
using Microsoft.Xna.Framework;
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

			Vector2? startPos = cutscene.BeginForWorld_Internal( 0 );
			if( !startPos.HasValue ) {
				result = "Cutscene "+cutsceneId+" doesn't allow being begun.";
				return false;
			}

			return this.BeginCutsceneForPlayer( cutsceneId, player, 0, startPos.Value, out result );
		}


		internal bool BeginCutsceneForPlayer(
					CutsceneID cutsceneId,
					Player player,
					int sceneIdx,
					Vector2 startPos,
					out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.CanBeginForPlayer( player ) ) {
				result = "Player still cannot play cutscene " + cutsceneId;
				return false;
			}

			cutscene.SetStartPosition( startPos );

			var myplayer = player.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDsForPlayer.Add( cutsceneId );
			myplayer.CurrentPlayingCutsceneForPlayer = cutsceneId;

			cutscene.BeginForPlayer_Internal( player, sceneIdx );

			if( Main.netMode == NetmodeID.Server ) {
				AMLCutsceneNetData.SendToClient( player.whoAmI, cutscene, sceneIdx );
			}

			result = "Success.";
			return true;
		}


		////////////////
		
		public void SetCutsceneScene( CutsceneID cutsceneId, int sceneIdx, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			cutscene.SetCurrentSceneForWorld( sceneIdx );

			for( int i = 0; i < Main.player.Length; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true ) { continue; }

				cutscene.SetCurrentSceneForPlayer( player, sceneIdx );
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( -1, cutscene, sceneIdx );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, sceneIdx );
				}
			}
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
					cutscene.OnEndForPlayer_Internal( player );
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
