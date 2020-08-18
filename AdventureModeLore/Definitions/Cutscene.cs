using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
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

		public void Reset() {
			this.ActiveInstances.Clear();
		}


		////////////////

		public bool IsPlaying() {
			return this.ActiveInstances.Count > 0;
		}
		
		public bool IsPlayingFor( int playsForWho ) {
			return this.ActiveInstances.ContainsKey( playsForWho );
		}


		////////////////

		protected T GetActiveCutscene<T>( Player playsFor ) where T : ActiveCutscene {
			if( playsFor?.active != null ) {
				LogHelpers.Warn( "Inactive player #" + playsFor.whoAmI );
				return null;
			}

			return this.ActiveInstances.GetOrDefault( playsFor.whoAmI ) as T;
		}

		public abstract AMLCutsceneNetData GetPacketPayload( Player playsFor, int sceneIdx );


		////////////////

		public abstract bool IsSiezingControls();
		
		////

		internal void SiezeControl_Internal( string control, ref bool state ) {
			this.SiezeControl( control, ref state );
		}

		protected virtual void SiezeControl( string control, ref bool state ) {
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


		////////////////

		internal bool SetScene_Internal( Player playsFor, int sceneIdx ) {
			if( !this.ActiveInstances.ContainsKey(playsFor.whoAmI) ) {
				return false;
			}

			this.ActiveInstances[ playsFor.whoAmI ].SetCurrentScene( sceneIdx );
			return true;
		}
	}
}
