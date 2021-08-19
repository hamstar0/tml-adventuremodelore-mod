using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore.Sequenced.Events {
	public partial class DialogueLoreEvents {
		public static int CountDiscoveredMirrors() {
			if( ModLoader.GetMod( "MountedMagicMirrors" ) != null ) {
				return DialogueLoreEvents.CountDiscoveredMirrors_WeakRef();
			}
			return 0;
		}

		private static int CountDiscoveredMirrors_WeakRef() {
			return MountedMagicMirrors.MountedMagicMirrorsAPI.GetDiscoveredMirrors( Main.LocalPlayer ).Count;
		}
	}
}
