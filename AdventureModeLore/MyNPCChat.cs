using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Dialogue;


namespace AdventureModeLore {
	public class NPCDialogueDefinitions {
		public string[] Added { get; }
		public string[] Blocked { get; }



		public NPCDialogueDefinitions( string[] added, string[] blocked ) {
			this.Added = added;
			this.Blocked = blocked;
		}
	}




	partial class AdventureModeNpcChat : ILoadable {
		public readonly IDictionary<int, NPCDialogueDefinitions> NPCDialogues = new Dictionary<int, NPCDialogueDefinitions> {
			{
				NPCID.Guide, new NPCDialogueDefinitions(
					added: new string[] {
						"Good thing the dungeon is sealed. I hear it's blighted with an undeath curse and filled with deadly fumes!",
						"The people who lived here once discovered ways to wield artifacts of power, and hid their secrets around this land.",
						"Rare magic crystals can be found hidden underground. Use your binoculars to pick up their trail. I also hear they resonate with nearby magical spell casting.",
						"You may find digging to be rather difficult. If you find yourself needing to squeeze into tight areas, a simple hammer of all things might be your best tool. Odd, huh?",
						"Not everything can be crafted. You'll have to learn to make do with what you can find or buy. Talk to me for more information, if in doubt.",
						"Need to add a few small patches or solid additions to a given area? You'll want to get your hands on some framing planks.",
						"Be sure to keep your eyes peeled for livable spaces to furnish. This is where our House Furnishing Kits will come in handy. They'll even provide you with new storage space and mirrors for fast travel!",
						"Wood is about the only non-ore material you can break freely. You'll need special framing planks to do any building, but they're limited. Best use house kits whenever possible.",
						"Need money? Sell your loot. Be sure to have me check your loot for available crafting recipes, first. Not everything gets used the way you might assume.",
						//
						"You can use your axe to chop down trees. Just place your cursor over the tile and click!",
						"We'll need to create settlements to progress our journey. Use House Furnishing Kits to convert closed areas into livable spaces.",
					},
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
				)
			},
			{
				NPCID.Dryad, new NPCDialogueDefinitions(
					added: new string[] {
						"Ever encounter that annoying Trickster? I hear it likes to reward those who think they can outwit it with quick thinking. I think it's up to something...",
					},
					blocked: new string[] {
					}
				)
			}
		};



		////////////////

		public void OnModsLoad() { }

		public void OnPostModsLoad() {
			foreach( (int npcType, NPCDialogueDefinitions chats) in this.NPCDialogues ) {
				foreach( string addedChat in chats.Added ) {
					DialogueEditor.AddChatForNPC( npcType, addedChat, 0.1f );
				}
				foreach( string blockedChat in chats.Blocked ) {
					DialogueEditor.AddChatRemoveFlatPatternForNPC( npcType, blockedChat );
				}
			}
		}

		public void OnModsUnload() { }


		////////////////

		private DynamicDialogueHandler GetGreetingFunc( int npcType, string[] consumableGreetings ) {
			if( consumableGreetings.Length == 0 ) {
				return null;
			}

			int i = 0;
			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( npcType );

			return new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					if( i < consumableGreetings.Length ) {
						return consumableGreetings[i++];
					}

					string npcKey = NPCID.GetUniqueKey( npcType );
					var myplayer = TmlHelpers.SafelyGetModPlayer<AMLPlayer>( Main.LocalPlayer );

					myplayer.AlreadyIntroducedNpcs.Add( npcKey );

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( npcType, oldHandler );
					}
					return msg;
				},
				isShowingAlert: () => i < consumableGreetings.Length
			);
		}
	}
}
