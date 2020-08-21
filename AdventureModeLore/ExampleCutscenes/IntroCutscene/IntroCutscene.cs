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
		public Vector2 ExteriorShipPos;
		public Vector2 InteriorShipPos;


		////////////////

		public override CutsceneID UniqueId { get; } = new CutsceneID( AMLMod.Instance.Name, typeof(IntroCutscene).Name );

		public override bool IsSiezingControls() => true;



		////////////////
		
		private IntroCutscene( Player playsFor ) : base( playsFor ) { }

		////////////////

		protected override Scene CreateInitialScene() {
			return new IntroCutsceneScene_00();
		}

		protected override Scene CreateScene( SceneID sceneId ) {
			if( sceneId == new SceneID(AMLMod.Instance, typeof(IntroCutsceneScene_00)) ) {
				return new IntroCutsceneScene_00();
			}

			return null;
		}


		////////////////

		internal void GetData( out Vector2 exteriorShipViewPos, out Vector2 interiorShipViewPos ) {
			exteriorShipViewPos = this.ExteriorShipPos;
			interiorShipViewPos = this.InteriorShipPos;
		}

		internal void SetData( Vector2 exteriorShipViewPos, Vector2 interiorShipViewPos ) {
			this.ExteriorShipPos = exteriorShipViewPos;
			this.InteriorShipPos = interiorShipViewPos;
		}


		////////////////

		public override bool CanBegin() {
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				return false;
			}
			if( NPC.AnyNPCs( NPCID.Merchant ) ) {
				return false;
			}
			return true;
		}


		////////////////

		public override AMLCutsceneNetData CreatePacketPayload( SceneID sceneId ) {
			return new IntroCutsceneNetData( this, sceneId );
		}


		////////////////

		protected override bool Update() {
			return true;
		}
	}
}
