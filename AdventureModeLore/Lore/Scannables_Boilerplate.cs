using System;
using Terraria;
using ModLibsCore.Classes.Loadable;
using Messages;
using Messages.Definitions;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		public static void CreateMessage(
					string msgId,
					string title,
					string msg,
					bool important=true,
					Message parent=null ) {
			MessagesAPI.AddMessagesCategoriesInitializeEvent( () => {
				MessagesAPI.AddMessage(
					title: title,
					description: msg,
					modOfOrigin: AMLMod.Instance,
					alertPlayer: MessagesAPI.IsUnread( msgId ),
					isImportant: important,
					parentMessage: parent ?? MessagesAPI.EventsCategoryMsg,
					id: msgId
				);
			} );
		}


		////////////////

		public static void CreatePercentObjective(
					string title,
					string msg,
					int units,
					PercentObjective.PercentObjectiveCondition condition ) {
			var objective = new PercentObjective(
				title: title,
				description: msg,
				units: units,
				condition: condition
			);

			ObjectivesAPI.AddObjective( objective, 0, true, out _ );
		}


		////////////////
		
		public static bool FindTileNear( int worldX, int worldY, Func<Tile, bool> criteria ) {
			int rectRadius = 24;

			for( int x = (worldX - rectRadius); x <= (worldX + rectRadius); x += rectRadius ) {
				int tileX = x / 16;
				
				for( int y = (worldY - rectRadius); y <= (worldY + rectRadius); y += rectRadius ) {
					int tileY = y / 16;
					if( !WorldGen.InWorld(x, y) ) {
						continue;
					}

					Tile tile = Main.tile[tileX, tileY];
					if( tile?.active() == true && criteria(tile) ) {
						return true;
					}
				}
			}

			return false;
		}
	}
}
