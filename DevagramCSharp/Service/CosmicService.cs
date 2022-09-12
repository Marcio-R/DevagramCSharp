using DevagramCSharp.Dtos;
using System.Drawing;
using System.Net.Http.Headers;

namespace DevagramCSharp.Service
{
    public class CosmicService
    {
        public string EnviarImagem(ImagemDto imagemdto)
        {
            Stream imagem = imagemdto.Imagem.OpenReadStream();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "b18tNYZlF0iC09rY9xw6dlscXZ39ojLiNitglrCDeJeQMKw6Or");

            var request = new HttpRequestMessage(HttpMethod.Post, "file");
            var conteudo = new MultipartFormDataContent
            {
                {new StreamContent(imagem),"media",imagemdto.Nome }
            };

            request.Content = conteudo;
            var retornoReq = client.PostAsync("https://upload.cosmicjs.com/v2/buckets/devmarcio-devagram/media", request.Content).Result;

            var urlretorno = retornoReq.Content.ReadFromJsonAsync<CosmicRespostaDto>();
            return urlretorno.Result.Media.url;
        }
    }
}
