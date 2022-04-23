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
		
		public static bool FindTileNear( int tileX, int tileY, Func<Tile, bool> criteria ) {
			int rad = 1;

			for( int x = (tileX - rad); x <= (tileX + rad); x += rad ) {
				for( int y = (tileY - rad); y <= (tileY + rad); y += rad ) {
					if( !WorldGen.InWorld(x, y) ) {
						continue;
					}

					Tile tile = Main.tile[x, y];
					if( tile?.active() == true && criteria(tile) ) {
						return true;
					}
				}
			}

			return false;
		}
	}
}
