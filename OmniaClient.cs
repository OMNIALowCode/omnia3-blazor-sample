using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using omnia_blazor_demo.Shared.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Spike_ExternalWebApp.Shared.Model;
using System.Linq;

namespace omnia_blazor_demo
{

    public class OmniaClient
    {
        public readonly HttpClient http;

        public OmniaClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Temporary> CreateTemporary(EntityMetadata entityMetadata)
        {
            var tmpResponse = await http.PostAsJsonAsync<EmptyQuery>($"{entityMetadata.Tenant}/{entityMetadata.Environment}/application/{entityMetadata.Entity}/{entityMetadata.DataSource}/Temporaries", new EmptyQuery());
            if (!tmpResponse.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to create the temporary.");

            return JsonConvert.DeserializeObject<Temporary>(await tmpResponse.Content.ReadAsStringAsync());
        }

        public async Task<EntityTemporary<T>> CreateTemporary<T>(EntityMetadata entityMetadata, string code)
        {
            var tmpResponse = await http.PostAsJsonAsync<EmptyQuery>($"{entityMetadata.Tenant}/{entityMetadata.Environment}/application/{entityMetadata.Entity}/{entityMetadata.DataSource}/{code}/CreateTemporary", new EmptyQuery());
            if (!tmpResponse.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to create the temporary.");

            return JsonConvert.DeserializeObject<EntityTemporary<T>>(await tmpResponse.Content.ReadAsStringAsync());
        }


        public async Task<Temporary> UpdateTemporary(EntityMetadata entityMetadata, string temporaryId, JsonPatchDocument jsonPatchDocument)
        {
            var uri = $"{entityMetadata.Tenant}/{entityMetadata.Environment}/application/{entityMetadata.Entity}/{entityMetadata.DataSource}/Temporaries/{temporaryId}";

            var requestBodyInJson = JsonConvert.SerializeObject(jsonPatchDocument);

            var buffer = System.Text.Encoding.UTF8.GetBytes(requestBodyInJson);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var tmpResponse = await http.PatchAsync(uri, byteContent);
            if (!tmpResponse.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to patch the temporary.");

            return JsonConvert.DeserializeObject<Temporary>(await tmpResponse.Content.ReadAsStringAsync());
        }

        public async Task<TemporarySaveResponse> SaveTemporary(EntityMetadata entityMetadata, string temporaryId)
        {
            var uri = $"{entityMetadata.Tenant}/{entityMetadata.Environment}/application/{entityMetadata.Entity}/{entityMetadata.DataSource}/Temporaries/{temporaryId}/Save";
            var tmpResponse = await http.PostAsJsonAsync<EmptyQuery>(uri, new EmptyQuery());
            if (!tmpResponse.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<ErrorResponse>(await tmpResponse.Content.ReadAsStringAsync());
                if (response == null)
                    throw new Exception("An error occurred when trying to save the temporary.");
                else
                {
                    var errors = response.errors == null || response.errors.Count == 0 ? response.message : string.Join(Environment.NewLine, response.errors.Select(e => $"{e.name}: {e.message}").ToList());
                    throw new Exception($"Error save the temporary: {errors}");
                }

            }
            return JsonConvert.DeserializeObject<TemporarySaveResponse>(await tmpResponse.Content.ReadAsStringAsync());
        }

        public async Task<List<T>> ExecuteQuery<T>(QueryMetadata queryMetadata)
        {
            var uri = $"{queryMetadata.Tenant}/{queryMetadata.Environment}/application/queries/{queryMetadata.Query}/{queryMetadata.DataSource}?page={queryMetadata.Page}&pageSize={queryMetadata.PageSize}";
            var response = await http.PostAsJsonAsync<OmniaQueryRequestBody>(uri, new OmniaQueryRequestBody
            {
                parameters = queryMetadata.parameters
            });
            if (!response.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to execute the query.");
            return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
        }

        public async void GetMe()
        {
            var tmpResponse = await http.GetAsync($"me");

            if (!tmpResponse.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to get me.");
        }

        public async Task<DeleteEntityResponse> DeleteEntity(EntityMetadata entityMetadata, string code)
        {
            var uri = $"{entityMetadata.Tenant}/{entityMetadata.Environment}/application/{entityMetadata.Entity}/{entityMetadata.DataSource}/{code}";
            var tmpResponse = await http.DeleteAsync(uri);
            if (!tmpResponse.IsSuccessStatusCode)
                throw new Exception("An error occurred when trying to delete entity.");
            return JsonConvert.DeserializeObject<DeleteEntityResponse>(await tmpResponse.Content.ReadAsStringAsync());
        }

    }
}
