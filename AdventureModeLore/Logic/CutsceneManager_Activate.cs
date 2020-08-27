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
			return this.CanBeginCutscene( cutsceneId, playsFor, out Cutscene _ );
		}

		private bool CanBeginCutscene( CutsceneID cutsceneId, Player playsFor, out Cutscene cutscene ) {
			if( this.GetCurrentCutscene_Player( playsFor ) != null ) {
				cutscene = null;
				return false;
			}
			if( this.HasCutscenePlayed_World( cutsceneId ) ) {
				cutscene = null;
				return false;
			}
			if( this.HasCutscenePlayed_Player( cutsceneId, playsFor ) ) {
				cutscene = null;
				return false;
			}

			cutscene = cutsceneId.Create( playsFor );
			return cutscene.CanBegin();
		}


		////////////////

		public bool TryBeginCutscene(
					CutsceneID cutsceneId,
					Player playsFor,
					bool sync,
					out string result ) {
			if( !this.CanBeginCutscene(cutsceneId, playsFor, out Cutscene cutscene) ) {
				result = "Cannot play cutscene " + cutsceneId;
				return false;
			}
			return this.TryBeginCutscene( cutscene, cutscene.FirstSceneId, playsFor, sync, out result );
		}
		
		public bool TryBeginCutscene(
					CutsceneID cutsceneId,
					SceneID sceneId,
					Player playsFor,
					bool sync,
					out string result ) {
			if( !this.CanBeginCutscene(cutsceneId, playsFor, out Cutscene cutscene) ) {
				result = "Cannot play cutscene " + cutsceneId;
				return false;
			}
			return this.TryBeginCutscene( cutscene, sceneId, playsFor, sync, out result );
		}

		////

		private bool TryBeginCutscene(
					Cutscene cutscene,
					SceneID sceneId,
					Player playsFor,
					bool sync, 
					out string result ) {
			if( this.GetCurrentCutscene_Player(playsFor) != null ) {
				result = playsFor.name+" ("+playsFor.whoAmI+") already playing cutscene "+cutscene.UniqueId;
				return false;
			}
LogHelpers.LogOnce("3 A");
			
			cutscene.BeginCutscene_Internal( sceneId );

			this._CutscenePerPlayer[ playsFor.whoAmI ] = cutscene;

			var myplayer = playsFor.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutscene.UniqueId );

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.TriggeredCutsceneIDs_World.Add( cutscene.UniqueId );
LogHelpers.LogOnce("4 A");

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
LogHelpers.LogOnce("5a A");
					AMLCutsceneNetData.SendToClients( cutscene: cutscene, sceneId: sceneId, ignoreWho: -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
LogHelpers.LogOnce("5b A");
					AMLCutsceneNetData.Broadcast( cutscene: cutscene, sceneId: sceneId );
				}
			}

			result = "Success.";
			return true;
		}

		////

		public bool TryBeginCutsceneFromNetwork(
					CutsceneID cutsceneId,
					SceneID sceneId,
					Player playsFor,
					AMLCutsceneNetData data,
					out string result ) {
			if( this.GetCurrentCutscene_Player(playsFor) != null ) {
				result = playsFor.name+" ("+playsFor.whoAmI+") already playing cutscene "+cutsceneId;
				return false;
			}
			if( !this.CanBeginCutscene( cutsceneId, playsFor, out Cutscene cutscene) ) {
				result = "Cannot play cutscene " + cutsceneId;
				return false;
			}
LogHelpers.LogOnce("3 B");
			
			cutscene.BeginCutsceneFromNetwork_Internal( sceneId, data );

			this._CutscenePerPlayer[ playsFor.whoAmI ] = cutscene;

			var myplayer = playsFor.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDs_Player.Add( cutsceneId );

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.TriggeredCutsceneIDs_World.Add( cutsceneId );
LogHelpers.LogOnce("4 B");

			result = "Success.";
			return true;
		}


		////////////////

		public bool SetCutsceneScene( CutsceneID cutsceneId, Player playsFor, SceneID sceneId, bool sync ) {
			Cutscene cutscene = this._CutscenePerPlayer.GetOrDefault( playsFor.whoAmI );
			if( cutscene == null ) {
				return false;
			}
			if( cutscene.UniqueId != cutsceneId ) {
				return false;
			}

			cutscene.SetCurrentScene_NoSync( sceneId );

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( cutscene: cutscene, sceneId: sceneId, ignoreWho: -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene: cutscene, sceneId: sceneId );
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

			cutscene.EndCutscene_Internal();

			this._CutscenePerPlayer.Remove( playsFor.whoAmI );

			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLCutsceneNetData.SendToClients( cutscene: cutscene, sceneId: null, ignoreWho: -1 );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLCutsceneNetData.Broadcast( cutscene: cutscene, sceneId: null );
				}
			}
			return true;
		}
	}
}
