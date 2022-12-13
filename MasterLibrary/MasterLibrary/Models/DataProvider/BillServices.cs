using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterLibrary.Models.DataProvider
{
    public class BillServices
    {
        private static BillServices _ins;
        public static BillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }
    }
}
