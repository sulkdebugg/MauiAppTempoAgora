using MauiAppTempoAgora.models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao =
                            $"Temperatura: {t.temp} °C\n" +
                            $"Sensação térmica: {t.feels_like} °C\n" +
                            $"Temperatura mínima: {t.temp_min} °C\n" +
                            $"Temperatura máxima: {t.temp_max} °C\n" +
                            $"Velocidade do vento: {t.speed} m/s\n" +
                            $"Nascer do sol: {t.sunrise}\n" +
                            $"Pôr do sol: {t.sunset}\n" +
                            $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nível do mar: {t.sea_level} m\n";

                        lbl_res.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?" +
                            $"type=map&location=coordinates&metricRain=mm&metricTemp=°C" +
                            $"&metricWind=km/h&zoom=6&overlay=wind&product=ecmwf&level=surface" +
                            $"&lat={t.lat.ToString().Replace(",", ".")}&lon={t.lon.ToString().Replace(",", ".")}&detailLat=-24.026&detailLon=-46.802&detail=true\" frameborder=\"0\"></iframe>";

                        wv_mapa.Source = new HtmlWebViewSource
                        {
                            Html = mapa
                        }; 
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }

                }
                else
                {
                    lbl_res.Text = "Digite uma cidade";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");

            }
        }

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest
                                                    (GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location? local = await Geolocation.Default.GetLocationAsync(request);

                if (local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude}\nLongitude: {local.Longitude}";

                    lbl_coords.Text = local_disp;

                    GetCidade(local.Latitude, local.Longitude);

                }
                else
                {
                    lbl_coords.Text = "Não foi possível obter a localização.";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro: Dispostivio não suportado", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException ex)
            {
                await DisplayAlert("Erro", "Erro: localização desabilitada", ex.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro", "Erro: permissão de localização negada", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro", ex.Message, "OK");
            }

        }

        private async void GetCidade(double lat, double lon)
        {
            try
            {



                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(lat, lon);

                Placemark place = places.FirstOrDefault();


                if (place != null)
                {
                    txt_cidade.Text = place.Locality ?? string.Empty;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro ao obter informações da cidade.", ex.Message, "OK");
            }
        }
    }

}

    
