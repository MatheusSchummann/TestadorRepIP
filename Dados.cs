
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelógioPontoExpress
{
    public class Dados
    {
        public void EscritaDadosFiliais(List<Filiais> filial, string arquivo)
        {
            StreamWriter sw = null;
            
            string linha;
            try
            {
                sw = new StreamWriter(arquivo);
                for (int i = 0; i < filial.Count; i++)
                {
                    linha = filial[i].Numero + ";" + filial[i].Nome + ";" + filial[i].Ip + ";" + filial[i].Modem + ";" + filial[i].Relogio;
                    sw.WriteLine(linha);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                sw.Close();
            }
        }
        public bool ExisteArquivo(string arquivo)
        {
            return File.Exists(arquivo);
        }
        public void LeituraDadosFiliais(string arquivo, List<Filiais> listaFiliais)
        {
            if (ExisteArquivo(arquivo))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        string linha;
                        int linhaAtual = 0;

                        while ((linha = sr.ReadLine()) != null)
                        {
                            linhaAtual++;

                            if (string.IsNullOrWhiteSpace(linha))
                                continue;
                            
                            string[] dados = linha.Split(';');

                            if (dados.Length != 5)
                            {
                                Console.WriteLine($"Linha {linhaAtual}: Número inválido de campos. Esperado: 5, Encontrado: {dados.Length}");
                                continue;
                             
                            }

                            try
                            {
                                int numero = int.Parse(dados[0]);
                                string nome = dados[1];
                                string ip = dados[2];
                                bool modem = false;
                                bool relogio = false;

                                if (dados[3] == "True") 
                                {
                                    modem = true;
                                }
                                if (dados[4] == "True")
                                {
                                    relogio = true;
                                }

                                Filiais filial = new Filiais(numero, nome, ip, modem, relogio);

                                listaFiliais.Add(filial);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao processar a linha {linhaAtual}: {ex.Message}");
                                Console.ReadKey();
                            }
                        }
                    }

                    Console.WriteLine("Dados das filiais carregados com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado.");
                Console.ReadKey();
            }
        }
    }


}
