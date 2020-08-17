using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.PlayerData;
using AdventureModeLore.Logic;


namespace AdventureModeLore {
	class AMLCustomPlayer : CustomPlayerData {
		protected override void OnEnter( object data ) {
			base.OnEnter( data );
			CutsceneManager.Instance.ResetCutscenes();
		}
	}
}