﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.CameraAnimation;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes {
	partial class IntroCutsceneScene_00 : Scene<IntroCutscene, IntroMovieSet> {
		public override SceneID UniqueId { get; } = new SceneID(
			mod: AMLMod.Instance,
			sceneType: typeof(IntroCutsceneScene_00)
		);



		////////////////

		public IntroCutsceneScene_00( IntroMovieSet set )  : base( false, false, set ) { }


		////////////////

		public override SceneID GetNextSceneId() {
			return null;
		}


		////////////////

		internal void GetData( out Vector2 exteriorShipViewPos, out Vector2 interiorShipViewPos ) {
			exteriorShipViewPos = this.Set.ExteriorShipView;
			interiorShipViewPos = this.Set.InteriorShipView;
		}

		internal void SetData( Vector2 exteriorShipViewPos, Vector2 interiorShipViewPos ) {
			this.Set.ExteriorShipView = exteriorShipViewPos;
			this.Set.InteriorShipView = interiorShipViewPos;
		}


		////////////////

		protected override void OnBegin( IntroCutscene parent ) {
			var cams = new List<CameraMover>();
			Vector2 exteriorShipView = this.Set.ExteriorShipView;
			Vector2 interiorShipView = this.Set.InteriorShipView;

			bool isShipOnLeft = (int)exteriorShipView.X < ((16 * Main.maxTilesX) / 2);

			Vector2 dungeonView = new Vector2( Main.dungeonX * 16, Main.dungeonY * 16 );
			dungeonView.X += isShipOnLeft ? (-32 * 16) : (32 * 16);
			dungeonView.Y += -32 * 16;

			int extShipViewScrollY = (int)exteriorShipView.Y - (6 * 16);
			interiorShipView.Y = interiorShipView.Y - (12f * 16f);

			this.BeginShot00_Title( parent );
			
			this.GetCam00_Title( cams, this.BeginShot01_ExteriorChat );
			this.GetCam01_ExteriorChat( cams, null, exteriorShipView );
			this.GetCam02_Dungeon( cams, this.BeginShot03_ExteriorAttack, dungeonView );
			this.GetCam03_ExteriorAttack( cams, this.BeginShot04_InteriorChat, exteriorShipView );
			this.GetCam04_InteriorChat( cams, null, interiorShipView );

			CameraMover.Current = cams[0];
		}


		////////////////

		protected override bool Update( IntroCutscene parent ) {
			var animCam = CameraMover.Current;
			if( animCam == null || !animCam.Name.StartsWith("AdventureModeIntro") || !animCam.IsAnimating() ) {
				return true;
			}

			return false;
		}


		////////////////

		public override void DrawInterface() {
			switch( CameraMover.Current.Name ) {
			case "AdventureModeIntro_Title":
				this.DrawInterface00_Title();
				break;
			}
		}
	}
}
