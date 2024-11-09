using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Collections.Specialized;

namespace Unidad3P1.WebApiClients
{
    /// <summary>
    /// Cliente de API corporativa
    /// </summary>
    public sealed class WebApiClient : IDisposable
    {
        private bool disposed = false;
        private string ApiBaseAddress;
        private string _username;
        private readonly WebClient _client = new WebClient();
        private readonly string _apiCredenciales;

        /// <summary>
        /// Constructor del cliente de API corporativa
        /// </summary>
        /// <param name="username">Nombre de usuario opcional</param>
        public WebApiClient(string username = "")
        {
            ApiBaseAddress = "https://localhost:7244";
            _apiCredenciales = "admin@email.com:As12345!";
            _username = username;
            ResetWebClient();
        }

        private void ResetWebClient()
        {
            _client.Headers.Clear();
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        /// <summary>
        /// Obtener categorías
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <returns>Resultado de la llamada a la API</returns>
        public CoorporateApiResult<T> GetCategoria<T>()
        {
            string address = "api/Categoria";
            return GetData<T>(address);
        }
        public CoorporateApiResult<T> GetCategoriaById<T>(int id)
        {
            string address = $"api/Categoria/{id}";
            return GetData<T>(address);
        }

        public CoorporateApiResult<bool> DeleteCategoria<T>(int id)
        {
            string resource = $"api/Categoria/{id}";
            string address = GenerateApiAddress(resource);
            return DeleteEntity<bool>(address);
        }












        private CoorporateApiResult<T> GetData<T>(string module)
        {
            string address = GenerateApiAddress(module);
            AddAuthenticationHeader();
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();

            try
            {
                var response = _client.DownloadString(address);
                result.Data = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private CoorporateApiResult<TU> PostData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);
            return PostData<T, TU>(address, model);
        }

        private CoorporateApiResult<TU> PutData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);
            return PutData<T, TU>(address, model);
        }

        private CoorporateApiResult<TU> PostData<T, TU>(string address, T model)
        {
            string serialisedData = JsonConvert.SerializeObject(model);
            AddAuthenticationHeader();
            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();

            try
            {
                var response = _client.UploadString(address, serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private CoorporateApiResult<TU> PostToken<TU>(string address, string token)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();

            try
            {
                var response = _client.UploadString(address, token);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private CoorporateApiResult<TU> PutData<T, TU>(string address, T model)
        {
            string serialisedData = JsonConvert.SerializeObject(model);
            AddAuthenticationHeader();
            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();

            try
            {
                var response = _client.UploadString(address, "PUT", serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private CoorporateApiResult<bool> DeleteEntity<T>(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<bool> result = new CoorporateApiResult<bool>();

            try
            {
                var response = _client.UploadString(address, "DELETE", string.Empty);
                result.Data = true;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private string GenerateApiAddress(string controller)
        {
            return string.Format("{0}/{1}", ApiBaseAddress, controller);
        }

        private void AddAuthenticationHeader(WebRequest webRequest = null, System.Net.Http.HttpClient httpClient = null)
        {
            ResetWebClient();
            var byteArray = Encoding.ASCII.GetBytes(_apiCredenciales);
            _client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(byteArray));
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
