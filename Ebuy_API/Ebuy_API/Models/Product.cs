namespace Ebuy_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int product_id { get; set; }

        [StringLength(50)]
        public string product_name { get; set; }

        public int? price { get; set; }

        [StringLength(350)]
        public string description { get; set; }

        public string image { get; set; }

        public int? cat_id { get; set; }

        public virtual Category Category { get; set; }
    }
}
