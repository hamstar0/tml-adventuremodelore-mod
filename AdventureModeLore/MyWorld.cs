using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Cutscenes;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		private bool IsThisWorldAdventureMode = false;



		////////////////

		public override void PostWorldGen() {
			LogHelpers.Log( "World "+Main.worldName+" prepped for Adventure Mode." );
			this.IsThisWorldAdventureMode = true;
		}

		////////////////

		public override void Load( TagCompound tag ) {
			if( tag.ContainsKey( "IsThisWorldAdventureMode" ) ) {
				this.IsThisWorldAdventureMode = true;
				CutsceneManager.Instance.LoadForWorld( tag );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "IsThisWorldAdventureMode", this.IsThisWorldAdventureMode },
			};
			CutsceneManager.Instance.SaveForWorld( tag );
			return tag;
		}

		////

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.IsThisWorldAdventureMode );
				CutsceneManager.Instance.NetSendForWorld( writer );
			} catch { }
		}

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.IsThisWorldAdventureMode = reader.ReadBoolean();
				CutsceneManager.Instance.NetReceiveForWorld( reader );
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