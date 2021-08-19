﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Objectives;
using AdventureModeLore.Lore.Dialogues.Events;


namespace AdventureModeLore.Lore {
	public partial class LoreEventManager : ILoadable {
		public static void RunForLocalPlayer() {
			if( !ObjectivesAPI.AreObjectivesLoadedForCurrentPlayer() ) {
				return;
			}

			var logic = ModContent.GetInstance<LoreEventManager>();

			foreach( LoreEvent stage in logic.Events.ToArray() ) {
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

		private IList<LoreEvent> Events = new List<LoreEvent>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }


		////////////////

		public void InitializeOnCurrentPlayerEnter() {
			this.Events.Clear();
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs00_Guide );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs01_OldMan );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs02_Merchant );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs03a_200hp );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs03b_BgPKE );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs03c_RescueGoblin );
			//this.Events.Add( DialogueLoreEvents.LoreDefs03d_RescueGoblin );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs04_DefeatEvil );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs05_FindMechanicAndWitchDoctor );
			this.Events.Add( DialogueLoreEventDefinitions.LoreDefs06_SummonWoF );

			// Pre-load all previously-finished objectives
			foreach( LoreEvent myevent in this.Events ) {
				myevent.Initialize();
			}
		}
	}
}
