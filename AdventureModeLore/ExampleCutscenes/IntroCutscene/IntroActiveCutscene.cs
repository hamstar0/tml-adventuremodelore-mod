using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.ExampleCutscenes.Intro {
	partial class IntroCutscene : Cutscene {
		protected class IntroActiveCutscene : ActiveCutscene {
			public Vector2 ExteriorShipPos;
			public Vector2 InteriorShipPos;



			////////////////

			public IntroActiveCutscene(
						Cutscene parent,
						Player playsFor,
						int sceneIdx,
						Vector2 shipExteriorPos,
						Vector2 shipInteriorPos )
					: base( parent, playsFor, sceneIdx ) {
				this.ExteriorShipPos = shipExteriorPos;
				this.InteriorShipPos = shipInteriorPos;
			}

			public override ActiveCutscene Clone() {
				Player plr = Main.player[this.PlaysForWhom];
				if( plr?.active != true ) {
					LogHelpers.Warn( "Inactive player " + this.PlaysForWhom );
					return null;
				}

				return new IntroActiveCutscene(
					this.Parent,
					plr,
					this.CurrentSceneIdx,
					this.ExteriorShipPos,
					this.InteriorShipPos
				);
			}

			////

			internal void SetData( Vector2 shipExteriorPos, Vector2 shipInteriorPos ) {
				this.ExteriorShipPos = shipExteriorPos;
				this.InteriorShipPos = shipInteriorPos;
			}
		}
	}
}
