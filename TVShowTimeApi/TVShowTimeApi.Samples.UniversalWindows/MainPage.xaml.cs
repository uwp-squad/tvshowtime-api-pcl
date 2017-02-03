using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TVShowTimeApi.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TVShowTimeApi.Samples.UniversalWindows
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ITVShowTimeApiService _apiService = new TVShowTimeApiService();
        private IReactiveTVShowTimeApiService _reactiveApiService = new ReactiveTVShowTimeApiService();

        private string _clientId = "<your-client-id>";
        private string _clientSecret = "<your-client-secret>";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool useReactive = true;

            if (useReactive)
            {
                // Authenticate
                _reactiveApiService.Login(_clientId, _clientSecret)
                    .Subscribe((result) =>
                    {
                        if (result.HasValue && result.Value)
                        {
                            // Try to use the API
                            _reactiveApiService.GetCurrentUser()
                                .Subscribe((userResult) =>
                                {
                                    // TODO
                                },
                                (error) =>
                                {
                                    // TODO
                                });

                            _reactiveApiService.GetAgenda()
                                .Subscribe((agendaResult) =>
                                {
                                    // TODO
                                },
                                (error) =>
                                {
                                    // TODO
                                });
                        }
                    },
                    (error) =>
                    {
                        // TODO
                    });
            }
            else
            {
                // Authenticate
                bool? isAuthenticated = await _apiService.LoginAsync(_clientId, _clientSecret);

                // Try to use the API
                try
                {
                    var user = await _apiService.GetCurrentUserAsync();
                    var agenda = await _apiService.GetAgendaAsync();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}
