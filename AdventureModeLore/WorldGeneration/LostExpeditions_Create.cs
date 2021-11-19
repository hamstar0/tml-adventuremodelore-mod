using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		private void CreateAllFEs( GenerationProgress progress, int count, int campWidth ) {
			this.CreateAtDungeonLE( campWidth );
			progress.Value += 1f / ((float)count + 3f);
			
			this.CreateAtMidMapLE( campWidth );
			progress.Value += 1f / ((float)count + 3f);

			this.CreateJungleOceanLE( campWidth );
			progress.Value += 1f / ((float)count + 3f);

			this.CreateAllUndergroundLEs( progress, count, campWidth );
		}
	}
}
