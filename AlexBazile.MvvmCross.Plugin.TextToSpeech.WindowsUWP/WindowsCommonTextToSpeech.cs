using Nito.AsyncEx;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace AlexBazile.MvvmCross.Plugin.TextToSpeech.WindowsCommon
{
    public class WindowsCommonTextToSpeech : ITextToSpeech, IDisposable
    {

        private SpeechSynthesizer _speechSynthesizer;
        private TaskCompletionSource<object> _tcs;
        private readonly AsyncLock _mutex = new AsyncLock();
        private Task<IDisposable> _lockAsync;
        public event EventHandler InitializeCompleted;

        MediaElement _element;
        public MediaElement MediaElement
        {
            get { return _element; }
            set { _element = value; }
        }

        public bool Initialized
        {
            get { return _speechSynthesizer != null; }
            
        }

        public SpeechLocale Locale
        {
            get;
            set;
        }


        /// <summary>
        /// Initialization
        /// </summary>
        public void Init()
        {
            if (_speechSynthesizer == null)
                _speechSynthesizer = new SpeechSynthesizer();

            if (_element == null)
            {
                _element = new MediaElement();
            }

            if(_tcs == null)
            {
                _tcs = new TaskCompletionSource<object>();
            }

            if (InitializeCompleted != null)
            {
                InitializeCompleted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Speak back text
        /// </summary>
        /// <param name="text">Text to speak</param>
        /// <param name="queue">If you want to chain together speak command or cancel current</param>
        /// <param name="crossLocale">Locale of voice</param>
        /// <param name="pitch">Pitch of voice</param>
        /// <param name="speechRate">Speak Rate of voice (All) (0.0 - 2.0f)</param>
        /// <param name="volume">Volume of voice (iOS/WP) (0.0-1.0)</param>
        public async Task SpeakAsync(string text, bool queue= false, CrossLocale? crossLocale = null, float? pitch = null , float? speechRate = null, float? volume= null)
        {
            if(!queue)
            { 
                if(_lockAsync != null)
                {
                    (await _lockAsync).Dispose();
                }
            }
            _lockAsync = _mutex.LockAsync();
            using (await _lockAsync)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return;

                if (_speechSynthesizer == null)
                {
                    Init();
                }
                var localCode = string.Empty;

                //nothing fancy needed here
                if (pitch == null && speechRate == null && volume == null)
                {
                    if (crossLocale.HasValue && !string.IsNullOrWhiteSpace(crossLocale.Value.Language))
                    {
                        localCode = crossLocale.Value.Language;
                        var voices = from voice in SpeechSynthesizer.AllVoices
                                     where (voice.Language == localCode
                                     && voice.Gender.Equals(VoiceGender.Female))
                                     select voice;
                        _speechSynthesizer.Voice = (voices.Any() ? voices.ElementAt(0) : SpeechSynthesizer.DefaultVoice);
                        _element.Language = crossLocale.Value.Language;
                    }
                    else
                    {
                        _speechSynthesizer.Voice = SpeechSynthesizer.DefaultVoice;
                    }
                }

                else
                {
                    
                    _element.PlaybackRate = speechRate.Value;
                    _element.Volume = volume.Value;
                }

                try
                {
                    var stream = await _speechSynthesizer.SynthesizeTextToStreamAsync(text);

                    _element.SetSource(stream, stream.ContentType);
                    _element.MediaEnded += (s, e) => _tcs.TrySetResult(null);
                    _element.MediaFailed += (s, e) => _tcs.TrySetException(new Exception(e.ErrorMessage));


                    _element.Play();

                    await _tcs.Task;
                    _tcs = new TaskCompletionSource<object>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to playback stream: " + ex);
                    _tcs.TrySetException(new Exception(ex.Message));
                }
            }
        }

        /// <summary>
        /// Get all installed and valid languages
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.IEnumerable<CrossLocale> GetInstalledLanguages()
        {
            return SpeechSynthesizer.AllVoices
              .OrderBy(a => a.Language)
              .Select(a => new CrossLocale { Language = a.Language, DisplayName = a.DisplayName })
              .GroupBy(c => c.ToString())
              .Select(g => g.First());
        }

        /// <summary>
        /// Dispose of TTS
        /// </summary>
        public void Dispose()
        {
            Release();
        }

        public void Initialize()
        {
            Init();
        }

        public void Release()
        {
            if (_speechSynthesizer != null)
                _speechSynthesizer = null;

            if (_element != null)
            {
                _element = null;
            }

            if (_tcs != null)
            {
                _tcs = null;
            }
        }
    }
}
