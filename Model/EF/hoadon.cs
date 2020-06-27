namespace Model.EF
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Table("coffee-house.hoadon")]
    public partial class hoadon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hoadon()
        {
            cthds = new HashSet<cthd>();
        }

        [Key]
        [Column(TypeName = "uint")]
        public long mahd { get; set; }

        [Column(TypeName = "uint")]
        public long makh { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaythanhtoan { get; set; }

        public float? tongtien { get; set; }

        [StringLength(200)]
        public string httt { get; set; }

        [StringLength(500)]
        public string ghichu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [JsonIgnore]
        public virtual ICollection<cthd> cthds { get; set; }

        [JsonIgnore]
        public virtual khachhang khachhang { get; set; }
    }
}
