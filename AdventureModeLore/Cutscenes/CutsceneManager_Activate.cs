using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Net;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager {
		public bool BeginCutscene( CutsceneID cutsceneId, Player player ) {
			Cutscene cutscene = this.Cutscenes[ cutsceneId ];
			if( !cutscene.CanBeginForPlayer(player) ) {
				return false;
			}

			var myworld = ModContent.GetInstance<AMLWorld>();
			myworld.TriggeredCutsceneIDsForWorld.Add( cutsceneId );
			myworld.CurrentPlayingCutsceneForWorld = cutsceneId;

			cutscene.BeginForWorld();
			this.BeginCutsceneForPlayer( cutsceneId, player );

			return true;
		}


		internal bool BeginCutsceneForPlayer( CutsceneID cutsceneId, Player player ) {
			Cutscene cutscene = this.Cutscenes[cutsceneId];
			if( !cutscene.CanBeginForPlayer( player ) ) {
				return false;
			}

			var myplayer = player.GetModPlayer<AMLPlayer>();
			myplayer.TriggeredCutsceneIDsForPlayer.Add( cutsceneId );
			myplayer.CurrentPlayingCutsceneForPlayer = cutsceneId;

			if( Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient ) {
				cutscene.BeginForPlayer( player );
			} else if( Main.netMode == NetmodeID.Server ) {
				AMLCutsceneNetData.SendToClient( player.whoAmI, cutsceneId );
			}

			return true;
		}
	}
}
