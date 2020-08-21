using System;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Terraria;
using System.Reflection;
using System.Runtime.Remoting;


namespace AdventureModeLore.Definitions {
	public class SceneID {
		public string ModName { get; }
		public string ClassName { get; }



		////////////////

		public SceneID( Mod mod, Type classType ) {
			this.ModName = mod.Name;
			this.ClassName = classType.Name;
		}

		public SceneID( string modName, string className ) {
			this.ModName = modName;
			this.ClassName = className;
		}

		////

		public override int GetHashCode() {
			return this.ModName.GetHashCode() ^ this.ClassName.GetHashCode();
		}

		public override bool Equals( object obj ) {
			var comp = obj as CutsceneID;
			if( comp == null ) { return false; }

			return comp.ModName == this.ModName && comp.ClassName == this.ClassName;
		}

		////

		public override string ToString() {
			return this.ModName+":"+this.ClassName;
		}


		////////////////

		internal Scene Create( Player playsFor, params object[] args ) {
			var newArgs = new object[args.Length + 1];
			newArgs[0] = playsFor;
			args.CopyTo( newArgs, 1 );

			ObjectHandle objHand = Activator.CreateInstance(
				assemblyName: this.ModName,
				typeName: this.ClassName,
				ignoreCase: false,
				bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
				binder: null,
				args: newArgs,
				culture: null,
				activationAttributes: new object[] { }
			);
			return objHand.Unwrap() as Scene;
		}
	}
}
