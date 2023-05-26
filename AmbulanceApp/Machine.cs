namespace AmbulanceApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Machine")]
    public partial class Machine
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string number { get; set; }

        public bool busy { get; set; }
    }
}
