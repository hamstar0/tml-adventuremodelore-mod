using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Example.Intro {
	partial class IntroCutscene : Cutscene {
		public static IntroCutscene Create( string affix ) {
			return new IntroCutscene( new CutsceneID(
				mod: AMLMod.Instance,
				name: "Intro_"+ affix
			) );
		}


		////////////////
		
		public static void GetSceneCoordinates( int width, out int boatLeft, out int boatTop, out bool isFlipped ) {
			isFlipped = Main.spawnTileX > (Main.maxTilesX / 2);

			if( isFlipped ) {
				boatLeft = (Main.maxTilesX - 40) - width;
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
		
		private IntroCutscene( CutsceneID uid ) : base(uid) { }

		////////////////

		protected override Vector2 OnBeginForWorld() {
			char d = Path.DirectorySeparatorChar;
			TileStructure shipInterior = TileStructure.Load( AMLMod.Instance, "Example" + d+"Intro"+d+"Ship Interior.dat" );
			TileStructure shipExterior = TileStructure.Load( AMLMod.Instance, "Example" + d+"Intro"+d+"Ship Exterior.dat" );
//LogHelpers.Log( "interior: "+ shipInterior.Bounds.ToString()+" ("+shipInterior.TileCount+")"
//	+", exterior: "+shipExterior.Bounds.ToString()+" ("+shipExterior.TileCount+")");
			int left, top;
			bool isFlipped;

			IntroCutscene.GetSceneCoordinates( shipExterior.Bounds.Width, out left, out top, out isFlipped );
			shipExterior.PaintToWorld( left, top, false, isFlipped, false );

			var startPos = new Vector2( left*16, top*16 );
			startPos.X += shipExterior.Bounds.Width * 8;	// (wid*16) / 2

			IntroCutscene.GetSceneCoordinates( shipInterior.Bounds.Width, out left, out top, out isFlipped );
			shipInterior.PaintToWorld( left, top - 160, false, isFlipped, false );

			return startPos;
		}
	}
}
