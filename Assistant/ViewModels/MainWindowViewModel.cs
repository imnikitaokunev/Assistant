using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using Assistant.Annotations;
using Assistant.Data;
using Assistant.Models.Recognizer;
using Assistant.Models.Speaker;
using Assistant.Services;
using Microsoft.CognitiveServices.Speech;

namespace Assistant.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IObserver<string>
    {
        private readonly Random _random;
        private readonly ISpeakable _speaker;
        private readonly IRecognizable _recognizer;
        private readonly IDialogService _dialogService;
        private readonly IContext _context;
        private Command _speakSelectedCommand;
        private Command _speakRandomCommand;
        private Command _openCommand;
        private Command _saveCommand;
        private Command _enableVoiceControlCommand;
        private string _selectedPhrase;
        private bool _isVoiceControlEnabled;
        private string _voiceControl = "Enable voice control";

        public ObservableCollection<string> Phrases { get; set; }

        public string SelectedPhrase
        {
            get => _selectedPhrase;
            set
            {
                _selectedPhrase = value;
                OnPropertyChanged(nameof(SelectedPhrase));
            }
        }

        public string VoiceControl
        {
            get => _voiceControl;
            set
            {
                _voiceControl = value;
                OnPropertyChanged(nameof(VoiceControl));
            }
        }

        public Command SpeakSelectedCommand => _speakSelectedCommand ?? (_speakSelectedCommand = new Command(obj =>
        {
            if (SelectedPhrase == null)
            {
                return;
            }

            _speaker.Speak(SelectedPhrase);
        }, obj => SelectedPhrase != null));

        public Command SpeakRandomCommand => _speakRandomCommand ?? (_speakRandomCommand = new Command(obj =>
        {
            var index = _random.Next(Phrases.Count);
            SelectedPhrase = Phrases[index];
            _speaker.Speak(SelectedPhrase);
        }, obj => Phrases.Count > 0));

        public Command OpenCommand => _openCommand ?? (_openCommand = new Command(obj =>
        {
            try
            {
                if (_dialogService.OpenFileDialog("TXT (*.txt)|*.txt"))
                {
                    Phrases.Clear();
                    foreach (var phrase in _context.Open(_dialogService.FilePath))
                    {
                        Phrases.Add(phrase);
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }));

        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(obj =>
        {
            try
            {
                if (_dialogService.SaveFileDialog("TXT (*.txt)|*.txt"))
                {
                    _context.Save(Phrases, _dialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }));

        public Command EnableVoiceControlCommand => _enableVoiceControlCommand ?? (_enableVoiceControlCommand = new Command(obj =>
        {
            if (_isVoiceControlEnabled)
            {
                _isVoiceControlEnabled = false;
                _recognizer.StopRecognize();
                VoiceControl = "Enable voice control";
            }
            else
            {
                _isVoiceControlEnabled = true;
                _recognizer.StartRecognize();
                VoiceControl = "Disable voice control";
            }
        }));

        public MainWindowViewModel()
        {
            var subscriptionKey = ConfigurationManager.AppSettings["subscriptionKey"];
            var region = ConfigurationManager.AppSettings["region"];
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);
            config.SpeechSynthesisLanguage = ConfigurationManager.AppSettings["synthesisLanguage"];
            config.SpeechRecognitionLanguage = ConfigurationManager.AppSettings["recognitionLanguage"];
            var phrase = ConfigurationManager.AppSettings["defaultPhrase"];

            _speaker = new Speaker();
            _speaker.Initialize(config);

            _recognizer = new Models.Recognizer.Recognizer();
            _recognizer.Initialize(config, phrase);
            ((IObservable<string>) _recognizer).Subscribe(this);

            _random = new Random();
            _dialogService = new DialogService();

            _context = new TxtContext();
            Phrases = new ObservableCollection<string>();

            OpenCommand.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNext(string value)
        {
            SpeakRandomCommand.Execute(Phrases.Count > 0);
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}