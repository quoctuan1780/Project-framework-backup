namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.donhang")]
    public partial class donhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public donhang()
        {
            ctdhs = new HashSet<ctdh>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long madh { get; set; }

        [Column(TypeName = "uint")]
        public long? makh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaydat { get; set; }

        public float? tongtien { get; set; }

        [StringLength(200)]
        public string httt { get; set; }

        [StringLength(500)]
        public string ghichu { get; set; }

        public int tttt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ctdh> ctdhs { get; set; }

        public virtual khachhang khachhang { get; set; }
    }
}
