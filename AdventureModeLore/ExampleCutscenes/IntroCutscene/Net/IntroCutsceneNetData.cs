using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro.Net {

	[Serializable]
	class IntroCutsceneNetData : AMLCutsceneNetData {
		public Vector2 InteriorShipViewPosition;
		public Vector2 ExteriorShipViewPosition;



		////////////////

		private IntroCutsceneNetData() : base() { }
		
		public IntroCutsceneNetData( IntroCutscene cutscene, SceneID sceneId ) : base( cutscene, sceneId ) {
			cutscene.GetData( out Vector2 exteriorShipPos, out Vector2 interiorShipPos );

			this.ExteriorShipViewPosition = exteriorShipPos;
			this.InteriorShipViewPosition = interiorShipPos;
		}


		////////////////

		protected override bool PreReceive() {
			Player playsFor = Main.player[ this.PlaysForWho ];
			IntroCutscene cutscene = CutsceneManager.Instance.GetCurrentCutscene_Player( playsFor ) as IntroCutscene;

			cutscene.SetData( this.ExteriorShipViewPosition, this.InteriorShipViewPosition );
			return true;
		}
	}
}
