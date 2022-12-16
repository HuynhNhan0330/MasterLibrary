using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using MasterLibrary.Views.Customer.ReportTroublePage;
using MasterLibrary.Views.MessageBoxML;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.ReportTroubleVM
{
    public partial class ReportTroubleViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<TroubleDTO> _ListTrouble;
        public ObservableCollection<TroubleDTO> ListTrouble
        {
            get { return _ListTrouble; }
            set { _ListTrouble = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TroubleDTO> _ListTrouble1;
        public ObservableCollection<TroubleDTO> ListTrouble1
        {
            get { return _ListTrouble1; }
            set { _ListTrouble1 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<StatusTroubleDTO> _ListStatusTrouble;
        public ObservableCollection<StatusTroubleDTO> ListStatusTrouble
        {
            get { return _ListStatusTrouble; }
            set { _ListStatusTrouble = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<TypeTroubleDTO> _ListTypeTrouble;
        public ObservableCollection<TypeTroubleDTO> ListTypeTrouble
        {
            get { return _ListTypeTrouble; }
            set { _ListTypeTrouble = value; OnPropertyChanged(); }
        }

        
        #endregion

        #region Icommand
        public ICommand FirstLoadReportTrouble { get; set; }
        public ICommand MaskNameReportTrouble { get; set; }
        public ICommand OpenAddTroubleCommand { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }

        #endregion

        public ReportTroubleViewModel()
        {
            #region ReportTrouble
            FirstLoadReportTrouble = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListTrouble = new ObservableCollection<TroubleDTO>(await TroubleServices.Ins.GetAllTroubleOfCustomer(MainCustomerViewModel.CurrentCustomer.MAKH));
                ListTrouble1 = new ObservableCollection<TroubleDTO>(ListTrouble);

                await loadStatusTrouble();
                await loadTypeTrouble();
            });

            MaskNameReportTrouble = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            OpenAddTroubleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                AddRTrouble w = new AddRTrouble();
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #endregion

            #region AddTrouble
            FirstLoadAddOrEditReport = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DayReportTrouble = DateTime.Now;
                resetPropertie();
            });

            UploadImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Title = "Chọn một tấm ảnh";
                openfile.Filter = "Image File (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg; *.png";
                if (openfile.ShowDialog() == true)
                {
                    filepath = openfile.FileName;
                    LoadImage();
                }
            });

            AddTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                string troubleImage = await CloudinaryService.Ins.UploadImage(filepath);

                if (troubleImage is null)
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", "Gặp vấn đề trong quá trình lưu ảnh. Vui lòng thử lại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                TroubleDTO newTrouble = new TroubleDTO()
                {
                    MaKH = MainCustomerViewModel.CurrentCustomer.MAKH,
                    TieuDe = TitleTrouble,
                    MoTa = DescribeTrouble,
                    NgayBaoCao = DayReportTrouble,
                    Img = troubleImage,
                    ChiPhi = 0,
                    TenTrangThaiSuCo = Utils.Trouble.STATUS.WAITTING,
                    TenLoaiSuCo = NameTypeTrouble
                };

                (bool isCreate, string lb) = await TroubleServices.Ins.CreateTrouble(newTrouble);

                if (isCreate == true)
                {
                    ListTrouble.Add(newTrouble);
                    ListTrouble1.Add(newTrouble);

                    MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });

            #endregion
        }

        public async Task loadStatusTrouble()
        {
            List<StatusTroubleDTO> _currentListStatusTrouble = new List<StatusTroubleDTO>
                {
                    new StatusTroubleDTO
                    {
                        MaTTSC = "0",
                        TenTrangThaiSuCo = "Toàn bộ"
                    }
                };

            _currentListStatusTrouble.AddRange(await TroubleServices.Ins.GetAllStatusTrouble());
            ListStatusTrouble = new ObservableCollection<StatusTroubleDTO>(_currentListStatusTrouble);
        }

        public async Task loadTypeTrouble()
        {
            List<TypeTroubleDTO> _currentListTypeTrouble = new List<TypeTroubleDTO>
                {
                    new TypeTroubleDTO
                    {
                        MaLSC = 0,
                        TenLoaiSuCo = "Toàn bộ"
                    }
                };

            _currentListTypeTrouble.AddRange(await TroubleServices.Ins.GetAllTypeTrouble());
            ListTypeTrouble = new ObservableCollection<TypeTroubleDTO>(_currentListTypeTrouble);
            ListTypeTroubleAddOrEdit = new ObservableCollection<TypeTroubleDTO>(await TroubleServices.Ins.GetAllTypeTrouble());
        }
    }
}
