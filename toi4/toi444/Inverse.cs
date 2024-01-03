namespace toi444
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inverse")]
    public partial class Inverse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string docs { get; set; }

        public int wordId { get; set; }

        public virtual Word Word { get; set; }
    }
}
