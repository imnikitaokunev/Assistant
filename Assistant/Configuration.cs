using System;
using Microsoft.CognitiveServices.Speech;

namespace Assistant
{
    public class Configuration
    {
        private event ConfigStateHandler KeyChanged;
        private event ConfigStateHandler ServiceRegionChanged;
        private event ConfigStateHandler KeyPhraseChanged;
        private event ConfigStateHandler DefaultRecognitionLanguageChanged;
        private event ConfigStateHandler DefaultSynthesisLanguageChanged;
        private event ConfigStateHandler RandomChanged;

        public SpeechConfig Config { get; private set; }

        public bool Random { get; private set; }

        public string Phrase { get; private set; }

        public string LogFileName { get; private set; }

        public bool VoiceControl { get; set; }

        public Configuration(string logFileName)
        {
            LogFileName = logFileName;
            string key = Properties.Settings.Default.defaultKey;
            string serviceRegion = Properties.Settings.Default.defaultServiceRegion;

            Config = SpeechConfig.FromSubscription(key, serviceRegion);

            Config.SpeechRecognitionLanguage = Properties.Settings.Default.defaultRecognitionLanguage;
            Config.SpeechSynthesisLanguage = Properties.Settings.Default.defaultSynthesisLanguage;

            Random = Properties.Settings.Default.random;
            Phrase = Properties.Settings.Default.defaultKeyPhrase;
        }

        private void CallEvent(ConfigurationEventArgs e, ConfigStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
            SaveLog(e.Message);
        }

        private void OnKeyChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, KeyChanged);
        }

        public void ChangeKey(string key)
        {
            OnKeyChanged(new ConfigurationEventArgs(
                $"Key changed at {DateTime.Now}\n" + $"Was: {Properties.Settings.Default.defaultKey}\n" +
                $"Become: {key}\n", key));
            Properties.Settings.Default.defaultKey = key;
        }

        private void OnServiceRegionChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, ServiceRegionChanged);
        }

        public void ChangeServiceRegion(string region)
        {
            OnServiceRegionChanged(new ConfigurationEventArgs(
                $"Service region changed at {DateTime.Now}\n" +
                $"Was: {Properties.Settings.Default.defaultServiceRegion}\n" + $"Become: {region}\n", region));
            Properties.Settings.Default.defaultServiceRegion = region;
        }

        private void OnDefaultRecognitionLanguageChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, DefaultRecognitionLanguageChanged);
        }

        public void ChangeDefaultRecognitionLanguage(string language)
        {
            OnDefaultRecognitionLanguageChanged(new ConfigurationEventArgs(
                $"Default recognition language changed at {DateTime.Now}\n" +
                $"Was: {Properties.Settings.Default.defaultRecognitionLanguage}\n" + $"Become: {language}\n",
                language));
            Properties.Settings.Default.defaultRecognitionLanguage = language;
        }

        private void OnDefaultSynthesisLanguageChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, DefaultSynthesisLanguageChanged);
        }

        public void ChangeDefaultSynthesisLanguage(string language)
        {
            OnDefaultSynthesisLanguageChanged(new ConfigurationEventArgs(
                $"Default synthesis language changed at {DateTime.Now}\n" +
                $"Was: {Properties.Settings.Default.defaultSynthesisLanguage}\n" + $"Become: {language}\n",
                language));
            Properties.Settings.Default.defaultSynthesisLanguage = language;
        }

        private void OnKeyPhraseChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, KeyPhraseChanged);
        }

        public void ChangeKeyPhrase(string phrase)
        {
            OnKeyPhraseChanged(new ConfigurationEventArgs(
                $"Key phrase changed at {DateTime.Now}\n" + $"Was: {Properties.Settings.Default.defaultKeyPhrase}\n" +
                $"Become: {phrase}\n", phrase));
            Properties.Settings.Default.defaultKeyPhrase = phrase;
        }

        private void OnRandomChanged(ConfigurationEventArgs e)
        {
            CallEvent(e, RandomChanged);
        }

        public void ChangeRandom(bool random)
        {
            OnRandomChanged(new ConfigurationEventArgs(
                $"Random changed at {DateTime.Now}\n" + $"Was: {Properties.Settings.Default.random}\n" +
                $"Become: {random}\n", random.ToString()));
            Properties.Settings.Default.random = random;
        }

        private void SaveLog(string message)
        {
            Log.Save(LogFileName, message);
        }
    }
}