using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Lore {
	public partial class LoreEvents : ILoadable {
		public static int CountDiscoveredMirrors() {
			if( ModLoader.GetMod( "MountedMagicMirrors" ) != null ) {
				return LoreEvents.CountDiscoveredMirrors_WeakRef();
			}
			return 0;
		}

		private static int CountDiscoveredMirrors_WeakRef() {
			return MountedMagicMirrors.MountedMagicMirrorsAPI.GetDiscoveredMirrors( Main.LocalPlayer ).Count;
		}
	}
}
