using System.Windows;
using Assistant.ViewModels;

namespace Assistant.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            //Config = new Configuration("Log.log");
            //Text = new TextToSpeech(Config, "Speak.log");
            //Text.SpeechRecognized += SpeakNext;
            //rnd = new Random();
        }

        //private void ButtonEnableVoiceControl_Click(object sender, RoutedEventArgs e)
        //{
        //    buttonDisableVoiceControl.IsEnabled = true;
        //    Config.VoiceControl = true;
        //    Text.Recognize();
        //}

        //private void ButtonDisableVoiceControl_Click(object sender, RoutedEventArgs e)
        //{
        //    Config.VoiceControl = false;
        //    buttonDisableVoiceControl.IsEnabled = false;
        //}

        //private void ButtonNext_Click(object sender, RoutedEventArgs e)
        //{
        //    SpeakNext(null, null);
        //}

        //private void OpenFile()
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.Filter = "Текстовые файлы(*.txt) | *.txt";
        //    dialog.InitialDirectory = Environment.CurrentDirectory;

        //    bool? result = dialog.ShowDialog();

        //    if (result == true)
        //    {
        //        string fileName = dialog.FileName;
        //        ReadFile(fileName);
        //    }
        //}

        //private async void ReadFile(string fileName)
        //{
        //    using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8))
        //    {
        //        string line;
        //        while ((line = await sr.ReadLineAsync()) != null)
        //        {
        //            listBox.Items.Add(line);
        //        }
        //    }
        //}

        //private void SaveFile()
        //{
        //    SaveFileDialog dialog = new SaveFileDialog();
        //    dialog.Filter = "Текстовые файлы(*.txt) | *.txt";
        //    dialog.InitialDirectory = Environment.CurrentDirectory;

        //    bool? result = dialog.ShowDialog();

        //    if (result == true)
        //    {
        //        string fileName = dialog.FileName;
        //        WriteFile(fileName);
        //    }
        //}

        //private async void WriteFile(string fileName)
        //{
        //    using (StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
        //    {
        //        foreach (var item in listBox.Items)
        //        {
        //            await sr.WriteLineAsync();
        //        }
        //    }
        //}

        //private void ButtonSettings(object sender, RoutedEventArgs e)
        //{
        //    SettingsWindow settings = new SettingsWindow(Config);
        //    settings.Show();
        //}

        //private void OpenFile_MenuItemClick(object sender, RoutedEventArgs e)
        //{
        //    OpenFile();
        //}

        //private void SaveFile_MenuItemClick(object sender, RoutedEventArgs e)
        //{
        //    SaveFile();
        //}

        //private void MenuItem_AboutClick(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("GitHub: @nikitkasss" + "\n" + "LinkedIn: @nikitkasss", "About");
        //}

        //public void SpeakNext(object sender, string text)
        //{
        //    if (Config.Random)
        //    {
        //        int index = rnd.Next(listBox.Items.Count);
        //        listBox.SelectedIndex = index;
        //        Text.Speak((string)listBox.Items[index]);
        //    }
        //    else
        //    {
        //        Text.Speak((string)listBox.SelectedValue);
        //        if (listBox.SelectedIndex == listBox.Items.Count - 1)
        //            listBox.SelectedIndex = 0;
        //        else
        //            listBox.SelectedIndex += 1;
        //    }
        //}

        //private void ButtonNext2_Click(object sender, RoutedEventArgs e)
        //{
        //    var speaker = new Speaker();

        //    string key = "c8e404ad11364b1aaf9035232445627c";
        //    string serviceRegion = "northeurope";
        //    var config = SpeechConfig.FromSubscription(key, serviceRegion);
        //    config.SpeechSynthesisLanguage = "ru-RU";

        //    speaker.Initialize(config);
        //    speaker.Speak("Привет!");
        //}
    }
}