using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace Assistant
{
    public class TextToSpeech : ISpeakable
    {
        public delegate void SpeechRecognizedHandler(object sender, string text);
        public event SpeechRecognizedHandler SpeechRecognized;
        public Configuration Config { get; private set; }
        public string LogFileName { get; private set; }

        public TextToSpeech(Configuration configuration, string logFileName)
        {
            Config = configuration;
            LogFileName = logFileName;
        }

        public async void Recognize()
        {
            while (Config.VoiceControl)
            {
                await RecognizeAsync();
            }
        }

        public void Speak(string text)
        {
            SpeakAsync(text);
        }

        private async Task SpeakAsync(string text)
        {
            using (var synthesizer = new SpeechSynthesizer(Config.Config))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Log.Save(LogFileName, $"{DateTime.Now}\n" + $"< {text}");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Log.Save(LogFileName, $"{DateTime.Now}\n" + $"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Log.Save(LogFileName, $"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Log.Save(LogFileName, $"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        }
                    }
                }
            }
        }

        private async Task RecognizeAsync()
        {
            using (var recognizer = new SpeechRecognizer(Config.Config))
            {
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    if (result.Text.ToLower().Contains(Config.Phrase.ToLower()))
                        RecognizeSpeech(result.Text);
                    Log.Save(LogFileName, $"{DateTime.Now}\n" + $"> {result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Log.Save(LogFileName, $"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Log.Save(LogFileName, $"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Log.Save(LogFileName, $"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Log.Save(LogFileName, $"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    }
                }
            }
        }

        private void CallEvent(string text, SpeechRecognizedHandler handler)
        {
            if (text != null)
                handler?.Invoke(this, text);
        }

        private void OnSpeechRecognized(string text)
        {
            CallEvent(text, SpeechRecognized);
        }

        public void RecognizeSpeech(string text)
        {
            OnSpeechRecognized(text);
        }
    }
}