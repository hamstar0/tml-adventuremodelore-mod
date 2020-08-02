using System;
using Terraria;


namespace AdventureModeLore.Cutscenes.Intro {
	public enum CutsceneID {
		Intro = 1
	}




	public abstract class Cutscene {
		public abstract CutsceneID UniqueId { get; }



		////////////////

		public abstract bool HasValidPlayerConditions( Player player );

		public abstract bool HasValidWorldConditions();


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
