using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
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

		public override int GetHashCode() {
			return this.ModName.GetHashCode() ^ this.Name.GetHashCode();
		}

		public override bool Equals( object obj ) {
			var comp = obj as CutsceneID;
			if( comp == null ) { return false; }

			return comp.ModName == this.ModName && comp.Name == this.Name;
		}
	}




	public abstract partial class Cutscene {
		public CutsceneID UniqueId { get; }

		public Scene[] Scenes { get; }

		public int CurrentScene { get; protected set; } = 0;

		////

		public Vector2 StartPosition { get; protected set; }



		////////////////

		protected Cutscene( CutsceneID uid ) {
			this.UniqueId = uid;
			this.Scenes = this.LoadScenes();
		}

		////

		protected abstract Scene[] LoadScenes();


		////////////////
		
		internal void SetStartPosition( Vector2 pos ) {
			this.StartPosition = pos;
		}


		////////////////

		public abstract bool IsSiezingControls();

		public virtual void SiezeControl( string control, ref bool state ) {
			if( control == "Inventory" ) { return; }
			state = false;
		}
		
		////////////////

		public virtual bool AllowInterfaceLayer( GameInterfaceLayer layer ) {
			return false;
		}
		
		////////////////
		
		public virtual bool AllowNPC( NPC npc ) {
			return npc.friendly;
		}
	}
}
