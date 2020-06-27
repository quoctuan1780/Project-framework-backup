namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.slide")]
    public partial class slide
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string link { get; set; }

        [Required]
        [StringLength(100)]
        public string hinhanh { get; set; }
    }
}
