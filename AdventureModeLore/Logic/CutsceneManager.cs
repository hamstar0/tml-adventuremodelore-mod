using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using AdventureModeLore.Definitions;
using AdventureModeLore.ExampleCutscenes.Intro;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		public static CutsceneManager Instance { get; private set; }



		////////////////

		public IReadOnlyDictionary<CutsceneID, Cutscene> Cutscenes { get; }


		////////////////

		private IDictionary<CutsceneID, Cutscene> _Cutscenes;



		////////////////

		internal CutsceneManager() {
			Cutscene exampleAmIntro = IntroCutscene.Create( "Opening" );

			this._Cutscenes = new Dictionary<CutsceneID, Cutscene> {
				{ exampleAmIntro.UniqueId, exampleAmIntro }
			};
			this.Cutscenes = new ReadOnlyDictionary<CutsceneID, Cutscene>( this._Cutscenes );
		}

		void ILoadable.OnModsLoad() {
			CutsceneManager.Instance = new CutsceneManager();
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() {
			CutsceneManager.Instance = null;
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


		////////////////

		public Cutscene GetCurrentPlayerCutscene( Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			if( myplayer.CurrentPlayingCutsceneForPlayer == null ) {
				return null;
			}

			return this.Cutscenes[ myplayer.CurrentPlayingCutsceneForPlayer ];
		}

		public Cutscene GetCurrentWorldCutscene() {
			var myworld = ModContent.GetInstance<AMLWorld>();
			if( myworld.CurrentPlayingCutsceneForWorld == null ) {
				return null;
			}

			return this.Cutscenes[ myworld.CurrentPlayingCutsceneForWorld ];
		}
	}
}
