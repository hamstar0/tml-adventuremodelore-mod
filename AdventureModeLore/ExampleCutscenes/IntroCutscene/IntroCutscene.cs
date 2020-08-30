using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using HamstarHelpers.Helpers.Tiles;
using AdventureModeLore.Definitions;
using AdventureModeLore.Net;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Net;


namespace AdventureModeLore.ExampleCutscenes.IntroCutscene {
	partial class IntroCutscene : Cutscene {
		private TileStructure _ShipExterior;
		private TileStructure _ShipInterior;
		private int _MovieSetChunksRequestRetryTimer = 0;


		////////////////

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
				var set = IntroMovieSet.Create( ref this._ShipExterior, ref this._ShipInterior, out _, out string __ );
				if( set != null ) {
					return new IntroCutsceneScene_00( set );
				}
			}

			return null;
		}

		protected override Scene CreateSceneFromNetwork( SceneID sceneId, AMLCutsceneNetData data ) {
			if( sceneId.Equals(this.FirstSceneId) ) {
				var set = IntroMovieSet.Create( ref this._ShipExterior, ref this._ShipInterior, out Rectangle chunkRange, out _ );
				if( set != null ) {
					return new IntroCutsceneScene_00( set );
				}

				if( this._MovieSetChunksRequestRetryTimer-- == 0 ) {
					this._MovieSetChunksRequestRetryTimer = 60 * 2;
					LogHelpers.Log("Requesting chunks from range "+chunkRange);
					TileWorldHelpers.RequestChunksFromServer( chunkRange );
				}
			}

			return null;
		}


		////////////////

		public override bool CanBegin( out string result ) {
			if( GameInfoHelpers.GetVanillaProgressList().Count > 0 ) {
				result = "World progress has occurred.";
				return false;
			}
			if( NPC.AnyNPCs(NPCID.Merchant) ) {
				result = "Merchants exist.";
				return false;
			}
			result = "Success.";
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
