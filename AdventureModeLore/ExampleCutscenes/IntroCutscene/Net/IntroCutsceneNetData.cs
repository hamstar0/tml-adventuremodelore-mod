using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;


namespace AdventureModeLore.ExampleCutscenes.Intro.Net {
	[Serializable]
	class IntroCutsceneNetData : AMLCutsceneNetData {
		public Vector2 InteriorShipViewPosition;



		////////////////

		private IntroCutsceneNetData() : base() { }
		
		public IntroCutsceneNetData( IntroCutscene cutscene, int sceneIdx ) : base( cutscene, sceneIdx ) { }


		////////////////

		protected override bool PreReceive( CutsceneID uid ) {
			f
			return true;
		}
	}
}
