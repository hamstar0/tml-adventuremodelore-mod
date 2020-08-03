using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager {
		public static CutsceneManager Instance { get; internal set; }



		////////////////

		public IReadOnlyDictionary<CutsceneID, Cutscene> Cutscenes { get; }

		public ISet<CutsceneID> TriggeredCutsceneIDs { get; } = new HashSet<CutsceneID>();

		public CutsceneID CurrentlyPlayingCutsceneID { get; private set; }


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
			return this.TriggeredCutsceneIDs.Contains( cutsceneId );
		}

		public bool IsCutsceneActivatedForPlayer( CutsceneID cutsceneId, Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			return myplayer.TriggeredCutsceneIDsForPlayer.Contains( cutsceneId );
		}
	}
}
