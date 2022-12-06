using Metsys.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterLibrary.DTOs;
using System.Data.Entity;
using MasterLibrary.Views.LoginWindow;
using System.Windows;

namespace MasterLibrary.Models.DataProvider
{
    public class CustormerServices
    {
        private CustormerServices() { }
        private static CustormerServices _ins;

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
                using (var context = new MasterlibraryEntities())
                {
                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var cus = await(from s in context.KHACHHANGs
                                      where s.USERNAME == username && s.USERPASSWORD == password && s.IDROLE == 2
                                      select new CustomerDTO
                                      {
                                          MAKH = s.MAKH,
                                          TENKH= s.TENKH,
                                          EMAIL= s.EMAIL,
                                          USERNAME= s.USERNAME,
                                          USERPASSWORD= s.USERPASSWORD,
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
            catch (Exception)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }

        public void Register(string fullname, string email, string username, string pass)
        {
            if (string.IsNullOrEmpty(fullname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Thông tin bị trống vui lòng nhập thêm.");
                return;
            }

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
        
        //public async Task <CustomerDTO> Find(int MaKH)
        //{
        //    try
        //    {
        //        using (var context = new MasterlibraryEntities())
        //        {
        //            // lây thông tin nếu tài khoản, mật khẩu đúng
        //            var cus = await (from s in context.KHACHHANGs
        //                             where s.USERNAME == username && s.USERPASSWORD == password && s.IDROLE == 2
        //                             select new CustomerDTO
        //                             {
        //                                 MAKH = s.MAKH,
        //                                 TENKH = s.TENKH,
        //                                 EMAIL = s.EMAIL,
        //                                 USERNAME = s.USERNAME,
        //                                 USERPASSWORD = s.USERPASSWORD,
        //                             }).FirstOrDefaultAsync();

        //            if (cus == null)
        //            {
        //                return (false, "Sai tài khoản hoặc mật khẩu", null);
        //            }
        //            return (true, "", cus);
        //        }

        //    }
        //    catch (System.Data.Entity.Core.EntityException)
        //    {
        //        return (false, "Mất kết nối cơ sở dữ liệu", null);
        //    }
        //    catch (Exception)
        //    {
        //        return (false, "Lỗi hệ thống", null);
        //    }
        //}
    }
}
