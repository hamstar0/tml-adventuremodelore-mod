using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public const string ObjectiveTitle_BackgroundPKE = "Neutralize Disruptive PKE Buildup";


		public static DialogueLoreEvent GetEvent_Dialogue03b_BgPKE() => new DialogueLoreEvent(
			name: "Background PKE Buildup Maxed",
			prereqs: new Func<bool>[] {
				DialogueLoreEventDefinitions.IsBackgroundPKEDangerous
			},
			npcType: NPCID.Guide,
			subStages: new DialogueLoreEventStage[] {
				new DialogueLoreEventStage(
					dialogue: () => "Uh oh! It seems conditions on this island have become more treacherous than usual. A"
						+" buildup of ambient spiritual energy is occurring that may disrupt some of your equipment, and"
						+" interfere with the mission. The source is an [c/88FF88:undefeated powerful entity] residing somewhere"
						+" on the island. Since we can't leave yet, it'll need to be destroyed.",
					objective: new FlatObjective(
						title: DialogueLoreEventDefinitions.ObjectiveTitle_BackgroundPKE,
						description: "Background spiritual energies (PKE) is on the rise. Equipment and mission at risk."
							+ "\n"+"Find and destroy an unconquered powerful entity associated with this phenomenon.",
						condition: ( obj ) => !DialogueLoreEventDefinitions.IsBackgroundPKEDangerous()
					)
				)
			},
			isRepeatable: false
		);



		////

		public static bool IsBackgroundPKEDangerous() {
			if( ModLoader.GetMod("BossReigns") == null ) {
				return false;
			}
			return DialogueLoreEventDefinitions.IsBackgroundPKEDangerous_BossReigns();
		}

		private static bool IsBackgroundPKEDangerous_BossReigns() {
			return BossReigns.BossReignsAPI.GetBackgroundPKEPercent() >= 1f;
		}
	}
}
