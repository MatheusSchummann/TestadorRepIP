using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelógioPontoExpress
{
    public class Main
    {
        public int comando;
        List<Filiais> listaFiliais = new List<Filiais>();
        Dados dados = new Dados();
        public void Menu()
        {

            if (dados.ExisteArquivo("Filiais.csv"))
            {
                dados.LeituraDadosFiliais("Filiais.csv", listaFiliais);
            }

            Console.WriteLine("Realizando a consulta dos relógios pontos...");
            for (int i = 0; i < listaFiliais.Count; i++)
            {
                if (listaFiliais[i].Ip.Length >= 9)
                {
                    var ip = listaFiliais[i].Ip;
                    var status = VerificarPing(ip) ? "Ativo" : "Inativo";
                    listaFiliais[i].Estado = status;
                }
                else
                {
                    listaFiliais[i].Estado = "Inativo";
                }
            }

            Console.Clear();
            do
            {
                Console.WriteLine("-----------Consulta IP filias-----------");
                Console.WriteLine("1- Listar Filiais");
                Console.WriteLine("2- Adicionar Filiais");
                Console.WriteLine("3- Editar Filiais");
                Console.WriteLine("4- Verificar IP Ativo");
                Console.WriteLine("5- Encerrar Consulta");
                comando = LerNumero("Comando: ");
                switch (comando)
                {
                    case 1:
                        int comando2;
                        Console.Clear();
                        Console.WriteLine("-----------Lista Filiais-----------");
                        Console.WriteLine("Deseja qual lista");
                        Console.WriteLine("1- Lista sem filtro");
                        Console.WriteLine("2- Lista com filtro");
                        Console.WriteLine("3- Voltar");
                        comando2 = LerNumero("Comando: ");
                        if (comando2 == 1)
                        {
                            Console.Clear();
                            for (int i = 0; i < listaFiliais.Count; i++)
                            {
                                Console.WriteLine($"{listaFiliais[i].Numero} / {listaFiliais[i].Nome} / {listaFiliais[i].Ip} / {listaFiliais[i].Estado} / Modem: {listaFiliais[i].Modem} / Relogio: {listaFiliais[i].Relogio}");
                            }
                            Console.Write("Digite qualquer tecla para continuar...");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        else if (comando2 == 2)
                        {

                            int contador1 = 0, contador2 = 0, contador3 =0;
                            Console.Clear();
                            Console.WriteLine("---------------------------Ativos---------------------------");
                            for (int i = 0; i < listaFiliais.Count; i++)
                            {
                                if (listaFiliais[i].Estado == "Ativo")
                                {
                                    Console.WriteLine($"{listaFiliais[i].Numero} / {listaFiliais[i].Nome} / {listaFiliais[i].Ip} / {listaFiliais[i].Estado} / Modem: {listaFiliais[i].Modem} / Relogio: {listaFiliais[i].Relogio}");
                                    contador1++;
                                }
                            }
                            Console.WriteLine($"Total de relogios Ativos: {contador1}\n");
                            Console.WriteLine("---------------------------Inativos com Modem---------------------------");
                            for (int i = 0; i < listaFiliais.Count; i++)
                            {
                                if (listaFiliais[i].Estado == "Inativo" && listaFiliais[i].Modem == true)
                                {
                                    Console.WriteLine($"{listaFiliais[i].Numero} / {listaFiliais[i].Nome} / {listaFiliais[i].Ip} / {listaFiliais[i].Estado} / Modem: {listaFiliais[i].Modem} / Relogio: {listaFiliais[i].Relogio}");
                                    contador2++;
                                }
                            }
                            Console.WriteLine($"Total de relogios Inativos com modem: {contador2}\n");
                            Console.WriteLine("---------------------------Inativos sem Modem---------------------------");
                            for (int i = 0; i < listaFiliais.Count; i++)
                            {
                                if (listaFiliais[i].Estado == "Inativo" && listaFiliais[i].Modem == false)
                                {
                                    Console.WriteLine($"{listaFiliais[i].Numero} / {listaFiliais[i].Nome} / {listaFiliais[i].Ip} / {listaFiliais[i].Estado} / Modem: {listaFiliais[i].Modem} / Relogio: {listaFiliais[i].Relogio}");
                                    contador3++;
                                }
                            }
                            Console.WriteLine($"Total de relogios Inativos sem modem: {contador3}");
                            Console.Write("Digite qualquer tecla para continuar...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (comando2 == 3)
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                        break;

                    case 2:
                        int confirmaNumeroFilial;
                        int numeroFilial;
                        string nomeFilial;
                        string ipFilial;
                        bool modemFilial = false;
                        string modemSN = "false";
                        bool relogioFilial = false;
                        string relogioSN = "false";
                        Console.Clear();
                        Console.WriteLine("-----------Nova Filial-----------");
                        numeroFilial = LerNumero("Digite o numero da nova Filial: ");
                        confirmaNumeroFilial = ConfereNumeroFilialExistente(numeroFilial);
                        Console.Write("Digite o nome da nova filial: ");
                        nomeFilial = Console.ReadLine();
                        Console.Write("Digite o ip ou o estado do relogio ponto da nova filial: ");
                        ipFilial = Console.ReadLine();
                        while (modemSN != "s" && modemSN != "n" && modemSN != "S" && modemSN != "N") 
                        {
                            Console.Write("A filial possuí modem? 'S' ou 'N': ");
                            modemSN = Console.ReadLine();
                            if (modemSN == "s" || modemSN == "S")
                            {
                                modemFilial = true;
                            }
                        }
                        while (relogioSN != "s" && relogioSN != "n" && relogioSN != "S" && relogioSN != "N")
                        {
                            Console.Write("A filial possuí relogio? 'S' ou 'N': ");
                            relogioSN = Console.ReadLine();
                            if (relogioSN == "s" || relogioSN == "S")
                            {
                                relogioFilial = true;
                            }
                        }
                        Filiais filial = new Filiais(confirmaNumeroFilial, nomeFilial, ipFilial, modemFilial, relogioFilial);
                        listaFiliais.Add(filial);
                        SortFiliais();
                        dados.EscritaDadosFiliais(listaFiliais, "Filiais.csv");
                        Console.WriteLine("Filial salva com sucesso!");
                        Console.Write("Digite qualquer tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        int confereNumeroFilial;
                        int comando3;
                        Console.Clear();
                        Console.WriteLine("-----------Editar Filiais-----------");
                        for (int i = 0; i < listaFiliais.Count; i++)
                        {
                            Console.WriteLine($"{listaFiliais[i].Numero} / {listaFiliais[i].Nome} / {listaFiliais[i].Ip} / Modem: {listaFiliais[i].Modem} / Relogio: {listaFiliais[i].Relogio}");
                        }
                        confereNumeroFilial = LerNumero("Digite o número da filial que deseja alterar: ");
                        for (int i = 0; i < listaFiliais.Count; i++)
                        {
                            if (confereNumeroFilial == listaFiliais[i].Numero)
                            {
                                int novoNumeroFilial;
                                int confirmaNovoNumeroFilial;
                                string novoNomeFilial;
                                string novoIpFilial;
                                string confirmaModemFilial = "False";
                                string confirmaRelogioFilial = "False";

                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("-----------Editar Filiais-----------");
                                    Console.WriteLine("O que deseja alterar");
                                    Console.WriteLine("1- Numero da filial");
                                    Console.WriteLine("2- Nome da filial");
                                    Console.WriteLine("3- IP da filial");
                                    Console.WriteLine("4- Modem da filial");
                                    Console.WriteLine("5- Relogio da filial");
                                    Console.WriteLine("6- Voltar");
                                    comando3 = LerNumero("Comando: ");
                                    switch (comando3)
                                    {
                                        case 1:
                                            Console.Clear();
                                            Console.WriteLine("-----------Editar Filiais-----------");
                                            novoNumeroFilial = LerNumero("Digite o novo numero da filial: ");
                                            confirmaNovoNumeroFilial = ConfereNumeroFilialExistente(novoNumeroFilial);
                                            listaFiliais[i].Numero = confirmaNovoNumeroFilial;
                                            Console.WriteLine("Novo numero salvo com sucesso!");
                                            Console.Write("Digite qualquer tecla para continuar...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            Console.WriteLine("-----------Editar Filiais-----------");
                                            Console.Write("Digite o novo nome da filial: ");
                                            novoNomeFilial = Console.ReadLine();
                                            listaFiliais[i].Nome = novoNomeFilial;
                                            Console.Write("Digite qualquer tecla para continuar...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        case 3:
                                            Console.Clear();
                                            Console.WriteLine("-----------Editar Filiais-----------");
                                            Console.Write("Digite o novo IP da filial: ");
                                            novoIpFilial = Console.ReadLine();
                                            listaFiliais[i].Ip = novoIpFilial;
                                            Console.Write("Digite qualquer tecla para continuar...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        case 4:
                                            bool novoModemFilial = false;
                                            Console.Clear();
                                            Console.WriteLine("-----------Editar Filiais-----------");
                                            while (confirmaModemFilial != "s" && confirmaModemFilial != "n" && confirmaModemFilial != "S" && confirmaModemFilial != "N")
                                            {
                                                Console.Write("A filial possuí modem? 'S' ou 'N': ");
                                                confirmaModemFilial = Console.ReadLine();
                                                if (confirmaModemFilial == "s" || confirmaModemFilial == "S")
                                                {
                                                    novoModemFilial = true;
                                                }
                                            }
                                            listaFiliais[i].Modem = novoModemFilial;
                                            Console.Write("Digite qualquer tecla para continuar...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        case 5:
                                            bool novoRelogioFilial = false;
                                            Console.Clear();
                                            Console.WriteLine("-----------Editar Filiais-----------");
                                            while (confirmaRelogioFilial != "s" && confirmaRelogioFilial != "n" && confirmaRelogioFilial != "S" && confirmaRelogioFilial != "N")
                                            {
                                                Console.Write("A filial possuí relogio? 'S' ou 'N': ");
                                                confirmaRelogioFilial = Console.ReadLine();
                                                if (confirmaModemFilial == "s" || confirmaModemFilial == "S")
                                                {
                                                    novoRelogioFilial = true;
                                                }
                                            }
                                            listaFiliais[i].Relogio = novoRelogioFilial;
                                            Console.Write("Digite qualquer tecla para continuar...");
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                    }
                                    Console.Clear();
                                } while (comando3 != 6);

                                System.IO.File.Delete("Filiais.csv");
                                dados.EscritaDadosFiliais(listaFiliais, "Filiais.csv");

                                break;
                            }
                            else if (i + 1 == listaFiliais.Count)
                            {
                                Console.Clear();
                                Console.WriteLine("-----------Editar Filiais-----------");
                                Console.WriteLine($"Nenhuma filial com o numero {confereNumeroFilial} foi encontrada...");
                                Console.Write("Digite qualquer tecla para continuar...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case 4:
                        int contOn = 0;
                        int contOff = 0;
                        Console.Clear();
                        Console.WriteLine("-----------Verificar IP Ativo-----------");
                        for (int i = 0; i < listaFiliais.Count; i++)
                        {
                            if (listaFiliais[i].Ip.Length >= 9)
                            {
                                var ip = listaFiliais[i].Ip;
                                var status = VerificarPing(ip) ? "Ativo" : "Inativo";
                                listaFiliais[i].Estado = status;
                                Console.WriteLine($"{listaFiliais[i].Nome} / {ip} / {status}");
                                if (status == "Ativo") 
                                    contOn++;
                                else 
                                    contOff++;
                            }
 
                        }
                        Console.WriteLine("-------------------------------");
                        Console.Write($"Total de Relógios Ativos: {contOn}\n");
                        Console.Write($"Total de Relógios Inativos: {contOff}\n");
                        Console.WriteLine("-------------------------------");
                        Console.Write("Digite qualquer tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (comando != 5);
        }

        private void SortFiliais()
        {
            listaFiliais.Sort((f1, f2) => f1.Numero.CompareTo(f2.Numero));
        }

        private int ConfereNumeroFilialExistente(int numeroFilial)
        {

            for (int i = 0; i < listaFiliais.Count; i++)
            {
                if (numeroFilial == listaFiliais[i].Numero)
                {
                    Console.WriteLine("Numero da filial já cadastrada! Digite novamente");
                    numeroFilial = LerNumero("Numero filial: ");
                    i = 0;
                }
            }
            return numeroFilial;

        }

        private bool VerificarPing(string ip)
        {
            try
            {
                using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
                {
                    var resposta = ping.Send(ip, 1000);
                    return resposta.Status == System.Net.NetworkInformation.IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private int LerNumero(string mensagem)
        {
            int numero;
            string entrada;
            while (true)
            {
                Console.Write(mensagem);
                entrada = Console.ReadLine();
                if (int.TryParse(entrada, out numero))
                    return numero;
                else
                    Console.WriteLine("Entrada inválida. Tente novamente.");
            }
        }
    }
}
