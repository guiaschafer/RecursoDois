﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Recurso2.Recurso1;
using Recurso2.Recurso3;

namespace Recurso2
{
    /// <summary>
    /// Summary description for WebApiRecurso2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebApiRecurso2 : System.Web.Services.WebService
    {
        [WebMethod]
        public string Concorrer(int id, string regiaoCritica, int count)
        {
            return ServicoConcorrencia.Concorrer(id, regiaoCritica, count);
        }

        [WebMethod]
        public string VisualizarArquivo()
        {

            return ServicoConcorrencia.Visualizar();

        }

        [WebMethod]
        public string SalvarArquivo()
        {
            return ServicoConcorrencia.Salvar();
        }

        [WebMethod]
        public string EditarArquivo()
        {
            return ServicoConcorrencia.Editar();
        }

        [WebMethod]
        public string IniciarCiclo()
        {
            return ServicoConcorrencia.ExecutarCiclo();
        }
    }
}
