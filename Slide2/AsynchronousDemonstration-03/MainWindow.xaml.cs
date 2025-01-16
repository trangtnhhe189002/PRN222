﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsynchronousDemonstration_03
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;

    public partial class MainWindow : Window
    {
        private readonly HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        private readonly IEnumerable<string> UrlList = new string[]
        {
        "https://docs.microsoft.com/",
        "https://docs.microsoft.com/azure",
        "https://docs.microsoft.com/powershell",
        "https://docs.microsoft.com/dotnet",
        "https://docs.microsoft.com/aspnet/core",
        "https://docs.microsoft.com/windows"
        };

        private async void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            btnStartButton.IsEnabled = false;
            txtResults.Clear();
            await SumPageSizesAsync();
            txtResults.Text += $"\nControl returned to {nameof(OnStartButtonClick)}.";
            btnStartButton.IsEnabled = true;
        }

        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            int total = 0;

            foreach (string url in UrlList)
            {
                int contentLength = await ProcessUrlAsync(url, client);
                total += contentLength;
            }

            stopwatch.Stop();
            txtResults.Text += $"\nTotal bytes returned: {total:#,#}";
            txtResults.Text += $"\nElapsed time: {stopwatch.Elapsed}\n";
        }

        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            byte[] content = await client.GetByteArrayAsync(url);
            DisplayResults(url, content);
            return content.Length;
        }

        private void DisplayResults(string url, byte[] content) =>
            txtResults.Text += $"{url,-60} {content.Length,10:#,#}\n";

        protected override void OnClosed(EventArgs e)
        {
            client.Dispose();
            base.OnClosed(e);
        }
    }
    // end MainWindow

}