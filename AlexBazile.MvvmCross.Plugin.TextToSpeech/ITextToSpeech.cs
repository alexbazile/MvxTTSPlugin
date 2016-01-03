using AlexBazile.MvvmCross.Plugin.TextToSpeech.WindowsCommon;
using System;
using System.Threading.Tasks;

namespace AlexBazile.MvvmCross.Plugin.TextToSpeech
{
    public interface ITextToSpeech
    {
        /// <summary>
        ///  Get The Plugin Initialized status
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// Gets or Set the Speech Locale
        /// </summary>
        SpeechLocale Locale { get; set; }

        /// <summary>
        /// Initialize Method
        /// </summary>
        void Initialize();

        /// <summary>
        /// Release method - used to tear down the plugin
        /// </summary>
        void Release();

        /// <summary>
        /// Event Handler when initialization is completed
        /// </summary>
        event EventHandler InitializeCompleted;

        /// <summary>
        /// Speaks in an asynchronous way
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task SpeakAsync(string text, bool queue, CrossLocale? crossLocale, float? pitch, float? speechRate, float? volum);
    }

    /// <summary>
    /// Defines the speech Locale, platform agnostic
    /// </summary>
    public class SpeechLocale
    {
        public static string English = "en";
        public static string French = "fr";
    }
}

