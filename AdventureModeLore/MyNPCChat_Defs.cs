using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore {
	public class NPCDialogueDefinitions {
		public string[] Added { get; }
		public string[] Blocked { get; }

		public Func<string, bool> IsAvailable { get; }



		public NPCDialogueDefinitions( string[] added, string[] blocked, Func<string, bool> isAvailable=null ) {
			this.Added = added;
			this.Blocked = blocked;
			this.IsAvailable = isAvailable;
		}
	}




	partial class AdventureModeNpcChat : ILoadable {
		public static readonly IDictionary<int, NPCDialogueDefinitions> NPCDialogues;


		static AdventureModeNpcChat() {
			AdventureModeNpcChat.NPCDialogues = new Dictionary<int, NPCDialogueDefinitions>();

			var guideDialogues = new List<string> {
				"Good thing the dungeon is sealed. I hear it's blighted with an undeath curse and filled with deadly fumes!",
				//"The people who lived here once discovered ways to wield artifacts of power, and hid their secrets around this land.",
				"Rare magic crystals can be found hidden underground. Use your binoculars to pick up their trail. I also hear they resonate with nearby magical spell casting.",
				"You may find digging to be rather difficult. If you find yourself needing to squeeze into tight areas, a simple hammer might be your best tool. Odd, huh?",
				"Not everything can be crafted. You'll have to learn to make do with what you can find or buy. Check with me for more information, if in doubt.",
				"Need to add a few small patches or solid additions to a given area? You'll want to get your hands on some framing planks.",
				"Be sure to keep your eyes peeled for livable spaces to furnish. This is where House Furnishing Kits will come in handy. They'll even provide you with new storage space and mirrors for fast travel!",
				"Besides certain \"soft\" materials, Wood is about the only non-ore material you can break freely. You'll need special framing planks to do any building, but their use is limited. Best use House Framing Kits and Scaffold Kits whenever possible.",
				"Need money? Sell your loot. Be sure to check them for available crafting recipes, first. Not everything gets used the way you might assume.",
				"Track Deployment Kits are useful for crossing nasty biomes, or else just getting around in a hurry. You should be able to craft more as needed, with a few base materials.",
				//
				"You can use your axe to chop down trees. Just place your cursor over the tile and click!",
				"We'll need to create settlements to progress our journey. Use House Furnishing Kits to convert closed areas into livable spaces.",
			};
			if( ModLoader.GetMod("Necrotis") != null ) {
				guideDialogues.Add(
					"This island emanates spiritual energy. It's saturated even into it's very soils. Your presence here may "
					+ "disturb these spirits. If they get too aggitated, something powerful may appear to attack and destroy you. "
					+ "Killing it will usually remove this aggitation effect... for a time. You may need to kill different "
					+ "types of such entities, for each of these disturbances."
				);
			}
			
			AdventureModeNpcChat.NPCDialogues[NPCID.Guide] = new NPCDialogueDefinitions(
				added: guideDialogues.ToArray(),
				blocked: new string[] {
					"You can use your pickaxe to dig through dirt",
					"If you want to survive",
					"When you have enough wood, create a workbench",
					"You can build a shelter by placing wood or other blocks in the world",
					"Once you have a wooden sword,",
					"To interact with backgrounds, use a hammer",
					"You can create a furnace out of torches, wood, and stone",
					"Anvils can be crafted out of iron",
					"they can be combined to create an item that will increase your magic capacity",
					"The ebonstone in the corruption can be purified",
					"You should make an attempt to max out your available life",
					"You can make a grappling hook from a hook",
				}
			);

			AdventureModeNpcChat.NPCDialogues[NPCID.Nurse] = new NPCDialogueDefinitions(
				added: new string[] {
					"I can heal bodily wounds, but this land does things to people that I cannot heal. Spiritual matters are "
					+ "not my forté, though. You'll want a priest or witch doctor or something for that, I think. I'm not "
					+ "being speaking figuratively. Do NOT let your spirits get down, or things here can harm you. If you "
					+ "survive, I can help with that, though.",
				},
				blocked: new string[] { },
				isAvailable: ( msg ) => ModLoader.GetMod("Necrotis") != null
			);
			
			AdventureModeNpcChat.NPCDialogues[NPCID.Dryad] = new NPCDialogueDefinitions(
				added: new string[] {
					"Ever encounter that annoying Trickster? I hear it likes to reward those who think they can outwit it with quick thinking. I think it's up to something...",
				},
				blocked: new string[] { },
				isAvailable: ( msg ) => ModLoader.GetMod("TheTrickster") != null
			);

			AdventureModeNpcChat.NPCDialogues[NPCID.ArmsDealer] = new NPCDialogueDefinitions(
				added: new string[] {
					"That's a mighty fine hand cannon you brought with you. I've never seen one like it. Really though, "
					+ "why not buy a real gun from me? Surely that old thing can't hit the broad side of a barn! Would "
					+ "probably level one just fine, with whatever you added to the powder.",
				},
				blocked: new string[] { },
				isAvailable: ( msg ) => ModLoader.GetMod( "TheMadRanger" ) != null
			);

			AdventureModeNpcChat.NPCDialogues[NPCID.Demolitionist] = new NPCDialogueDefinitions(
				added: new string[] {
					"Remember you can craft Seismic Charges using plain bombs, an orb, and some obsidian. These vibrate "
					+ "to make a sonic frequency that lets you destroy materials that normal explosives can't touch. ",
				},
				blocked: new string[] { },
				isAvailable: ( msg ) => ModLoader.GetMod( "TheMadRanger" ) != null
			);
		}
	}
}
