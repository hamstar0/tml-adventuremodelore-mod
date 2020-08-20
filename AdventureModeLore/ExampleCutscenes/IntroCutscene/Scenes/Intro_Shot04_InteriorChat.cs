﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene> {
		private void GetCam04_InteriorChat( IList<CameraMover> cams, Vector2 interiorShipView ) {
			int next = cams.Count;
			var cam = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)interiorShipView.X,
				moveYFrom: (int)interiorShipView.Y,
				moveXTo: (int)interiorShipView.X,
				moveYTo: (int)interiorShipView.Y,
				toDuration: 0,
				lingerDuration: 60 * 5,
				froDuration: 0
				//onStop: () => CameraMover.Current = cams[next + 1]
			);

			cams.Add( cam );
		}
	}
}
