using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Multas
    {
        [Key]
        
        public int ID { get; set; } // chave primaria 

        //dados da multa

        public string Infracao { get; set; }

        public string LocalDaMulta { get; set; }

        public decimal ValorMulta { get; set; }

        public DateTime DataDaMulta { get; set; }

        //construçao das chaves forasteiras 
        [ForeignKey("Agente")]
        //FK Agentes
        //ForeignKey NomeAtributoQueEFK references TABELA(PkDaTabela)
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }

        [ForeignKey("Viatura")]
        //FK Viatura
        public int ViaturaFK { get; set; }
        public virtual Viaturas Viatura { get; set; }

        [ForeignKey("Condutor")]
        //FK Condutores
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }




    }
}