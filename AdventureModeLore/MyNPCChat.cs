using System;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Services.Dialogue;


namespace AdventureModeLore {
	partial class AdventureModeNpcChat : ILoadable {
		public void OnModsLoad() { }

		public void OnPostModsLoad() {
			foreach( (int npcType, NPCDialogueDefinitions chats) in this.NPCDialogues ) {
				foreach( string addedChat in chats.Added ) {
					if( chats.IsAvailable?.Invoke(addedChat) ?? true ) {
						DialogueEditor.AddChatForNPC( npcType, addedChat, 0.1f );
					}
				}
				foreach( string blockedChat in chats.Blocked ) {
					DialogueEditor.AddChatRemoveFlatPatternForNPC( npcType, blockedChat );
				}
			}
		}

		public void OnModsUnload() { }
	}
}
