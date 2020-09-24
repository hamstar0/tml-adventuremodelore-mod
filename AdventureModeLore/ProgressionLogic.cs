using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;


namespace AdventureModeLore {
	partial class ProgressionLogic : ILoadable {
		public static void Initialize() {
			var logic = ModContent.GetInstance<ProgressionLogic>();

			logic.Events.Clear();
			logic.Events.Add( ProgressionLogic.Run00_Guide );
			logic.Events.Add( ProgressionLogic.Run01_OldMan );
			logic.Events.Add( ProgressionLogic.Run02_Merchant );
			logic.Events.Add( ProgressionLogic.Run03_200hp );
			logic.Events.Add( ProgressionLogic.Run04_Dryad );
			logic.Events.Add( ProgressionLogic.Run05_Goblin );
			logic.Events.Add( ProgressionLogic.Run06_WitchDoctor );
		}


		////

		public static void Run() {
			var logic = ModContent.GetInstance<ProgressionLogic>();

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
