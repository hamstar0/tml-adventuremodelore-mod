using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using Objectives;
using AdventureModeLore.Lore.Dialogues.Events;
using AdventureModeLore.Lore.General.Events;


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

		private List<LoreEvent> Events = new List<LoreEvent>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }


		////////////////

		public void InitializeOnCurrentPlayerEnter() {
			this.Events.Clear();

			GeneralLoreEventDefinitions.Initialize();

			this.Events.AddRange( DialogueLoreEventDefinitions.GetDefinitions() );
			this.Events.AddRange( GeneralLoreEventDefinitions.GetDefinitions() );

			// Pre-load all previously-finished objectives
			foreach( LoreEvent myevent in this.Events ) {
				myevent.Initialize();
			}
		}
	}
}
