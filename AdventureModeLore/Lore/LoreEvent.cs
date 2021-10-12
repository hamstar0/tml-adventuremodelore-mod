using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace AdventureModeLore.Lore {
	public abstract partial class LoreEvent {
		public string Name { get; private set; }

		public Func<bool>[] Prerequisites { get; private set; }

		public bool IsRepeatable { get; private set; }



		////////////////

		public LoreEvent(
					string name,
					Func<bool>[] prereqs,
					bool isRepeatable ) {
			this.Name = name;
			this.Prerequisites = prereqs;
			this.IsRepeatable = isRepeatable;
		}

		////

		internal abstract void Initialize();


		////////////////

		public bool ArePrerequisitesMet() {
			foreach( Func<bool> prereq in this.Prerequisites ) {
				if( !prereq.Invoke() ) {
					return false;
				}
			}

			return true;
		}
	}
}
