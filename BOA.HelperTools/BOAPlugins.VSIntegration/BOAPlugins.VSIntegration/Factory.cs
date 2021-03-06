﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BOA.Common.Helpers;
using WhiteStone;
using WhiteStone.Services;

namespace BOAPlugins.VSIntegration
{
    class Factory
    {
        #region Static Fields
        static bool _isInitialized;
        #endregion

        #region Properties
        internal static Configuration Configuration => SM.Get<Configuration>();

        internal static string      WhiteStoneBinDirectory => Configuration.PluginDirectory + "bin" + Path.DirectorySeparatorChar;
        static          ISerializer Serializer             => SM.Get<ISerializer>() ?? SM.Set<ISerializer>(new JsonSerializer());
        #endregion

        #region Public Methods
        public static ICommunication GetCommunication(IVisualStudioLayer vs)
        {
            return new Communication(vs);
        }

        public static void InitializeApplicationServices()
        {
            if (_isInitialized)
            {
                Log.Push("Already initialized.");
                return;
            }

            Log.Push("Started to initialize.");

            _isInitialized = true;

            if (Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.AddAssemblySearchDirectory(@"D:\work\BOA.Retired\Dev\BOA.Kernel.DataAccess\BOAPlugins.VSIntegration\bin\Debug\");
            }
            else
            {
                AppDomain.CurrentDomain.AddAssemblySearchDirectory(Configuration.PluginDirectory);
                AppDomain.CurrentDomain.AddAssemblySearchDirectory(WhiteStoneBinDirectory);
            }

            Configuration.LoadFromFile();

            Task.Run(() => Configuration.CheckDeepEndsDownloaded());
        }
        #endregion
    }
}