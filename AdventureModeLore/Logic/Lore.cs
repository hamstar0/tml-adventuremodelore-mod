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
			logic.Events.Add( LoreLogic.Run00_Guide );
			logic.Events.Add( LoreLogic.Run01_OldMan );
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

			foreach( Func<bool> myevent in logic.Events.ToArray() ) {
				if( !myevent.Invoke() ) {
					logic.Events.Remove( myevent );
				}
			}
		}



		////////////////

		private IList<Func<bool>> Events = new List<Func<bool>>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }
	}
}
