using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Logic;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene.Net {
	[Serializable]
	class IntroCutsceneNetData : AMLCutsceneNetData {
		public Vector2 InteriorShipView;
		public Vector2 ExteriorShipView;



		////////////////

		private IntroCutsceneNetData() : base() { }
		
		public IntroCutsceneNetData( IntroCutscene cutscene, SceneID sceneId ) : base( cutscene, sceneId ) {
			cutscene.GetIntroScene().GetData( out this.ExteriorShipView, out this.InteriorShipView );
		}


		////////////////

		protected override bool PreReceive() {
			Player playsFor = Main.player[ this.PlaysForWho ];
			IntroCutscene cutscene = CutsceneManager.Instance.GetCurrentCutscene_Player( playsFor ) as IntroCutscene;

			cutscene.GetIntroScene().SetData( this.ExteriorShipView, this.InteriorShipView );
			return true;
		}
	}
}
