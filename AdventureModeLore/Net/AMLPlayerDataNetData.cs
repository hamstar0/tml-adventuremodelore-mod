using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


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

		public string[] CurrentCutsceneModNames_World;
		public string[] CurrentCutsceneNames_World;
		public string[] ActivatedCutsceneModNames_World;
		public string[] ActivatedCutsceneNames_World;
		public string[] ActivatedCutsceneModNames_Player;
		public string[] ActivatedCutsceneNames_Player;



		////////////////

		private AMLPlayerDataNetData() { }
		
		private AMLPlayerDataNetData( AMLPlayer myplayer ) {
			var myworld = ModContent.GetInstance<AMLWorld>();
			var cutMngr = CutsceneManager.Instance;
			IEnumerable<Cutscene> activeCutscenes = cutMngr.GetActiveCutscenes_World();

			this.FromWho = myplayer.player.whoAmI;
			this.IsAdventureModePlayer = myplayer.IsAdventureModePlayer;

			this.CurrentCutsceneModNames_World = activeCutscenes
				.Select( c=>c.UniqueId.ModName ).ToArray();
			this.CurrentCutsceneNames_World = activeCutscenes
				.Select( c=>c.UniqueId.Name ).ToArray();

			this.ActivatedCutsceneModNames_World = myworld.TriggeredCutsceneIDs_World
				.Select( c=>c.ModName ).ToArray();
			this.ActivatedCutsceneNames_World = myworld.TriggeredCutsceneIDs_World
				.Select( c=>c.Name ).ToArray();

			this.ActivatedCutsceneModNames_Player = myplayer.TriggeredCutsceneIDs_Player
				.Select( c=>c.ModName ).ToArray();
			this.ActivatedCutsceneNames_Player = myplayer.TriggeredCutsceneIDs_Player
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
