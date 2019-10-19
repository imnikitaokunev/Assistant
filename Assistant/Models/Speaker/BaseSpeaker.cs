using Microsoft.CognitiveServices.Speech;

namespace Assistant.Models.Speaker
{
    public abstract class BaseSpeaker : ISpeakable
    {
        private bool _isInitialized;

        public bool Initialize(SpeechConfig config) => _isInitialized || (_isInitialized = InternalInitialize(config));

        public void Speak(string text)
        {
            if (!_isInitialized || string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            InternalSpeak(text);
        }

        protected abstract bool InternalInitialize(SpeechConfig config);

        protected abstract void InternalSpeak(string text);
    }
}
