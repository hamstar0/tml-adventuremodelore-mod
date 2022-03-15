using System;
using Messages;
using ModLibsCore.Classes.Loadable;
using Objectives;
using Objectives.Definitions;


namespace AdventureModeLore.Lore {
	partial class Scannables : ILoadable {
		public static void CreateMessage( string msgId, string title, string msg ) {
			MessagesAPI.AddMessage(
				title: title,
				description: msg,
				modOfOrigin: AMLMod.Instance,
				alertPlayer: MessagesAPI.IsUnread( msgId ),
				isImportant: true,
				parentMessage: MessagesAPI.EventsCategoryMsg,
				id: msgId
			);
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
	}
}
