using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureModeLore.Lore.Sequenced;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremodelore-mod";


		////////////////

		public static AMLMod Instance { get; private set; }



		////////////////

		private int _CheckTimer = 0;



		////////////////

		public AMLMod() {
			AMLMod.Instance = this;
		}

		public override void Load() {
		}

		public override void PostSetupContent() {
			if( ModLoader.GetMod( "PKEMeter" ) != null ) {
				AMLMod.InitializePKE();
			}
		}

		////

		public override void Unload() {
			AMLMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			if( Main.gameMenu ) {
				return;
			}
			if( Main.netMode == NetmodeID.Server ) {
				return;
			}

			if( this._CheckTimer-- <= 0 ) {
				this._CheckTimer = 60;

				SequencedLoreEventManager.RunForLocalPlayer();
			}
		}
	}
}