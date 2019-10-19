using System;
using Microsoft.CognitiveServices.Speech;

namespace Assistant.Models.Recognizer
{
    public interface IRecognizable : IDisposable
    {
        bool Initialize(SpeechConfig config, string phrase);
        void StartRecognize();
        void StopRecognize();
    }
}   