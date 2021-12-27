namespace Ebuy_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        public int user_id { get; set; }

        [StringLength(50)]
        public string user_name { get; set; }

        [StringLength(320)]
        public string email { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        [StringLength(150)]
        public string address { get; set; }

        [StringLength(11)]
        public string phone { get; set; }

        public int? age { get; set; }
    }
}
