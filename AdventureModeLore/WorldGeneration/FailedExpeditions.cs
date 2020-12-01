using System;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		public FailedExpeditionsGen() : base( "Failed Expeditions", 1f ) { }

		
		////

		public override void Apply( GenerationProgress progress ) {
			int count = 14;

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

			for( int expedNum=0; expedNum<count; expedNum++ ) {
				(int x, int y)? expedPoint = null;

				for( int loop=0; loop<1000; loop++ ) {
					expedPoint = this.FindExpeditionLocation();
					if( !expedPoint.HasValue ) {
						continue;
					}

					this.GenerateExpeditionAt( expedPoint.Value.x, expedPoint.Value.y );
					break;
				}

				if( !expedPoint.HasValue ) {
					LogHelpers.Log( "Could not finish generating all failed expeiditions ("+expedNum+" of "+count+")" );
					break;
				}

				progress.Value = (float)expedNum / (float)count;
			}
		}


		////////////////

		private void GenerateExpeditionAt( int tileX, int tileY ) {
			f
		}
	}
}
