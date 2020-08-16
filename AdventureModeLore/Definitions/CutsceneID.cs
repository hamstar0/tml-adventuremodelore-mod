using System;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public class CutsceneID {
		public string ModName { get; }
		public string Name { get; }



		////////////////

		public CutsceneID( Mod mod, string name ) {
			this.ModName = mod.Name;
			this.Name = name;
		}

		public CutsceneID( string modName, string name ) {
			this.ModName = modName;
			this.Name = name;
		}

		////

		public override int GetHashCode() {
			return this.ModName.GetHashCode() ^ this.Name.GetHashCode();
		}

		public override bool Equals( object obj ) {
			var comp = obj as CutsceneID;
			if( comp == null ) { return false; }

			return comp.ModName == this.ModName && comp.Name == this.Name;
		}

		////

		public override string ToString() {
			return this.ModName+":"+this.Name;
		}
	}
}
