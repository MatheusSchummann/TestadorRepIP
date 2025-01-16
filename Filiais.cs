using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelógioPontoExpress
{
    public class Filiais
    {
        private string nome;
        private string ip;
        private string estado;
        private int numero;
        private bool modem;
        private bool relogio;

        public Filiais(int numeroFilial, string nome, string ip, bool modem, bool relogio)
        {
            this.numero = numeroFilial;
            this.nome = nome;
            this.ip = ip;
            this.modem = modem;
            this.relogio = relogio; 
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }
        public string Ip
        {
            get { return this.ip; }
            set { this.ip = value; }
        }
        public string Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }
        public int Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }
        public bool Modem
        {
            get { return this.modem; }
            set { this.modem = value; }
        }
        public bool Relogio 
        {
            get { return this.relogio; }
            set { this.relogio = value; }
        }
    }
}
