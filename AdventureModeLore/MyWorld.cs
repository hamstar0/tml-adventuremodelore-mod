using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AdventureModeLore.Cutscenes.Intro;
using AdventureModeLore.Cutscenes;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		private bool IsThisWorldAdventureMode = false;

		internal ISet<CutsceneID> ActivatedCutscenes = new HashSet<CutsceneID>();



		////////////////

		public override void PostWorldGen() {
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

			CutsceneManager.Instance.UpdateForWorld();
		}
	}
}