using System;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Cutscenes.Intro {
	public enum CutsceneID {
		Intro = 1
	}




	public abstract class Cutscene {
		public abstract CutsceneID UniqueId { get; }



		////////////////

		public virtual bool CanBeginForWorld() {
			var myworld = ModContent.GetInstance<AMLWorld>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myworld.CurrentPlayingCutsceneForWorld != 0 ) {
LogHelpers.LogOnce("Fail 1a");
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForWorld(this.UniqueId) ) {
LogHelpers.LogOnce("Fail 2a");
				return false;
			}
			
			return true;
		}

		public virtual bool CanBeginForPlayer( Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			var cutsceneMngr = CutsceneManager.Instance;

			if( myplayer.CurrentPlayingCutsceneForPlayer != 0 ) {
LogHelpers.LogOnce("Fail 1b");
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForPlayer(this.UniqueId, player) ) {
LogHelpers.LogOnce("Fail 2b");
				return false;
			}
			
			return true;
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


		////////////////

		public abstract void BeginForPlayer( Player player );

		public abstract void BeginForWorld();

		////
		
		internal virtual void UpdateForWorld() { }

		internal virtual void UpdateForPlayer( AMLPlayer myplayer ) { }


		////////////////

		/*void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }*/
	}
}
