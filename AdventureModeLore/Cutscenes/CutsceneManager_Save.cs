using System;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager {
		internal void LoadForWorld( TagCompound tag ) {
			this.TriggeredCutsceneIDs.Clear();

			if( !tag.ContainsKey( "TriggeredCutsceneCount" ) ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutsceneCount" );

			for( int i = 0; i < count; i++ ) {
				this.TriggeredCutsceneIDs.Add( (CutsceneID)tag.GetInt( "TriggeredCutscene_" + i ) );
			}
		}

		internal void SaveForWorld( TagCompound tag ) {
			int count = this.TriggeredCutsceneIDs.Count;
			tag["TriggeredCutsceneCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in this.TriggeredCutsceneIDs ) {
				tag["TriggeredCutscene_" + i] = uid;
				i++;
			}
		}

		////

		internal void NetSendForWorld( BinaryWriter writer ) {
			writer.Write( (int)this.TriggeredCutsceneIDs.Count );

			foreach( CutsceneID uid in this.TriggeredCutsceneIDs ) {
				writer.Write( (int)uid );
			}
		}

		internal void NetReceiveForWorld( BinaryReader reader ) {
			this.TriggeredCutsceneIDs.Clear();

			int count = reader.ReadInt32();

			for( int i=0; i<count; i++ ) {
				this.TriggeredCutsceneIDs.Add( (CutsceneID)reader.ReadInt32() );
			}
		}


		////////////////

		internal void LoadForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			myplayer.TriggeredCutsceneIDsForPlayer.Clear();

			if( !tag.ContainsKey("TriggeredCutsceneCount") ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutsceneCount" );

			for( int i=0; i<count; i++ ) {
				myplayer.TriggeredCutsceneIDsForPlayer.Add( (CutsceneID)tag.GetInt("TriggeredCutscene_"+i) );
			}
		}

		internal void SaveForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			int count = myplayer.TriggeredCutsceneIDsForPlayer.Count;
			tag["TriggeredCutsceneCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myplayer.TriggeredCutsceneIDsForPlayer ) {
				tag["TriggeredCutscene_" + i] = uid;
				i++;
			}
		}
	}
}
