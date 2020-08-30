﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Net;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene {
	partial class IntroCutscene : Cutscene {
		protected override SceneBase CreateScene( SceneID sceneId ) {
			if( sceneId.Equals(this.FirstSceneId) ) {
				return this.CreateIntroScene();
			}

			return null;
		}

		////

		protected override SceneBase CreateSceneFromNetwork( SceneID sceneId, AMLCutsceneNetData data ) {
			if( sceneId.Equals(this.FirstSceneId) ) {
				return this.CreateIntroSceneFromNetwork( data );
			}

			return null;
		}


		////////////////

		private SceneBase CreateIntroScene() {
			var set = IntroMovieSet.Create( ref this._ShipExterior, ref this._ShipInterior, out _, out string __ );
			if( set != null ) {
				return new IntroCutsceneScene_00( set );
			}

			return null;
		}

		////

		private SceneBase CreateIntroSceneFromNetwork( AMLCutsceneNetData data ) {
			IntroMovieSet set = IntroMovieSet.Create(
				ref this._ShipExterior,
				ref this._ShipInterior,
				out Rectangle chunkRange,
				out string result
			);
			if( set != null ) {
				return new IntroCutsceneScene_00( set );
			}

			if( result != "Found null tile." ) {
				set = IntroMovieSet.Create( (IntroCutsceneNetData)data );
				return new IntroCutsceneScene_00( set );
			}

			// Request and await tile chunks from server
			if( this._MovieSetChunksRequestRetryTimer-- <= 0 ) {
				this._MovieSetChunksRequestRetryTimer = 60 * 2;	// Retry every 2s until timeout

				LogHelpers.Log( "Requesting chunks from range " + chunkRange );
				TileWorldHelpers.RequestChunksFromServer( chunkRange );
			}

			return null;
		}
	}
}
