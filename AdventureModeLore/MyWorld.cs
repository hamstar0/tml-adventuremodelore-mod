using System;
using System.IO;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AdventureModeLore.WorldGeneration;


namespace AdventureModeLore {
	class AMLWorld : ModWorld {
		internal ISet<(int tileX, int tileY)> FailedExpeditions = new HashSet<(int, int)>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.FailedExpeditions.Clear();

			if( !tag.ContainsKey("expeditions") ) {
				return;
			}

			int count = tag.GetInt( "expeditions" );

			for( int i=0; i<count; i++ ) {
				int x = tag.GetInt( "expedition_x_" + i );
				int y = tag.GetInt( "expedition_y_" + i );

				this.FailedExpeditions.Add( (x, y) );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound { { "expeditions", this.FailedExpeditions.Count } };

			int i = 0;
			foreach( (int x, int y) in this.FailedExpeditions ) {
				tag[ "expedition_x_"+i ] = x;
				tag[ "expedition_y_"+i ] = y;
				i++;
			}
			return tag;
		}

		////

		public override void NetReceive( BinaryReader reader ) {
			this.FailedExpeditions.Clear();

			try {
				int count = reader.ReadInt32();
				
				for( int i=0; i<count; i++ ) {
					int x = reader.ReadInt32();
					int y = reader.ReadInt32();

					this.FailedExpeditions.Add( (x, y) );
				}
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				int count = this.FailedExpeditions.Count;

				writer.Write( this.FailedExpeditions.Count );

				foreach( (int x, int y) in this.FailedExpeditions ) {
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
			tasks.Insert( idx, new FailedExpeditionsGen() );
		}
	}
}
