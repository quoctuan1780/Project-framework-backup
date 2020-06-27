namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.gioithieu")]
    public partial class gioithieu
    {
        [Key]
        [Column(TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long magt { get; set; }

        public int? nam { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string tieude { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string noidung { get; set; }

        [StringLength(255)]
        public string hinhanh { get; set; }
    }
}
