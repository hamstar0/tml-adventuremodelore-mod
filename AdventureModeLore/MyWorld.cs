using System;
using System.IO;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using AdventureModeLore.WorldGeneration;


namespace AdventureModeLore {
	partial class AMLWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( t => t.Name.Equals("Settle Liquids Again") );
			if( idx == -1 ) {
				idx = tasks.Count;
			} else {
				idx += 1;
			}

			tasks.Insert( idx, new FallenCyborgsGen() );
		}
	}
}
