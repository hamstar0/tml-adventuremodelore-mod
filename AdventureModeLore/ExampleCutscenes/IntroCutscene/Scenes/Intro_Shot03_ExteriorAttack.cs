﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene, IntroMovieSet> {
		private void BeginShot03_ExteriorAttack() {

		}


		////////////////

		private void GetCam03_ExteriorAttack(
					IList<CameraMover> cams,
					Action onCamStop,
					Vector2 exteriorShipView,
					float extShipViewY ) {
			int next = cams.Count;
			var cam = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)exteriorShipView.X,
				moveYFrom: (int)extShipViewY,
				moveXTo: (int)exteriorShipView.X,
				moveYTo: (int)extShipViewY,
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
	}
}