using System;
using Terraria;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public static void GetSceneCoordinates( int width, out int boatLeft, out int boatTop, out bool isFlipped ) {
			isFlipped = Main.spawnTileX > (Main.maxTilesX / 2);

			if( isFlipped ) {
				boatLeft = (Main.maxTilesX - 40) - width;
			} else {
				boatLeft = 40;
			}

			boatTop = Math.Max( Main.spawnTileY - 100, 20 );

			for( Tile tile = Framing.GetTileSafely( boatLeft, boatTop );
				tile.liquid == 0 && boatTop < WorldHelpers.SurfaceLayerBottomTileY;
				tile = Framing.GetTileSafely( boatLeft, ++boatTop ) ) {
			}

			boatTop -= 18;
//LogHelpers.Log( "left:"+boatLeft+" ("+Main.maxTilesX+")"
//	+", top:"+boatTop+" ("+Main.maxTilesY+", "+Math.Max(Main.spawnTileY - 100, 20)+")");
		}



		////////////////

		public override CutsceneID UniqueId => CutsceneID.Intro;



		////////////////
		
		public override bool HasValidWorldConditions() {
			return true;
		}

		public override void BeginForWorld() {
			TileStructure shipInterior = TileStructure.Load( AMLMod.Instance, "Ship Interior.dat" );
			TileStructure shipExterior = TileStructure.Load( AMLMod.Instance, "Ship Exterior.dat" );
//LogHelpers.Log( "interior: "+ shipInterior.Bounds.ToString()+" ("+shipInterior.TileCount+")"
//	+", exterior: "+shipExterior.Bounds.ToString()+" ("+shipExterior.TileCount+")");
			int left, top;
			bool isFlipped;

			IntroCutscene.GetSceneCoordinates( shipExterior.Bounds.Width, out left, out top, out isFlipped );
			shipExterior.PaintToWorld( left, top, false, isFlipped, false );

			IntroCutscene.GetSceneCoordinates( shipInterior.Bounds.Width, out left, out top, out isFlipped );
			shipInterior.PaintToWorld( left, top - 160, false, isFlipped, false );
		}


		////////////////

		internal override void UpdateForWorld() {
		}
	}
}
