﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using HamstarHelpers.Helpers.World;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public static void GetSceneCoordinates( int width, out int boatLeft, out int boatTop, out bool isFlipped ) {
			isFlipped = Main.spawnTileX > (Main.maxTilesX / 2);

			if( isFlipped ) {
				boatLeft = (Main.maxTilesX - 40) - width;
				if( (boatLeft % 2) == 0 ) {
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

		public override bool CanBeginForWorld() {
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				return false;
			}
			if( NPC.AnyNPCs(NPCID.Merchant) ) {
				return false;
			}
			
			return base.CanBeginForWorld();
		}

		////////////////

		protected override Vector2 OnBeginForWorld() {
			char d = Path.DirectorySeparatorChar;
			TileStructure shipInterior = TileStructure.Load(
				AMLMod.Instance,
				"ExampleCutscenes"+d+"IntroCutscene"+d+"Ship Interior.dat" );
			TileStructure shipExterior = TileStructure.Load(
				AMLMod.Instance,
				"ExampleCutscenes"+d+"IntroCutscene"+d+"Ship Exterior.dat" );
//LogHelpers.Log( "interior: "+ shipInterior.Bounds.ToString()+" ("+shipInterior.TileCount+")"
//	+", exterior: "+shipExterior.Bounds.ToString()+" ("+shipExterior.TileCount+")");
			int left, top;
			bool isFlipped;

			IntroCutscene.GetSceneCoordinates( shipExterior.Bounds.Width, out left, out top, out isFlipped );
			shipExterior.PaintToWorld(
				leftTileX: left,
				topTileY: top,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );

			var startPos = new Vector2( left*16, top*16 );
			startPos.X += shipExterior.Bounds.Width * 8;	// (wid*16) / 2

			IntroCutscene.GetSceneCoordinates( shipInterior.Bounds.Width, out left, out top, out isFlipped );
			shipInterior.PaintToWorld(
				leftTileX: left,
				topTileY: top - 160,
				paintAir: false,
				respectLiquids: true,
				flipHorizontally: isFlipped,
				flipVertically: false );

			return startPos;
		}
	}
}
