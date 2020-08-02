using System;
using Terraria;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using AdventureModeLore.Cutscenes;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Net {
	[Serializable]
	class AMLCutsceneNetData : NetProtocolClientPayload {
		public static void SendToClient( int toWho, CutsceneID cutsceneId ) {
			var protocol = new AMLCutsceneNetData( cutsceneId );

			NetIO.SendToClients( protocol, toWho, -1 );
		}



		////////////////

		public int CutsceneID;



		////////////////

		private AMLCutsceneNetData() { }
		
		private AMLCutsceneNetData( CutsceneID cutsceneId ) {
			this.CutsceneID = (int)cutsceneId;
		}


		////////////////

		public override void ReceiveOnClient( int fromWho ) {
			CutsceneManager.Instance.BeginCutsceneForPlayer( (CutsceneID)this.CutsceneID, Main.LocalPlayer );
		}
	}
}
