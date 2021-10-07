using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDbLib.Models
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(80), Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Order() {}
    }
}
