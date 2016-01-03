using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace AlexBazile.MvvmCross.Plugin.TextToSpeech.WindowsCommon
{
    public class Plugin
        : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterType<ITextToSpeech, WindowsCommonTextToSpeech>();
        }
    }
}
