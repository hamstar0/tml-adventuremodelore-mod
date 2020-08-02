using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager : ILoadable {
		public static CutsceneManager Instance => ModContent.GetInstance<CutsceneManager>();



		////////////////

		public IReadOnlyDictionary<CutsceneID, Cutscene> Cutscenes { get; }

		public CutsceneID CurrentActiveCutscene { get; private set; }


		////////////////

		private IDictionary<CutsceneID, Cutscene> _Cutscenes = new Dictionary<CutsceneID, Cutscene> {
			{ CutsceneID.Intro, new IntroCutscene() }
		};



		////////////////

		internal CutsceneManager() {
			this.Cutscenes = new ReadOnlyDictionary<CutsceneID, Cutscene>( this._Cutscenes );
		}

		void ILoadable.OnModsLoad() { }
		void ILoadable.OnPostModsLoad() { }
		void ILoadable.OnModsUnload() { }


		////////////////
		
		public bool IsCutsceneActivatedForWorld( CutsceneID cutsceneId ) {
			var myworld = ModContent.GetInstance<AMLWorld>();
			return myworld.ActivatedCutscenes.Contains( cutsceneId );
		}

		public bool IsCutsceneActivatedForPlayer( CutsceneID cutsceneId, Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			return myplayer.ActivatedCutscenes.Contains( cutsceneId );
		}
	}
}
