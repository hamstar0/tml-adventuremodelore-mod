using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene {
	class IntroMovieSet : MovieSet {
		public static bool GetSceneCoordinates( int width, out int boatLeft, out int boatTop, out bool isFlipped ) {
			//isFlipped = Main.spawnTileX > ( Main.maxTilesX / 2 );
			isFlipped = false;
			
			if( isFlipped ) {
				boatLeft = (Main.maxTilesX - 40) - width;
				if( (boatLeft % 2) == 0 ) {
					boatLeft++;
				}
			} else {
				boatLeft = 40;
			}

			boatTop = Math.Max( Main.spawnTileY - 100, 40 );
			int oceanTop = boatTop;

			Tile tile = Main.tile[ boatLeft, boatTop ];
			while(
					tile != null
					&& tile.liquid == 0
					&& !TileHelpers.IsSolid(tile, true, true)
					&& boatTop < WorldHelpers.SurfaceLayerBottomTileY ) {
				boatTop++;
				oceanTop++;
				tile = Main.tile[ boatLeft, boatTop ];
			}

			boatTop -= 18;
			//LogHelpers.Log( "left:"+boatLeft+" ("+Main.maxTilesX+")"
			//	+", top:"+boatTop+" ("+Main.maxTilesY+", "+Math.Max(Main.spawnTileY - 100, 20)+")");

			return tile != null
				&& tile.liquid != 0
				&& !TileHelpers.IsSolid( tile, true, true )
				&& oceanTop < WorldHelpers.SurfaceLayerBottomTileY;
		}



		////////////////

		public Vector2 ExteriorShipView;
		public Vector2 InteriorShipView;



		////////////////

		public IntroMovieSet() {
			char d = Path.DirectorySeparatorChar;
			TileStructure shipInterior = TileStructure.Load(
				mod: AMLMod.Instance,
				pathOfModFile: "ExampleCutscenes" + d + "IntroCutscene" + d + "Ship Interior.dat"
			);
			TileStructure shipExterior = TileStructure.Load(
				mod: AMLMod.Instance,
				pathOfModFile: "ExampleCutscenes" + d + "IntroCutscene" + d + "Ship Exterior.dat"
			);
			//LogHelpers.Log( "interior: "+ shipInterior.Bounds.ToString()+" ("+shipInterior.TileCount+")"
			//	+", exterior: "+shipExterior.Bounds.ToString()+" ("+shipExterior.TileCount+")");
			int extLeft, extTop;
			int intLeft, intTop;
			bool isFlipped;

			IntroMovieSet.GetSceneCoordinates( shipExterior.Bounds.Width, out extLeft, out extTop, out isFlipped );
			IntroMovieSet.GetSceneCoordinates( shipInterior.Bounds.Width, out intLeft, out intTop, out isFlipped );
			intTop = Math.Max( intTop - 160, 40 );

			this.ExteriorShipView = new Vector2( extLeft * 16, extTop * 16 );
			this.ExteriorShipView.X += shipExterior.Bounds.Width * 8;    // (wid*16) / 2
			this.ExteriorShipView.Y += -8 * 16;

			this.InteriorShipView = new Vector2( intLeft * 16, intTop * 16 );
			this.InteriorShipView.X += shipInterior.Bounds.Width * 8;    // (wid*16) / 2
			this.InteriorShipView.Y += 32 * 16;

			shipExterior.PaintToWorld(
				leftTileX: extLeft,
				topTileY: extTop,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );

			shipInterior.PaintToWorld(
				leftTileX: intLeft,
				topTileY: intTop,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );
		}
	}
}
