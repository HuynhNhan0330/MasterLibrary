
using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class AdminServices
    {
        private AdminServices() { }
        private static AdminServices _ins;
        public static AdminServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AdminServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> Login(string username, string password)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var admin = await (from s in context.KHACHHANGs
                                       where s.USERNAME == username && s.USERPASSWORD == password && s.IDROLE == 1
                                       select new AdminDTO
                                       {
                                           MAKH = s.MAKH
                                       }).FirstOrDefaultAsync();



                    if (admin == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu");
                    }
                    return (true, "");
                }

            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống");
            }
        }

    }
}
