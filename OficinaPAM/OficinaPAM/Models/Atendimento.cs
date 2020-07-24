using System;
using System.Collections.Generic;
using System.Text;

namespace OficinaPAM.Models
{
    public class Atendimento
    {
        public int? AtendimentoID { get; set; }
        public int? ClienteID { get; set; }
        public string Veiculo { get; set; }
        public DateTime DataHoraChegada { get; set; }
        public DateTime DataHoraPrometida { get; set; }
        public DateTime? DataHoraEntrega { get; set; }
        public Cliente Cliente { get; set; }
        public virtual List<AtendimentoFoto> Fotos { get; set; }
        public bool EstaFinalizado => DataHoraEntrega != null;
                
        public Atendimento()
        {
            this.DataHoraChegada = DateTime.Now;
            this.DataHoraPrometida = DateTime.Now;
            this.DataHoraEntrega = null;
            this.Fotos = new List<AtendimentoFoto>();
        }
    }
}
