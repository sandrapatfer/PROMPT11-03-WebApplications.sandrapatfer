using System.Web;

public class WaterMarkHandlerFactory : IHttpHandlerFactory {
    private static WaterMarkHandler instance;

    public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated) {

        WaterMarkHandler aux;

        if(instance == null) {
            aux = new WaterMarkHandler("AULA de PI");
            if(aux.IsReusable) {
                instance = aux;
                return instance;
            }
            return aux;
        }
        return instance;
    }

    public void ReleaseHandler(IHttpHandler handler) {
    }
}