using System;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.TModLoader;
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


		////////////////

		private DynamicDialogueHandler GetGreetingFunc( int npcType, string[] consumableGreetings ) {
			if( consumableGreetings.Length == 0 ) {
				return null;
			}

			int i = 0;
			DynamicDialogueHandler oldHandler = DialogueEditor.GetDynamicDialogueHandler( npcType );

			return new DynamicDialogueHandler(
				getDialogue: ( msg ) => {
					if( i < consumableGreetings.Length ) {
						return consumableGreetings[i++];
					}

					string npcKey = NPCID.GetUniqueKey( npcType );
					var myplayer = TmlHelpers.SafelyGetModPlayer<AMLPlayer>( Main.LocalPlayer );

					myplayer.AlreadyIntroducedNpcs.Add( npcKey );

					if( oldHandler != null ) {
						DialogueEditor.SetDynamicDialogueHandler( npcType, oldHandler );
					}
					return msg;
				},
				isShowingAlert: () => i < consumableGreetings.Length
			);
		}
	}
}
