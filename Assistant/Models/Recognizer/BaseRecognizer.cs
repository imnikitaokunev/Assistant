using Microsoft.CognitiveServices.Speech;

namespace Assistant.Models.Recognizer
{
    public abstract class BaseRecognizer : IRecognizable
    {
        private bool _isInitialized;

        public bool Initialize(SpeechConfig config, string phrase) => _isInitialized || (_isInitialized = InternalInitialize(config, phrase));

        public void StartRecognize()
        {
            if (!_isInitialized)
            {
                return;
            }

            InternalStartRecognize();
        }

        public void StopRecognize()
        {
            if (!_isInitialized)
            {
                return;
            }

            InternalStopRecognize();
        }

        public abstract void Dispose();

        protected abstract bool InternalInitialize(SpeechConfig config, string phrase);

        protected abstract void InternalStartRecognize();

        protected abstract void InternalStopRecognize();
    }
}
