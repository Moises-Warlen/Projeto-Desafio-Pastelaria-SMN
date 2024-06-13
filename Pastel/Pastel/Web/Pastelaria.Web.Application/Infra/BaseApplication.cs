namespace Pastelaria.Web.Application.Infra
{
    public abstract class BaseApplication
    {
        protected readonly Request Request;

        protected BaseApplication(string uri, int timeout = 60000)
        {
            Request = new Request(uri.EndsWith("/") ? uri : uri + "/", timeout);
        }

        /// <summary>
        /// Definara o timeout de todas requisicoes desta instancia do HttpCliente.
        /// Utilizar apenas uma vez por chamada da tela para a ActionResult em questao.
        /// </summary>
        /// <param name="timeout">(segundos multiplicado por 1000</param>
        public void SetTimeout(int timeout) => Request.SetTimeout(timeout);
    }
}