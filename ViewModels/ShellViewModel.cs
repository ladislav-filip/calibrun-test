using System;
using System.Dynamic;
using System.Windows;
using CalibrumTest.ViewModels.eshop;
using Caliburn.Micro;
using DevExpress.Mvvm.DataAnnotations;

namespace CalibrumTest.ViewModels
{
    public class ShellViewModel : Conductor<object> //PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;

        private ProductViewModel _selectedProduct;

        private string _productName;

        private string _title;

        string name;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Products = new BindableCollection<ProductViewModel>()
            {
                new ProductViewModel() {Id = 1, Name = "Kafe"},
                new ProductViewModel() {Id = 2, Name = "Pivo"},
                new ProductViewModel() {Id = 3, Name = "Rum"},
                new ProductViewModel() {Id = 4, Name = "Chleba"},
                new ProductViewModel() {Id = 5, Name = "Rohlik"}
            };
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }

        public BindableCollection<ProductViewModel> Products
        {
            get; private set;
        }

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                _productName = value.Name;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => ProductName);
            }
        }

        public string ProductName
        {
            get { return _productName; }
        }

        public string Title
        {
            get { return _title; }
        }

        public void TitleChanged()
        {
            _title = DateTime.Now.ToString();
            NotifyOfPropertyChange(() => Title);
        }

        public void ModalDialog()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.MinWidth = 450;
            settings.Title = "Novy modální dialog";

            var model = new InfoViewModel();
            //IWindowManager mng = new WindowManager();
            _windowManager.ShowDialog(model, null, settings);            
        }

        public void ShowWindow()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            settings.MinWidth = 650;
            settings.Title = "Novy dialog";

            var model = new InfoViewModel();
            //IWindowManager mng = new WindowManager();
            _windowManager.ShowWindow(model, null, settings);
        }

        public void ShowPageOne()
        {
            var model = new PageOneViewModel();
            ActivateItem(model);
        }

        public void ShowEshop()
        {
            var model = new EshopViewModel();
            ActivateItem(model);
        }
    }
}