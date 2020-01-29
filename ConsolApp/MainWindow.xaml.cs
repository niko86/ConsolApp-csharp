using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.Win32;
using static ConsolApp.ConsolData;

namespace ConsolApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public GeneratePlot ViewModel = new GeneratePlot();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            SaveXMLButton.IsEnabled = false;
            FileComboBox.IsEnabled = false;
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog
            {
                // Set filter for file extension and default file extension 
                DefaultExt = ".txt",
                Filter = "Text Files (*.txt)|*.txt",
                Multiselect = true,
            };

            // Get the selected file name(s) and display in a TextBox 
            if (dlg.ShowDialog() == true)
            {
                FileNames = dlg.FileNames;
                FileComboBox.Items.Clear(); 

                foreach (string file_name in FileNames)
                {
                    FileComboBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file_name));
                }    

                SaveXMLButton.IsEnabled = !SaveXMLButton.IsEnabled;
                FileComboBox.IsEnabled = !FileComboBox.IsEnabled;
            }
        }

        private void SaveXMLButton_Click(object sender, RoutedEventArgs e)
        {
            ConsolXML consolXML = new ConsolXML();
            consolXML.GenerateXML();
            SaveXMLButton.IsEnabled = !SaveXMLButton.IsEnabled;
            FileComboBox.IsEnabled = !FileComboBox.IsEnabled;
        }

        private void FileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileComboBox.SelectedIndex != -1)
            {
                int comboIndex = FileComboBox.SelectedIndex;

                ConsolData consolData = new ConsolData();

                //PlotTestData = consolData.ParseFile(FileNames[comboIndex]);
                ViewModel.NewPlot(consolData.ParseFile(FileNames[comboIndex]));
                ConsolPlot.InvalidatePlot(true);
            }
        }

        public static string[] FileNames { get; private set; }
    }
}
