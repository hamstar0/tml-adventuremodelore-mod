using System;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Classes.Errors;
using AdventureModeLore.Cutscenes.Intro;


namespace AdventureModeLore.Cutscenes {
	public partial class CutsceneManager : ILoadable {
		internal void LoadForWorld( AMLWorld myworld, TagCompound tag ) {
			if( tag.GetBool("IntroCutsceneActivatedForWorld") ) {
				myworld.ActivatedCutscenes.Add( CutsceneID.Intro );
			}
		}

		internal void SaveForWorld( AMLWorld myworld, TagCompound tag ) {
			tag["IntroCutsceneActivatedForWorld"] = myworld.ActivatedCutscenes.Contains( CutsceneID.Intro );
		}

		////

		internal void NetSendForWorld( AMLWorld myworld, BinaryWriter writer ) {
			writer.Write( myworld.ActivatedCutscenes.Contains(CutsceneID.Intro) );
		}

		internal void NetReceiveForWorld( AMLWorld myworld, BinaryReader reader ) {
			if( reader.ReadBoolean() ) {
				myworld.ActivatedCutscenes.Add( CutsceneID.Intro );
			}
		}


		////////////////

		internal void LoadForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			if( tag.GetBool( "IntroCutsceneActivatedForPlayer" ) ) {
				myplayer.ActivatedCutscenes.Add( CutsceneID.Intro );
			}
		}

		internal void SaveForPlayer( AMLPlayer myplayer, TagCompound tag ) {
			tag["IntroCutsceneActivatedForPlayer"] = myplayer.ActivatedCutscenes.Contains( CutsceneID.Intro );
		}
	}
}
