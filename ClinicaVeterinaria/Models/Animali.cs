namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Animali")]
    public partial class Animali
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animali()
        {
            Ricoveri = new HashSet<Ricoveri>();
            Visite = new HashSet<Visite>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipologia { get; set; }

        [StringLength(50)]
        public string ColoreMantello { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascita { get; set; }

        public bool? Microchip { get; set; }

        [StringLength(50)]
        public string NumeroMicrochip { get; set; }

        [StringLength(100)]
        public string NomeProprietario { get; set; }

        [StringLength(100)]
        public string CognomeProprietario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ricoveri> Ricoveri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visite> Visite { get; set; }
    }
}
