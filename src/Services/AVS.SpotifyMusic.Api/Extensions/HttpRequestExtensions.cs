namespace AVS.SpotifyMusic.Api.Extensions
{
	public static class HttpRequestExtensions
	{
		public static string GetUrl(this HttpRequest request) 
		{
			var httpContext = request.HttpContext;
			return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}";
		}
	}
}
