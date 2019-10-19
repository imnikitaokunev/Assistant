using Microsoft.CognitiveServices.Speech;

namespace Assistant.Models.Speaker
{
    public class Speaker : BaseSpeaker
    {
        private SpeechSynthesizer _synthesizer;

        protected override bool InternalInitialize(SpeechConfig config)
        {
            if (config == null)
            {
                return false;
            }

            _synthesizer = new SpeechSynthesizer(config);
            return true;
        }

        protected override void InternalSpeak(string text)
        {
            _synthesizer.SpeakTextAsync(text);
        }
    }
}