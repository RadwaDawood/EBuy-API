namespace Ebuy_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int admin_id { get; set; }

        [StringLength(50)]
        public string admin_name { get; set; }

        [StringLength(50)]
        public string password { get; set; }
    }
}
