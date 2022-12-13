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
        private string _cusID { get; set; }
        public string cusID
        {
            get
            {
                if (_cusID == null) return "-1";
                return _cusID;
            }
            set
            {
                _cusID = value;
            }
        }
        public int MAHD { get; set; }
        public decimal TRIGIA { get; set; }
        public DateTime NGHD { get; set; }

        public string TRIGIAstr
        {
            get { return Helper.FormatVNMoney(TRIGIA); }
        }
    }
}
