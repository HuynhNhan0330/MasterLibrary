using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BillDTO
    {
        public int MaHD { get; set; }
        public DateTime NGHD { get; set; }
        public int MaKH { get; set; }
        public decimal TriGia { get; set; }
    }
}
