using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;
using AdventureModeLore.ExampleCutscenes.Intro.Scenes;
using AdventureModeLore.ExampleCutscenes.Intro.Net;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		public static IntroCutscene Create( string affix ) {
			return new IntroCutscene( new CutsceneID(
				mod: AMLMod.Instance,
				name: "Intro_"+ affix
			) );
		}



		////////////////

		public override bool IsSiezingControls() => true;



		////////////////

		private IntroCutscene( CutsceneID uid ) : base(uid) { }


		////////////////

		protected override Scene[] LoadScenes() {
			return new Scene[] {
				new AA_OpeningScene()
			};
		}


		////////////////

		public override bool CanBegin( Player playsFor ) {
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				return false;
			}
			if( NPC.AnyNPCs( NPCID.Merchant ) ) {
				return false;
			}

			return base.CanBegin( playsFor );
		}


		////////////////
		
		public override AMLCutsceneNetData GetPacketPayload( Player playsFor, int sceneIdx ) {
			return new IntroCutsceneNetData( this, playsFor, sceneIdx );
		}


		////////////////

		internal void GetData( Player playsFor, out Vector2 exteriorShipViewPos, out Vector2 interiorShipViewPos ) {
			var actCut = this.GetActiveCutscene<IntroActiveCutscene>( playsFor );

			exteriorShipViewPos = actCut.ExteriorShipPos;
			interiorShipViewPos = actCut.InteriorShipPos;
		}

		internal void SetData( Player playsFor, Vector2 exteriorShipViewPos, Vector2 interiorShipViewPos ) {
			var actCut = this.GetActiveCutscene<IntroActiveCutscene>( playsFor );

			actCut.SetData( exteriorShipViewPos, interiorShipViewPos );
		}
	}
}
