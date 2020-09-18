using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Services.Timers;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore {
	class AMLPlayer : CustomPlayerData {
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
			if( (bool)amMod.Call("IsContextAuthentic") ) {
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
