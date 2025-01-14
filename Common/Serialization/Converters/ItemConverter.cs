using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using StardewValley;
using SObject = StardewValley.Object;

namespace Leclair.Stardew.Common.Serialization.Converters
{
	public class ItemConverter : JsonConverter {
		public override bool CanConvert(Type objectType) {
			// This will get easier in 1.6. For now we only care about SObjects.
			return typeof(SObject).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			switch (reader.TokenType) {
				case JsonToken.Null:
					return null;
				case JsonToken.StartObject:
					return ReadObject(JObject.Load(reader));
				default:
					throw new JsonReaderException($"Can't parse Item from {reader.TokenType} node (path: {reader.Path}).");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			if (value is not SObject sobj) {
				writer.WriteNull();
				return;
			}

			var jo = new JObject {
				{"Id", sobj.ParentSheetIndex},
				{"Type", sobj.bigCraftable.Value ? "BigObject" : "Object"}
			};

			jo.WriteTo(writer);
		}

		private static Item ReadObject(JObject obj) {
			string id = obj.ValueIgnoreCase<string>("Id");
			string type = obj.ValueIgnoreCase<string>("Type");

			int nid = Convert.ToInt32(id);

			switch(type) {
				case "BigObject":
					return new SObject(Vector2.Zero, nid);

				case "Object":
					return new SObject(nid, 1);
			}

			return null;
		}
	}
}
