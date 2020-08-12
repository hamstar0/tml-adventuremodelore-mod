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

			if( !tag.ContainsKey( "TriggeredCutscenesCount" ) ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutscenesCount" );

			for( int i = 0; i < count; i++ ) {
				string modName = tag.GetString( "TriggeredCutsceneMod_"+i );
				string name = tag.GetString( "TriggeredCutsceneName_"+i );
				var uid = new CutsceneID( modName, name );

				myworld.TriggeredCutsceneIDsForWorld.Add( uid );
			}
		}

		internal void SaveForWorld( AMLWorld myworld, TagCompound tag ) {
			int count = myworld.TriggeredCutsceneIDsForWorld.Count;
			tag["TriggeredCutscenesCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDsForWorld ) {
				tag["TriggeredCutsceneMod_" + i] = uid.ModName;
				tag["TriggeredCutsceneName_" + i] = uid.Name;
				i++;
			}
		}

		////

		internal void NetSendForWorld( AMLWorld myworld, BinaryWriter writer ) {
			writer.Write( (int)myworld.TriggeredCutsceneIDsForWorld.Count );

			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDsForWorld ) {
				writer.Write( uid.ModName );
				writer.Write( uid.Name );
			}
		}

		internal void NetReceiveForWorld( AMLWorld myworld, BinaryReader reader ) {
			myworld.TriggeredCutsceneIDsForWorld.Clear();

			int count = reader.ReadInt32();

			for( int i=0; i<count; i++ ) {
				string modName = reader.ReadString();
				string name = reader.ReadString();
				var uid = new CutsceneID( modName, name );

				myworld.TriggeredCutsceneIDsForWorld.Add( uid );
			}
		}


		////////////////

		internal void LoadForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			myplayer.TriggeredCutsceneIDsForPlayer.Clear();

			if( !tag.ContainsKey("TriggeredCutscenesCount") ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutscenesCount" );

			for( int i=0; i<count; i++ ) {
				string modName = tag.GetString( "TriggeredCutsceneMod_" + i );
				string name = tag.GetString( "TriggeredCutsceneName_" + i );
				var uid = new CutsceneID( modName, name );

				myplayer.TriggeredCutsceneIDsForPlayer.Add( uid );
			}
		}

		internal void SaveForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			int count = myplayer.TriggeredCutsceneIDsForPlayer.Count;
			tag["TriggeredCutscenesCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myplayer.TriggeredCutsceneIDsForPlayer ) {
				tag["TriggeredCutsceneMod_" + i] = uid.ModName;
				tag["TriggeredCutsceneName_" + i] = uid.Name;
				i++;
			}
		}
	}
}
