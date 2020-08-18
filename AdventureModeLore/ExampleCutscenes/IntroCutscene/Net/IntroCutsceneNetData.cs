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
		
		public IntroCutsceneNetData( IntroCutscene cutscene, Player playsFor, int sceneIdx )
					: base( cutscene, playsFor, sceneIdx ) {
			cutscene.GetData( playsFor, out Vector2 exteriorShipPos, out Vector2 interiorShipPos );

			this.ExteriorShipViewPosition = exteriorShipPos;
			this.InteriorShipViewPosition = interiorShipPos;
		}


		////////////////

		protected override bool PreReceive() {
			var cutscene = CutsceneManager.Instance.GetCutscene<IntroCutscene>();

			cutscene.SetData( Main.player[this.PlaysForWho], this.ExteriorShipViewPosition, this.InteriorShipViewPosition );
			return true;
		}
	}
}
