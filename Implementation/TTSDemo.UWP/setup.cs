using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSDemo.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TTSDemo.UWP
{
    public class Setup : MvxWindowsSetup
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            //var builder = new MvxWindowsBindingBuilder();
            //builder.DoRegistration();
        }

        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new UAP.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeViewLookup()
        {
            var mainViewModelLookup = new Dictionary<Type, Type>
                {
                    {typeof(MainViewModel), typeof(MainPage)}
                };

            var container = Mvx.Resolve<IMvxViewsContainer>();
            container.AddAll(mainViewModelLookup);
        }
    }
}
