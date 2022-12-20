using MasterLibrary.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using ComboBox = System.Windows.Controls.ComboBox;
using DatePicker = System.Windows.Controls.DatePicker;
using ComboBoxItem = System.Windows.Controls.ComboBoxItem;
using Grid = System.Windows.Controls.Grid;
using MasterLibrary.Views.Admin.HistoryPage;
using Microsoft.Win32;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using MasterLibrary.Views.MessageBoxML;
using MasterLibrary.Models.DataProvider;
using System.Windows.Navigation;
using System.Windows;

namespace MasterLibrary.ViewModel.AdminVM.HistoryVM
{
    public class HistoryViewModel : BaseViewModel
    {
        #region property
        private int view = 0;
        private bool _IsGettingSource;
        public bool IsGettingSource 
        {
            get => _IsGettingSource;
            set { _IsGettingSource = value; OnPropertyChanged(); }
        }

        private DateTime _getCurrentDate;
        public DateTime GetCurrentDate
        {
            get => _getCurrentDate; 
            set { _getCurrentDate = value; }
        }

        private string _setCurrentDate;
        public string SetCurrentDate
        {
            get => _setCurrentDate;
            set { _setCurrentDate = value; }
        }

        private DateTime _SelectedRevenueDate;
        public DateTime SelectedRevenueDate
        {
            get => _SelectedRevenueDate;
            set { _SelectedRevenueDate = value; OnPropertyChanged(); } 
        }

        private ComboBoxItem _SelectedRevenueFilter;
        public ComboBoxItem SelectedRevenueFilter
        { 
            get => _SelectedRevenueFilter; 
            set { _SelectedRevenueFilter = value; OnPropertyChanged(); } 
        }

        private ComboBoxItem _SelectedExpenseFilter;
        public ComboBoxItem SelectedExpenseFilter
        {
            get => _SelectedExpenseFilter;
            set { _SelectedExpenseFilter = value; OnPropertyChanged(); }
        }

        private int _SelectedRevenueMonth;
        public int SelectedRevenueMonth
        { 
            get => _SelectedRevenueMonth; 
            set { _SelectedRevenueMonth = value; OnPropertyChanged(); } 
        }

        private int _SelectedExpenseMonth;
        public int SelectedExpenseMonth
        {
            get => _SelectedExpenseMonth;
            set { _SelectedExpenseMonth = value; OnPropertyChanged(); }
        }

        private BillDTO _SelectedItemRevenue;
        public BillDTO SelectedItemRevenue
        {
            get => _SelectedItemRevenue;
            set { _SelectedItemRevenue = value; OnPropertyChanged(); }
        }

        private BillDTO _DetailRevenue;
        public BillDTO DetailRevenue
        {
            get => _DetailRevenue;
            set { _DetailRevenue = value; OnPropertyChanged(); }
        }

        public static Grid MaskName { get; set; }

        private ObservableCollection<InputBookDTO> _ListExpense;
        public ObservableCollection<InputBookDTO> ListExpense
        {
            get => _ListExpense;
            set { _ListExpense = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BillDTO> _ListRevenue;
        public ObservableCollection<BillDTO> ListRevenue
        {
            get => _ListRevenue;
            set { _ListRevenue = value; OnPropertyChanged(); }
        }

        #endregion

        #region Icommand
        public ICommand LoadExpensePage { get; set; }
        public ICommand LoadRevenuePage { get; set; }
        public ICommand ExportFileML { get; set; }
        public ICommand MaskNameML { get; set; }
        public ICommand CheckSelectedExpenseFilterML { get; set; }
        public ICommand SelectedExpenseMonthML { get; set; }
        public ICommand CheckSelectedRevenueFilterML { get; set; }
        public ICommand SelectedRevenueMonthML { get; set; }
        public ICommand SelectedRevenueDateML { get; set; }
        public ICommand LoadInforRevenueML { get; set; }
        public ICommand closeML { get; set; }
        #endregion

        public HistoryViewModel()
        {
            GetCurrentDate = DateTime.Today;
            SelectedRevenueDate = GetCurrentDate;
            SelectedRevenueMonth = DateTime.Now.Month - 1;
            SelectedExpenseMonth = DateTime.Now.Month - 1;

            MaskNameML = new RelayCommand<Grid>((p) => { return true; }, (p) => { MaskName = p; });
            closeML = new RelayCommand<Window>((p) => { return true; }, (p) =>
            { 
                MaskName.Visibility = Visibility.Collapsed;
                SelectedItemRevenue = null;
                p.Close();
            });

            SelectedExpenseMonthML = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await checkExpenseMonthFilter();
            });

            SelectedRevenueMonthML = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await checkRevenueMonthFilter();
            });

            SelectedRevenueDateML = new RelayCommand<DatePicker>((p) => { return true; }, async (p) => 
            {
                await GetRevenueListSource("date");
            });

            CheckSelectedExpenseFilterML = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await checkExpenseFilter();
            });

            CheckSelectedRevenueFilterML = new RelayCommand<ComboBox>((p) => { return true; }, async (p) => 
            {
                await checkRevenueFilter();
            });

            LoadExpensePage = new RelayCommand<Frame>((p) => { return true; }, async (p) => 
            {
                view = 0;
                IsGettingSource = true;
                ListExpense = new ObservableCollection<InputBookDTO>();
                await GetExpenseListSource("");
                IsGettingSource = false;
                ExpensePage page = new ExpensePage();
                p.Content = page;
            });

            LoadRevenuePage = new RelayCommand<Frame>((p) => { return true; }, async(p) =>
            {
                view = 1;
                IsGettingSource = true;
                ListRevenue = new ObservableCollection<BillDTO>();
                await GetRevenueListSource("");
                IsGettingSource=false;
                RevenuePage page = new RevenuePage();
                p.Content = page;
            });

            ExportFileML = new RelayCommand<object>((p) => { return true; }, async(p) =>
            {
                ExportFile();
            });

            LoadInforRevenueML = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedItemRevenue != null)
                {
                    try
                    {
                        IsGettingSource = true;
                        //DetailRevenue = await Task.Run(() => BillServices.Ins.GetDetail(SelectedItemRevenue.MAHD));
                        IsGettingSource = false;
                    }
                    catch (System.Data.Entity.Core.EntityException e)
                    {
                        MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        throw;
                    }
                    catch
                    {
                        MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        throw;
                    }
                    RevenueDetail rd = new RevenueDetail();
                    rd.idBill.Content = DetailRevenue.MAHD;
                }
            });

        }

        public async Task checkExpenseMonthFilter()
        {
            try
            {
                ListExpense = new ObservableCollection<InputBookDTO>(await InputBookServices.Ins.GetBookInput(SelectedExpenseMonth + 1));
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch
            {
                MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }

        public async Task checkRevenueMonthFilter()
        {
            try
            {
                ListRevenue = new ObservableCollection<BillDTO>(await BillServices.Ins.GetBillByMonth(SelectedRevenueMonth + 1));
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch
            {
                MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }

        public async Task GetRevenueListSource(string s="")
        {
            ListRevenue = new ObservableCollection<BillDTO>();
            switch(s)
            {
                case "date":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListRevenue = new ObservableCollection<BillDTO>((System.Collections.Generic.IEnumerable<BillDTO>)await BillServices.Ins.GetBillByDate(SelectedRevenueDate));
                            IsGettingSource = false;
                            return;
                        }
                        catch(System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "month":
                    {
                        IsGettingSource = true;
                        await checkRevenueMonthFilter();
                        IsGettingSource = false;
                        return;
                    }
                case "":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListRevenue = new ObservableCollection<BillDTO>((System.Collections.Generic.IEnumerable<BillDTO>)await BillServices.Ins.GetAllBill());
                            IsGettingSource = false;
                            return;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
            }
        }

        public async Task GetExpenseListSource(string s = "")
        {
            ListExpense = new ObservableCollection<InputBookDTO>();
            switch (s)
            {
                case "":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListExpense = new ObservableCollection<InputBookDTO>(await InputBookServices.Ins.GetBookInput());
                            IsGettingSource = false;
                            return;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "month":
                    {
                        IsGettingSource = true;
                        await checkExpenseMonthFilter();
                        IsGettingSource = false;
                        return;
                    }
            }
        }

        public async Task checkExpenseFilter()
        {
            switch(SelectedExpenseFilter.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetExpenseListSource("");
                        return;
                    }
                case "Theo tháng":
                    {
                        await GetExpenseListSource("month");
                        return;
                    }
            }
        }

        public async Task checkRevenueFilter()
        {
            switch(SelectedRevenueFilter.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetRevenueListSource("");
                        return;
                    }
                case "Theo ngày":
                    {
                        await GetRevenueListSource("date");
                        return;
                    }
                case "Theo tháng":
                    {
                        await GetRevenueListSource("month");
                        return;
                    }
            }
        }

        public void ExportFile()
        {
            switch(view)
            {
                case 0:
                    {
                        SaveFileDialog sf = new SaveFileDialog
                        {
                            Filter = "Excel |*.xlxs",
                            ValidateNames = true
                        };
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                            app.Visible = false;
                            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                            ws.Cells[1, 1] = "Mã đơn";
                            ws.Cells[1, 2] = "Tên sách";
                            ws.Cells[1, 3] = "Ngày nhập";
                            ws.Cells[1, 4] = "Số lượng";
                            ws.Cells[1, 5] = "Giá nhập";

                            int count = 2;
                            foreach (var item in ListExpense)
                            {
                                ws.Cells[count, 1] = item.IDInput;
                                ws.Cells[count, 2] = item.TenSach;
                                ws.Cells[count, 3] = item.NgNhap;
                                ws.Cells[count, 4] = item.SoLuong;
                                ws.Cells[count, 5] = item.GiaNhap;

                                count++;
                            }
                            ws.SaveAs(sf.FileName);
                            wb.Close();
                            app.Quit();

                            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                            MessageBoxML mb = new MessageBoxML("Thông báo", "Xuất file thành công", MessageType.Accept, MessageButtons.OK);
                            mb.ShowDialog();
                        }
                        break;
                    }

                case 1:
                    {
                        SaveFileDialog sf = new SaveFileDialog
                        {
                            Filter = "Excel |*.xlxs",
                            ValidateNames = true
                        };
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                            app.Visible = false;
                            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                            ws.Cells[1, 1] = "Mã đơn";
                            ws.Cells[1, 2] = "Mã khách hàng";
                            ws.Cells[1, 3] = "Tên khách hàng";
                            ws.Cells[1, 4] = "Ngày bán";
                            ws.Cells[1, 5] = "Tổng giá";

                            int count = 2;
                            foreach (var item in ListRevenue)
                            {
                                ws.Cells[count, 1] = item.MAHD;
                                ws.Cells[count, 2] = item.MAKH;
                                ws.Cells[count, 3] = item.cusName;
                                ws.Cells[count, 4] = item.NGHD;
                                ws.Cells[count, 5] = item.TRIGIA;

                                count++;
                            }
                            ws.SaveAs(sf.FileName);
                            wb.Close();
                            app.Quit();

                            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                            MessageBoxML mb = new MessageBoxML("Thông báo", "Xuất file thành công", MessageType.Accept, MessageButtons.OK);
                            mb.ShowDialog();
                        }
                        break;
                    }
            }
        }

    }
}
