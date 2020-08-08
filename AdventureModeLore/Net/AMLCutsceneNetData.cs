using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using AdventureModeLore.Cutscenes;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Net {
	[Serializable]
	class AMLCutsceneNetData : NetProtocolBroadcastPayload {
		public static void Broadcast( Cutscene cutscene, int sceneIdx ) {
			var protocol = new AMLCutsceneNetData( cutscene, sceneIdx );

			NetIO.Broadcast( protocol );
		}

		public static void SendToClients( int ignoreWho, Cutscene cutscene, int sceneIdx ) {
			var protocol = new AMLCutsceneNetData( cutscene, sceneIdx );

			NetIO.SendToClients( protocol, ignoreWho );
		}

		public static void SendToClient( int toWho, Cutscene cutscene, int sceneIdx ) {
			var protocol = new AMLCutsceneNetData( cutscene, sceneIdx );

			NetIO.SendToClient( protocol, toWho );
		}



		////////////////

		public int CutsceneID;
		public int SceneIdx;



		////////////////

		private AMLCutsceneNetData() { }
		
		private AMLCutsceneNetData( Cutscene cutscene, int sceneIdx ) {
			this.CutsceneID = (int)cutscene.UniqueId;
			this.SceneIdx = sceneIdx;
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
			var uid = (CutsceneID)this.CutsceneID;

			if( this.SceneIdx >= 0 ) {
				string result;
				mngr.BeginCutsceneForPlayer( uid, Main.LocalPlayer, this.SceneIdx, out result );

				LogHelpers.Log( "Cutscene " + uid + " result for client: " + result );
			} else {
				mngr.EndCutscene( uid, false );
			}
		}
	}
}
