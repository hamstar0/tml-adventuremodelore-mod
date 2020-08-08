using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Cutscenes.Intro.Scenes;


namespace AdventureModeLore.Cutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public override Scene[] Scenes => new Scene[] {
			new IntroScene()
		};
	}
}
