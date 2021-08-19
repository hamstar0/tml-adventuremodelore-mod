﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Objectives.Definitions;
using AdventureModeLore.Lore.Dialogue;


namespace AdventureModeLore.Lore.Sequenced.Events {
	public partial class DialogueLoreEvents {
		public const string ObjectiveTitle_BackgroundPKE = "Neutralize Disruptive PKE Buildup";


		public static DialogueLoreEvent LoreDefs03b_BgPKE => new DialogueLoreEvent(
			name: "Background PKE Buildup Maxed",
			prereqs: new Func<bool>[] {
				DialogueLoreEvents.IsBackgroundPKEDangerous
			},
			npcType: NPCID.Guide,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "Uh oh! It seems conditions on this island have become more treacherous than usual. A"
						+" buildup of ambient spiritual energy is occurring that may disrupt some of your equipment, and"
						+" interfere with the mission. The source is an [c/88FF88:undefeated powerful entity] residing somewhere"
						+" on the island. Since we can't leave yet, it'll need to be destroyed.",
					objective: new FlatObjective(
						title: DialogueLoreEvents.ObjectiveTitle_BackgroundPKE,
						description: "Background spiritual energies (PKE) is on the rise. Equipment and mission at risk."
							+ "\n"+"Find and destroy an unconquered powerful entity associated with this phenomenon.",
						condition: ( obj ) => !DialogueLoreEvents.IsBackgroundPKEDangerous()
					)
				)
			},
			isRepeatable: true
		);



		////

		public static bool IsBackgroundPKEDangerous() {
			if( ModLoader.GetMod("BossReigns") == null ) {
				return false;
			}
			return DialogueLoreEvents._IsBackgroundPKEDangerous();
		}

		private static bool _IsBackgroundPKEDangerous() {
			return BossReigns.BossReignsAPI.GetBackgroundPKEPercent() >= 1f;
		}
	}
}
