using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Definitions;
using System.Linq;

namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		public bool BeginCutscene_Host( Player playsFor, CutsceneID cutsceneId, out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			if( cutscene.IsPlayingFor(playsFor.whoAmI) ) {
				result = "Already playing cutscene " + cutsceneId
					+ " for "+playsFor.name+" ("+playsFor.whoAmI+")";
				return false;
			}
			if( !cutscene.CanBegin_World( playsFor ) ) {
				result = "World cannot play cutscene " + cutsceneId;
				return false;
			}
			if( !cutscene.CanBegin_Player(playsFor) ) {
				result = "Could not begin cutscene for player " + playsFor.name + " (" + playsFor.whoAmI + ")";
				return false;
			}

			if( !cutscene.Begin_World_Internal(playsFor, 0) ) {
				result = "Cutscene "+cutsceneId+" doesn't allow being begun.";
				return false;
			}
			if( !this.BeginCutscene_Player(cutsceneId, playsFor, 0, true, out result) ) {
				LogHelpers.Warn( "Failed to begin cutscene for player "+playsFor.name+" ("+playsFor.whoAmI+") - "+result );
				return false;
			}

			myworld.TriggeredCutsceneIDs_World.Add( cutsceneId );
			myworld.CurrentPlayingCutscenes_World.Add( cutsceneId );

			result = "Success.";
			return true;
		}


		internal bool BeginCutscene_Player(
					CutsceneID cutsceneId,
					Player playsFor,
					int sceneIdx,
					bool sync,
					out string result ) {
			Cutscene cutscene = this.Cutscenes[ cutsceneId ];
			if( !cutscene.CanBegin_Player( playsFor ) ) {
				result = "Player still cannot play cutscene " + cutsceneId;
				return false;
			}

			if( Main.netMode != NetmodeID.Server ) {
				cutscene.Begin_Player_Internal( playsFor, sceneIdx );
			}

			var myplayer = playsFor.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutsceneId );
			myplayer.CurrentPlayingCutscene_Player = cutsceneId;

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClient( playsFor, -1, cutscene, sceneIdx );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( playsFor, cutscene, sceneIdx );
				}
			}

			result = "Success.";
			return true;
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


		////////////////

		public void EndCutscene_Any( Player playsFor, CutsceneID cutsceneId, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			cutscene.End_World_Internal( playsFor );

			for( int i = 0; i < Main.player.Length; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true ) { continue; }

				cutscene.End_Player_Internal( player );
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( playsFor, -1, cutscene, -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( playsFor, cutscene, -1 );
				}
			}
		}
	}
}
