/*using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Loadable;
using Objectives.Definitions;


namespace AdventureModeLore.Lore.Sequenced.Events {
	public partial class SequencedLoreEventManager : ILoadable {
		public const string ObjectiveTitle_RescueMechanic = "Find Live Occupant Inside Dungeon";


		public static NPCLoreStage LoreDefs03d_RescueGoblin => new NPCLoreStage(
			name: "Mechanic Rescue",
			prereqs: new Func<bool>[] {
				() => NPC.savedGoblin
			},
			npcType: NPCID.GoblinTinkerer,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "I know little about this 'plague' everyone speaks of. Goblins are naturally resistant to "
						+"the undeath corruption of other races. I only know that strange activities have been reported at that "
						+"big dungeon building you encountered. It's said to be uninhabited, but maybe there yet lives someone "
						+"inside to tell of its tale?",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_RescueMechanic,
						description: "The dungeon has been seen to have strange activities lately. Maybe someone yet lives inside?",
						condition: ( obj ) => NPC.savedMech
					)
				)
			},
			isRepeatable: false
		);
	}
}*/
