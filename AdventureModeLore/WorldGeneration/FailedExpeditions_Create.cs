using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class AbandonedExpeditionsGen : GenPass {
		private void CreateAllFEs( GenerationProgress progress, int count, int campWidth ) {
			this.CreateAtMidMapFE( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateJungleOceanFE( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateAllUndergroundFEs( progress, count, campWidth );
		}
	}
}
