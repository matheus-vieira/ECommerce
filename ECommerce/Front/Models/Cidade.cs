using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        public string Nome { get; set; }
        public string CodigoIbge { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}