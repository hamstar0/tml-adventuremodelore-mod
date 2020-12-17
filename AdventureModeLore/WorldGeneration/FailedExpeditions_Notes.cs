using System;
using Terraria;
using Terraria.World.Generation;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class FailedExpeditionsGen : GenPass {
		public static readonly (string title, string[] pages)[] LoreNotes = {
			( "The Plague", new string[] {
				"Also known as 'The Curse', 'The Undeath Plague', and even ominously to some as 'Signs',"
				+ "\nthis is a phenomena of recent years accounting for mysterious afflictions of madness of"
				+ "\nsome, who also exhibit an unnatural resilience against dying.",
				"At first attributed to an unknown virulent organism, we now know this to be something"
				+ "\naltogether new to science and medicine, though apparently not to theology.",
				"Ancient records of civilizations falling to mysterious 'feral behavior' maladies can be"
				+ "\nfound, often coinciding with the equally-mysterious rise of new civilizations.",
				"Some thought this to be the work of secretive cults, noting curiously-recurrent signs"
				+ "\nand witness accounts during such times. These cults were known to have zealotous"
				+ "\nfollowings bordering on abject lunacy.",
				"Perhaps they accomplished their dark work through some sort of chemical or even"
				+ "\nsupernatural subversive means?",
				"More pragmatically, the plague may be attributed simply to a viral source that yet"
				+ "\nevades scientific comprehension. This has not yet given adaquate explaination for its"
				+ "\neffect on things that are not of the living, however..."
			} ),
			( "Ectoplasm", new string[] {
				"The environmental effects of concentrations of \"negatively-charged spiritual residue\""
				+ "\nare numerous. Commonly called Ectoplasm, this wispy substance can animate and alter"
				+ "\nthings around it, often destructively.",
				"Large concentrations of naturally-occuring ectoplasm have been known to turn wildlife"
				+ "\ninto vicious killers, re-animate corpses, and even give life to organic detritous or"
				+ "\nother microbial matter (a phenomena often known as 'slimes').",
				"A large surge of this residue has recently been reported emanating from an island now"
				+ "\nknown as Terraria. Its effects are being felt all around the world, and some believe"
				+ "\nit to be the cause of the undeath plague. Modern science, however, has yet to"
				+ "\ncorroborate on any of these assertions. Efforts to acquire, or even definitively"
				+ "\nconfirm the presence of this substance, have so far proven unfruitful."
			} ),
			( "The Island of Terraria", new string[] {
				"The island of Terraria is a curious place. It is shrouded in much mystery, and although"
				+ "\nindications of a long history are evident, few documented records exist to tell of it.",
				"Upon arrival, one may note a curious conglomeration of varying extreme biomes, terrain,"
				+ "\nand other elements. Their proximity and relative intensity of presence seem to altogether"
				+ "\ndefy nature and plausibility.",
				"Even more notable are the occurrence of elements that aren't even found in nature, and"
				+ "\nare yet to be fully understood by science.",
				"Among these phenomena: A large biome of festering \"corruption\", giant trees towering"
				+ "\nabove any of the island's native foliage, and even levitating pieces of terrain amidst"
				+ "\nthe cloud.",
				"Perhaps most curiously, there exists a single, colossal structure built of an material"
				+ "\nunlike anything found anywhere in the world."
			} ),
			( "The Lunatic Cult", new string[] {
				"One of the few natives of the land of Terraria, this group acts as \"agents of ancient"
				+ "\ncosmic powers seeking to create a new home for their elder god masters\", allegedly.",
				"Witness accounts exist of their gatherings associated with all manner of sorcery, which"
				+ "\nthey claim is enabled by the use and presence of a substance called \"ectoplasm\".",
				"They claim to have build the large dungeon-like structure on the island, but they do"
				+ "\nnot appear to reside there at present.",
				"They also claim this structure is tied to the source of their sorcery, and by extension,"
				+ "\nsupply of \"ectoplasm\".",
				"According to rumors, this substance is sourced from the \"spiritual essence of the dead\","
				+ "\nwhich would strongly suggest this massive dungeon is rather a place of human sacrifice,"
				+ "\nrather than incarceration.",
				"Cultist gatherings and recruitments seem to be on the rise of late, as are ever more"
				+ "\nexotic and disturbing tales of their acts of sorcery all across the island.",
				"Could this have any bearing on the rise of the undeath plague in recent years?"
			} ),
			( "The 'Lihzarhd' Tribe", new string[] {
				"A hardy, humanoid tribe of survivors of the great war. They hid away as best they could"
				+ "\nto excape the Curse, but it still took its toll on them.",
				"Rather than succumbing to madness (or worse) like most others, these people lost their"
				+ "\nhumanity, becoming lizard-like beasts of only questionably-sapient nature.",
				"They appear to reside deep within the island's jungle, hidden away in deep, uncharted"
				+ "\nplaces.",
				"Rumor has it they seek some sort of revenge, but none have been found of late to verify"
				+ "\nthis intent.",
				"Though less have been seen away from their jungle home of late, no indication exists"
				+ "\nthat their numbers have reduced.",
				"Despite their appearance and behavior, they do seem to be quite intelligent, and are"
				+ "\noften noted for creating elaborate traps far and wide and even wielding"
				+ "\ncuriously-advanced technologies.",
				"Rumors also exist that they may even be practicing sorcery in recent years, seemingly"
				+ "\nobsessed with \"apocalyptic magics\" in particular."
			} ),
			( "The Goblin Army", new string[] {
				"The other survivors of the Great War. They dwell in the shadows and far corners of the"
				+ "\nisland. Also afflicted by the curse, similar to Lihzarhds. They are always warring"
				+ "\namong themselves and their neighbors.",
				"It is not clear if they retain full sapience since their transformation, as their"
				+ "\nwarlike temperament suggests a distinctly primal behaviorial shift.",
				"Contact with them has proven unfruitful, as even their scouts frequently become hostile"
				+ "\nto outsiders. If any signs of intelligence is to be found, it is well hidden."
			} ),
			( "The Great War", new string[] {
				"Where once a great civilization once dwelt on the island, now only utter ruin and"
				+ "\ndesolation can be found. Stories tell of the old civilization discovering the power of"
				+ "\nectoplasm, which could only be acquired from the souls of the dead.",
				"To acquire this, a great many lives were sacrificed in the process. Fed up with being"
				+ "\nfed on, the population revolted. Afterwards, factions formed, but they were played"
				+ "\nagainst each other.",
				"A cult appeared (possibly from the surviving elites of the old civilization), subverting"
				+ "\nthe factions, and a calamity nearly ensued.",
				"Survivors of these factions were forced to hide in the depths of the island, gradually"
				+ "\nlosing their humanity to the powers they wielded or were wielded upon them."
			} ),
			( "The Calamity", new string[] {
				"They say the ancient cultists nearly succeeded in summoning an elder god to Terraria"
				+ "\nitself. A surviving faction calling themselves the Dryads sacrificed themselves to"
				+ "\nrepel this calamity.",
				"Did the ancient cult go into hiding? Were they themselves wiped out? None can say. All"
				+ "\nthat's known is a recent appearance of a cult, presumably with ties to their ancient"
				+ "\npredecesors, have begun appearing on this island now, of all times.",
				"Could this really be a Sign of a new calamity ready to befall our world?"
			} ),
			( "The Abomination Of The Deep", new string[] {
				"Legends tell of an abomination created from a magic spell of the Dryads to try to"
				+ "\ncleanse the land of the Curse.",
				"This spell was meant to draw evil spiritual energy (tainted ectoplasm) from the land and"
				+ "\nseal it into the bowels of the world. It was cast in hopes of averting another calamity.",
				"Sadly, the spell seems to be failing, or maybe the world itself has reached a saturation."
				+ "\nEither way, a terrifying presence has since appeared in the depths as a result.",
				"It dwells in the deepest reaches of the world, exuding a dark influence on the world"
				+ "\nabove. No one knows if or when it may make its presense known to the world above..."
				+ "\nor what calamity may result!"
			} ),
			( "The Ritual", new string[] {
				"There exists a way to directly empower a person or object with magical powers, but it"
				+ "\nrequires ectoplasm.",
				"It is also the main method to extract fresh ectoplasm from a person's soul in times of"
				+ "\nantiquity; a secret now (thankfully) lost to time.",
				"This ritual requires a focus object (usually a voodoo doll or other symbolic"
				+ "\nrepresentation) of the person or object to act upon.",
				"If the focus is destroyed, it dramatically releases any gathered ectoplasm with"
				+ "\nfrightful and unpredictable results."
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
