namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ricoveri")]
    public partial class Ricoveri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ricoveri()
        {
            RimborsiRicoveri = new HashSet<RimborsiRicoveri>();
        }

        public int ID { get; set; }

        public int IDAnimale { get; set; }

        public DateTime DataInizioRicovero { get; set; }

        public string Foto { get; set; }

        public virtual Animali Animali { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RimborsiRicoveri> RimborsiRicoveri { get; set; }
    }
}
