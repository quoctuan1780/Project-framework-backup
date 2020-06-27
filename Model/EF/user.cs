namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.users")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            khachhangs = new HashSet<khachhang>();
        }

        [Column(TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string tentk { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        [StringLength(100)]
        public string remember_token { get; set; }

        [Column(TypeName = "uint")]
        public long? maquyen { get; set; }

        public int ttdn { get; set; }

        [StringLength(255)]
        public string hinhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<khachhang> khachhangs { get; set; }

        public virtual quyen quyen { get; set; }

        public virtual quyen quyen1 { get; set; }
    }
}
