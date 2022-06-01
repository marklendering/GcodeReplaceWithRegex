using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace GcodeRegex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static readonly string pattern = @"[Y][0-9.]*";
        Regex rg {get;init;}
        public MainWindow()
        {
            InitializeComponent();
            rg = new Regex(pattern);  
        }

        private async void ConvertButtonClick(object sender, RoutedEventArgs e)
        {

            OpenFileDialog d = new OpenFileDialog();
            if(d.ShowDialog() == true)
            {
                String[] lines = await File.ReadAllLinesAsync(d.FileName, cancellationToken:default);
                foreach(String line in lines)
                {
                    //print lines to unchanged output
                    GcodeInput.Text += $"{line}\n";
                    var match = rg.Match(line);
                    if(match.Success)
                    {
                        //we have a match, we need to read it and replace with new calculated value.
                        GcodeOutput.Text += $"{rg.Replace(line, ParseAndConvertFromYToA(match.Value))}\n";
                    }
                    else
                    {
                        GcodeOutput.Text += $"{line}\n";
                    }
                }
            }
        }

        private string ParseAndConvertFromYToA(string input)
        {
            float parsedValue;
            if(float.TryParse(input.Substring(1), out parsedValue))
            {
                parsedValue += 13.37f;
            }
            else
            {
                //error, could not parse value. Should do something here.
            }

            return $"A{parsedValue:0.000}";
        }
    }
}
