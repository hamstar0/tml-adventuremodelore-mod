using System;
using Terraria;
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

			Type genSceneType = typeof( Scene<,> );
			if( parentType.IsGenericType && parentType.GetGenericTypeDefinition() == genSceneType ) {
				return true;
			}

			return this.ValidateSceneType( parentType );
		}


		////////////////

		public abstract SceneID GetNextSceneId();


		////////////////

		/// <summary></summary>
		/// <returns>`true` signifies scene has ended.</returns>
		internal virtual bool Update_Internal( Cutscene parent ) {
			return false;
		}


		////////////////
		
		internal virtual void Begin_Internal( Cutscene parent ) {
			if( parent.PlaysForWhom == Main.myPlayer ) {
				this.Dialogue?.ShowDialogue();
			}
		}

		////////////////
		
		internal virtual void End_Internal( Cutscene parent ) {
			if( parent.PlaysForWhom == Main.myPlayer ) {
				this.Dialogue?.HideDialogue();
			}
		}


		////////////////

		public virtual void DrawInterface() { }
	}
}
