using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static void InitializeSpiritWalking() {
			void OnWalkActivation( Player plr, bool isOn ) {
				if( plr.whoAmI != Main.myPlayer || isOn ) {
					return;
				}

				string id = "AML_SpiritWalking_WalkLore";
				string title = "Break on through to the Other Side";
				string msg = "In ancient times, spirit walking was a journey of one's metaphysical self to the"
					+" land of the. It is ussually done by way of dreams, meditation, special drugs, or near"
					+" even death experiences. While often very lucid seeming to some, it was never something"
					+" \nyou expected to take part in as if traveling to an actual concrete place."
					+"\n \nDue to ongoing research into the nature of spiritual energy (PKE) and parallel"
					+" dimensions, it appears people have found a means to enter the spirit realm: body and soul."
					+" Enter the Shadow Mirror. Twisting the magical workings of a regular Magic Mirror, a Shadow"
					+" Mirror may phase shift its user's entire being into this dark world of spirits..."
					+" but at a cost!"
					+"\n \nTo safely enter the spirit world, one should first become spiritually fortified."
					+" It's not clearly understood what this means, but it is said to be an achievement of monks"
					+" or certain gifted individuals. Without this, death will surely await you, for you are"
					+" already a hair's breadth away just to have reached this realm. As is, any stay in this"
					+" realm will put a strain on your spirit; usually recoverable, but dire if overdone. The"
					+" degree of this survivability can be quantified with a concept known as Anima."
					+"\n \nOne with a 'fortified spirit', however, is usually safe from this lethality, but still"
					+" subject to a duration limit imposed by their Anima supply.";

				Messages.MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
					Messages.MessagesAPI.AddMessage(
						title: title,
						description: msg,
						modOfOrigin: SpiritWalking.SpiritWalkingMod.Instance,
						alertPlayer: Messages.MessagesAPI.IsUnread(id),
						isImportant: false,
						parentMessage: Messages.MessagesAPI.StoryLoreCategoryMsg,
						id: id
					);
				} );
			}

			//

			SpiritWalking.SpiritWalkingAPI.AddSpiritWalkActivationHook( OnWalkActivation );
		}
	}
}