using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		public CutsceneID UniqueId { get; }

		////

		protected Scene[] Scenes { get; }

		////

		protected IDictionary<int, ActiveCutscene> ActiveInstances { get; }
			= new ConcurrentDictionary<int, ActiveCutscene>();



		////////////////

		protected Cutscene( CutsceneID uid ) {
			this.UniqueId = uid;
			this.Scenes = this.LoadScenes();
		}

		////

		protected abstract Scene[] LoadScenes();


		////////////////
		
		public bool IsPlayingFor( int playsForWho ) {
			return this.ActiveInstances.ContainsKey( playsForWho );
		}


		////////////////

		public abstract AMLCutsceneNetData GetPacketPayload( Player playsFor, int sceneIdx );


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
