using System;
using System.Linq;
using Terraria;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;


namespace AdventureModeLore.Net {
	[Serializable]
	class AMLPlayerDataNetData : NetIOBidirectionalPayload {
		public static void SendToServer( AMLPlayer plrData ) {
			var protocol = new AMLPlayerDataNetData( plrData );

			NetIO.SendToServer( protocol );
		}

		public static void SendToClients( AMLPlayer plrData, int toWho, int ignoreWho ) {
			var protocol = new AMLPlayerDataNetData( plrData );

			NetIO.SendToClients( protocol, toWho, ignoreWho );
		}



		////////////////

		public int FromWho;

		public bool IsAdventureModePlayer = false;

		public string[] ActivatedCutsceneModNames;
		public string[] ActivatedCutsceneNames;



		////////////////

		private AMLPlayerDataNetData() { }
		
		private AMLPlayerDataNetData( AMLPlayer plrData ) {
			this.FromWho = plrData.player.whoAmI;
			this.IsAdventureModePlayer = plrData.IsAdventureModePlayer;
			this.ActivatedCutsceneModNames = plrData.TriggeredCutsceneIDsForPlayer
				.Select( c=>c.ModName ).ToArray();
			this.ActivatedCutsceneNames = plrData.TriggeredCutsceneIDsForPlayer
				.Select( c=>c.Name ).ToArray();
		}


		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			var myplr = Main.player[this.FromWho].GetModPlayer<AMLPlayer>();
			myplr.SyncFromNet( this );
		}

		public override void ReceiveOnClient() {
			var myplr = Main.player[this.FromWho].GetModPlayer<AMLPlayer>();
			myplr.SyncFromNet( this );
		}
	}
}
