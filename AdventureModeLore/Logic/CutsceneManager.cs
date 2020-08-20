﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.DotNET.Extensions;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		public static CutsceneManager Instance { get; private set; }



		////////////////

		public IReadOnlyDictionary<int, Cutscene> CutscenePerPlayer { get; }


		////////////////

		private IDictionary<int, Cutscene> _CutscenePerPlayer;



		////////////////

		internal CutsceneManager() {
			this._CutscenePerPlayer = new Dictionary<int, Cutscene>();
			this.CutscenePerPlayer = new ReadOnlyDictionary<int, Cutscene>( this._CutscenePerPlayer );
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
			this._CutscenePerPlayer.Clear();
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

		public IEnumerable<T> GetCutscenes<T>() where T : Cutscene {
			return this._CutscenePerPlayer.Values
				.Where( c => c.GetType() == typeof(T) )
				.Select( c => c as T );
		}


		////////////////

		public Cutscene GetCurrentCutscene_Player( Player player ) {
			return this._CutscenePerPlayer.GetOrDefault( player.whoAmI );
		}

		public IEnumerable<Cutscene> GetActiveCutscenes_World() {
			return this._CutscenePerPlayer.Values;
		}
	}
}
