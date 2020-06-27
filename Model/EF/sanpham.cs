namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.sanpham")]
    public partial class sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sanpham()
        {
            ctdhs = new HashSet<ctdh>();
            cthds = new HashSet<cthd>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long masp { get; set; }

        [StringLength(100)]
        public string tensp { get; set; }

        [Column(TypeName = "uint")]
        public long? maloaisp { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string mota { get; set; }

        public float? gia { get; set; }

        public float? giakm { get; set; }

        [StringLength(255)]
        public string hinhanh { get; set; }

        [StringLength(255)]
        public string dvt { get; set; }

        public sbyte? moi { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaynhap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ctdh> ctdhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cthd> cthds { get; set; }

        public virtual loaisanpham loaisanpham { get; set; }
    }
}
