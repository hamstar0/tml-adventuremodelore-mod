using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace AdventureModeLore.Definitions {
	public abstract partial class Cutscene {
		protected abstract class ActiveCutscene {
			/// <summary></summary>
			public abstract void OnBegin_World( int sceneIdx );

			/// <summary></summary>
			/// <param name="sceneIdx"></param>
			public virtual void OnBegin_Player( Player player, int sceneIdx ) { }


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
