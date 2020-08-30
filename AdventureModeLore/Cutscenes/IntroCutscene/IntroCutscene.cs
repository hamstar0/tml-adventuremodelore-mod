using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.TileStructure;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Info;
using AdventureModeLore.ExampleCutscenes.IntroCutscene.Scenes;
using CutsceneLib.Definitions;


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

		protected override bool Update() {
			return false;
		}
	}
}
