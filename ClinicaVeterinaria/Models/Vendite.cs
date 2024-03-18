namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendite")]
    public partial class Vendite
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string CodiceFiscaleCliente { get; set; }

        public int IDProdotto { get; set; }

        [StringLength(50)]
        public string NumeroRicettaMedica { get; set; }

        public DateTime DataVendita { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
