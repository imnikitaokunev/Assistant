using Microsoft.CognitiveServices.Speech;

namespace Assistant.Models.Speaker
{
    public interface ISpeakable
    {
        bool Initialize(SpeechConfig config);
        void Speak(string text);
    }
}
