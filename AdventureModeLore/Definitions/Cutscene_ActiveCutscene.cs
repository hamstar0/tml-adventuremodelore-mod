using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		protected abstract partial class ActiveCutscene {
			public Cutscene Parent { get; }

			public int PlaysForWhom { get; }

			////

			public int CurrentSceneIdx { get; internal set; } = 0;



			////////////////

			public ActiveCutscene( Cutscene parent, Player playsFor, int sceneIdx ) {
				this.Parent = parent;
				this.PlaysForWhom = playsFor.whoAmI;
				this.CurrentSceneIdx = sceneIdx;
			}

			public abstract ActiveCutscene Clone();


			////////////////

			public virtual void OnEnd_World() { }

			public virtual void OnEnd_Player( Player player ) { }


			////////////////

			public virtual bool Update_World() {
				return false;
			}

			public virtual bool Update_Player( Player player ) {
				return false;
			}
		}
	}
}
