using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Condutores
    {
        public Condutores()
        {
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]

        public int ID { get; set; } // chave primaria 

        //dados proprios do condutor 
        public string Nome { get; set; }

        public string BI { get; set; }

        public string Telemovel { get; set; }

        public DateTime DataNascimento { get; set; }

        // dados carta de conduçao do condutor 
        public string numCartaConducao { get; set; }

        public string LocalEmissao { get; set; }

        public DateTime DataValidadeCarta { get; set; }

        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}