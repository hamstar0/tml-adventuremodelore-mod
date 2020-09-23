using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore {
	partial class AMLPlayer : ModPlayer {
		internal ISet<string> AlreadyIntroducedNpcs { get; } = new HashSet<string>();

		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			this.AlreadyIntroducedNpcs.Clear();

			if( tag.ContainsKey( "introduced_npc_count" ) ) {
				int count = tag.GetInt( "introduced_npc_count" );

				for( int i = 0; i < count; i++ ) {
					string uniqueKey = tag.GetString( "introduced_npc_" + i );
					this.AlreadyIntroducedNpcs.Add( uniqueKey );
				}
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "introduced_npc_count", this.AlreadyIntroducedNpcs.Count }
			};

			int i = 0;
			foreach( string key in this.AlreadyIntroducedNpcs ) {
				tag["introduced_npc_" + i] = key;
				i++;
			}

			return tag;
		}


		////////////////

		public override void PreUpdate() {
		}
	}
}
