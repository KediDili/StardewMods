using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using StardewValley;
using StardewValley.Network;
using StardewValley.Objects;

namespace Leclair.Stardew.Common.Inventory
{
	public class StorageFurnitureProvider : BaseInventoryProvider<StorageFurniture> {
		public override bool CanExtractItems(StorageFurniture obj, GameLocation location, Farmer who) {
			return true;
		}

		public override bool CanInsertItems(StorageFurniture obj, GameLocation location, Farmer who) {
			return true;
		}

		public override void CleanInventory(StorageFurniture obj, GameLocation location, Farmer who) {
			obj.ClearNulls();
		}

		public override int GetActualCapacity(StorageFurniture obj, GameLocation location, Farmer who) {
			return 36;
		}

		public override IList<Item> GetItems(StorageFurniture obj, GameLocation location, Farmer who) {
			// TODO: Implement a managed item list that does the stuff storage
			// furniture does when accessing items.
			return obj.heldItems;
		}

		public override Rectangle? GetMultiTileRegion(StorageFurniture obj, GameLocation location, Farmer who) {
			// TODO: Implement this
			return null;
		}

		public override NetMutex GetMutex(StorageFurniture obj, GameLocation location, Farmer who) {
			return obj.mutex;
		}

		public override Vector2? GetTilePosition(StorageFurniture obj, GameLocation location, Farmer who) {
			return obj.TileLocation;
		}

		public override bool IsValid(StorageFurniture obj, GameLocation location, Farmer who) {
			return true;
		}
	}
}
