using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene.Net {
	[Serializable]
	class IntroCutsceneNetData : AMLCutsceneNetData {
		private IntroCutsceneNetData() : base() { }
		
		public IntroCutsceneNetData( IntroCutscene cutscene, SceneID sceneId ) : base( cutscene, sceneId ) {
		}


		////////////////

		protected override bool PreReceive() {
			if( this.PlaysForWho != Main.myPlayer ) {
				return true;
			}

			return true;
		}
	}
}
