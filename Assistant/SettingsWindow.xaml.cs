using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assistant
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private CultureInfo[] cultures;
        public Configuration Configuration { get; private set; }

        private bool isDefaultRecognitionLanguageChanged,
            isDefaultSynthesisLanguageChanged,
            isKeyChanged,
            isServiceRegionChanged,
            isKeyPhraseChanged,
            isRandomChanged;

        public SettingsWindow(Configuration configuration)
        {
            InitializeComponent();

            Configuration = configuration;
            cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (var culture in cultures)
            {
                ComboBoxDefaultRecognitionLanguage.Items.Add(culture.DisplayName);
                ComboBoxDefaultSynthesisLanguage.Items.Add(culture.DisplayName);
            }

            textBoxKey.Text = Properties.Settings.Default.defaultKey;
            textBoxServiceRegion.Text = Properties.Settings.Default.defaultServiceRegion;
            textBoxKeyPhrase.Text = Properties.Settings.Default.defaultKeyPhrase;
            checkBoxRandom.IsChecked = Properties.Settings.Default.random;
        }

        private void SettingsClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isDefaultRecognitionLanguageChanged)
            {
                string language = cultures[ComboBoxDefaultRecognitionLanguage.SelectedIndex].ToString();
                Configuration.ChangeDefaultRecognitionLanguage(language);
            }

            if (isDefaultSynthesisLanguageChanged)
            {
                string language = cultures[ComboBoxDefaultSynthesisLanguage.SelectedIndex].ToString();
                Configuration.ChangeDefaultSynthesisLanguage(language);
            }

            if (isKeyChanged)
            {
                string key = textBoxKey.Text;
                Configuration.ChangeKey(key);
            }

            if (isServiceRegionChanged)
            {
                string region = textBoxServiceRegion.Text;
                Configuration.ChangeServiceRegion(region);
            }

            if (isKeyPhraseChanged)
            {
                string phrase = textBoxKeyPhrase.Text;
                Configuration.ChangeKeyPhrase(phrase);
            }

            if (isRandomChanged)
            {
                bool random = false;

                if (checkBoxRandom.IsChecked == true)
                    random = true;
                if (checkBoxRandom.IsChecked == false)
                    random = false;

                Configuration.ChangeRandom(random);
            }

            Properties.Settings.Default.Save();
        }

        private void ComboBoxDefaultRecognitionLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isDefaultRecognitionLanguageChanged = true;
        }

        private void ComboBoxDefaultSynthesisLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isDefaultSynthesisLanguageChanged = true;
        }

        private void TextBoxKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            isKeyChanged = true;
        }

        private void TextBoxServiceRegion_TextChanged(object sender, TextChangedEventArgs e)
        {
            isServiceRegionChanged = true;
        }

        private void TextBoxKeyPhrase_TextChanged(object sender, TextChangedEventArgs e)
        {
            isKeyPhraseChanged = true;
        }

        private void CheckBoxRandom_Checked(object sender, RoutedEventArgs e)
        {
            isRandomChanged = true;
        }
    }
}