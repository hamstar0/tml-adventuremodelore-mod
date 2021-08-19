using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Objectives;


namespace AdventureModeLore.Lore.Sequenced {
	public partial class SequencedLoreEventManager : ILoadable {
		public static void RunForLocalPlayer() {
			if( !ObjectivesAPI.AreObjectivesLoadedForCurrentPlayer() ) {
				return;
			}

			var logic = ModContent.GetInstance<SequencedLoreEventManager>();

			foreach( SequencedLoreStage stage in logic.Events.ToArray() ) {
				(bool CanBegin, bool IsDone) status = stage.GetStatusForLocalPlayer();

				if( status.CanBegin && status.IsDone && stage.IsRepeatable ) {
					stage.BeginForLocalPlayer( true );
				} else if( status.CanBegin && !status.IsDone ) {
					stage.BeginForLocalPlayer( false );
				}

				if( (status.IsDone || status.CanBegin) && !stage.IsRepeatable ) {
					logic.Events.Remove( stage );
				}
			}
		}



		////////////////

		private IList<SequencedLoreStage> Events = new List<SequencedLoreStage>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }


		////////////////

		public void InitializeOnCurrentPlayerEnter() {
			this.Events.Clear();
			this.Events.Add( SequencedLoreEventManager.LoreDefs00_Guide );
			this.Events.Add( SequencedLoreEventManager.LoreDefs01_OldMan );
			this.Events.Add( SequencedLoreEventManager.LoreDefs02_Merchant );
			this.Events.Add( SequencedLoreEventManager.LoreDefs03a_200hp );
			this.Events.Add( SequencedLoreEventManager.LoreDefs03b_BgPKE );
			this.Events.Add( SequencedLoreEventManager.LoreDefs03c_RescueGoblin );
			//this.Events.Add( LoreEvents.LoreDefs03d_RescueGoblin );
			this.Events.Add( SequencedLoreEventManager.LoreDefs04_DefeatEvil );
			this.Events.Add( SequencedLoreEventManager.LoreDefs05_FindMechanicAndWitchDoctor );
			this.Events.Add( SequencedLoreEventManager.LoreDefs06_SummonWoF );

			// Pre-load all previously-finished objectives
			foreach( SequencedLoreStage stage in this.Events ) {
				foreach( SequencedLoreSubStage substage in stage.SubStages ) {
					if( substage.OptionalObjective == null ) {
						continue;
					}
					if( !ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(substage.OptionalObjective.Title) ) {
						continue;
					}

					if( ObjectivesAPI.GetObjective(substage.OptionalObjective.Title) == null ) {
						ObjectivesAPI.AddObjective( substage.OptionalObjective, 0, false, out _ );
					}
				}
			}
		}
	}
}
