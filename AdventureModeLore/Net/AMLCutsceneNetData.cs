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
		public static void Broadcast( Cutscene cutscene, int sceneIdx ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException("Not server");
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneIdx );

			NetIO.Broadcast( protocol );
		}

		public static void SendToClients( Cutscene cutscene, int sceneIdx, int ignoreWho ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException( "Not client" );
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneIdx );

			NetIO.SendToClients(
				data: protocol,
				ignoreWho: ignoreWho
			);
		}

		public static void SendToClient( Cutscene cutscene, int sceneIdx, int toWho ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModHelpersException( "Not client" );
			}

			AMLCutsceneNetData protocol = cutscene.CreatePacketPayload( sceneIdx );

			NetIO.SendToClient(
				data: protocol,
				toWho: toWho
			);
		}



		////////////////

		public int PlaysForWho;
		public string ModName;
		public string Name;
		public int SceneIdx;



		////////////////

		protected AMLCutsceneNetData() { }

		protected AMLCutsceneNetData( Cutscene cutscene, Player playsFor, int sceneIdx ) {
			this.PlaysForWho = playsFor.whoAmI;
			this.ModName = cutscene.UniqueId.ModName;
			this.Name = cutscene.UniqueId.ClassName;
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
			var uid = new CutsceneID( this.ModName, this.Name );
			Player playsFor = Main.player[ this.PlaysForWho ];
			if( playsFor?.active != true ) {
				LogHelpers.Warn( "Missing player #"+this.PlaysForWho );
				return;
			}

			if( !this.PreReceive() ) {
				return;
			}

			if( this.SceneIdx == 0 ) {
				string result;
				mngr.TryBeginCutscene( uid, playsFor, this.SceneIdx, false, out result );

				LogHelpers.Log( "Beginning cutscene "+uid+" result for client: " + result );
			} else if( this.SceneIdx > 0 ) {
				mngr.SetCutsceneScene( uid, playsFor, this.SceneIdx, false );
			} else {
				mngr.EndCutscene( uid, playsFor, false );
			}
		}


		////

		protected abstract bool PreReceive();
	}
}
