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
		public bool BeginCutscene(
					CutsceneID cutsceneId,
					Player playsFor,
					int sceneIdx,
					bool sync, 
					out string result ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			if( cutscene.IsPlayingFor(playsFor.whoAmI) ) {
				result = playsFor.name+" ("+playsFor.whoAmI+") already playing cutscene"+cutsceneId;
				return false;
			}
			if( !cutscene.CanBegin(playsFor) ) {
				result = "Cannot play cutscene " + cutsceneId;
				return false;
			}

			if( !cutscene.Begin_Internal(playsFor, sceneIdx ) ) {
				result = "Cutscene "+cutsceneId+" doesn't allow being begun.";
				return false;
			}

			var myplayer = playsFor.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutsceneId );

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.TriggeredCutsceneIDs_World.Add( cutsceneId );

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

		public void SetCutsceneScene( CutsceneID cutsceneId, Player playsFor, int sceneIdx, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			cutscene.SetScene_Internal( playsFor, sceneIdx );

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( playsFor, - 1, cutscene, sceneIdx );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( playsFor, cutscene, sceneIdx );
				}
			}
		}


		////////////////

		public void EndCutscene( CutsceneID cutsceneId, Player playsFor, bool sync ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];

			cutscene.End_Internal( playsFor );

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
