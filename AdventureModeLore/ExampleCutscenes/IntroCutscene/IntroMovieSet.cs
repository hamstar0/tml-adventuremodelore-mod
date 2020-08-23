﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.World;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene {
	class IntroMovieSet : MovieSet {
		public static void GetSceneCoordinates( int width, out int boatLeft, out int boatTop, out bool isFlipped ) {
			//isFlipped = Main.spawnTileX > ( Main.maxTilesX / 2 );
			isFlipped = false;

			if( isFlipped ) {
				boatLeft = ( Main.maxTilesX - 40 ) - width;
				if( ( boatLeft % 2 ) == 0 ) {
					boatLeft++;
				}
			} else {
				boatLeft = 40;
			}

			boatTop = Math.Max( Main.spawnTileY - 100, 20 );

			Tile tile = Framing.GetTileSafely( boatLeft, boatTop );
			while( tile.liquid == 0 && boatTop < WorldHelpers.SurfaceLayerBottomTileY ) {
				tile = Framing.GetTileSafely( boatLeft, ++boatTop );
			}

			boatTop -= 18;
			//LogHelpers.Log( "left:"+boatLeft+" ("+Main.maxTilesX+")"
			//	+", top:"+boatTop+" ("+Main.maxTilesY+", "+Math.Max(Main.spawnTileY - 100, 20)+")");
		}



		////////////////

		public Vector2 ExteriorShipPos;
		public Vector2 InteriorShipPos;



		////////////////

		public IntroMovieSet() {
			char d = Path.DirectorySeparatorChar;
			TileStructure shipInterior = TileStructure.Load(
				AMLMod.Instance,
				"ExampleCutscenes" + d + "IntroCutscene" + d + "Ship Interior.dat" );
			TileStructure shipExterior = TileStructure.Load(
				AMLMod.Instance,
				"ExampleCutscenes" + d + "IntroCutscene" + d + "Ship Exterior.dat" );
			//LogHelpers.Log( "interior: "+ shipInterior.Bounds.ToString()+" ("+shipInterior.TileCount+")"
			//	+", exterior: "+shipExterior.Bounds.ToString()+" ("+shipExterior.TileCount+")");
			int left, top;
			bool isFlipped;

			IntroMovieSet.GetSceneCoordinates( shipExterior.Bounds.Width, out left, out top, out isFlipped );
			shipExterior.PaintToWorld(
				leftTileX: left,
				topTileY: top,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );

			this.ExteriorShipPos = new Vector2( left * 16, top * 16 );
			this.ExteriorShipPos.X += shipExterior.Bounds.Width * 8;    // (wid*16) / 2

			IntroMovieSet.GetSceneCoordinates( shipInterior.Bounds.Width, out left, out top, out isFlipped );
			top = Math.Max( top - 160, 40 );
			shipInterior.PaintToWorld(
				leftTileX: left,
				topTileY: top,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );

			this.InteriorShipPos = new Vector2( left * 16, top * 16 );
			this.InteriorShipPos.X += shipInterior.Bounds.Width * 8;    // (wid*16) / 2
			this.InteriorShipPos.Y += 16 * 16;
		}
	}
}