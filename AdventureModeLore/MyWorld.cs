using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.Logic;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		private bool IsThisWorldAdventureMode = false;


		////////////////

		internal ISet<CutsceneID> TriggeredCutsceneIDs_World { get; } = new HashSet<CutsceneID>();



		////////////////

		public override void Initialize() {
			this.IsThisWorldAdventureMode = false;
			this.TriggeredCutsceneIDs_World.Clear();
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
				CutsceneManager.Instance.Load_World( this, tag );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "IsThisWorldAdventureMode", this.IsThisWorldAdventureMode },
			};
			CutsceneManager.Instance.Save_World( this, tag );
			return tag;
		}

		////

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.IsThisWorldAdventureMode );
				CutsceneManager.Instance.NetSend_World( this, writer );
			} catch { }
		}

		public override void NetReceive( BinaryReader reader ) {
			try {
				this.IsThisWorldAdventureMode = reader.ReadBoolean();
				CutsceneManager.Instance.NetReceive_World( this, reader );
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

			CutsceneManager.Instance.Update_WorldAndHost( this );
		}
	}
}