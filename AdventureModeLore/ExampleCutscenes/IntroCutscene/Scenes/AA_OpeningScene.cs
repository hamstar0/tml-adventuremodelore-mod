using System;
using Terraria;
using HamstarHelpers.Services.Camera;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Scenes {
	class AA_OpeningScene : Scene<IntroCutscene> {
		public AA_OpeningScene()  : base( false ) { }


		////////////////

		protected override void OnBeginOnPlayer( IntroCutscene parent, Player player ) {
			CameraMover.Current = new CameraMover(
				name: "AdventureModeIntro",
				moveXFrom: (int)parent.CurrentPosition.X,
				moveYFrom: (int)parent.CurrentPosition.Y,
				moveXTo: (int)parent.CurrentPosition.X,
				moveYTo: (int)parent.CurrentPosition.Y - (12 * 16),
				toDuration: 60 * 3,
				lingerDuration: 60 * 3,
				froDuration: 0
			);
		}

		////////////////

		protected override bool UpdateOnLocal( IntroCutscene parent ) {
			var animCam = CameraMover.Current;
			if( animCam?.Name != "AdventureModeIntro" || !animCam.IsAnimating() ) {
				return true;
			}

			return false;
		}

		protected override bool UpdateOnWorld( IntroCutscene parent ) {
			return false;
		}
	}
}
