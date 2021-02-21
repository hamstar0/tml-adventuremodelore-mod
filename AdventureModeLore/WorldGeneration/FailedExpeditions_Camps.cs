using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private void CreateCamps( GenerationProgress progress, int count, int campWidth ) {
			this.CreateMidMapPKECamp( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateJungleOceanCamp( campWidth );
			progress.Value = 1f / ((float)count + 2f);

			this.CreateUndergroundCamps( progress, count, campWidth );
		}
	}
}
