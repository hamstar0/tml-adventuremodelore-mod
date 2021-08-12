using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class LostExpeditionsGen : GenPass {
		public static readonly (string title, string[] pages)[] LoreNotes = {
			( "The Plague", new string[] {
				"Also known as 'The Curse', 'The Undeath Plague', and even ominously as 'Signs', this is"
				+ "\na phenomena of recent years to identify a mysterious afflictions of sickness and"
				+ "\nmadness, but also an unnatural resilience against death.",
				"At first attributed to an unknown virulent organism, we now know this to be something"
				+ "\naltogether new to science and medicine, though apparently not to history (and theoloy,"
				+ "\napparently).",
				"Ancient records of civilizations falling to mysterious 'feral behavior' maladies can be"
				+ "\nfound, often coinciding with the equally-mysterious rise of new civilizations.",
				"More pragmatically, the plague may be attributed simply to a viral source that yet"
				+ "\nevades scientific comprehension. This has not yet given adaquate explaination for its"
				+ "\neffect on things that are not of the living, however..."
			} ),
			( "Ectoplasm", new string[] {
				"The environmental effects of concentrations of \"negatively-charged spiritual residue\""
				+ "\nare numerous. Commonly called Ectoplasm, this wispy substance can animate and alter"
				+ "\nthings around it, though often only destructively.",
				"Large concentrations of naturally-occuring ectoplasm have been known to turn wildlife"
				+ "\ninto rabid killing machines, re-animate corpses, and even give life to organic detritous"
				+ "\nor microbial matter (a phenomena often known as 'slimes').",
				"A large surge of this residue has recently been reported emanating from an island now"
				+ "\nknown as Terraria. Its effects are being felt all around the world, and some believe"
				+ "\nit to be the cause of the plague. Modern science, however, has yet to corroborate on any"
				+ "\nof these assertions. Efforts to acquire, or even definitively confirm the presence of"
				+ "\nthis substance, have so far proven unfruitful."
			} ),
			( "The Island of Terraria", new string[] {
				"The island of Terraria is a mysterious place. Although indications of a long history are"
				+ "\nevident, few documented records exist to tell of it.",
				"Upon arrival, one may note a curious conglomeration of varying extreme biomes, terrain"
				+ "\nfeatures, and other elements. Their proximity and relative significance of presence seem"
				+ "\nto defy nature and plausibility.",
				"Even more notable are the occurrence of phenomena that aren't even found in nature, and"
				+ "\nare yet to be fully understood by science.",
				"Among these phenomena: A large biome of festering \"corruption\", giant trees towering"
				+ "\nabove any of the island's native foliage, and even levitating pieces of terrain amidst"
				+ "\nthe clouds.",
				"Perhaps most curiously, there exists a colossal, fortress-like structure reaching deep"
				+ "\nunderground, and built of a material unlike anything found anywhere in the world."
				+ "\nTeams of investigators have gone missing while attempting to explore it, and it's"
				+ "\nmaterial composition seems nigh impervious to conventional explosives. It does not,"
				+ "\nhowever, seem to be currently occupied, although reports exist of strange folk"
				+ "\noccasionally seen near its entrance..."
			} ),
			( "The Lunatic Cult", new string[] {
				"One of the few natives of the land of Terraria, this group acts as \"agents of ancient"
				+ "\ncosmic powers seeking a new home for their elder god masters\", allegedly.",
				"Witnesses say their gatherings are associated with all manner of sorcery and mysterious"
				+ "\nhappenings, though rumors of human sacrifices so far appear to be unconfirmed.",
				"They claim to have built the large \"dungeon\" structure on the island, but they do not"
				+ "\npresently appear to reside there. They claim this structure is important to their"
				+ "\nmission, and that it plays a role in their quest for \"ectoplasm\"; the apparent"
				+ "\nfuel source for their sorcery.",
				"They say this substance is produced from the spirits of the dead, which would suggest"
				+ "\ntheir \"dungeon\" is not a place of mere incarceration...",
				"Cultist gatherings and recruitments seem to be on the rise of late, as are ever more"
				+ "\nexotic and disturbing tales of their acts of sorcery all across the island.",
				"Could this have any correlation with the rise of the undeath plague in recent years?"
			} ),
			( "The 'Lihzarhd' Tribe", new string[] {
				"Another tribe of natives of the land, these hardy humanoids appear to have survived in"
				+ "\nhiding since the great war. Despite that, the Curse appears to have again taken its"
				+ "\ntoll.",
				"Rather than succumbing to illness or madness (or worse) like most others, these people"
				+ "\nsimply lost their human forms, instead becoming lizard-like beasts.",
				"They appear to reside deep within the island's jungle, hidden away in some uncharted"
				+ "\nrefuge or other, somehow enduring against the plague even where its effects may be"
				+ "\nfelt more than most.",
				"Rumor has it they seek some sort of revenge, but as any proper contact has yet to be"
				+ "\nmade with their tribe, this claim has yet to be verified.",
				"They rarely leave their jungle home, and lately seem to be more active and reclusive"
				+ "\nthan ever. Perhaps something connects this to the recent rise of cultist activities"
				+ "\non the island?",
				"Ignoring their appearance and behavior, they do seem to be quite intelligent, as they"
				+ "\nare noted for creating elaborate traps all around the island. Rumors also exist of"
				+ "\ntheir use of advanced technology and magic, though they at least appear to prefer"
				+ "\na more primitive lifestyle.",
				"Reports also exist that they may even be practicing religion in recent years, with"
				+ "\nemphasis on sun worship and strong allusions to a soon-coming apocalypse..."
			} ),
			( "The Goblin Army", new string[] {
				"Yet more survivors of the Great War. They dwell in the shadows and far corners of the"
				+ "\nisland. Also afflicted by the Curse, similar to the \"lihzarhds\", they too have"
				+ "\nmutated from their original human forms.",
				"Though they are always warring among themselves and their neighbors, it appears some"
				+ "\nsemblance of society exists among them. They are known to be adept magicians, and"
				+ "\neven appear to have some occasional (albeit rare) technological proficency.",
				"Direct communication has proven unfruitful, as even their scouts frequently become"
				+ "\nhostile to outsiders. One wonders if they've retained their own intelligence, or else"
				+ "\nif some master's hand guides them..."
			} ),
			( "The Great War", new string[] {
				"Where once a great civilization once dwelt on the island, now only ruin and desolation"
				+ "\ncan be found. Stories tell of the old civilization discovering the \"power of the"
				+ "\ndead\", a discovery that both propelled them to greatness and then to calamity.",
				"To acquire this power, a great many lives were sacrificed in the process. At some point,"
				+ "\ntheir population revolted. Factions formed, dynasties fell, and dangerous new powers"
				+ "\narose.",
				"Stories tell of \"outsiders\" suddenly appearing amidst the chaos attempting to bring"
				+ "\ncalamity to the entire world. Their forces fed off of the ill will of the times,"
				+ "\nand may have even had a hand in their prior instigations.",
				"It was not until a unified effort of the warring factions did a desperate defense get"
				+ "\nmade, but the cost was dire... both to the people, and to the land itself.",
				"Survivors of these factions were forced to hide or flee, either slowly losing their"
				+ "\nhumanity to the powers they once wielded, or else their ancient cultures and"
				+ "\nidentities with their exodus. Their stories thereafter are conspiciously hidden"
				+ "\nfrom the pages of most recorded history..."
			} ),
			( "The Calamity From The Stars", new string[] {
				"They say an ancient cult nearly succeeded in summoning an evil god from beyond this world"
				+ "\nto the land of Terraria itself. Survivors of the warring factions, pooling their"
				+ "\nspecial knowledge and powers, made the ultimate sacrifice to defeat this foe.",
				"But what became of the ancient cult? Did they survive the war and calamity after all this"
				+ "\ntime? Or is it a new cult (with ties to their ancient predecesors?) that now appears on"
				+ "\nthis island? Why now, of all times?",
				"Could we be again doomed to repeat the mistakes of the past?"
			} ),
			( "The Abomination Of The Deep", new string[] {
				"Legends tell of an abomination lurking in the far depths of the world, born of the great"
				+ "\nwar and sorrows of the past.",
				"Powerful magics meant to draw away evil from the land itself could only manage to seal it"
				+ "\ninto the bowels of the world, hidden from sight. Alas, this temporary respite may have"
				+ "\ngiven rise to new horrors, yet to be seen.",
				"As the spells of old fail (or maybe simply the world itself reaches a saturation point),"
				+ "\nthe gathered spiritual energies coallesce into a new being: A terrifying abomination"
				+ "\nthat begins to exude its presence on the world above.",
				"No one knows if or when it may make its presense known to the world above... nor what"
				+ "\ndisaster may result!"
			} ),
			( "The Ritual", new string[] {
				"Rediscovered from ancient times, research into the workings of the plague have revealed"
				+ "\nthe old methods for empowering people and objects with \"magic powers\".",
				"The cost? It requires the \"souls of the dead\", materially found in a substance known"
				+ "\nas \"ectoplasm\".",
				"Furthermore, the rituals speak of ways to acquire this substance via. human sacrifices."
				+ "\nThe exact process for doing this, however, is unknown.",
				"What is known is that a focus object (usually a \"voodoo doll\" or other symbolic"
				+ "\ncomponent) of the person or object to act upon is needed, and will thereafter become"
				+ "\na vital part of the channeling of magic.",
				"If the focus is destroyed, the empowered being may \"materially manifest upon their"
				+ "\naccumulated spiritual essence\". The exact meaning of this is unknown, but it is"
				+ "\nthought this may be an explanation for the existance of powerful monsters or"
				+ "\ndangerous environmental phenomena..."
			} )
			//The Dungeon Guardian
			//The Dryads
			//Demihumans (Nymphs, Harpies, Imps)
			//The Corruption
			//The Underworld
			//Wildlife
			//Eyes in the Sky
			//Floating Islands
			//Alchemy
			//The Undead
			//Mining Prospects
			//Life in Terraria
			//The Moon
			//The Jungle
			//Orbs
			//The Elder God
			//Enchantments
			//Ancient Heroes
		};
	}
}
