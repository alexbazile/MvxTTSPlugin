using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.WindowsCommon.Views;
using Windows.UI.Xaml;
//using Cirrious.MvvmCross.Binding.BindingContext;

namespace TTSDemo.UWP.Base
{
    public class WindowsBasePage : MvxWindowsPage, IMvxView//, IMvxBindingContextOwner
    {
        public string Title { get; set; }

        public IMvxViewModel ViewModel
        {
            get; set;
        }

        public object DataContext
        {
            get; set;
        }

        //public IMvxBindingContext BindingContext
        //{
        //    get;set;
        //}

        //public WindowsBasePage()
        //{
        //    BindingContext = new MvxBindingContext();

        //}

        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

    }
}
