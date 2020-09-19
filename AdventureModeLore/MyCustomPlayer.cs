using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Services.Timers;
using Objectives;


namespace AdventureModeLore {
	class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				Timers.SetTimer( 1, false, () => {
					this.PostOnEnter();
					return false;
				} );
			}
		}

		private void PostOnEnter() {
			var amMod = ModLoader.GetMod( "AdventureMode" );
			if( (bool)amMod.Call("IsCurrentPlayerAndWorldValid") ) {
				this.LoadInitialObjective();
			}
		}

		private void LoadInitialObjective() {
			ObjectivesAPI.AddObjective(
				objective: ObjectiveDefinitions.InvestigateDungeon(),
				order: -1,
				alertPlayer: true,
				out string _
			);
		}
	}
}
