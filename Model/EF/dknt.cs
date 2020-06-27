namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.dknt")]
    public partial class dknt
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string email { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngaydk { get; set; }
    }
}
