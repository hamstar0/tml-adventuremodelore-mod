﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using Objectives;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public static void Run() {
			if( !ObjectivesAPI.AreObjectivesLoadedForCurrentPlayer() ) {
				return;
			}

			var logic = ModContent.GetInstance<LoreEvents>();

			foreach( NPCLoreStage stage in logic.Events.ToArray() ) {
				if( stage.Begin() ) {
					logic.Events.Remove( stage );
				}
			}
		}



		////////////////

		private IList<NPCLoreStage> Events = new List<NPCLoreStage>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }


		////////////////

		public void InitializeOnPlayerEnter() {
			this.Events.Clear();
			this.Events.Add( LoreEvents.LoreDefs00_Guide );
			this.Events.Add( LoreEvents.LoreDefs01_OldMan );
			this.Events.Add( LoreEvents.LoreDefs02_Merchant );
			this.Events.Add( LoreEvents.LoreDefs03_200hp );
			this.Events.Add( LoreEvents.LoreDefs04_DefeatEvil );
			this.Events.Add( LoreEvents.LoreDefs05_FindMechanicAndWitchDoctor );
			this.Events.Add( LoreEvents.LoreDefs06_SummonWoF );

			// Pre-load all previously-finished objectives
			foreach( NPCLoreStage stage in this.Events ) {
				foreach( NPCLoreSubStage substage in stage.SubStages ) {
					if( substage.Objective == null ) { continue; }
					if( !ObjectivesAPI.HasRecordedObjectiveByNameAsFinished(substage.Objective.Title) ) { continue; }

					if( ObjectivesAPI.GetObjective(substage.Objective.Title) == null ) {
						ObjectivesAPI.AddObjective( substage.Objective, 0, false, out _ );
					}
				}
			}
		}
	}
}