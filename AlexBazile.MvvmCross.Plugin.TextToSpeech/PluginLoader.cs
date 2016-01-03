﻿using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace AlexBazile.MvvmCross.Plugin.TextToSpeech
{
    public class PluginLoader
        : IMvxPluginLoader
    {
        public static readonly PluginLoader Instance = new PluginLoader();

        public void EnsureLoaded()
        {
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
    }
}

