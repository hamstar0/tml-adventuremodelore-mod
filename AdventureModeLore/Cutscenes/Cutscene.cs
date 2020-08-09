using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	public enum CutsceneID {
		Intro = 1
	}




	public abstract partial class Cutscene {
		public abstract CutsceneID UniqueId { get; }

		public Scene[] Scenes { get; }

		public int CurrentScene { get; protected set; } = 0;

		////

		public Vector2 StartPosition { get; protected set; }



		////////////////

		protected Cutscene() {
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
