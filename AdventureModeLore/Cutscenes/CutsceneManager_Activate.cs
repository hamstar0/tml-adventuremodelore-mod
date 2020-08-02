using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Net;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager : ILoadable {
		public bool CanBeginForWorld( CutsceneID cutsceneId ) {
			if( this.CurrentActiveCutscene != 0 ) {
				return false;
			}
			if( this.IsCutsceneActivatedForWorld(cutsceneId) ) {
				return false;
			}

			return this.Cutscenes[cutsceneId]
				.HasValidWorldConditions();
		}

		public bool CanBeginForPlayer( CutsceneID cutsceneId, Player player ) {
			if( this.CurrentActiveCutscene != 0 ) {
				return false;
			}
			if( this.IsCutsceneActivatedForPlayer(cutsceneId, player) ) {
				return false;
			}

			return this.Cutscenes[cutsceneId]
				.HasValidPlayerConditions( player );
		}


		////////////////

		public bool BeginCutscene( CutsceneID cutsceneId, Player player ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.HasValidPlayerConditions(player) ) {
				return false;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.ActivatedCutscenes.Add( cutsceneId );

			cutscene.BeginForWorld();
			this.BeginCutsceneForPlayer( cutsceneId, player );

			return true;
		}


		////

		internal bool BeginCutsceneForPlayer( CutsceneID cutsceneId, Player player ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.HasValidPlayerConditions( player ) ) {
				return false;
			}

			if( Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient ) {
				cutscene.BeginForPlayer( player );
			} else if( Main.netMode == NetmodeID.Server ) {
				AMLCutsceneNetData.SendToClient( player.whoAmI, cutsceneId );
			}

			return true;
		}
	}
}
