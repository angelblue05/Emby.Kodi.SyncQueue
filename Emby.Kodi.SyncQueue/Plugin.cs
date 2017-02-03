﻿using System;
using System.Collections.Generic;
using System.Reflection;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Serialization;
using Emby.Kodi.SyncQueue.Configuration;
using MediaBrowser.Model.Plugins;
using Emby.Kodi.SyncQueue.Data;
using System.IO;

namespace Emby.Kodi.SyncQueue
{
    class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public static ILogger Logger { get; set; }

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer, ILogger logger, IJsonSerializer json)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;

            Logger = logger;

            //Logger.Info("Emby.Kodi.SyncQueue: Write Out Reference if it Doesn't Exist!");

            //if (!File.Exists(Path.Combine(applicationPaths.ProgramSystemPath, "Emby.Kodi.NanoApi.dll")) ||
            //    !File.Exists(Path.Combine(applicationPaths.ProgramSystemPath, "Emby.Kodi.SyncJson.dll")))
            //{
            //    var names = Assembly.GetEntryAssembly().GetManifestResourceNames();
            //    foreach (var name in names)
            //    {
            //        Logger.Info("Emby.Kodi.SyncQueue: " + name);
            //    }
            //    using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream("Emby.Kodi.SyncQueue.Resources.Emby.Kodi.NanoApi.dll"))
            //    using (Stream output = File.Create(Path.Combine(applicationPaths.ProgramSystemPath, "NanoApi.JsonFile.dll")))
            //    {
            //        CopyStream(input, output);
            //    }

            //    using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream("Emby.Kodi.SyncQueue.Resources.Emby.Kodi.SyncJson.dll"))
            //    using (Stream output = File.Create(Path.Combine(applicationPaths.ProgramSystemPath, "NanoApi.JsonFile.dll")))
            //    {
            //        CopyStream(input, output);
            //    }
            //}

            Logger.Info("Emby.Kodi.SyncQueue IS NOW STARTING!!!");

            DbRepo.dbPath = applicationPaths.DataPath;
            DbRepo.json = json;
            DbRepo.logger = logger;
            DbRepo.Instance.Initialize();
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// Gets the name of the plugin
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get { return "Emby.Kodi Sync Queue"; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description
        {
            get
            {
                return "Allows for shorter Sync Times on Kodi Startup";
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new PluginPageInfo[]
            {
                new PluginPageInfo
                {
                    Name = "Emby.Kodi.SyncQueue",
                    EmbeddedResourcePath = "Emby.Kodi.SyncQueue.Configuration.configPage.html"
                }
            };
        }
    }
}
