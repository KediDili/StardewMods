using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

using StardewValley;
using StardewValley.Network;
using StardewValley.Objects;

namespace Leclair.Stardew.Common.Inventory {
	public class ChestProvider : BaseInventoryProvider<Chest> {

		public readonly Chest.SpecialChestTypes[] AllowedTypes;

		public ChestProvider() {
			AllowedTypes = new[] {
				Chest.SpecialChestTypes.AutoLoader,
				Chest.SpecialChestTypes.None
			};
		}

		public ChestProvider(IEnumerable<Chest.SpecialChestTypes> types) {
			AllowedTypes = types.ToArray();
		}

		public ChestProvider(bool any) {
			AllowedTypes = any ? CommonHelper.GetValues<Chest.SpecialChestTypes>().ToArray() : new Chest.SpecialChestTypes[0];
		}


		public override bool CanExtractItems(Chest obj, GameLocation location, Farmer who) => IsValid(obj, location, who);

		public override bool CanInsertItems(Chest obj, GameLocation location, Farmer who) => IsValid(obj, location, who);

		public override void CleanInventory(Chest obj, GameLocation location, Farmer who) {
			obj.clearNulls();
		}

		public override int GetActualCapacity(Chest obj, GameLocation location, Farmer who) {
			return obj.GetActualCapacity();
		}

		public override IList<Item> GetItems(Chest obj, GameLocation location, Farmer who) {
			if (who == null)
				return obj.items;

			return obj.GetItemsForPlayer(who.UniqueMultiplayerID);
		}

		public override Rectangle? GetMultiTileRegion(Chest obj, GameLocation location, Farmer who) {
			return null;
		}

		public override NetMutex GetMutex(Chest obj, GameLocation location, Farmer who) {
			return obj.GetMutex();
		}

		public override Vector2? GetTilePosition(Chest obj, GameLocation location, Farmer who) {
			return TileHelper.GetRealPosition(obj, location);
		}

		public override bool IsValid(Chest obj, GameLocation location, Farmer who) {
			if (location != null) {
				Vector2? pos = GetTilePosition(obj, location, who);
				if (!pos.HasValue)
					return false;

				if (!TileHelper.GetObjectAtPosition(location, pos.Value, out var sobj) || sobj != obj)
					return false;
			}

			return AllowedTypes.Contains(obj.SpecialChestType);
		}
	}
}
