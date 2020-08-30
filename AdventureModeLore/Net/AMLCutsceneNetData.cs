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

		public int PlaysForWho = -1;
		public string CutsceneModName = null;
		public string CutsceneClassFullName = null;
		public string SceneModName = null;
		public string SceneClassFullName = null;



		////////////////

		protected AMLCutsceneNetData() { }

		protected AMLCutsceneNetData( Cutscene cutscene, SceneID sceneId ) {
			this.PlaysForWho = cutscene.PlaysForWhom;
			this.CutsceneModName = cutscene.UniqueId.ModName;
			this.CutsceneClassFullName = cutscene.UniqueId.FullClassName;
			this.SceneModName = sceneId.ModName;
			this.SceneClassFullName = sceneId.FullClassName;
/*LogHelpers.Log( "SEND "
	+"PlaysForWho:"+this.PlaysForWho
	+ ", CutsceneModName:" + this.CutsceneModName
	+ ", CutsceneClassFullName:" + this.CutsceneClassFullName
	+ ", SceneModName:" + this.SceneModName
	+ ", SceneClassFullName:" + this.SceneClassFullName );*/
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
/*LogHelpers.Log( "RECEIVE "
	+"PlaysForWho:"+this.PlaysForWho
	+ ", CutsceneModName:" + this.CutsceneModName
	+ ", CutsceneClassFullName:" + this.CutsceneClassFullName
	+ ", SceneModName:" + this.SceneModName
	+ ", SceneClassFullName:" + this.SceneClassFullName );*/
			var mngr = CutsceneManager.Instance;
			var cutsceneId = new CutsceneID( this.CutsceneModName, this.CutsceneClassFullName );

			Player playsFor = Main.player[ this.PlaysForWho ];
			if( playsFor?.active != true ) {
				LogHelpers.Warn( "Missing player #"+this.PlaysForWho );
				return;
			}

			if( !this.PreReceive() ) {
				return;
			}

			if( !mngr.CanBeginCutscene(cutsceneId, playsFor, out string result) ) {
				LogHelpers.Warn( "Cannot play cutscene " + cutsceneId + ": "+result );
				return;
			}

			SceneID sceneId = null;
			if( !string.IsNullOrEmpty(this.SceneModName) ) {
				sceneId = new SceneID( this.SceneModName, this.SceneClassFullName );
			}

			if( sceneId != null ) {
				void onSuccess( string myResult ) {
					LogHelpers.Log( "Beginning cutscene " + cutsceneId + " result for client: " + myResult );
				}
				void onFail( string myResult ) {
					if( !mngr.SetCutsceneScene( cutsceneId, playsFor, sceneId, false ) ) {
						LogHelpers.Warn( "Cannot play cutscene " + cutsceneId + ": " + myResult );
					}
				}

				mngr.TryBeginCutsceneFromNetwork( cutsceneId, sceneId, playsFor, this, onSuccess, onFail );
			} else {
				if( mngr.EndCutscene(cutsceneId, this.PlaysForWho, false) ) {
					LogHelpers.Warn( "Cannot end cutscene "+cutsceneId+"." );
				}
			}
		}


		////

		protected abstract bool PreReceive();
	}
}
