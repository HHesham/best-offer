using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BestOffer.Client;
using BestOffer.Models;
using BestOffer.Models.Responses;
using BestOffer.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;
using System.IO;
using Newtonsoft.Json;
using FluentAssertions;

namespace BestOffer.tests
{
    public class OffersClientTest
    {
        private readonly OffersClient _offersClient;


        public OffersClientTest()
        {

            var mockHttp = CreateMockHttpMessageHandler();
            var client = new HttpClient(mockHttp);

            var options = Options.Create(new ConfigKeys
            {
                ServiceUrl = "http://localhost",
                CompanyAKey = "CompanyA",
                CompanyBKey = "CompanyB",
                CompanyCKey = "CompanyC"
            });

            var offersClientlogger = Mock.Of<ILogger<OffersClient>>();
            var offersHttpClientLogger = Mock.Of<ILogger<OffersHttpClient>>();

            OffersHttpClient offersHttpClient = new OffersHttpClient(client, options, offersHttpClientLogger);

            _offersClient = new OffersClient(offersClientlogger, options, offersHttpClient);
        }

        private MockHttpMessageHandler CreateMockHttpMessageHandler() {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost/api/v1/companyA/offers")
                    .Respond("application/json", "{\"total\": 10, \"id\": \"1a673809-7bfa-4957-b26d-7802b2b0bd9b\", \"description\": \"CompanyA offer\"}"); // Respond with JSON
            mockHttp.When("http://localhost/api/v1/companyB/offers")
                    .Respond("application/json", "{ \"amount\": 30,  \"id\": \"d3385d20-c709-4f63-a899-d2bd707243ba\",  \"description\": \"CompanyB offer\" }"); // Respond with JSON
            string xmlResponse = @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <Response>
                            <Id>cebddca0-26a1-4783-89ce-2ae4951b80e5</Id>
                            <Description>CompanyC offer</Description>
                            <Quote>20</Quote>
                         </Response>";
            mockHttp.When("http://localhost/api/v1/companyC/offers")
                    .Respond("application/xml", xmlResponse); // Respond with Xml

            return mockHttp;
        }

        [Fact]
        public async Task GetBestOfferTest()
        {
            Dictionary<string, string> urls = new Dictionary<string, string>() {
                {"CompanyA","http://localhost/api/v1/companyA/offers" },
                {"CompanyB","http://localhost/api/v1/companyB/offers" },
                {"CompanyC","http://localhost/api/v1/companyC/offers" }
            };

            Address source = new Address {
                lat = 1.0f,
                lng = 11.0f
            };
            Address destination = new Address
            {
                lat = 20.0f,
                lng = 10.0f
            };
            CartonDimentions[] dimentions = {
                new CartonDimentions(){
                    Length = 10,
                    Width = 20,
                    Height=5
                }
            };
            OffersResponse response = await _offersClient.GetBestOfferAsync(urls, source, destination, dimentions);

            response.GetType().Should().Be(typeof(CompanyAOffersResponse));
            CompanyAOffersResponse fullResponse = (CompanyAOffersResponse)response;
            fullResponse.Total.Should().Be(10.0);
        }

        [Fact]
        public async Task GetBestOfferFromFileTest()
        {
            Input input = LoadJson();
            OffersResponse response = await _offersClient.GetBestOfferAsync(input.Urls, input.Source, input.Destination, input.Dimentions);

            response.GetType().Should().Be(typeof(CompanyAOffersResponse));
            CompanyAOffersResponse fullResponse = (CompanyAOffersResponse)response;
            fullResponse.Total.Should().Be(10.0);
        }

        private Input LoadJson()
        {
            string fileName = "testcase.json";
            string path = Path.Combine(Environment.CurrentDirectory, "../../../", fileName);

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                Input intput = JsonConvert.DeserializeObject<Input>(json);
                return intput;
            }
        }
    }

    class Input {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public CartonDimentions[] Dimentions { get; set; }
        public Dictionary<string,string> Urls { get; set; }
    }
}
