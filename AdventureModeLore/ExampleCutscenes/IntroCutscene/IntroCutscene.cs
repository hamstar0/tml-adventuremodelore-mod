using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;
using AdventureModeLore.ExampleCutscenes.Intro.Scenes;
using AdventureModeLore.ExampleCutscenes.Intro.Net;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public class IntroData {
			public Vector2 ExteriorShipViewPosition;
			public Vector2 InteriorShipViewPosition;
		}



		////////////////

		public static IntroCutscene Create( string affix ) {
			return new IntroCutscene( new CutsceneID(
				mod: AMLMod.Instance,
				name: "Intro_"+ affix
			) );
		}



		////////////////

		internal IntroData Data;



		////////////////
		
		private IntroCutscene( CutsceneID uid ) : base(uid) { }


		////////////////

		public override AMLCutsceneNetData GetPacketPayload( int sceneIdx ) {
			return new IntroCutsceneNetData( this, sceneIdx );
		}


		////////////////

		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new AA_OpeningScene()
			};
		}
	}
}
