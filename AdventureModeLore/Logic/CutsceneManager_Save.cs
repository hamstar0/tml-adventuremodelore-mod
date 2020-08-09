using System;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using AdventureModeLore.Definitions;


namespace AdventureModeLore.Logic {
	public partial class CutsceneManager : ILoadable {
		internal void LoadForWorld( AMLWorld myworld, TagCompound tag ) {
			myworld.TriggeredCutsceneIDsForWorld.Clear();

			if( !tag.ContainsKey( "TriggeredCutsceneCount" ) ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutsceneCount" );

			for( int i = 0; i < count; i++ ) {
				var cutsceneId = (CutsceneID)tag.GetInt( "TriggeredCutscene_" + i );
				myworld.TriggeredCutsceneIDsForWorld.Add( cutsceneId );
			}
		}

		internal void SaveForWorld( AMLWorld myworld, TagCompound tag ) {
			int count = myworld.TriggeredCutsceneIDsForWorld.Count;
			tag["TriggeredCutsceneCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDsForWorld ) {
				tag["TriggeredCutscene_" + i] = (int)uid;
				i++;
			}
		}

		////

		internal void NetSendForWorld( AMLWorld myworld, BinaryWriter writer ) {
			writer.Write( (int)myworld.TriggeredCutsceneIDsForWorld.Count );

			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDsForWorld ) {
				writer.Write( (int)uid );
			}
		}

		internal void NetReceiveForWorld( AMLWorld myworld, BinaryReader reader ) {
			myworld.TriggeredCutsceneIDsForWorld.Clear();

			int count = reader.ReadInt32();

			for( int i=0; i<count; i++ ) {
				var cutsceneId = (CutsceneID)reader.ReadInt32();
				myworld.TriggeredCutsceneIDsForWorld.Add( cutsceneId );
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
				tag["TriggeredCutscene_" + i] = (int)uid;
				i++;
			}
		}
	}
}
