using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Logic;


namespace AdventureModeLore.ExampleCutscenes.Intro.Net {
	[Serializable]
	class IntroCutsceneNetData : AMLCutsceneNetData {
		public Vector2 InteriorShipViewPosition;
		public Vector2 ExteriorShipViewPosition;



		////////////////

		private IntroCutsceneNetData() : base() { }
		
		public IntroCutsceneNetData( Player playsForWho, IntroCutscene cutscene, int sceneIdx )
					: base( playsForWho, cutscene, sceneIdx ) {
			this.InteriorShipViewPosition = cutscene.Data.InteriorShipViewPosition;
			this.ExteriorShipViewPosition = cutscene.Data.ExteriorShipViewPosition;
		}


		////////////////

		protected override bool PreReceive() {
			var cutscene = CutsceneManager.Instance.GetCutscene<IntroCutscene>();
			cutscene.SetData( this.InteriorShipViewPosition, this.ExteriorShipViewPosition );
			return true;
		}
	}
}
