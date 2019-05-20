using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripRoutingApp.Exceptions;
using TripRoutingApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using TripRoutingApp.Models.Search;
using TripRoutingApp.Models.Uber;
using TripRoutingApp.Models.Google;
using TripRoutingApp.Models.Lyft;
using System.Text;
using System.Linq;

namespace TripRoutingApp.Controllers
{
    public class ResultsController : Controller
    {
        #region Members

        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpClient googleClient = new HttpClient();
        private static readonly HttpClient LyftClient = new HttpClient();

        #endregion Members

        public async Task<IActionResult> SearchResults(SearchModel model)
        {

            string uberCoordinates = await GetLongLatCoordinatesForUber(model.startingPoint, model.destination);
            string lyftCoordinates = await GetLongLatCoordinatesForLyft(model.startingPoint, model.destination, false);
            string lyftCoordinatesForETA = await GetLongLatCoordinatesForLyft(model.startingPoint, model.destination, true);

            SearchResultsModel RequestResults = new SearchResultsModel
            {
                UberCostEstimate = await GetUberPriceEstimate(uberCoordinates),
                LyftCostEstimate = await GetLyftPriceEstimate(lyftCoordinates),
                UberArrivalTimeEstimate = await GetUberTimeEstimate(uberCoordinates),
                LyftArrivalTimeEstimate = await GetLyftETAEstimate(lyftCoordinatesForETA)
            };


            ResultsPresentationModel ResultsViewModel = new ResultsPresentationModel();
            ResultsViewModel.UberServices = new List<ResultsDataFilteredModel>();
            ResultsViewModel.LyftServices = new List<ResultsDataFilteredModel>();

            foreach (var service in RequestResults.UberCostEstimate.prices) 
            {
                if (service != null)
                {
                    ResultsDataFilteredModel newService = new ResultsDataFilteredModel();
                    newService.Name = service.display_name;
                    newService.PriceEstimate = service.estimate;

                    if (RequestResults.UberArrivalTimeEstimate.times.FirstOrDefault
                                                        (a => a.display_name == service.display_name) != null)
                    {
                        newService.ArrivalTimeEstimate = (RequestResults.UberArrivalTimeEstimate.times.FirstOrDefault
                                                        (a => a.display_name == service.display_name).estimate) / 60;
                    }
                    ResultsViewModel.UberServices.Add(newService);
                }
            }

            foreach (var service in RequestResults.LyftCostEstimate.PriceEstimates)
            {
                if (service != null)
                {
                    ResultsDataFilteredModel newService = new ResultsDataFilteredModel();
                    newService.Name = service.display_name;
                    newService.PriceEstimate = "$"+(service.estimated_cost_cents_min / 60).ToString() + " - " +
                                                (service.estimated_cost_cents_max / 60).ToString();
                    if (RequestResults.LyftArrivalTimeEstimate.times.FirstOrDefault
                                                    (a => a.display_name == service.display_name) != null)
                    {
                        newService.ArrivalTimeEstimate = (RequestResults.LyftArrivalTimeEstimate.times.FirstOrDefault
                                                        (a => a.display_name == service.display_name).eta_seconds) / 60;
                    }

                    ResultsViewModel.LyftServices.Add(newService);
                }
            }

            return View(ResultsViewModel);
        }

        #region Uber Private Functions

        private async Task<PriceList> GetUberPriceEstimate(string coordinates)
        {
            try
            {
                if (client.DefaultRequestHeaders.Authorization == null)
                    SetupHttpClientForUberAPI();
                var response = await client
                .GetAsync("/v1.2/estimates/price?" + coordinates);
                string content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        ExceptionContent = content
                    };
                }

                return JsonConvert.DeserializeObject<PriceList>(content);
            }
            catch (Exception ex)
            {
                Exception reason = ex.GetBaseException();
            }

            return null;
        }

        private async Task<ArrivalTimesList> GetUberTimeEstimate(string coordinates) 
        {
            try
            {
                //SetupHttpClientForUberAPI();
                var response = await client.GetAsync("/v1.2/estimates/time?" + coordinates);
                string content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        ExceptionContent = content
                    };
                }

                return JsonConvert.DeserializeObject<ArrivalTimesList>(content);
            }
            catch(Exception ex)
            {
                Exception reason = ex.GetBaseException();
            }

            return null;
        }

        private void SetupHttpClientForUberAPI()
        {
            client.DefaultRequestHeaders.AcceptLanguage.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
            AuthenticationHeaderValue authentication = new AuthenticationHeaderValue("Token", "OWMCu36s91apz7vi0YsuhBqr2OqbJevhwBV07N4v");
            client.DefaultRequestHeaders.Authorization = authentication;
            client.BaseAddress = new Uri("https://api.uber.com");
        }

        #endregion Uber Private Functions

        #region Lyft Private Functions

        private async Task<LyftPriceEstimatesList> GetLyftPriceEstimate(string coordinates) 
        {
            try
            {
                string bearerToken = await GetLyftAccessToken();

                LyftClient.DefaultRequestHeaders.Clear();
                LyftClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                AuthenticationHeaderValue authentication = new AuthenticationHeaderValue("Bearer", bearerToken);
                LyftClient.DefaultRequestHeaders.Authorization = authentication;
                var response = await LyftClient.GetAsync("https://api.lyft.com/v1/cost?" + coordinates);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        ExceptionContent = content
                    };
                }
                return JsonConvert.DeserializeObject<LyftPriceEstimatesList>(content);
            }
            catch (Exception ex) 
            {
                Exception reason = ex.GetBaseException();
                return null;
            }

        }

        private async Task<LyftArrivalTimes> GetLyftETAEstimate (string coordinates) 
        { 
            try
            {
                var response = await LyftClient.GetAsync("https://api.lyft.com/v1/eta?" + coordinates);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        ExceptionContent = content
                    };
                }

                return JsonConvert.DeserializeObject<LyftArrivalTimes>(content);
            }

            catch (Exception ex) 
            {
                Exception reason = ex.GetBaseException();
                return null;
            }
        }

        private async Task<string> GetLyftAccessToken() 
        {
            try
            {
                LyftClient.DefaultRequestHeaders.Clear();
                LyftClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var byteArray = Encoding.ASCII.GetBytes("Q_WyevSA-V_8:V_k3o4g1GhJDtGvyKOIH1fpVtn76IdKU");
                LyftClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.lyft.com/oauth/token");
                var requestContent = new FormUrlEncodedContent(new[] {
                                                                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                                                                new KeyValuePair<string, string>("scope", "public")
                                                                });
                request.Content = requestContent;
                var response = await LyftClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        ExceptionContent = responseContent
                    };
                }

                LyftToken tokenInfo =  JsonConvert.DeserializeObject<LyftToken>(responseContent);
                return tokenInfo.access_token;
            }
            catch(Exception ex) 
            {
                Exception reason = ex.GetBaseException();
                return String.Empty;
            }
        }

        #endregion Lyft Private Functions

        #region Google Private Functions

        private async Task<string> GetLongLatCoordinatesForUber(string start, string dest)
        {
            string _start = start.Replace(" ", "+");
            string _dest = dest.Replace(" ", "+");
            Tuple<double, double> startCoordinates = await GetGeocodingData(_start);
            Tuple<double, double> destCoordinates = await GetGeocodingData(_dest);

            return "start_latitude=" + startCoordinates.Item1 + "&start_longitude=" + startCoordinates.Item2 +
                   "&end_latitude=" + destCoordinates.Item1 + "&end_longitude=" + destCoordinates.Item2;
        }

        private async Task<string> GetLongLatCoordinatesForLyft(string start, string dest, bool eta)
        {
            string _start = start.Replace(" ", "+");
            string _dest = dest.Replace(" ", "+");
            Tuple<double, double> startCoordinates = await GetGeocodingData(_start);
            Tuple<double, double> destCoordinates = await GetGeocodingData(_dest);
            if (!eta)
            {
                return "start_lat=" + startCoordinates.Item1 + "&start_lng=" + startCoordinates.Item2 +
                       "&end_lat=" + destCoordinates.Item1 + "&end_lng=" + destCoordinates.Item2;
            }
            else
                return "lat=" + startCoordinates.Item1 + "&lng=" + startCoordinates.Item2;
        }

        private async Task<Tuple<double, double>> GetGeocodingData(string address) 
        {
            string key = "AIzaSyAlrgDQgcbdPAy5gVha7BRP9HOeJWdStxI";
            string uri = "https://maps.googleapis.com/maps/api/geocode/json?address="
                          + address
                          + "&key="
                          + key;

            var response = await googleClient.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    ExceptionContent = content
                };
            }
            GeocodingData geocodingData = JsonConvert.DeserializeObject<GeocodingData>(content);

            double lat = geocodingData.results[0].geometry.location.lat;
            double lng = geocodingData.results[0].geometry.location.lng;

            return Tuple.Create(lat, lng);

        }

        #endregion Google Private Functions


    }
}
