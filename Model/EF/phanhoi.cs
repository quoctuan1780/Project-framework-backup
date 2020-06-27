namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.phanhoi")]
    public partial class phanhoi
    {
        [Key]
        [Column(TypeName = "uint")]
        public long maph { get; set; }

        [Required]
        [StringLength(200)]
        public string hoten { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string vande { get; set; }

        [Required]
        [StringLength(255)]
        public string noidung { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayph { get; set; }
    }
}
