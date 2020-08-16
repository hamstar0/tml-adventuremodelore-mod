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
		internal void Load_World( AMLWorld myworld, TagCompound tag ) {
			myworld.TriggeredCutsceneIDs_World.Clear();
			myworld.CurrentPlayingCutscenes_World.Clear();

			if( !tag.ContainsKey( "TriggeredCutscenesCount" ) ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutscenesCount" );

			for( int i = 0; i < count; i++ ) {
				string modName = tag.GetString( "TriggeredCutsceneMod_"+i );
				string name = tag.GetString( "TriggeredCutsceneName_"+i );
				var uid = new CutsceneID( modName, name );

				myworld.TriggeredCutsceneIDs_World.Add( uid );
			}
		}

		internal void Save_World( AMLWorld myworld, TagCompound tag ) {
			int count = myworld.TriggeredCutsceneIDs_World.Count;
			tag["TriggeredCutscenesCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDs_World ) {
				tag["TriggeredCutsceneMod_" + i] = uid.ModName;
				tag["TriggeredCutsceneName_" + i] = uid.Name;
				i++;
			}
		}

		////

		internal void NetSend_World( AMLWorld myworld, BinaryWriter writer ) {
			writer.Write( (int)myworld.TriggeredCutsceneIDs_World.Count );

			foreach( CutsceneID uid in myworld.TriggeredCutsceneIDs_World ) {
				writer.Write( uid.ModName );
				writer.Write( uid.Name );
			}
		}

		internal void NetReceive_World( AMLWorld myworld, BinaryReader reader ) {
			myworld.TriggeredCutsceneIDs_World.Clear();

			int count = reader.ReadInt32();

			for( int i=0; i<count; i++ ) {
				string modName = reader.ReadString();
				string name = reader.ReadString();
				var uid = new CutsceneID( modName, name );

				myworld.TriggeredCutsceneIDs_World.Add( uid );
			}
		}


		////////////////

		internal void Load_Player( AMLPlayer myplayer, TagCompound tag ) {
			myplayer.TriggeredCutsceneIDs_Player.Clear();
			myplayer.CurrentPlayingCutscene_Player = null;

			if( !tag.ContainsKey("TriggeredCutscenesCount") ) {
				return;
			}
			int count = tag.GetInt( "TriggeredCutscenesCount" );

			for( int i=0; i<count; i++ ) {
				string modName = tag.GetString( "TriggeredCutsceneMod_" + i );
				string name = tag.GetString( "TriggeredCutsceneName_" + i );
				var uid = new CutsceneID( modName, name );

				myplayer.TriggeredCutsceneIDs_Player.Add( uid );
			}
		}

		internal void Save_Player( AMLPlayer myplayer, TagCompound tag ) {
			int count = myplayer.TriggeredCutsceneIDs_Player.Count;
			tag["TriggeredCutscenesCount"] = count;

			int i = 0;
			foreach( CutsceneID uid in myplayer.TriggeredCutsceneIDs_Player ) {
				tag["TriggeredCutsceneMod_" + i] = uid.ModName;
				tag["TriggeredCutsceneName_" + i] = uid.Name;
				i++;
			}
		}
	}
}
