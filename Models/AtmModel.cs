using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;

namespace Models
{
    public class AtmModel : IModel
    {
        [Key]
        public int ModelDbId { get; set; }
        [Index(IsUnique = true)]
        public Guid Guid { get; set; }
    }
}