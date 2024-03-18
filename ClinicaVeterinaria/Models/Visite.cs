namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visite")]
    public partial class Visite
    {
        public int ID { get; set; }

        public int IDAnimale { get; set; }

        public DateTime DataVisita { get; set; }

        public string EsameObiettivo { get; set; }

        public string CuraPrescritta { get; set; }

        public virtual Animali Animali { get; set; }
    }
}
