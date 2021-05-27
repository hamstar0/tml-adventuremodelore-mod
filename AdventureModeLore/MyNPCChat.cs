using System;
using Terraria;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsNPCDialogue.Services.Dialogue;


namespace AdventureModeLore {
	partial class AdventureModeNpcChat : ILoadable {
		public void OnModsLoad() { }

		public void OnPostModsLoad() {
			foreach( (int npcType, NPCDialogueDefinitions chats) in AdventureModeNpcChat.NPCDialogues ) {
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
