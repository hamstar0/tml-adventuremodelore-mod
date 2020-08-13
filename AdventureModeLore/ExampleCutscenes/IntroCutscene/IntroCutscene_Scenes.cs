using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.ExampleCutscenes.Intro.Scenes;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new AA_OpeningScene()
			};
		}
	}
}
