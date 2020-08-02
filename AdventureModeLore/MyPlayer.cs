using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AdventureModeLore.Net;
using AdventureModeLore.Cutscenes;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore {
	partial class AMLPlayer : ModPlayer {
		internal ISet<CutsceneID> ActivatedCutscenes = new HashSet<CutsceneID>();


		////////////////

		public bool IsAdventureModePlayer { get; internal set; } = false;

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = clientClone as AMLPlayer;
			myclone.ActivatedCutscenes = new HashSet<CutsceneID>( this.ActivatedCutscenes );
			myclone.IsAdventureModePlayer = this.IsAdventureModePlayer;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "IsAdventureModePlayer" ) ) {
				this.IsAdventureModePlayer = true;
				CutsceneManager.Instance.LoadForPlayer( this, tag );
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
				|| !this.ActivatedCutscenes.SetEquals( myclone.ActivatedCutscenes );

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
			this.ActivatedCutscenes = new HashSet<CutsceneID>(
				payload.ActivatedCutscenes.Select( c => (CutsceneID)c )
			);
		}


		////////////////

		public override void PreUpdate() {
			CutsceneManager.Instance.UpdateForPlayer( this );
		}
	}
}