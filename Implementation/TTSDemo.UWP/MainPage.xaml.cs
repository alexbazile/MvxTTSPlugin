using AlexBazile.MvvmCross.Plugin.TextToSpeech;
using AlexBazile.MvvmCross.Plugin.TextToSpeech.WindowsCommon;
using Cirrious.CrossCore;
using TTSDemo.UWP.Base;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TTSDemo.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : WindowsBasePage
    {
        WindowsCommonTextToSpeech tts;
        public MainPage()
        {
            this.InitializeComponent();

            //Get a reference to the Singleton Instance of the tts for UWP
            tts = Mvx.Resolve<ITextToSpeech>() as WindowsCommonTextToSpeech;

            //Initialize the tt plugin Media Element
            tts.MediaElement = new MediaElement();

            //Add the Media Element to the XAML Tree
            LayoutRoot.Children.Add(tts.MediaElement);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string text = textBox.Text;

            if (!string.IsNullOrEmpty(text))
            {
                await tts.SpeakAsync(text);
            }
        }
    }
}
