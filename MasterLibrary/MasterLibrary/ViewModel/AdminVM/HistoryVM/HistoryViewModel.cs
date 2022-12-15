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
        public ICommand closeML { get; set; }
        #endregion

        public HistoryViewModel()
        {
            GetCurrentDate = DateTime.Today;
            SelectedRevenueDate = GetCurrentDate;
            SelectedRevenueMonth = DateTime.Now.Month - 1;
            SelectedExpenseMonth = DateTime.Now.Month - 1;

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
                await GetExpenseListSource();
                IsGettingSource = false;
                ExpensePage page = new ExpensePage();
                p.Content = page;
            });

            LoadRevenuePage = new RelayCommand<Frame>((p) => { return true; }, async(p) =>
            {
                view = 1;
                IsGettingSource = true;
                ListRevenue = new ObservableCollection<BillDTO>();
                await GetRevenueListSource("date");
                IsGettingSource=false;
                RevenuePage page = new RevenuePage();
                p.Content = page;
            });

            ExportFileML = new RelayCommand<object>((p) => { return true; }, async(p) =>
            {
                ExportFile();
            });
            
        }

        public async Task checkExpenseMonthFilter()
        {

        }

        public async Task checkRevenueMonthFilter()
        {

        }

        public async Task GetRevenueListSource(string s="")
        {
            ListRevenue = new ObservableCollection<BillDTO>();
            switch(s)
            {
                case "":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListRevenue = new ObservableCollection<BillDTO>((System.Collections.Generic.IEnumerable<BillDTO>)await InputBookServices.Ins.GetBookInput());
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
            }
        }

        public async Task checkExpenseFilter()
        {

        }

        public async Task checkRevenueFilter()
        {

        }

        public async Task GetExpenseListSource()
        {

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
