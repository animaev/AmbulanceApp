namespace AmbulanceApp.ModelBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Call")]
    public partial class Call
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string number { get; set; }

        [Required]
        public string adress { get; set; }

        public string phone { get; set; }
    }
}
