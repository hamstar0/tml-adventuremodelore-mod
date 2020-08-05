using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager {
		public static CutsceneManager Instance { get; internal set; }



		////////////////

		public IReadOnlyDictionary<CutsceneID, Cutscene> Cutscenes { get; }


		////////////////

		private IDictionary<CutsceneID, Cutscene> _Cutscenes = new Dictionary<CutsceneID, Cutscene> {
			{ CutsceneID.Intro, new IntroCutscene() }
		};



		////////////////

		internal CutsceneManager() {
			this.Cutscenes = new ReadOnlyDictionary<CutsceneID, Cutscene>( this._Cutscenes );
		}


		////////////////
		
		public bool IsCutsceneActivatedForWorld( CutsceneID cutsceneId ) {
			var myworld = ModContent.GetInstance<AMLWorld>();
			return myworld.TriggeredCutsceneIDsForWorld.Contains( cutsceneId );
		}

		public bool IsCutsceneActivatedForPlayer( CutsceneID cutsceneId, Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			return myplayer.TriggeredCutsceneIDsForPlayer.Contains( cutsceneId );
		}
	}
}
