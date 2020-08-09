using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.Example.Intro.Scenes;


namespace AdventureModeLore.Example.Intro {
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
