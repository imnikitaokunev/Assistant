using System;
using System.Collections.Generic;
using Microsoft.CognitiveServices.Speech;
using NLog;

namespace Assistant.Models.Recognizer
{
    public class Recognizer : BaseRecognizer, IObservable<string>
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IList<IObserver<string>> _observers = new List<IObserver<string>>();
        private SpeechRecognizer _recognizer;
        private string _phrase;

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public override void Dispose()
        {
            _recognizer.Dispose();
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }

        protected override bool InternalInitialize(SpeechConfig config, string phrase)
        {
            if (config == null || phrase == null)
            {
                return false;
            }

            _recognizer = new SpeechRecognizer(config);
            _recognizer.Recognized += OnRecognized;
            _phrase = phrase;
            return true;
        }

        protected override void InternalStartRecognize()
        {
            _recognizer.StartContinuousRecognitionAsync();
        }

        protected override void InternalStopRecognize()
        {
            _recognizer.StopContinuousRecognitionAsync();
        }

        private void OnRecognized(object sender, SpeechRecognitionEventArgs a)
        {
            _logger.Info(a.Result.Text);

            if (!a.Result.Text.ToUpper().Contains(_phrase.ToUpper()))
            {
                return;
            }

            Notify(a.Result.Text);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly IObserver<string> _investor;
            private readonly IList<IObserver<string>> _investors;

            public Unsubscriber(IList<IObserver<string>> investors, IObserver<string> investor)
            {
                _investors = investors;
                _investor = investor;
            }

            public void Dispose()
            {
                _investors.Remove(_investor);
            }
        }
    }
}