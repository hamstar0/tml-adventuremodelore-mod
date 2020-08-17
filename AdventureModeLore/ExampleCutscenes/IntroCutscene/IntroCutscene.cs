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

		internal IntroData Data = null;



		////////////////
		
		private IntroCutscene( CutsceneID uid ) : base(uid) { }


		////////////////

		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new AA_OpeningScene()
			};
		}


		////////////////

		public override AMLCutsceneNetData GetPacketPayload( Player playsFor, int sceneIdx ) {
			return new IntroCutsceneNetData( playsFor, this, sceneIdx );
		}

		public void SetData( Vector2 exteriorShipViewPosition, Vector2 interiorShipViewPosition ) {
			this.Data = new IntroData {
				ExteriorShipViewPosition = exteriorShipViewPosition,
				InteriorShipViewPosition = interiorShipViewPosition
			};
		}
	}
}
