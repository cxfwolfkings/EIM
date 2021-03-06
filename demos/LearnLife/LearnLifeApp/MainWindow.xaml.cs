﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace LearnLifeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchInfo searchInfo;
        private object lockList = new object();
        private CancellationTokenSource cts = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
            searchInfo = new SearchInfo();
            this.DataContext = searchInfo;

            BindingOperations.EnableCollectionSynchronization(searchInfo.List, lockList);
        }

        private IEnumerable<IImageRequest> GetSearchRequests()
        {
            return new List<IImageRequest>
            {
                new BingRequest { SearchTerm = searchInfo.SearchTerm },
                new FlickrRequest { SearchTerm = searchInfo.SearchTerm}
            };
        }

        /// <summary>
        /// 同步模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSearchSync(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                WebClient client = new WebClient();
                client.Credentials = req.Credentials;
                string resp = client.DownloadString(req.Url);
                IEnumerable<SearchItemResult> images = req.Parse(resp);
                foreach (var image in images)
                {
                    searchInfo.List.Add(image);
                }

            }
        }

        /// <summary>
        /// 异步模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSeachAsyncPattern(object sender, RoutedEventArgs e)
        {
            Func<string, ICredentials, string> downloadString = (address, cred) =>
            {
                var client = new WebClient();
                client.Credentials = cred;
                return client.DownloadString(address);
            };

            Action<SearchItemResult> addItem = item => searchInfo.List.Add(item);

            foreach (var req in GetSearchRequests())
            {
                downloadString.BeginInvoke(req.Url, req.Credentials, ar =>
                {
                    string resp = downloadString.EndInvoke(ar);
                    var images = req.Parse(resp);
                    foreach (var image in images)
                    {
                        this.Dispatcher.Invoke(addItem, image);
                    }
                }, null);
            }
        }

        /// <summary>
        /// 基于事件的异步模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAsyncEventPattern(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                var client = new WebClient();
                client.Credentials = req.Credentials;
                client.DownloadStringCompleted += (sender1, e1) =>
                {
                    string resp = e1.Result;
                    var images = req.Parse(resp);
                    foreach (var image in images)
                    {
                        searchInfo.List.Add(image);
                    }
                };
                client.DownloadStringAsync(new Uri(req.Url));
            }
        }

        /// <summary>
        /// 基于任务的异步模式(带异常处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTaskBasedAsyncPattern(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();//取消任务类
            try
            {
                foreach (var req in GetSearchRequests())
                {
                    var clientHandler = new HttpClientHandler
                    {
                        Credentials = req.Credentials
                    };
                    var client = new HttpClient(clientHandler);

                    var response = await client.GetAsync(req.Url, cts.Token);
                    string resp = await response.Content.ReadAsStringAsync();

                    await Task.Run(() =>
                    {
                        var images = req.Parse(resp);
                        foreach (var image in images)
                        {
                            cts.Token.ThrowIfCancellationRequested();//如果取消了，就抛出异常
                            searchInfo.List.Add(image);
                        }
                    }, cts.Token);
                }
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 基于任务的异步模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTaskBasedAsyncPattern1(object sender, RoutedEventArgs e)
        {
            foreach (var req in GetSearchRequests())
            {
                var client = new WebClient();
                client.Credentials = req.Credentials;
                string resp = await client.DownloadStringTaskAsync(req.Url);

                var images = req.Parse(resp);
                foreach (var image in images)
                {
                    searchInfo.List.Add(image);
                }
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (cts != null)
                cts.Cancel();

        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            searchInfo.List.Clear();
        }

    }
}
