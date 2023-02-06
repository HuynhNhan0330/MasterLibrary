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
using MasterLibrary.Views.MessageBoxML;

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
                    string _HashPassword = Utils.Helper.HashPassword(password);

                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var cus = await(from s in context.KHACHHANGs
                                      where s.USERNAME == username && s.USERPASSWORD == _HashPassword && s.IDROLE == 2
                                      select new CustomerDTO
                                      {
                                          MAKH = s.MAKH,
                                          TENKH= s.TENKH,
                                          EMAIL= s.EMAIL,
                                          USERNAME= s.USERNAME,
                                          USERPASSWORD= s.USERPASSWORD,
                                          DIACHI = s.DIACHI,
                                      }).FirstOrDefaultAsync();

                    if (cus == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu", null);
                    }
                    else
                    {
                        return (true, "", cus);
                    }
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
                MessageBoxML ms = new MessageBoxML("Thông báo", "Thông tin bị trống vui lòng nhập thêm.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            KHACHHANG cus = new KHACHHANG();
            cus.USERNAME = username;
            cus.USERPASSWORD = Utils.Helper.HashPassword(pass);
            cus.TENKH = fullname;
            cus.IDROLE = 2;
            cus.EMAIL = email;

            using (var context = new MasterlibraryEntities())
            {
                context.KHACHHANGs.Add(cus);
                context.SaveChanges();
                MessageBoxML ms = new MessageBoxML("Thông báo", "Đăng ký thành công!", MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }    
        }

        public async Task<CustomerDTO> FindCustomer(int MaKH)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = await (from s in context.KHACHHANGs
                                     where s.MAKH == MaKH
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();

                    return cus;
                }
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Không tìm thấy khách hàng", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return null;
            }
        }

        public async Task<bool> CheckEmailCustormer(string _email, int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = await (from s in context.KHACHHANGs
                                     where s.EMAIL == _email && s.MAKH != _makh
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();
                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }

        public async Task<bool> updateCustomer(int _makh, string _tenkh, string _email, string _diachi)
        {
            try
            {
                // Cập nhật thông tin
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return false;

                    cus.TENKH = _tenkh;
                    cus.EMAIL = _email;
                    cus.DIACHI= _diachi;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(bool, string)> ChangePassword(int _makh, string _newpassword)
        {
            try
            {
                // Đổi mật khẩu
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return (false, "Không tìm thấy khách hàng");

                    cus.USERPASSWORD = Utils.Helper.HashPassword(_newpassword);
                    
                    context.SaveChanges();
                    return (true, "Đổi mật khẩu thành công");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }
    }
}
