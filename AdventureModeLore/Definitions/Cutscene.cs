using System;
using Terraria;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public CutsceneID UniqueId { get; }

		protected Scene[] Scenes { get; }

		public int CurrentSceneIdx { get; protected set; } = 0;

		////////////////

		protected ActiveCutscene ActiveForWorld = null;

		protected ActiveCutscene[] ActiveForPlayer = new ActiveCutscene[255];



		////////////////

		protected Cutscene( CutsceneID uid ) {
			this.UniqueId = uid;
			this.Scenes = this.LoadScenes();
		}

		////

		protected abstract Scene[] LoadScenes();

		protected abstract ActiveCutscene CreateActiveCutscene();


		////////////////

		public abstract AMLCutsceneNetData GetPacketPayload( int sceneIdx );


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
