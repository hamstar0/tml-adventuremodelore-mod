using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.ExampleCutscene.Intro.Scenes;


namespace AdventureModeLore.ExampleCutscene.Intro {
	partial class IntroCutscene : Cutscene {
		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new IntroScene(
					camBegin: this.StartPosition,
					camEnd: this.StartPosition + new Vector2( 0f, -4f ),
					camMoveDuration: 60 * 5,
					camLingerDuration: 0
				)
			};
		}
	}
}
