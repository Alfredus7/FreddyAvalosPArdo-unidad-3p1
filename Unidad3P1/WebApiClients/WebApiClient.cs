using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Unidad3P1.WebApiClients
{
    /// <summary>
    /// The Corporate Api Client
    /// </summary>
    public sealed class WebApiClient : IDisposable
    {
        private bool disposed = false;
        private string ApiBaseAddress;
        private string _username;
        private readonly WebClient _client = new WebClient();
        private readonly string _apiCredenciales;

        /// <summary>
        /// The Corporate Api Client
        /// </summary>
        /// <param name="transmittionMedium">Type of medium you wish the api client to talk in (Default is JSON)</param>
        public WebApiClient(string username = "")
        {
            string value = string.Empty;
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

        #region Methods

        #region Categorias

        /// <summary>
        /// GetPartnerCodes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public CoorporateApiResult<T> GetCategorias<T>()
        {
            string address = "api/Categoria";

            return GetData<T>(address);
        }

        public CoorporateApiResult<T> GetCategoriaById<T>(int id)
        {
            string address = $"api/Categoria/{id}";

            return GetData<T>(address);
        }

        public CoorporateApiResult<bool> DeleteCategoriaById<T>(int id)
        {
            string recurso = $"api/Categoria/{id}";

            string address = GenerateApiAddress(recurso);

            return DeleteEntity<bool>(address);
        }

        public CoorporateApiResult<CategoryViewModel> PostCategoria<CategoryViewModel>(CategoryViewModel modelCategoria)
        {
            string recurso = $"api/Categoria";
            string source = GenerateApiAddress(recurso);
            return PostData<CategoryViewModel, CategoryViewModel>(source, modelCategoria);
        }

        public CoorporateApiResult<TU> PutCategoria<T, TU>(int id, T model)
        {
            string recurso = $"api/Categoria/{id}";
            string address = GenerateApiAddress(recurso);
            return PutData<T, TU>(model, address);
        }
        #endregion

        #region Productos

        /// <summary>
        /// GetProductos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public CoorporateApiResult<T> GetProductos<T>()
        {
            string address = "api/Producto";

            return GetData<T>(address);
        }

        public CoorporateApiResult<T> GetProductoById<T>(int id)
        {
            string address = $"api/Producto/{id}";

            return GetData<T>(address);
        }

        public CoorporateApiResult<bool> DeleteProductoById<T>(int id)
        {
            string recurso = $"api/Producto/{id}";

            string address = GenerateApiAddress(recurso);

            return DeleteEntity<bool>(address);
        }

        public CoorporateApiResult<ProductViewModel> PostProducto<ProductViewModel>(ProductViewModel modelProducto)
        {
            string recurso = $"api/Producto";
            string address = GenerateApiAddress(recurso);
            return PostData<ProductViewModel, ProductViewModel>(address, modelProducto);
        }

        public CoorporateApiResult<TU> PutProducto<T, TU>(int id, T model)

        {

            string address = $"api/Producto/{id}";

            return PutData<T, TU>(model, address);

        }


        #endregion


        #endregion

        #region Private Methods

        private CoorporateApiResult<T> GetData<T>(string module)
        {
            string address = GenerateApiAddress(module);

            AddAuthenticationHeader();
            CoorporateApiResult<T> result = new CoorporateApiResult<T>();
            try
            {
                // Make the request
                var response = _client.DownloadString(address);
                result.Data = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;

        }

        private CoorporateApiResult<TU> PostData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);

            // Deserialize the response into a GUID
            return PostData<T, TU>(address, model);
        }

        private CoorporateApiResult<TU> PutData<T, TU>(T model, string module)
        {
            string address = GenerateApiAddress(module);

            // Deserialize the response into a GUID
            return PutData<T, TU>(address, model);
        }

        private CoorporateApiResult<TU> PostData<T, TU>(string address, T model)
        {
            // Serialize the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();

            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<TU> PostToken<TU>(string address, string token)
        {
            // Serialize the data we are sending in to JSON
            //string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();

            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, token);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<TU> PutData<T, TU>(string address, T model)
        {
            // Serialize the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(model);

            AddAuthenticationHeader();


            CoorporateApiResult<TU> result = new CoorporateApiResult<TU>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, "PUT", serialisedData);
                result.Data = JsonConvert.DeserializeObject<TU>(response);
            }
            catch (Exception ex)
            {
                throw;
            }

            // Deserialize the response into a GUID
            return result;
        }

        private CoorporateApiResult<bool> DeleteEntity<T>(string address)
        {
            AddAuthenticationHeader();
            CoorporateApiResult<bool> result = new CoorporateApiResult<bool>();
            try
            {
                // Make the request
                var response = _client.UploadString(address, "DELETE", string.Empty);
                result.Data = true;
            }
            catch (Exception ex)
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
            var byteArray = Encoding.ASCII.GetBytes(_apiCredenciales); // Encoding the username and password as ASCII.
            _client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(byteArray));

        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _client.Dispose();
                }

                // Dispose unmanaged managed resources.

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion
    }
}
