﻿using System;
using System.Reflection;
using System.Runtime.Remoting;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public class CutsceneID {
		public string ModName { get; }
		public string ClassName { get; }



		////////////////

		public CutsceneID( Mod mod, string name ) {
			this.ModName = mod.Name;
			this.ClassName = name;
		}

		public CutsceneID( string modName, string name ) {
			this.ModName = modName;
			this.ClassName = name;
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

		internal Cutscene Create( Player playsFor, params object[] args ) {
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
			return objHand.Unwrap() as Cutscene;
		}
	}
}
