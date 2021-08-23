using System;
using System.IO;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.DotNET.Extensions;
using AdventureModeLore.WorldGeneration;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		internal IDictionary<(int tileX, int tileY), bool> LostExpeditions = new Dictionary<(int, int), bool>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.LostExpeditions.Clear();

			if( !tag.ContainsKey("expeditions") ) {
				return;
			}

			int count = tag.GetInt( "expeditions" );

			for( int i=0; i<count; i++ ) {
				int x = tag.GetInt( "expedition_x_" + i );
				int y = tag.GetInt( "expedition_y_" + i );
				bool found = tag.GetBool( "expedition_" + i );

				this.LostExpeditions[ (x, y) ] = found;
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound { { "expeditions", this.LostExpeditions.Count } };

			int i = 0;
			foreach( ((int x, int y) tile, bool isFound) in this.LostExpeditions ) {
				tag[ "expedition_x_"+i ] = tile.x;
				tag[ "expedition_y_"+i ] = tile.y;
				tag[ "expedition_"+i ] = isFound;
				i++;
			}
			return tag;
		}

		////

		public override void NetReceive( BinaryReader reader ) {
			this.LostExpeditions.Clear();

			try {
				int count = reader.ReadInt32();
				
				for( int i=0; i<count; i++ ) {
					int x = reader.ReadInt32();
					int y = reader.ReadInt32();
					bool found = reader.ReadBoolean();

					this.LostExpeditions[ (x, y) ] = found;
				}
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				int count = this.LostExpeditions.Count;

				writer.Write( this.LostExpeditions.Count );

				foreach( ((int x, int y), bool isFound) in this.LostExpeditions ) {
					writer.Write( x );
					writer.Write( y );
					writer.Write( isFound );
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
			tasks.Insert( idx, new LostExpeditionsGen() );
		}
	}
}
