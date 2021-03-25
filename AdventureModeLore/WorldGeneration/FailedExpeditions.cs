using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		private int CurrentLoreNote = 0;



		////////////////

		public FailedExpeditionsGen() : base( "Failed Expeditions", 1f ) { }


		////

		public override void Apply( GenerationProgress progress ) {
			this.CurrentLoreNote = 0;

			int count = 14;
			int campWidth = 12;

			switch( WorldHelpers.GetSize() ) {
			case WorldSize.SubSmall:
				count = 11;
				break;
			case WorldSize.Small:
				count = 14;
				break;
			case WorldSize.Medium:
				count = 21;
				break;
			case WorldSize.Large:
				count = 28;
				break;
			case WorldSize.SuperLarge:
				count = 35;
				break;
			}

			this.CreateAllFEs( progress, count, campWidth );
		}
	}
}
