using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Net {
	[Serializable]
	public abstract class AMLCutsceneNetData : NetIOBroadcastPayload {
		public static void Broadcast( Cutscene cutscene, int sceneIdx ) {
			AMLCutsceneNetData protocol = cutscene.GetPacketPayload( sceneIdx );

			NetIO.Broadcast( protocol );
		}

		public static void SendToClients( int ignoreWho, Cutscene cutscene, int sceneIdx ) {
			AMLCutsceneNetData protocol = cutscene.GetPacketPayload( sceneIdx );

			NetIO.SendToClients( protocol, ignoreWho );
		}

		public static void SendToClient( int toWho, Cutscene cutscene, int sceneIdx ) {
			AMLCutsceneNetData protocol = cutscene.GetPacketPayload( sceneIdx );

			NetIO.SendToClient( protocol, toWho );
		}



		////////////////

		public string ModName;
		public string Name;
		public int SceneIdx;
		public Vector2 CurrentPosition;



		////////////////

		protected AMLCutsceneNetData() { }

		protected AMLCutsceneNetData( Cutscene cutscene, int sceneIdx ) {
			this.ModName = cutscene.UniqueId.ModName;
			this.Name = cutscene.UniqueId.Name;
			this.SceneIdx = sceneIdx;
			this.CurrentPosition = cutscene.CurrentPosition;
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

			if( !this.PreReceive(uid) ) {
				return;
			}

			if( this.SceneIdx == 0 ) {
				string result;
				mngr.BeginCutsceneForPlayer( uid, Main.LocalPlayer, 0, this.CurrentPosition, out result );

				LogHelpers.Log( "Cutscene " + uid + " result for client: " + result );
			} else if( this.SceneIdx > 0 ) {
				mngr.SetCutsceneScene( uid, this.SceneIdx, false );
			} else {
				mngr.EndCutscene( uid, false );
			}
		}


		////

		protected abstract bool PreReceive( CutsceneID uid );
	}
}
