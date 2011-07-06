using System.Web;

namespace Mod03_WebApplications.ThumbsAndWatermarking
{

    public class WaterMarkHandlerFactory : IHttpHandlerFactory
    {
        private static ThumbsAndWaterMarkHandler instance;

        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {

            ThumbsAndWaterMarkHandler aux;

            if (instance == null)
            {
                aux = new ThumbsAndWaterMarkHandler();
                if (aux.IsReusable)
                {
                    instance = aux;
                    return instance;
                }
                return aux;
            }
            return instance;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}