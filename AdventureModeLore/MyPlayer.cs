using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore {
	partial class AMLPlayer : ModPlayer {
		internal ISet<string> CompletedLoreStages { get; } = new HashSet<string>();

		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			this.CompletedLoreStages.Clear();

			if( tag.ContainsKey( "lore_done_count" ) ) {
				int count = tag.GetInt( "lore_done_count" );

				for( int i = 0; i < count; i++ ) {
					string uniqueKey = tag.GetString( "lore_done_" + i );
					this.CompletedLoreStages.Add( uniqueKey );
				}
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "lore_done_count", this.CompletedLoreStages.Count }
			};

			int i = 0;
			foreach( string key in this.CompletedLoreStages ) {
				tag["lore_done_" + i] = key;
				i++;
			}

			return tag;
		}
	}
}
