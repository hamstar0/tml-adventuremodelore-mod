using System;
using System.Linq;
using Terraria;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;


namespace AdventureModeLore.Net {
	[Serializable]
	class AMLPlayerDataNetData : NetProtocolBroadcastPayload {
		public static void SendToClients( int ignoreWho, AMLPlayer plrData ) {
			var protocol = new AMLPlayerDataNetData( plrData );

			NetIO.SendToClients( protocol, ignoreWho );
		}

		public static void BroadcastFromCurrentPlayer( AMLPlayer plrData ) {
			var protocol = new AMLPlayerDataNetData( plrData );

			NetIO.Broadcast( protocol );
		}



		////////////////

		public int FromWho;

		public bool IsAdventureModePlayer = false;

		public int[] ActivatedCutscenes;



		////////////////

		private AMLPlayerDataNetData() { }
		
		private AMLPlayerDataNetData( AMLPlayer plrData ) {
			this.FromWho = plrData.player.whoAmI;
			this.IsAdventureModePlayer = plrData.IsAdventureModePlayer;
			this.ActivatedCutscenes = plrData.ActivatedCutscenes.Select( c=>(int)c ).ToArray();
		}


		////////////////

		public override void ReceiveOnServerBeforeRebroadcast( int fromWho ) {
			var myplr = Main.player[this.FromWho].GetModPlayer<AMLPlayer>();
			myplr.SyncFromNet( this );
		}

		public override void ReceiveBroadcastOnClient( int fromWho ) {
			var myplr = Main.player[this.FromWho].GetModPlayer<AMLPlayer>();
			myplr.SyncFromNet( this );
		}
	}
}
