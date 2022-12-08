using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BillDTO
    {
        public BillDTO()
        {

        }

        public int MAKH { get; set; }
        public int MAHD { get; set; }
        public int TRIGIA { get; set; }

        public string TRIGIAstr
        {
            get { return Helper.FormatVNMoney(TRIGIA); }
        }
    }


}
