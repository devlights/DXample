using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevExpress.Mvvm.POCO;

namespace DXample.Server.UI
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            NumberOfEmployees = 100;
        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public virtual decimal NumberOfEmployees
        {
            get;
            set;
        }
    }
}

