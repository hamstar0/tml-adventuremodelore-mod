using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.Dialogues.Events {
	public partial class DialogueLoreEventDefinitions {
		public static int CountDiscoveredMirrors() {
			if( ModLoader.GetMod( "MountedMagicMirrors" ) != null ) {
				return DialogueLoreEventDefinitions.CountDiscoveredMirrors_WeakRef();
			}
			return 0;
		}

		private static int CountDiscoveredMirrors_WeakRef() {
			return MountedMagicMirrors.MountedMagicMirrorsAPI.GetDiscoveredMirrors( Main.LocalPlayer ).Count;
		}
	}
}
