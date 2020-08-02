using System;
using Terraria;


namespace AdventureModeLore.Cutscenes.Intro {
	public enum CutsceneID {
		Intro = 1
	}




	public abstract class Cutscene {
		public abstract CutsceneID UniqueId { get; }



		////////////////

		public virtual bool CanBeginForWorld() {
			var cutsceneMngr = CutsceneManager.Instance;

			if( cutsceneMngr.CurrentActiveCutscene != 0 ) {
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForWorld(this.UniqueId) ) {
				return false;
			}

			return true;
		}

		public virtual bool CanBeginForPlayer( Player player ) {
			var cutsceneMngr = CutsceneManager.Instance;

			if( cutsceneMngr.CurrentActiveCutscene != 0 ) {
				return false;
			}
			if( cutsceneMngr.IsCutsceneActivatedForPlayer(this.UniqueId, player) ) {
				return false;
			}

			return true;
		}


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
