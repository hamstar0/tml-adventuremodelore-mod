using System;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.ModLoader;
using AdventureModeLore.WorldGeneration;


namespace AdventureModeLore {
	class AMLWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( t => t.Name.Equals("Settle Liquids Again") );
			if( idx == -1 ) {
				idx = tasks.Count;
			} else {
				idx += 1;
			}

			tasks.Insert( idx, new FailedExpeditionsGen() );
		}
	}
}
