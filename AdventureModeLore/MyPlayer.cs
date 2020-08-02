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
				AMLPlayerDataNetData.SendToClients( fromWho, this );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				AMLPlayerDataNetData.BroadcastFromCurrentPlayer( this );
			}
		}

		public override void SendClientChanges( ModPlayer clientPlayer ) {
			var myclone = clientPlayer as AMLPlayer;
			bool isDesynced = myclone.IsAdventureModePlayer != this.IsAdventureModePlayer
				|| !this.ActivatedCutscenes.SetEquals( myclone.ActivatedCutscenes );

			if( isDesynced ) {
				AMLPlayerDataNetData.BroadcastFromCurrentPlayer( this );
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