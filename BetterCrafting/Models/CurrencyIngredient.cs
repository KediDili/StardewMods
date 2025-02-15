using System.Collections.Generic;

using Leclair.Stardew.Common;
using Leclair.Stardew.Common.Crafting;
using Leclair.Stardew.Common.Inventory;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley;

using SObject = StardewValley.Object;

namespace Leclair.Stardew.BetterCrafting.Models;

public enum CurrencyType {
	Money,
	FestivalPoints,
	ClubCoins,
	QiGems
};

public class CurrencyIngredient : IIngredient {

	public readonly CurrencyType Type;

	public bool SupportsQuality => true;

	public CurrencyIngredient(CurrencyType type, int quantity) {
		Type = type;
		Quantity = quantity;
	}

	public string DisplayName {
		get {
			switch (Type) {
				case CurrencyType.Money:
					return "Gold";
				case CurrencyType.FestivalPoints:
					return "Points";
				case CurrencyType.ClubCoins:
					return "Club Coins";
				case CurrencyType.QiGems:
					return "Qi Gems";
				default:
					return "???";
			}
		}
	}

	public Texture2D Texture => Game1.mouseCursors;
	public Rectangle SourceRectangle {
		get {
			switch(Type) {
				case CurrencyType.Money:
					return new Rectangle(193, 373, 9, 10);
				case CurrencyType.FestivalPoints:
					return new Rectangle(202, 373, 9, 10);
				case CurrencyType.ClubCoins:
					return new Rectangle(211, 373, 9, 10);
			}

			return Rectangle.Empty;
		}
	}

	public int Quantity { get; }

	public void Consume(Farmer who, IList<IInventory> inventories, int max_quality, bool low_quality_first) {
		switch (Type) {
			case CurrencyType.Money:
				who.Money -= Quantity;
				break;
			case CurrencyType.FestivalPoints:
				who.festivalScore -= Quantity;
				break;
			case CurrencyType.ClubCoins:
				who.clubCoins -= Quantity;
				break;
			case CurrencyType.QiGems:
				who.QiGems -= Quantity;
				break;
		}
	}

	public int GetAvailableQuantity(Farmer who, IList<Item> items, IList<IInventory> inventories, int max_quality) {
		switch (Type) {
			case CurrencyType.Money:
				return who.Money;
			case CurrencyType.FestivalPoints:
				return who.festivalScore;
			case CurrencyType.ClubCoins:
				return who.clubCoins;
			case CurrencyType.QiGems:
				return who.QiGems;
		}

		return 0;
	}
}
