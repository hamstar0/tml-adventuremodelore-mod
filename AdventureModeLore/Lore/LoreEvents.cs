using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using Objectives;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public static void Initialize() {
			var logic = ModContent.GetInstance<LoreEvents>();

			logic.Events.Clear();
			logic.Events.Add( LoreEvents.LoreDefs00_Guide );
			logic.Events.Add( LoreEvents.LoreDefs01_OldMan );
			logic.Events.Add( LoreEvents.LoreDefs02_Merchant );
			logic.Events.Add( LoreEvents.LoreDefs03_200hp );
			logic.Events.Add( LoreEvents.LoreDefs04_DefeatEvil );
			logic.Events.Add( LoreEvents.LoreDefs05_FindMechanicAndWitchDoctor );
			logic.Events.Add( LoreEvents.LoreDefs06_SummonWoF );
		}


		////

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
	}
}
