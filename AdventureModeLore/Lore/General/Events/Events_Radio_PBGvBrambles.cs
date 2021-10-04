using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using Messages;
using Messages.Definitions;


namespace AdventureModeLore.Lore.General.Events {
	public partial class GeneralLoreEventDefinitions {
		private static bool Event_Radio_PBGvBrambles_PreReq() {
			int pkeType = ModContent.ItemType<PKEMeter.Items.PKEMeterItem>();
			bool hasPKE = Main.LocalPlayer.inventory
				.Any( i => i?.active == true && i.type == pkeType );
			if( !hasPKE ) {
				return false;
			}

			int brambleType = ModContent.TileType<CursedBrambles.Tiles.CursedBrambleTile>();
			int tileX = (int)Main.LocalPlayer.Center.X / 16;
			int tileY = (int)Main.LocalPlayer.Center.Y / 16;
			int mixX = tileX - 16;
			int mixY = tileY - 16;
			int maxX = tileX + 16;
			int maxY = tileY + 16;

			// Near brambles
			for( int x=mixX; x<maxX; x++ ) {
				for( int y=mixY; y<maxY; y++ ) {
					if( !WorldGen.InWorld(x, y) ) {
						continue;
					}

					Tile tile = Main.tile[x, y];
					if( tile?.active() != true || tile.type != brambleType ) {
						continue;
					}

					return true;
				}
			}

			return false;
		}


		////////////////

		private static GeneralLoreEvent GetEvent_Radio_PBGvBrambles() {
			bool PreReq() {
				if( ModLoader.GetMod("SoulBarriers") == null ) {
					return false;
				}
				if( ModLoader.GetMod("PKEMeter") == null ) {
					return false;
				}
				if( ModLoader.GetMod("CursedBrambles") == null ) {
					return false;
				}
				return GeneralLoreEventDefinitions.Event_Radio_PBGvBrambles_PreReq();
			}

			//

			string msgId = "AML_Radio_PBGvBrambles";
			string msg = Message.RenderFormattedDescription( NPCID.Guide,
				"Curse those brambles! I guess you can now see where they get their name. Use your [c/88FF88:P.B.G] to clear them"
				+" out of your way, like most other metaphysical threats."
			);

			return new GeneralLoreEvent(
				name: "Radio - PBGvBrambles",
				prereqs: new Func<bool>[] { PreReq },
				myevent: () => {
					Timers.SetTimer( 60 * 5, false, () => {
						// No bosses
						if( Main.npc.Any(n => n?.active == true && n.boss) ) {
							return true;
						}

						MessagesAPI.AddMessage(
							title: "About Cursed Brambles",
							description: msg,
							modOfOrigin: AMLMod.Instance,
							alertPlayer: MessagesAPI.IsUnread(msgId),
							isImportant: true,
							parentMessage: MessagesAPI.EventsCategoryMsg,
							id: msgId
						);
						return false;
					} );
				},
				isRepeatable: false
			);
		}
	}
}
