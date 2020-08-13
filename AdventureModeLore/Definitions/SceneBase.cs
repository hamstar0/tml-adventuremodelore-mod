﻿using HamstarHelpers.Classes.Errors;
using System;
using Terraria;
using Terraria.ID;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		protected CutsceneDialogue Dialogue = null;


		////////////////

		public bool MustSync { get; }



		////////////////

		protected Scene( bool mustSync ) {
			if( !this.ValidateSceneType(this.GetType()) ) {
				throw new ModHelpersException( "Invalid Scene type "+this.GetType().Name );
			}

			this.MustSync = mustSync;
		}

		private bool ValidateSceneType( Type sceneType ) {
			Type parentType = sceneType.BaseType;
			if( parentType == null || parentType == typeof(object) ) {
				return false;
			}

			if( parentType == typeof( Scene<> ) ) {
				return true;
			}

			return this.ValidateSceneType( parentType );
		}


		////////////////

		internal virtual void BeginOnPlayer_Internal( Cutscene parent, Player player ) {
			if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
				this.Dialogue?.ShowDialogue();
			}

			//this.OnBeginOnPlayer( parent, player );
		}

		internal virtual void BeginOnWorld_Internal( Cutscene parent ) {
			//this.OnBeginOnWorld( parent );
		}

		////////////////

		internal virtual void EndForWorld_Internal( Cutscene parent ) {
			//this.OnEndOnWorld();
		}
		
		internal virtual void EndForPlayer_Internal( Cutscene parent, Player player ) {
			this.Dialogue?.HideDialogue();
			//this.OnEndOnPlayer( player );
		}
	}
}