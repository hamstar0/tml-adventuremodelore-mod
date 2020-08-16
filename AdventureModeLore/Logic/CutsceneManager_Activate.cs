using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		public bool BeginCutscene_Host( CutsceneID cutsceneId, out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			var myworld = ModContent.GetInstance<AMLWorld>();
			if( !myworld.CurrentPlayingCutscenes_World.Contains(cutsceneId) ) {
				result = "Could not play cutscene " + cutsceneId
					+ "; world already playing cutscenes "
					+ string.Join(", ", myworld.CurrentPlayingCutscenes_World);
				return false;
			}
			if( !cutscene.CanBegin_World() ) {
				result = "World cannot play cutscene " + cutsceneId;
				return false;
			}

			if( !cutscene.Begin_World_Internal(0) ) {
				result = "Cutscene "+cutsceneId+" doesn't allow being begun.";
				return false;
			}

			for( int i=0; i<Main.player.Length; i++ ) {
				Player plr = Main.player[i];
				if( plr?.active != true ) { continue; }

				if( !cutscene.CanBegin_Player(plr) ) {
					LogHelpers.Warn( "Could not begin cutscene for player "+plr.name+" ("+i+")" );
					continue;
				}

				if( !this.BeginCutscene_Player(cutsceneId, plr, 0, true, out result) ) {
					LogHelpers.Warn( "Failed to begin cutscene for player "+plr.name+" ("+i+") - "+result );
					continue;
				}
			}

			myworld.TriggeredCutsceneIDs_World.Add( cutsceneId );
			myworld.CurrentPlayingCutscenes_World.Add( cutsceneId );

			result = "Success.";
			return true;
		}


		internal bool BeginCutscene_Player(
					CutsceneID cutsceneId,
					Player player,
					int sceneIdx,
					bool sync,
					out string result ) {
			Cutscene cutscene = this.Cutscenes[ cutsceneId ];
			if( !cutscene.CanBegin_Player( player ) ) {
				result = "Player still cannot play cutscene " + cutsceneId;
				return false;
			}

			if( Main.netMode != NetmodeID.Server ) {
				cutscene.Begin_Player_Internal( player, sceneIdx );
			}

			var myplayer = player.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutsceneId );
			myplayer.CurrentPlayingCutscene_Player = cutsceneId;

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClient( player.whoAmI, cutscene, sceneIdx );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, sceneIdx );
				}
			}

			result = "Success.";
			return true;
		}


		////////////////

		public void EndCutscene_Any( CutsceneID cutsceneId, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			var myworld = ModContent.GetInstance<AMLWorld>();
			if( !myworld.CurrentPlayingCutscenes_World.Contains(cutsceneId) ) {
				LogHelpers.Warn( "Incorrect current cutscene;"
					+" expected "+cutsceneId.ToString()+", found "
					+string.Join(", ", myworld.CurrentPlayingCutscenes_World) );
			} else {
				myworld.CurrentPlayingCutscenes_World.Remove( cutsceneId );
				cutscene.OnEnd_World_Internal();
			}

			for( int i=0; i<Main.player.Length; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true ) { continue; }

				var myplayer = player.GetModPlayer<AMLPlayer>();
				if( myplayer.CurrentPlayingCutscene_Player != cutsceneId ) {
					LogHelpers.Warn( "Incorrect cutscene for player "+player.name+" ("+i+");"
						+" expected "+cutsceneId.ToString()
						+", found "+myplayer.CurrentPlayingCutscene_Player.ToString() );
					continue;
				}

				if( Main.netMode != NetmodeID.Server && i == Main.myPlayer ) {
					myplayer.CurrentPlayingCutscene_Player = null;

					cutscene.OnEnd_Player_Internal( player );
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


		////////////////

		public void SetCutsceneScene_Any( CutsceneID cutsceneId, int sceneIdx, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			cutscene.SetCurrentScene_World( sceneIdx );

			for( int i = 0; i < Main.player.Length; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true ) { continue; }

				cutscene.SetCurrentScene_Player( player, sceneIdx );
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( -1, cutscene, sceneIdx );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, sceneIdx );
				}
			}
		}
	}
}
