using System.Collections.Generic;

namespace Front.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }

        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}