﻿using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Lore {
	public partial class NPCLoreStage {
		public Func<bool>[] Prerequisites { get; private set; }

		public int NPCType { get; private set; }

		public NPCLoreSubStage[] SubStages { get; private set; }



		////////////////

		public NPCLoreStage( Func<bool>[] prereqs, int npcType, NPCLoreSubStage[] subStages ) {
			this.Prerequisites = prereqs;
			this.NPCType = npcType;
			this.SubStages = subStages;
		}


		////

		public bool ArePrerequisitesMet() {
			foreach( Func<bool> prereq in this.Prerequisites ) {
				if( !prereq.Invoke() ) {
					return false;
				}
			}

			return true;
		}
	}
}