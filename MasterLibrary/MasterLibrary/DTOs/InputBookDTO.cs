using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class InputBookDTO
    {
        public InputBookDTO() { }

        public int IDBook { get; set; }
        public string TenSach { get; set; }
        public int GiaNhap { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgNhap { get; set; }
    }
}
