﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace AdventureModeLore.Tiles {
	class FallenCyborgTile : ModTile {
		public override void SetDefaults() {
			Main.tileLighted[ this.Type ] = true;
			Main.tileFrameImportant[ this.Type ] = true;
			Main.tileNoAttach[ this.Type ] = true;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style2xX );
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.addTile( this.Type );

			ModTranslation name = this.CreateMapEntryName();
			name.SetDefault( "Fallen Cyborg" );

			this.AddMapEntry( new Color( 160, 160, 180 ), name );

			this.dustType = 1;
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { this.Type };
		}


		public override void NumDust( int i, int j, bool fail, ref int num ) {
			num = fail ? 1 : 3;
		}
	}
}