using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using AdventureModeLore.Lore;


namespace AdventureModeLore {
	public partial class AMLMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-adventuremodelore-mod";


		////////////////

		public static AMLMod Instance { get; private set; }



		////////////////

		private int _CheckTimer = 0;


		////////////////

		public bool HasFurnishedAHouse { get; internal set; } = false;



		////////////////

		public override void Load() {
			AMLMod.Instance = this;
		}

		public override void PostSetupContent() {
			if( ModLoader.GetMod( "PKEMeter" ) != null ) {
				AMLMod.InitializePKE();
			}
			if( ModLoader.GetMod( "SpiritWalking" ) != null ) {
				AMLMod.InitializeSpiritWalking();
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

			if( Main.netMode != NetmodeID.Server ) {
				if( this._CheckTimer-- <= 0 ) {
					this._CheckTimer = 60;

					LoreEventManager.RunPerSecond_Local();
				}
			}
		}
	}
}