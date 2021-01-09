using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using Objectives;


namespace AdventureModeLore.Logic {
	public partial class LoreLogic : ILoadable {
		public static void Initialize() {
			var logic = ModContent.GetInstance<LoreLogic>();

			logic.Events.Clear();
			logic.Events.Add( LoreLogic.LoreDefs00_Guide );
			logic.Events.Add( LoreLogic.LoreDefs01_OldMan );
			logic.Events.Add( LoreLogic.Run02_Merchant );
			logic.Events.Add( LoreLogic.Run03_200hp );
			logic.Events.Add( LoreLogic.Run04_Dryad );
			logic.Events.Add( LoreLogic.Run05_Goblin );
			logic.Events.Add( LoreLogic.Run06_WitchDoctor );
		}


		////

		public static void Run() {
			if( !ObjectivesAPI.AreObjectivesLoadedForCurrentPlayer() ) {
				return;
			}

			var logic = ModContent.GetInstance<LoreLogic>();

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
