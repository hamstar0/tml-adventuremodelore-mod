using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Errors;


namespace AdventureModeLore.Definitions {
	public abstract partial class Scene {
		protected CutsceneDialogue Dialogue = null;


		////////////////

		public bool WorldControlsScenesOnly { get; }

		public bool DefersToHostForSync { get; }



		////////////////

		protected Scene( bool worldControlsSyncOnly, bool defersToHostForSync ) {
			if( !this.ValidateSceneType(this.GetType()) ) {
				throw new ModHelpersException( "Invalid Scene type "+this.GetType().Name );
			}

			this.WorldControlsScenesOnly = worldControlsSyncOnly;
			this.DefersToHostForSync = defersToHostForSync;
		}

		////

		private bool ValidateSceneType( Type sceneType ) {
			Type parentType = sceneType.BaseType;
			if( parentType == null || parentType == typeof(object) ) {
				return false;
			}

			Type genSceneType = typeof( Scene<> );
			if( parentType.IsGenericType && parentType.GetGenericTypeDefinition() == genSceneType ) {
				return true;
			}

			return this.ValidateSceneType( parentType );
		}


		////////////////

		internal virtual void Begin_Player_Internal( Cutscene parent, Player player ) {
			if( Main.netMode != NetmodeID.Server && player.whoAmI == Main.myPlayer ) {
				this.Dialogue?.ShowDialogue();
			}
		}

		internal virtual void Begin_World_Internal( Cutscene parent ) {
		}

		////////////////
		
		internal virtual void End_Player_Internal( Cutscene parent, Player player ) {
			this.Dialogue?.HideDialogue();
			//this.OnEndOnPlayer( player );
		}

		internal virtual void End_World_Internal( Cutscene parent ) {
			//this.OnEndOnWorld();
		}
	}
}
