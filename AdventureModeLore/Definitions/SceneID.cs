using System;
using System.Reflection;
using System.Runtime.Remoting;
using Terraria.ModLoader;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public class SceneID {
		public string ModAssemblyName { get; }
		public string FullClassName { get; }



		////////////////

		public SceneID( Mod mod, Scene instance ) : this( mod, instance.GetType() ) { }

		public SceneID( Mod mod, Type sceneType ) : this( mod.Code.GetName().Name, sceneType.FullName ) {
			for( Type baseType= sceneType.BaseType; baseType!=typeof(Scene); baseType = baseType.BaseType ) {
				if( baseType == typeof(object) ) {
					throw new ModHelpersException( sceneType.Name + " is not a `Scene`." );
				}
			}
		}

		internal SceneID( string modAssemblyName, string fullClassName ) {
			this.ModAssemblyName = modAssemblyName;
			this.FullClassName = fullClassName;
		}

		////

		public override int GetHashCode() {
			return this.ModAssemblyName.GetHashCode() ^ this.FullClassName.GetHashCode();
		}

		public override bool Equals( object obj ) {
			var comp = obj as SceneID;
			if( comp == null ) { return false; }

			return comp.ModAssemblyName == this.ModAssemblyName && comp.FullClassName == this.FullClassName;
		}

		////

		public override string ToString() {
			return this.ModAssemblyName+":"+this.FullClassName;
		}


		////////////////

		internal Scene Create( Player playsFor, params object[] args ) {
			var newArgs = new object[args.Length + 1];
			newArgs[0] = playsFor;
			args.CopyTo( newArgs, 1 );

			ObjectHandle objHand = Activator.CreateInstance(
				assemblyName: this.ModAssemblyName,
				typeName: this.FullClassName,
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
