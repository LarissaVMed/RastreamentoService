using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp3
{
    using System.Text.RegularExpressions;
    using webServiceRastreamento;   

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Insira o numero de rastreamento:");
            string numeroDeRastreamento = Console.ReadLine();

            var objetoResponse = RastreamentoObjeto(numeroDeRastreamento);

            if (objetoResponse.erro == null)
            {
                Console.WriteLine("Numero Do Objeto:" + objetoResponse.numero +  " Nome: " + objetoResponse.nome + " Categoria :" + objetoResponse.categoria + " Data Da Postagem :" +objetoResponse.dataPostagem);
                Console.ReadLine();
            }
            else
                Console.WriteLine("Erro:" + objetoResponse.erro);
                Console.ReadLine();
        }

        private static ObjectResponse RastreamentoObjeto(string numeroDeRastreamento)
        {
            webServiceRastreamento.ServiceClient client = new webServiceRastreamento.ServiceClient();
            string[] numeroDeRastreamentoString = { numeroDeRastreamento };
            var objectResponse = new ObjectResponse();

            var requestlist = new RequestList();
            requestlist.usuario = "ECT";
            requestlist.senha = "SRO";
            requestlist.resultado = "U";
            requestlist.tipo = "L";
            requestlist.lingua = "101";
            requestlist.objeto = numeroDeRastreamento;

            var resp = client.buscaEventos(requestlist.usuario, requestlist.senha, requestlist.tipo, requestlist.resultado, requestlist.lingua, requestlist.objeto);

            if (resp.objeto[0].erro == null)
            {
                objectResponse.numero = resp.objeto[0].numero;
                objectResponse.nome = resp.objeto[0].nome;
                objectResponse.categoria = resp.objeto[0].categoria;
                objectResponse.dataPostagem = resp.objeto[0].evento[0].data;
                return objectResponse;
            }
            else
            {
                objectResponse.erro = resp.objeto[0].erro;
                return objectResponse;
            }
        }

        
    }
}
