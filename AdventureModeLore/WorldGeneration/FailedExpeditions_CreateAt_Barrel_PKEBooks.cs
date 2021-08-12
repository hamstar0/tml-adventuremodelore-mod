using System;
using Terraria;
using Terraria.World.Generation;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.WorldGeneration {
	partial class AbandonedExpeditionsGen : GenPass {
		public static (string title, string[] pages) MissionBriefingBookInfo = (
			"Mission Briefing - Codename\n          'Tinman'",
			new string[] {
				"As you know, the island has become a PKE hotbed in recent months. Readings are on the"
				+ "\nrise, and it has become unsafe to send in living agents. Hence why you're here, Omega.",
				"Due to previous failed expeditions, we have since sent in agents Fender, Sigma, and"
				+ "\nOmicron, but they too have ceased signal contact. We can only hope it's due to"
				+ "\ninterference, but we've no time for search and recovery.",
				"Before contact dropped, Sigma gave us a conclusive report that the specimen has indeed"
				+ "\nfound its way back to the island. It MUST be captured ALIVE at all costs!",
				"You've been outfitted with special defense systems and detection equipment calibrated"
				+ "\nspecifically for locating and apprehending the specimen, though due to its nature,"
				+ "\neven this may be a challenge. The damn thing gets smarter by the minute!",
				"Reports are coming in of other parties taking interest in the island and its recent"
				+ "\nactivities. Avoid any encounters with the locals or other parties. You are well"
				+ "\nequipped to handle threats, but we cannot predict who you'll encounter...",
				"Should you find any information on the whereabouts of the missing expeditions or"
				+ "\nagents, report to command when convenient, but above all: Find that specimen!"
			}
		);

		public static (string title, string[] pages) PKEBookInfo = (
			"PKE Meter - Custom Order\n      From Ectocorp™",
			new string[] {
				"The special-issue PKE Detector Mk. VII comes now with separate sensors calibrated"
				+ "\nspecifically for your mission. Avoid letting it fall into non-agency hands.",
				"Due to conditions on the island, the most important reading is the background PKE,"
				+ "\nindicated by the [c/FF4444:red] bar. When this reading reaches critical, you must retreat to"
				+ "\nminimal safe distance off the island, and await further instructions.",
				"Safety aside, your mission remains of vital importance. Your mark should be detected"
				+ "\non the [c/FFFF44:yellow] channel, though precise calibration remains difficult. Do your best.",
				"Other phenomena on the island may warrant separate consideration. It is not in your"
				+ "\nmission specification, but we would like it if you could track the remains of the"
				+ "\nother failed expeditions, and also your fellow agents, if possible.",
				"As the expeditions prior were primarily recon, you may be able to locate them by"
				+ "\nreadings from any gathered artifacts from their time on the island. The [c/44FF44:green] channel"
				+ "\nshould be attuned accordingly.",
				"Finally, we have reason to believe a threat may exist on the island other than the"
				+ "\nusual suspects. They may even be responsible for our missing crews and agents!",
				"We have no leads, but previous agent reports indicate a strange PKE valence not"
				+ "\nrecorded on the usual channels. We've attuned the [c/4444FF:blue] channel to the latest"
				+ "\nreadings. Investigate at your discretion."
			}
		);
	}
}
