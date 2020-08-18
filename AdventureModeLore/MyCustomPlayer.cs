using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.PlayerData;
using AdventureModeLore.Logic;


namespace AdventureModeLore {
	class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( object data ) {
			base.OnEnter( data );

			this.Player.GetModPlayer<AMLPlayer>().IsAdventureModePlayer = true;
			LogHelpers.Log( "Player "+this.Player.name+" prepped for Adventure Mode." );
		}

		protected override object OnExit() {
			CutsceneManager.Instance.ResetCutscenes();
			return base.OnExit();
		}
	}
}