namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RimborsiRicoveri")]
    public partial class RimborsiRicoveri
    {
        public int ID { get; set; }

        public int IDRicovero { get; set; }

        public DateTime DataRimborso { get; set; }

        public decimal Importo { get; set; }

        public virtual Ricoveri Ricoveri { get; set; }
    }
}
