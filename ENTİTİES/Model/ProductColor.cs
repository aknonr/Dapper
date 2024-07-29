using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public string ProductColorId { get; set; }
        public string ProductId { get; set; }
        public DateTime TransferDate { get; set; }
        public bool IsTransferred { get; set; }
    }
}
