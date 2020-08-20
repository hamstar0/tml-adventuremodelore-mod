using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene> {
		private void GetCam02_Dungeon( IList<CameraMover> cams, Action onCamStop, Vector2 dungeonView ) {
			int next = cams.Count;
			var cam = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)dungeonView.X,
				moveYFrom: (int)dungeonView.Y,
				moveXTo: (int)dungeonView.X,
				moveYTo: (int)dungeonView.Y - ( 4 * 16 ),
				toDuration: 60 * 5,
				lingerDuration: 0,
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
