namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.loaisanpham")]
    public partial class loaisanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loaisanpham()
        {
            sanphams = new HashSet<sanpham>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long maloaisp { get; set; }

        [Required]
        [StringLength(100)]
        public string tenloaisp { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string mota { get; set; }

        [StringLength(255)]
        public string hinhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sanpham> sanphams { get; set; }
    }
}
