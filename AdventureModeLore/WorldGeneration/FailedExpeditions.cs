using System;
using Terraria;
using Terraria.ID;
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
			int campWidth = 11;

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

			this.CreateExpeditions( progress, count, campWidth );
		}


		////

		private void CreateExpeditions( GenerationProgress progress, int count, int campWidth ) {
			(int x, int y)? expedPoint;
			int paveTileType = TileID.Dirt;
			int chestIdx;

			// Middle-map PKE meter camp

			expedPoint = this.FindMiddleSurfaceExpeditionLocation( campWidth, out paveTileType );
			if( !expedPoint.HasValue ) {
				LogHelpers.Log( "Could not generate first failed expedition" );
				return;
			}

			chestIdx = this.CreateExpeditionAt( expedPoint.Value.x, expedPoint.Value.y, campWidth, paveTileType, false );
			this.FillExpeditionBarrel(
				tileX: expedPoint.Value.x,
				tileY: expedPoint.Value.y,
				chestIdx: chestIdx,
				hasLoreNote: false,
				speedloaderCount: 0,
				orbCount: 0,
				canopicJarCount: 0,
				hasPKEMeter: true
			);

			// Jungle ocean camp

			expedPoint = this.FindJungleSideBeachExpeditionLocation( campWidth, out paveTileType );
			if( !expedPoint.HasValue ) {
				LogHelpers.Log( "Could not generate jungle failed expedition" );
				return;
			}

			chestIdx = this.CreateExpeditionAt( expedPoint.Value.x, expedPoint.Value.y, campWidth, paveTileType, true );
			this.FillExpeditionBarrel(
				tileX: expedPoint.Value.x,
				tileY: expedPoint.Value.y,
				chestIdx: chestIdx,
				hasLoreNote: true,
				speedloaderCount: WorldGen.genRand.NextFloat() < (2f / 3f) ? 1 : 0,
				orbCount: WorldGen.genRand.Next( 1, 3 ),
				canopicJarCount: WorldGen.genRand.Next( 0, 2 ),
				hasPKEMeter: false
			);

			progress.Value = 1f / (float)count;

			// Underground camps

			for( int expedNum=1; expedNum<count; expedNum++ ) {
				expedPoint = this.FindRandomExpeditionLocation( campWidth, out paveTileType );
				if( !expedPoint.HasValue ) {
					LogHelpers.Log( "Could not finish generating all failed expeditions ("+expedNum+" of "+count+")" );
					break;
				}

				chestIdx = this.CreateExpeditionAt( expedPoint.Value.x, expedPoint.Value.y, campWidth, paveTileType, true );
				this.FillExpeditionBarrel(
					tileX: expedPoint.Value.x,
					tileY: expedPoint.Value.y,
					chestIdx: chestIdx,
					hasLoreNote: true,
					speedloaderCount: WorldGen.genRand.NextFloat() < (2f / 3f) ? 1 : 0,
					orbCount: WorldGen.genRand.Next( 1, 3 ),
					canopicJarCount: WorldGen.genRand.Next( 0, 2 ),
					hasPKEMeter: false
				);
				
				progress.Value = (float)expedNum / (float)count;
			}
		}
	}
}
