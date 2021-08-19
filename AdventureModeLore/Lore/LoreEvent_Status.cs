using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore {
	public abstract partial class LoreEvent {
		public (bool CanBegin, bool IsDone) GetStatusForLocalPlayer() {
			var myplayer = Main.LocalPlayer.GetModPlayer<AMLPlayer>();
			if( myplayer.CompletedLoreStages.Contains( this.Name ) ) {
				return (false, true);
			}

			return (this.ArePrerequisitesMet(), this.HasEventFinished());
		}


		protected abstract bool HasEventFinished();


		////////////////

		public abstract void BeginForLocalPlayer( bool isRepeat );
	}
}
