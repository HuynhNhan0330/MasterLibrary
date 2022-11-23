using Metsys.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterLibrary.DTOs;
using System.Data.Entity;
<<<<<<< HEAD
=======
using MasterLibrary.Views.LoginWindow;
using System.Windows;
>>>>>>> hoangminh

namespace MasterLibrary.Models.DataProvider
{
    public class CustormerServices
    {
        private CustormerServices() { }
        private static CustormerServices _ins;
<<<<<<< HEAD
=======

>>>>>>> hoangminh
        public static CustormerServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustormerServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string, CustomerDTO)> Login(string username, string password)
        {
            try
            {
<<<<<<< HEAD
                using (var context = new MasterLibraryModelsEntities())
                {
                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var cus = await(from s in context.Users
                                      where s.UserName == username && s.Password == password && s.isRole == false
                                      select new CustomerDTO
                                      {
                                          Id = s.Id,
                                          Name = s.DisplayName,
                                          Email = s.Email,
                                          User = s.UserName,
                                          Password = s.Password
=======
                using (var context = new MasterlibraryEntities())
                {
                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var cus = await(from s in context.KHACHHANGs
                                      where s.USERNAME == username && s.USERPASSWORD == password && s.IDROLE == 2
                                      select new CustomerDTO
                                      {
                                          MAKH = s.MAKH
>>>>>>> hoangminh
                                      }).FirstOrDefaultAsync();



                    if (cus == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu", null);
                    }
                    return (true, "", cus);
                }

            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }
<<<<<<< HEAD
=======

        public void Register(string fullname, string email, string username, string pass)
        {
            KHACHHANG cus = new KHACHHANG();
            cus.USERNAME = username;
            cus.USERPASSWORD = pass;
            cus.TENKH = fullname;
            cus.IDROLE = 2;
            cus.EMAIL = email;

            using (var context = new MasterlibraryEntities())
            {
                context.KHACHHANGs.Add(cus);
                context.SaveChanges();
                MessageBox.Show("Đăng ký thành công!");
            }    
        }    
>>>>>>> hoangminh
    }
}
