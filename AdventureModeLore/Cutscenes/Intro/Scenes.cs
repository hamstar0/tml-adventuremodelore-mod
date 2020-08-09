using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Cutscenes.Intro.Scenes;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new IntroScene(
					cameraBegin: this.StartPosition,
					cameraEnd: this.StartPosition + new Vector2( 0f, -4f ),
					cameraMoveDuration: 60 * 5,
					cameraLingerDuration: 0
				)
			};
		}
	}
}
