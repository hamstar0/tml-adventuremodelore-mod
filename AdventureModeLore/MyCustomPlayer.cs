using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.PlayerData;


namespace AdventureModeLore {
	partial class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			ProgressionLogic.Initialize();
		}
	}
}
