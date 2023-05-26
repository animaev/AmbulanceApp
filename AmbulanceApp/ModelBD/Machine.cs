namespace AmbulanceApp.ModelBD
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
        [StringLength(10)]
        public string number { get; set; }

        public bool busy { get; set; }

        public string ActualText
        {
            get
            {
                return busy ? "На вызове" : "Свободна";
            }
        }
    }
}
