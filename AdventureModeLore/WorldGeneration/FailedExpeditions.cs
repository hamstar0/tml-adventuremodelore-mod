using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		public FailedExpeditionsGen() : base( "Failed Expeditions", 1f ) { }

		
		////

		public override void Apply( GenerationProgress progress ) {
			int count = 14;
			int campWidth = 8;

			switch( WorldHelpers.GetSize() ) {
			case WorldSize.SubSmall:
				count = 7;
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

			int paveTileType = TileID.Dirt;

			for( int expedNum=0; expedNum<count; expedNum++ ) {
				(int x, int y)? expedPoint = null;

				for( int loop=0; loop<2000; loop++ ) {
					expedPoint = this.FindExpeditionLocation( campWidth, out paveTileType );
					if( expedPoint.HasValue ) {
						break;
					}
				}
				if( !expedPoint.HasValue ) {
					LogHelpers.Log( "Could not finish generating all failed expeiditions ("+expedNum+" of "+count+")" );
					break;
				}

				this.GenerateExpeditionAt( expedPoint.Value.x, expedPoint.Value.y, campWidth, paveTileType );

				progress.Value = (float)expedNum / (float)count;
			}
		}
	}
}
