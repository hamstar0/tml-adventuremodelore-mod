using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static void Initialize_Ergophobia_WeakRef() {
			Ergophobia.ErgophobiaAPI.OnPostHouseFurnish( GeneralLoreEventDefinitions.ErgophobiaHookPostHouseFurnish );
		}

		private static void ErgophobiaHookPostHouseFurnish(
					(int x, int y) innerTopLeft,
					(int x, int y) innerTopRight,
					(int x, int y) outerTopLeft,
					(int x, int y) outerTopRight,
					int floorLeft,
					int floorRight,
					int floorY,
					(int x, int y) farTopLeft,
					(int x, int y) farTopRight ) {
			AMLMod.Instance.HasFurnishedAHouse = true;
		}



		////////////////

		internal static void Initialize() {
			if( ModLoader.GetMod("Ergophobia") != null ) {
				GeneralLoreEventDefinitions.Initialize_Ergophobia_WeakRef();
			}
		}


		////////////////

		internal static IList<GeneralLoreEvent> GetDefinitions() {
			return new List<GeneralLoreEvent> {
				GeneralLoreEventDefinitions.GetEvent_Radio_AnimaEmpty(),
				GeneralLoreEventDefinitions.GetEvent_Radio_CursedBones(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Dungeon(),
				GeneralLoreEventDefinitions.GetEvent_Radio_HouseFurnished(),
				GeneralLoreEventDefinitions.GetEvent_Radio_LostExpeditions(),
				GeneralLoreEventDefinitions.GetEvent_Radio_MagicSecrets(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ManaShardHints1(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ManaShardHints2(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Orbs(),
				GeneralLoreEventDefinitions.GetEvent_Radio_PBGvBrambles(),
				GeneralLoreEventDefinitions.GetEvent_Radio_PKE(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Purification(),
				GeneralLoreEventDefinitions.GetEvent_Radio_RedBar(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Settlements(),
				GeneralLoreEventDefinitions.GetEvent_Radio_ShadowMirror(),
				GeneralLoreEventDefinitions.GetEvent_Radio_StrangePlants(),
				GeneralLoreEventDefinitions.GetEvent_Radio_StrongGates(),
				GeneralLoreEventDefinitions.GetEvent_Radio_Trickster(),
				GeneralLoreEventDefinitions.GetEvent_Radio_UGDesert(),
				//
				GeneralLoreEventDefinitions.GetEvent_Message_JungleWarn(),
				GeneralLoreEventDefinitions.GetEvent_Message_UnderworldWarn()
			};
		}
	}
}
