namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DisposizioneMedicinali")]
    public partial class DisposizioneMedicinali
    {
        public int ID { get; set; }

        public int IDProdotto { get; set; }

        public int CodiceArmadietto { get; set; }

        public int CodiceCassetto { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
