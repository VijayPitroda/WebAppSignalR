using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Blazored.Toast.Services;
using BlazorSignalRClient;
using BlazorSignalRClient.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;

namespace BlazorSignalRClient.Pages
{
    public partial class Index:IAsyncDisposable
    {
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        IToastService toastService { get; set; }

        private HubConnection hubConnection;
        //private List<string> messages = new List<string>();


        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("https://host.docker.internal:5001/notify"),
                options => {
                    var httpClientHandler = new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        //ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                        //{
                        //    return true;
                        //}
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                    options.HttpMessageHandlerFactory = _ => httpClientHandler;
                })
                .Build();

            hubConnection.On<string>("ReceiveMessage", message =>
            {
                OnShowHtml($"{message}");
                //var encodedMsg = $"{message}";
                //messages.Add(encodedMsg);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }

        private void OnShowHtml(string msg)
        {
            toastService.ShowInfo(@msg, "INFO");
        }

    }
}
