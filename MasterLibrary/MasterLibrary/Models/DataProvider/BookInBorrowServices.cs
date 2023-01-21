using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookInBorrowServices
    {
        private static BookInBorrowServices _ins;
        public static BookInBorrowServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookInBorrowServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        
    }
}
