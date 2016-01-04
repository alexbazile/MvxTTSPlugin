using AlexBazile.MvvmCross.Plugin.TextToSpeech;
using AlexBazile.MvvmCross.Plugin.TextToSpeech.WindowsCommon;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using TTSDemo.UWP.ViewModels;

namespace TTSDemo.UWP.UAP
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterSingleton<ITextToSpeech>(new WindowsCommonTextToSpeech());
            // Services
            CreatableTypes().EndingWith("Service").AsInterfaces().RegisterAsLazySingleton();

            // Singleton services
            CreatableTypes().EndingWith("ServiceSingleton").AsInterfaces().RegisterAsLazySingleton();

            // Set the start point
            RegisterAppStart<MainViewModel>();
        }
    }
}
