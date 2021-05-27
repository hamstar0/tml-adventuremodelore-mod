using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FallenCyborgsGen : GenPass {
		public FallenCyborgsGen() : base( "Fallen Cyborgs", 1f ) { }


		////

		public override void Apply( GenerationProgress progress ) {
			int count = 24;

			switch( WorldLibraries.GetSize() ) {
			case WorldSize.SubSmall:
				count = 20;
				break;
			case WorldSize.Small:
				count = 24;
				break;
			case WorldSize.Medium:
				count = 31;
				break;
			case WorldSize.Large:
				count = 38;
				break;
			case WorldSize.SuperLarge:
				count = 45;
				break;
			}

			this.CreateCyborgs( progress, count );
		}
	}
}
