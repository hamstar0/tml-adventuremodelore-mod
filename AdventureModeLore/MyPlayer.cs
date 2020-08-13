using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Definitions;
using AdventureModeLore.Logic;


namespace AdventureModeLore {
	public partial class AMLPlayer : ModPlayer {
		internal ISet<CutsceneID> TriggeredCutsceneIDsForPlayer = new HashSet<CutsceneID>();


		////////////////

		public bool IsAdventureModePlayer { get; internal set; } = false;

		public CutsceneID CurrentPlayingCutsceneForPlayer { get; internal set; }

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = clientClone as AMLPlayer;
			myclone.TriggeredCutsceneIDsForPlayer = new HashSet<CutsceneID>( this.TriggeredCutsceneIDsForPlayer );
			myclone.IsAdventureModePlayer = this.IsAdventureModePlayer;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "IsAdventureModePlayer" ) ) {
				this.IsAdventureModePlayer = true;
				CutsceneManager.Instance?.LoadForPlayer( this, tag );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "IsAdventureModePlayer", this.IsAdventureModePlayer },
			};
			CutsceneManager.Instance.SaveForPlayer( this, tag );
			return tag;
		}


		////////////////

		public override void SetupStartInventory( IList<Item> items, bool mediumcoreDeath ) {
			if( !mediumcoreDeath ) {
				this.IsAdventureModePlayer = true;
				LogHelpers.Log( "Player " + this.player.name + " prepped for Adventure Mode." );
			}
		}


		////////////////
		
		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == NetmodeID.Server ) {
				AMLPlayerDataNetData.SendToClients( this, toWho, fromWho );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				AMLPlayerDataNetData.SendToServer( this );
			}
		}

		public override void SendClientChanges( ModPlayer clientPlayer ) {
			var myclone = clientPlayer as AMLPlayer;
			bool isDesynced = myclone.IsAdventureModePlayer != this.IsAdventureModePlayer
				|| !this.TriggeredCutsceneIDsForPlayer.SetEquals( myclone.TriggeredCutsceneIDsForPlayer );

			if( isDesynced ) {
				if( Main.netMode == NetmodeID.Server ) {
					AMLPlayerDataNetData.SendToClients( this, -1, this.player.whoAmI );
				} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
					AMLPlayerDataNetData.SendToServer( this );	// server only?
				}
			}
		}

		/////

		internal void SyncFromNet( AMLPlayerDataNetData payload ) {
			this.IsAdventureModePlayer = this.IsAdventureModePlayer;
			this.TriggeredCutsceneIDsForPlayer = new HashSet<CutsceneID>();

			int len = payload.ActivatedCutsceneModNames.Length;
			for( int i=0; i<len; i++ ) {
				string modName = payload.ActivatedCutsceneModNames[i];
				string name = payload.ActivatedCutsceneNames[i];

				this.TriggeredCutsceneIDsForPlayer.Add( new CutsceneID(modName, name) );
			}
		}


		////////////////

		public override void PreUpdate() {
			CutsceneManager.Instance.UpdateForPlayer( this );
		}
	}
}