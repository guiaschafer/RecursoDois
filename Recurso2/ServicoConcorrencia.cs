﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Recurso2.Recurso1;
using Recurso2.Recurso3;
using Recurso2.Recurso4;
using Recurso2.Recurso5;

namespace Recurso2
{
    public static class ServicoConcorrencia
    {
        static List<Tuple<int, string>> filaRespondeu = new List<Tuple<int, string>>();
        static List<int> filaEspera = new List<int>();
        private static bool emExecucao = false;
        private static int countClient = 0;
        private static bool emRequisicao = false;
        private static int qtsVezesExecutado = 0;

        public static string Concorrer(int id, string regiao, int count)
        {
            var retorno = "NOK";
            countClient++;
            if (!emExecucao && !emRequisicao)
            {
                retorno = "OK";
            }
            else if (emExecucao || emRequisicao)
            {
                if (countClient < count)
                    retorno = "OK";
                else
                {
                    countClient = count;

                    filaEspera.Add(id);

                    while (filaEspera.Count > 0)
                    {
                        Debug.WriteLine($"2 - WebService {id} esperando resposta");

                    }
                    retorno = "OK";
                }
            }

            return retorno;
        }

        private static void WebService1()
        {

            var ws1 = new WebApiRecurso1();
            ws1.Timeout = Int32.MaxValue;
            filaRespondeu.Add(new Tuple<int, string>(1, ws1.Concorrer(2, "RC", countClient)));
            Debug.WriteLine("WebService 1 resopndeu");

        }

        private static void WebService3()
        {
            var ws3 = new WebApiRecurso3();
            ws3.Timeout = Int32.MaxValue;
            filaRespondeu.Add(new Tuple<int, string>(3, ws3.Concorrer(2, "RC", countClient)));
            Debug.WriteLine("WebService 3 Resopndeu");

        }

        private static void WebService4()
        {
            var ws4 = new WebApiRecurso4();
            ws4.Timeout = Int32.MaxValue;
            filaRespondeu.Add(new Tuple<int, string>(4, ws4.Concorrer(2, "RC", countClient)));
            Debug.WriteLine("WebService 4 respondeu");

        }

        private static void WebService5()
        {
            var ws5 = new WebApiRecurso5();
            ws5.Timeout = Int32.MaxValue;
            filaRespondeu.Add(new Tuple<int, string>(5, ws5.Concorrer(2, "RC", countClient)));
            Debug.WriteLine("WebService 5 respondeu");

        }

        public static string ObterOK()
        {
            emRequisicao = true;
            countClient++;
            
            var w1 = new Thread(WebService1);
            var w3 = new Thread(WebService3);
            var ws4 = new Thread(WebService4);
            var ws5 = new Thread(WebService5);

            w1.Start();
            w3.Start();
            ws4.Start();
            ws5.Start();

            while (!(filaRespondeu.Count == 4 && !filaRespondeu.Any(x => x.Item2 == "NOK")))
            {
                Debug.WriteLine("WebService 2 esperando respostas");

            }

            emRequisicao = false;
            return "OK";
        }

        public static string Visualizar()
        {
            if (ObterOK() == "OK")
            {
                emExecucao = true;
                countClient++;
                var wsServer = new Server.Server();
                var retorno = wsServer.Visualizar();
                emExecucao = false;
                filaEspera.Clear();
                filaRespondeu.Clear();
                return retorno;
            }
            return "NOK";
        }
        public static string Salvar()
        {
            if (ObterOK() == "OK")
            {
                emExecucao = true;
                countClient++;
                var wsServer = new Server.Server();
                var retorno = wsServer.Salvar();
                emExecucao = false;
                filaEspera.Clear();
                filaRespondeu.Clear();
                return retorno;
            }

            return "NOK";
        }
        public static string Editar()
        {
            if (ObterOK() == "OK")
            {
                emExecucao = true;
                countClient++;
                var wsServer = new Server.Server();
                var retorno = wsServer.Editar();
                emExecucao = false;
                filaEspera.Clear();
                filaRespondeu.Clear();
                return retorno;
            }
            return "NOK";
        }

        public static string ExecutarCiclo()
        {
            try
            {
                while (qtsVezesExecutado <= 20)
                {
                    Visualizar();
                    Salvar();
                    Editar();

                    qtsVezesExecutado++;
                }

                return "OK";
            }
            catch (Exception e)
            {
                return "NOK";
            }
            
        }
    }
}