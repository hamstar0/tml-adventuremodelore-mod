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
				objective: new FlatObjective(
					title: "Investigate Dungeon",
					description: "There appears to be a large, ominous structure with a suspicious old man wandering around it's entrance. Recommend an investigation.",
					condition: ( obj ) => {
						return Main.player.Any( plr => {
							if( plr?.active != true ) {
								return false;
							}

							NPC oldMan = Main.npc.FirstOrDefault( n => n.type == NPCID.OldMan );
							if( oldMan?.active != true ) {
								return false;
							}

							return ( plr.position - oldMan.position ).LengthSquared() < ( 256f * 256f );
						} );
					}
				),
				order: -1,
				alertPlayer: true,
				out string _
			);
		}
	}
}
