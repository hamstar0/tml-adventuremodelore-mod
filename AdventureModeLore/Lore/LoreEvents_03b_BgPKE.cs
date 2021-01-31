using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using Objectives.Definitions;
using Terraria.ModLoader;

namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public const string ObjectiveTitle_BackgroundPKE = "Neutalize Disruptive PKE";


		public static NPCLoreStage LoreDefs03b_BgPKE { get; } = new NPCLoreStage(
			name: "Background PKE Maxed",
			prereqs: new Func<bool>[] {
				LoreEvents.IsBackgroundPKEDangerous
			},
			npcType: NPCID.Guide,
			subStages: new NPCLoreSubStage[] {
				new NPCLoreSubStage(
					dialogue: () => "Uh oh! It seems conditions on this island have become more treacherous than usual. Don't"
						+" ask me how I know, but it seems there's a buildup of ambient spiritual energy. It may disrupt some"
						+" of your equipment, which will interfere with our mission. It's usually associated with a powerful"
						+" entity residing somewhere on the island. It should disperse once it's destroyed. Find and kill it,"
						+" if you can.",
					objective: new FlatObjective(
						title: LoreEvents.ObjectiveTitle_BackgroundPKE,
						description: "Background spiritual energies (PKE) is on the rise. Equipment and mission at risk."
							+ "\n"+"Find and destroy the powerful entity associated with this phenomenon.",
						condition: ( obj ) => !LoreEvents.IsBackgroundPKEDangerous()
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
			return LoreEvents._IsBackgroundPKEDangerous();
		}

		private static bool _IsBackgroundPKEDangerous() {
			return BossReigns.BossReignsAPI.GetBackgroundPKEPercent() >= 1f;
		}
	}
}
