using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		protected Vector2 CameraStart;

		////

		protected CutsceneDialogue Dialogue = null;


		////////////////

		public bool MustSync { get; }



		////////////////

		protected Scene( bool mustSync, Vector2 cameraBegin ) {
			this.MustSync = mustSync;
			this.CameraStart = cameraBegin;
		}


		////////////////

		internal void BeginOnPlayer_Internal( Cutscene parent, Player player ) {
			if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
				this.Dialogue?.ShowDialogue();
			}

			this.OnBeginOnPlayer( parent, player );
		}

		internal void BeginOnWorld_Internal( Cutscene parent ) {
			this.OnBeginOnWorld( parent );
		}

		////

		protected virtual void OnBeginOnPlayer( Cutscene parent, Player player ) { }

		protected virtual void OnBeginOnWorld( Cutscene parent ) { }

		////////////////

		internal void EndForWorld_Private() {
			this.OnEndOnWorld();
		}
		
		internal void EndForPlayer_Private( Player player ) {
			this.Dialogue?.HideDialogue();
			this.OnEndOnPlayer( player );
		}

		////

		protected virtual void OnEndOnPlayer( Player player ) { }

		protected virtual void OnEndOnWorld() { }
	}
}
