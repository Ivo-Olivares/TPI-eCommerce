using System;
using System.Web;

namespace eCommerce.Web
{
    public static class RedireccionSegura
    {
        private const string ParametroReturnUrl = "ReturnUrl";

        public static string ObtenerDestino(HttpRequest request, string destinoDefault)
        {
            string returnUrl = ObtenerReturnUrl(request);

            if (EsUrlLocal(returnUrl))
                return returnUrl;

            return destinoDefault;
        }

        public static string CrearUrlConRetorno(string destino, HttpRequest request)
        {
            string returnUrl = ObtenerReturnUrl(request);

            if (!EsUrlLocal(returnUrl))
                return destino;

            return destino + "?" + ParametroReturnUrl + "=" + HttpUtility.UrlEncode(returnUrl);
        }

        public static bool EsRetornoCheckout(HttpRequest request)
        {
            string returnUrl = ObtenerReturnUrl(request);

            if (!EsUrlLocal(returnUrl))
                return false;

            return returnUrl.IndexOf("Checkout.aspx", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string ObtenerReturnUrl(HttpRequest request)
        {
            if (request == null)
                return "";

            return request.QueryString[ParametroReturnUrl] ?? "";
        }

        private static bool EsUrlLocal(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (url.Contains("://"))
                return false;

            if (url.StartsWith("//") || url.StartsWith(@"\"))
                return false;

            return url.StartsWith("~/") || url.StartsWith("/");
        }
    }
}
