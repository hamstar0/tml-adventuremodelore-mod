using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using Objectives;


namespace AdventureModeLore.Logic {
	public partial class AMLLogic : ILoadable {
		public static void Initialize() {
			var logic = ModContent.GetInstance<AMLLogic>();

			logic.Events.Clear();
			logic.Events.Add( AMLLogic.Run00_Guide );
			logic.Events.Add( AMLLogic.Run01_OldMan );
			logic.Events.Add( AMLLogic.Run02_Merchant );
			logic.Events.Add( AMLLogic.Run03_200hp );
			logic.Events.Add( AMLLogic.Run04_Dryad );
			logic.Events.Add( AMLLogic.Run05_Goblin );
			logic.Events.Add( AMLLogic.Run06_WitchDoctor );
		}


		////

		public static void Run() {
			if( !ObjectivesAPI.AreObjectivesLoadedForCurrentPlayer() ) {
				return;
			}

			var logic = ModContent.GetInstance<AMLLogic>();

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
