﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene, IntroMovieSet> {
		private void BeginShot00_Title( IntroCutscene cutscene ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			//cutscene.AddActor( NPCID.Guide, )
		}


		////////////////

		private void GetCam00_Title( IList<CameraMover> cams, Action onCamStop ) {
			int next = cams.Count;
			var cam = new CameraMover(
				name: "AdventureModeIntro_Title",
				moveXFrom: (Main.maxTilesX - 40) - (Main.screenWidth / 16),
				moveYFrom: 40,
				moveXTo: (Main.maxTilesX - 40) - (Main.screenWidth / 16),
				moveYTo: 40,
				toDuration: 0,
				lingerDuration: 60 * 5,
				froDuration: 0,
				onStop: () => {
					onCamStop?.Invoke();
					CameraMover.Current = cams[next + 1];
				}
			);

			cams.Add( cam );
		}


		////////////////
		
		private void DrawInterface00_Title() {
			string titleText = "Test Title";
			Vector2 titleDim = Main.fontMouseText.MeasureString( titleText );
			var pos = new Vector2( (Main.screenWidth / 2) - titleDim.X, (Main.screenHeight / 2) - titleDim.Y );

			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: titleText,
				x: pos.X,
				y: pos.Y,
				textColor: Color.White,
				borderColor: Color.Black,
				origin: default(Vector2),
				scale: 2f
			);
		}
	}
}
