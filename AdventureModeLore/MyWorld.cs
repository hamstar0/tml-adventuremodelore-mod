using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Cutscenes;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		private bool IsThisWorldAdventureMode = false;


		////////////////

		internal ISet<CutsceneID> TriggeredCutsceneIDsForWorld { get; } = new HashSet<CutsceneID>();

		public CutsceneID CurrentPlayingCutsceneForWorld { get; internal set; }



		////////////////

		public override void Initialize() {
			this.IsThisWorldAdventureMode = false;
			this.TriggeredCutsceneIDsForWorld.Clear();
		}


		////////////////

		public override void PostWorldGen() {
			LogHelpers.Log( "World "+Main.worldName+" prepped for Adventure Mode." );
			this.IsThisWorldAdventureMode = true;
		}

		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "IsThisWorldAdventureMode" ) ) {
				this.IsThisWorldAdventureMode = true;
				CutsceneManager.Instance.LoadForWorld( this, tag );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "IsThisWorldAdventureMode", this.IsThisWorldAdventureMode },
			};
			CutsceneManager.Instance.SaveForWorld( this, tag );
			return tag;
		}

		////

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.IsThisWorldAdventureMode );
				CutsceneManager.Instance.NetSendForWorld( this, writer );
			} catch { }
		}

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.IsThisWorldAdventureMode = reader.ReadBoolean();
				CutsceneManager.Instance.NetReceiveForWorld( this, reader );
			} catch { }
		}


		////////////////

		public override void PreUpdate() {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}
			if( !this.IsThisWorldAdventureMode ) {
				return;
			}

			CutsceneManager.Instance.UpdateForWorld( this );
		}
	}
}