using System;
using System.Collections.Generic;
using System.Reflection;

using StardewModdingAPI;

using Leclair.Stardew.Common.Types;

namespace Leclair.Stardew.Common.Events {
	public abstract class ModSubscriber : Mod {

		private Dictionary<MethodInfo, RegisteredEvent> Events;

		public override void Entry(IModHelper helper) {
			RegisterEvents();
		}

		public virtual void Log(string message, LogLevel level = LogLevel.Debug, Exception ex = null, LogLevel? exLevel = null) {
			Monitor.Log(message, level: level);
			if (ex != null)
				Monitor.Log($"Details:\n{ex}", level: exLevel ?? level);
		}

		protected override void Dispose(bool disposing) {
			base.Dispose(disposing);
			UnregisterEvents();
		}

		public void RegisterEvents(Action<string, LogLevel> logger = null) {
			Events = EventHelper.RegisterEvents(this, Helper.Events, Events, logger);
		}

		public void UnregisterEvents() {
			if (Events == null)
				return;

			EventHelper.UnregisterEvents(Events);
			Events = null;
		}


		public void CheckRecommendedIntegrations() {
			// Missing Integrations?
			RecommendedIntegration[] integrations;

			try {
				integrations = Helper.Data.ReadJsonFile<RecommendedIntegration[]>("assets/recommended_integrations.json");
				if (integrations == null) {
					Log("No recommendations found. Our data file seems to be missing.");
					return;
				}
			} catch (Exception ex) {
				Log($"Unable to load recommended integrations data file.", LogLevel.Warn, ex);
				return;
			}

			LoadingHelper.CheckIntegrations(this, integrations);
		}

	}
}
