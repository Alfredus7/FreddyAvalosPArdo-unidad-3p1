using System.Text;
Console.WriteLine("Test Api Categorias");

Console.ReadLine();

await Testing();

static async Task Testing()
{
    HttpClient client = new HttpClient();

    client.BaseAddress = new Uri("https://localhost:7240");

    client.DefaultRequestHeaders.Accept.Clear();

    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

    var byteArray = Encoding.ASCII.GetBytes("admin@gmail.com:As12345!");
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

    HttpResponseMessage response = await client.GetAsync("api/Categorias");

    var res = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : $"Error: {response.StatusCode}";

    Console.WriteLine(res);

    Console.ReadLine();
}