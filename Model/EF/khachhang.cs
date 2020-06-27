namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.khachhang")]
    public partial class khachhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public khachhang()
        {
            donhangs = new HashSet<donhang>();
            hoadons = new HashSet<hoadon>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long makh { get; set; }

        [Required]
        [StringLength(100)]
        public string hoten { get; set; }

        [Required]
        [StringLength(10)]
        public string gioitinh { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string diachi { get; set; }

        [Required]
        [StringLength(20)]
        public string sodt { get; set; }

        [StringLength(200)]
        public string ghichu { get; set; }

        [Column(TypeName = "uint")]
        public long? matk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<donhang> donhangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hoadon> hoadons { get; set; }

        public virtual user user { get; set; }
    }
}
