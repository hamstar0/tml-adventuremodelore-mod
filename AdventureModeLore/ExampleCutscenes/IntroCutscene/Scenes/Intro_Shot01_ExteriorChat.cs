﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene> {
		private void BeginShot01_ExteriorChat() {

		}


		////////////////

		private void GetCam01_ExteriorChat( IList<CameraMover> cams, Action onCamStop, Vector2 exteriorShipView ) {
			int next = cams.Count;
			var cam = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)exteriorShipView.X,
				moveYFrom: (int)exteriorShipView.Y,
				moveXTo: (int)exteriorShipView.X,
				moveYTo: (int)exteriorShipView.Y - ( 12 * 16 ),
				toDuration: 60 * 3,
				lingerDuration: 60 * 3,
				froDuration: 0,
				onStop: () => {
					onCamStop?.Invoke();
					CameraMover.Current = cams[next + 1];
				}
			);

			cams.Add( cam );
		}
	}
}
