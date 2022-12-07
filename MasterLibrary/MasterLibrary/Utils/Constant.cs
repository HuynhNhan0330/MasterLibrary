using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Utils
{
    public class ROLE
    {
        public static readonly string Admin = "Quản lý";
        public static readonly string Customer = "Khách hàng";

        
    }

    public class baseBook
    {
        public static readonly List<string> ListTheLoai = new List<string>
        {
            "Chính trị",
            "Khoa học",
            "Kinh tế",
            "Văn học",
            "Lịch sử",
            "Tiểu thuyết",
            "Tâm lý",
            "Sách thiếu nhi"
        };
    }
}
