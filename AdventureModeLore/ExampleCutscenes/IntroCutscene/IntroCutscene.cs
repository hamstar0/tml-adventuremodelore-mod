﻿using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Net;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene {
	partial class IntroCutscene : Cutscene {
		public override CutsceneID UniqueId { get; } = new CutsceneID(
			mod: AMLMod.Instance,
			cutsceneType: typeof(IntroCutscene)
		);

		public override SceneID FirstSceneId { get; } = new SceneID(
			mod: AMLMod.Instance,
			sceneType: typeof(IntroCutsceneScene_00)
		);


		////

		public override bool IsSiezingControls() => true;



		////////////////
		
		private IntroCutscene( Player playsFor ) : base( playsFor ) { }

		////

		protected override Scene CreateScene( SceneID sceneId ) {
			if( sceneId.Equals(this.FirstSceneId) ) {
				if( !IntroMovieSet.GetSceneCoordinates(1, out int _, out int __, out bool ___) ) {
					return null;
				}

				var set = new IntroMovieSet();
				return new IntroCutsceneScene_00( set );
			}

			return null;
		}

		protected override Scene CreateSceneFromNetwork( SceneID sceneId, AMLCutsceneNetData data ) {
			return this.CreateScene( sceneId );
		}

		////

		internal IntroCutsceneScene_00 GetIntroScene() {
			return this.CurrentScene as IntroCutsceneScene_00;
		}


		////////////////

		public override bool CanBegin() {
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				return false;
			}
			if( NPC.AnyNPCs(NPCID.Merchant) ) {
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
			return false;
		}
	}
}
