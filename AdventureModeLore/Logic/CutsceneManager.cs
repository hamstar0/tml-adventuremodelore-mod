using System;
using System.Linq;
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

		public void ResetCutscenes() {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				cutscene.Reset();
			}
		}


		////////////////

		public bool HasCutscenePlayed_World( CutsceneID cutsceneId ) {
			var myworld = ModContent.GetInstance<AMLWorld>();
			return myworld.TriggeredCutsceneIDs_World.Contains( cutsceneId );
		}

		public bool HasCutscenePlayed_Player( CutsceneID cutsceneId, Player player ) {
			var myplayer = player.GetModPlayer<AMLPlayer>();
			return myplayer.TriggeredCutsceneIDs_Player.Contains( cutsceneId );
		}


		////////////////

		public T GetCutscene<T>() where T : Cutscene {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( typeof(T) == cutscene.GetType() ) {
					return (T)cutscene;
				}
			}
			return null;
		}


		////////////////

		public Cutscene GetCurrentCutscene_Player( Player player ) {
			foreach( Cutscene cutscene in this.Cutscenes.Values ) {
				if( cutscene.IsPlayingFor(player.whoAmI) ) {
					return cutscene;
				}
			}
			return null;
		}

		public IEnumerable<Cutscene> GetActiveCutscenes_World() {
			return this.Cutscenes
				.Select( kv => kv.Value )
				.Where( c => c.IsPlaying() );
		}
	}
}
