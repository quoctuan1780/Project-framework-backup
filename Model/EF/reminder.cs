namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coffee-house.reminders")]
    public partial class reminder
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Column(TypeName = "uint")]
        public long user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string code { get; set; }

        public bool completed { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? completed_at { get; set; }
    }
}
