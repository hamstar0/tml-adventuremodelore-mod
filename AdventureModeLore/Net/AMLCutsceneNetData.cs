using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Net {
	[Serializable]
	public abstract class AMLCutsceneNetData : NetIOBroadcastPayload {
		public static void Broadcast( Cutscene cutscene, SceneID sceneId ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException("Not server");
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneId );

			NetIO.Broadcast( protocol );
		}

		public static void SendToClients( Cutscene cutscene, SceneID sceneId, int ignoreWho ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException( "Not client" );
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneId );

			NetIO.SendToClients(
				data: protocol,
				ignoreWho: ignoreWho
			);
		}

		public static void SendToClient( Cutscene cutscene, SceneID sceneId, int toWho ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException( "Not client" );
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneId );

			NetIO.SendToClient(
				data: protocol,
				toWho: toWho
			);
		}



		////////////////

		public int PlaysForWho;
		public string CutsceneModName;
		public string CutsceneClassName;
		public string SceneModName;
		public string SceneClassName;
		public int SceneIdx;



		////////////////

		protected AMLCutsceneNetData() { }

		protected AMLCutsceneNetData( Cutscene cutscene, SceneID sceneId ) {
			this.PlaysForWho = cutscene.PlaysForWhom;
			this.CutsceneModName = cutscene.UniqueId.ModName;
			this.CutsceneClassName = cutscene.UniqueId.ClassName;
			this.SceneModName = sceneId?.ModName ?? "";
			this.SceneClassName = sceneId?.ClassName ?? "";
		}


		////////////////

		public override bool ReceiveOnServerBeforeRebroadcast( int fromWho ) {
			this.Receive();
			return true;
		}

		public override void ReceiveBroadcastOnClient() {
			this.Receive();
		}

		////

		private void Receive() {
			var mngr = CutsceneManager.Instance;
			var uid = new CutsceneID( this.CutsceneModName, this.CutsceneClassName );
			Player playsFor = Main.player[ this.PlaysForWho ];
			if( playsFor?.active != true ) {
				LogHelpers.Warn( "Missing player #"+this.PlaysForWho );
				return;
			}

			if( !this.PreReceive() ) {
				return;
			}

			SceneID sceneId = null;
			if( !string.IsNullOrEmpty(this.SceneModName) ) {
				sceneId = new SceneID( this.SceneModName, this.SceneClassName );
			}

			if( this.SceneIdx == 0 ) {
				string result;
				mngr.TryBeginCutscene( uid, playsFor, sceneId, false, out result );

				LogHelpers.Log( "Beginning cutscene "+uid+" result for client: " + result );
			} else if( this.SceneIdx > 0 ) {
				mngr.SetCutsceneScene( uid, playsFor, sceneId, false );
			} else {
				mngr.EndCutscene( uid, playsFor, false );
			}
		}


		////

		protected abstract bool PreReceive();
	}
}
