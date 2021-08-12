using System;
using System.IO;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AdventureModeLore.WorldGeneration;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		internal ISet<(int tileX, int tileY)> AbandonedExpeditions = new HashSet<(int, int)>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.AbandonedExpeditions.Clear();

			if( !tag.ContainsKey("expeditions") ) {
				return;
			}

			int count = tag.GetInt( "expeditions" );

			for( int i=0; i<count; i++ ) {
				int x = tag.GetInt( "expedition_x_" + i );
				int y = tag.GetInt( "expedition_y_" + i );

				this.AbandonedExpeditions.Add( (x, y) );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound { { "expeditions", this.AbandonedExpeditions.Count } };

			int i = 0;
			foreach( (int x, int y) in this.AbandonedExpeditions ) {
				tag[ "expedition_x_"+i ] = x;
				tag[ "expedition_y_"+i ] = y;
				i++;
			}
			return tag;
		}

		////

		public override void NetReceive( BinaryReader reader ) {
			this.AbandonedExpeditions.Clear();

			try {
				int count = reader.ReadInt32();
				
				for( int i=0; i<count; i++ ) {
					int x = reader.ReadInt32();
					int y = reader.ReadInt32();

					this.AbandonedExpeditions.Add( (x, y) );
				}
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				int count = this.AbandonedExpeditions.Count;

				writer.Write( this.AbandonedExpeditions.Count );

				foreach( (int x, int y) in this.AbandonedExpeditions ) {
					writer.Write( x );
					writer.Write( y );
				}
			} catch { }
		}


		////////////////

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( t => t.Name.Equals("Settle Liquids Again") );
			if( idx == -1 ) {
				idx = tasks.Count;
			} else {
				idx += 1;
			}

			tasks.Insert( idx, new FallenCyborgsGen() );
			tasks.Insert( idx, new AbandonedExpeditionsGen() );
		}
	}
}
