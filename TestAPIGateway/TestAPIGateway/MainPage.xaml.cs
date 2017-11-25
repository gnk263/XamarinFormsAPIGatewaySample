using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace TestAPIGateway
{
    public class RequestItem
    {
        public int Id { get; set; }
        public int Param1 { get; set; }
        public int Param2 { get; set; }
    }
    public class ResponseItem
    {
        public int Id { get; set; }
        public int Sum { get; set; }
    }



    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            /* 送信用データを作成 */
            var item = new List<RequestItem>
            {
                new RequestItem
                {
                    Id = 1,
                    Param1 = 1,
                    Param2 = 3,
                },
                new RequestItem
                {
                    Id = 2,
                    Param1 = 4,
                    Param2 = 7,
                },
                new RequestItem
                {
                    Id = 3,
                    Param1 = 2,
                    Param2 = 5,
                }
            };

            var json = JsonConvert.SerializeObject(item, Formatting.Indented);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");


            /* 送信(URIは適宜変更してください) */
            var response = await httpClient.PostAsync("https://xxxxxxxxx.execute-api.ap-northeast-1.amazonaws.com/prod/TestAPIGateway", content);


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("Success Status");
                System.Diagnostics.Debug.WriteLine(result);


                var resultJson = JsonConvert.DeserializeObject<List<ResponseItem>>(result);
                
                ResultStatus.Text = "Success";
                Id1Text.Text = $"'Id': {resultJson[0].Id}, 'Sum': {resultJson[0].Sum}";
                Id2Text.Text = $"'Id': {resultJson[1].Id}, 'Sum': {resultJson[1].Sum}";
                Id3Text.Text = $"'Id': {resultJson[2].Id}, 'Sum': {resultJson[2].Sum}";
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not Success Status: " + response.StatusCode.ToString() + "  -  Reason: " + response.ReasonPhrase);

                ResultStatus.Text = "Fail";
            }
        }
    }
}
