using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using AdventureModeLore.Net;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		public bool CanBeginCutscene( CutsceneID cutsceneId, Player playsFor ) {
			if( this.GetCurrentCutscene_Player( playsFor ) != null ) {
				return false;
			}
			if( this.HasCutscenePlayed_World( cutsceneId ) ) {
				return false;
			}
			if( this.HasCutscenePlayed_Player( cutsceneId, playsFor ) ) {
				return false;
			}

			return true;
		}


		////////////////

		public bool TryBeginCutscene(
					CutsceneID cutsceneId,
					Player playsFor,
					int sceneIdx,
					bool sync, 
					out string result ) {
			if( this.GetCurrentCutscene_Player(playsFor) != null ) {
				result = playsFor.name+" ("+playsFor.whoAmI+") already playing cutscene "+cutsceneId;
				return false;
			}

			if( !this.CanBeginCutscene(cutsceneId, playsFor) ) {
				result = "Cannot play cutscene " + cutsceneId;
				return false;
			}

			Cutscene cutscene = null;f

			this._CutscenePerPlayer[ playsFor.whoAmI ] = cutscene;

			var myplayer = playsFor.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutsceneId );

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.TriggeredCutsceneIDs_World.Add( cutsceneId );

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClient( cutscene, playsFor, sceneIdx, -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, playsFor, sceneIdx );
				}
			}

			result = "Success.";
			return true;
		}


		////////////////

		public bool SetCutsceneScene( CutsceneID cutsceneId, Player playsFor, int sceneIdx, bool sync ) {
			Cutscene cutscene = this._CutscenePerPlayer.GetOrDefault( playsFor.whoAmI );
			if( cutscene == null ) {
				return false;
			}
			if( cutscene.UniqueId != cutsceneId ) {
				return false;
			}

			cutscene.SetCurrentScene_NoSync( sceneIdx );

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( cutscene, playsFor, sceneIdx, - 1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, playsFor, sceneIdx );
				}
			}
			return true;
		}


		////////////////

		public bool EndCutscene( CutsceneID cutsceneId, Player playsFor, bool sync ) {
			Cutscene cutscene = this._CutscenePerPlayer.GetOrDefault( playsFor.whoAmI );
			if( cutscene == null ) {
				return false;
			}
			if( cutscene.UniqueId != cutsceneId ) {
				return false;
			}

			cutscene.End_Internal();

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( cutscene: cutscene, playsFor: playsFor, sceneIdx: -1, ignoreWho: -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene, playsFor, -1 );
				}
			}
			return true;
		}
	}
}
