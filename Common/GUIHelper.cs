using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StardewValley;
using StardewValley.Menus;

namespace Leclair.Stardew.Common {
	public static class GUIHelper {

		public enum Side {
			Up,
			Down,
			Left,
			Right
		}


		public static float GetLayerDepth(float yPos) {
			return yPos / 10000f;
		}

		public static void LinkComponents(Side side, Func<int, ClickableComponent> getComponent, params ClickableComponent[] components) {
			ClickableComponent last = null;

			// Example:
			// - trashCan
			// - btnInsert
			// - btnExtract

			// Arg Pairs
			// [0] last: trashCan, cmp: btnInsert
			// [1] last: btnInsert, cmp: btnExtract

			foreach (ClickableComponent cmp in components) {
				if (cmp == null)
					continue;

				if (last != null) {
					int myID = cmp.myID;
					int lastID = last.myID;

					// TODO: Re-link existing neighbors?

					switch (side) {
						case Side.Up:
							cmp.downNeighborID = lastID;
							last.upNeighborID = myID;
							break;
						case Side.Down:
							last.downNeighborID = myID;
							cmp.upNeighborID = lastID;
							break;
						case Side.Left:
							cmp.rightNeighborID = lastID;
							last.leftNeighborID = myID;
							break;
						case Side.Right:
							last.rightNeighborID = myID;
							cmp.leftNeighborID = lastID;
							break;
						default:
							return;
					}
				}

				last = cmp;
			}
		}

		public static void MoveComponents(Side side, int spacing = -1, params ClickableComponent[] components) {
			if (spacing < 0)
				spacing = IClickableMenu.borderWidth;

			ClickableComponent last = null;

			// Example:
			// - trashCan
			// - btnInsert
			// - btnExtract

			// Arg Pairs
			// [0] last: trashCan, cmp: btnInsert
			// [1] last: btnInsert, cmp: btnExtract

			foreach (ClickableComponent cmp in components) {
				if (cmp == null)
					continue;

				if (last != null) {
					int x = last.bounds.X, y = last.bounds.Y;

					switch (side) {
						case Side.Up:
							y = y - spacing - cmp.bounds.Height;
							break;
						case Side.Down:
							y = y + last.bounds.Height + spacing;
							break;
						case Side.Left:
							x = x - spacing - cmp.bounds.Width;
							break;
						case Side.Right:
							x = x + last.bounds.Width + spacing;
							break;
						default:
							return;
					}

					if (x != cmp.bounds.X || y != cmp.bounds.Y)
						cmp.bounds = new Rectangle(x, y, cmp.bounds.Width, cmp.bounds.Height);
				}

				last = cmp;
			}
		}

	}
}
