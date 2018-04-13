using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes
    {
        public Agentes()
        {
            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        public int ID { get; set; } // chave primaria 
            
        [Required(ErrorMessage ="O {0} é de preenchimento obrigatorio")] // o atributo nome e de preenchimento obrigatorio
        [RegularExpression("[A-ZÂÍ][a-záéíóúãõàèìòâêîôûäëïöüç]+(( | de | da | dos | d' |-|. )[A-ZÂÍ][a-záéíóúãõàèìòâêîôûäëïöüç]+){1,3}", ErrorMessage ="Nome invalido.Cada palvara começa por uma maiuscula, seguida de minuscula")]
        [StringLength(40)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatorio")] // o atributo nome e de preenchimento obrigatorio
        public string Fotografia { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatorio")] // o atributo nome e de preenchimento obrigatorio
        [RegularExpression("[A-Za-z 0-9-]+",ErrorMessage ="Nome invalido")] 
        public string Esquadra { get; set; }

        //complementar a informaçao sobre o relacionamento de um agente com as multas por ele passadas
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}