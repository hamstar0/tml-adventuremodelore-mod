using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Dialogue;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public bool BeginForLocalPlayer() {
			var myplayer = Main.LocalPlayer.GetModPlayer<AMLPlayer>();
			if( myplayer.CompletedLoreStages.Contains(this.Name) ) {
				return true;
			}

			if( !this.ArePrerequisitesMet() ) {
				return false;
			}

			if( this.AreAllObjectivesComplete() ) {
				myplayer.CompletedLoreStages.Add( this.Name );

				return true;
			}

			//

			DynamicDialogueHandler oldDialogueHandler = DialogueEditor.GetDynamicDialogueHandler( this.NPCType );
			int currSubStage = 0;

			DialogueEditor.SetDynamicDialogueHandler( this.NPCType, new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					msg = this.GetDialogueAndAdvanceSubStage( msg, ref currSubStage, out bool isFinal );

					if( isFinal ) {
						if( oldDialogueHandler != null ) {
							DialogueEditor.SetDynamicDialogueHandler( this.NPCType, oldDialogueHandler );
						} else {
							DialogueEditor.RemoveDynamicDialogueHandler( this.NPCType );
						}
					}

					return msg;
				},
				isShowingAlert: () => true
			) );

			return true;
		}


		////////////////

		private string GetDialogueAndAdvanceSubStage( string existingMessage, ref int currSubStage, out bool isFinal ) {
			NPCLoreSubStage subStage = this.StepSubStage( ref currSubStage, out isFinal );

			return subStage?.Dialogue.Invoke()
				?? existingMessage;
		}


		////////////////

		private NPCLoreSubStage StepSubStage( ref int currStage, out bool isFinal ) {
			NPCLoreSubStage resultSubStage = null;

			for( ; currStage < this.SubStages.Length; currStage++ ) {
				NPCLoreSubStage currSubStage = this.SubStages[ currStage ];

				if( currSubStage.Objective == null ) {
					resultSubStage = currSubStage;
					break;
				}

				Objective obj = ObjectivesAPI.GetObjective( currSubStage.Objective.Title );

				if( obj == null ) {
					obj = currSubStage.Objective;

					ObjectivesAPI.AddObjective(
						objective: obj,
						order: -1,
						alertPlayer: true,
						out string _
					);
				}

				if( !obj.IsComplete ) {
					resultSubStage = currSubStage;
					break;
				}
			}

			currStage++;

			isFinal = currStage >= this.SubStages.Length;
			return resultSubStage;
		}
	}
}
