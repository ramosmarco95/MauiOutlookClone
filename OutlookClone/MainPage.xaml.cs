using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace OutlookClone
{
    public partial class MainPage : ContentPage
    {
        private string contentUri = "https://thesimpsonsquoteapi.glitch.me/quotes?count=20";
        public ObservableCollection<Simpson> Simpsons = new();

        public MainPage()
        {
            InitializeComponent();
            MessageCollection.ItemsSource = Simpsons;
        }

        protected override async void OnAppearing()
        {
            LoadingIndicator.IsVisible = true;
            base.OnAppearing();
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, contentUri);
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<List<Simpson>>();
                jsonResponse.ForEach(s => Simpsons.Add(s));
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }

            LoadingIndicator.IsVisible = false;
        }
    }

}
