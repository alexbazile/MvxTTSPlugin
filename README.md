# MvxTTSPlugin

Fully asynchronous version of Text-to-speech as an MVVMCross Plugin.
#V1.

Contains full implementation for Windows phone 8.1 (runtime), Windows Store and UWP

#usage
```csharp
Mvx.RegisterSingleton<ITextToSpeech>(new WindowsCommonTextToSpeech());

ITextToSpeech tts = Mvx.Resolve<ITextToSpeech>();

await tts.SpeakAsync("Text Goes Here")
```
