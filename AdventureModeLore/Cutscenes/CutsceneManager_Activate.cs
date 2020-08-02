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
		internal bool CanBeginForWorld( CutsceneID cutsceneId ) {
			return this.Cutscenes[ cutsceneId ].CanBeginForWorld();
		}

		public bool CanBeginForPlayer( CutsceneID cutsceneId, Player player ) {
			return this.Cutscenes[ cutsceneId ].CanBeginForPlayer( player );
		}


		////////////////

		public bool BeginCutscene( CutsceneID cutsceneId, Player player ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.CanBeginForPlayer(player) ) {
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
			if( !cutscene.CanBeginForPlayer( player ) ) {
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
