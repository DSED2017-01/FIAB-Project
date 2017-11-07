
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ReactiveUI;
using System.Diagnostics;

namespace Excel_ImportWPF
{
    public class ViewModel : ReactiveObject
    {
        private string _CodeColumn;
        public string CodeColumn
        {
            get { return _CodeColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.code_flag = (value != string.Empty) ? true : false;

                this.RaiseAndSetIfChanged(ref _CodeColumn, value);
                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Code Column is " + _CodeColumn);
            }

        }

        private string _ScientificColumn;
        public string ScientificColumn
        {
            get { return _ScientificColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.scientific_flag = (value != string.Empty) ? true : false;
                this.RaiseAndSetIfChanged(ref _ScientificColumn, value);

                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Scientific Column is " + _ScientificColumn);
            }
        }

        private string _CommonColumn;
        public string CommonColumn
        {
            get { return _CommonColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.common_flag = (value != string.Empty) ? true : false;
                this.RaiseAndSetIfChanged(ref _CommonColumn, value);
                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Common Column is " + _CommonColumn);
            }
        }

        private string _SizeColumn;
        public string SizeColumn
        {
            get { return _SizeColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.size_flag = (value != string.Empty) ? true : false;
                this.RaiseAndSetIfChanged(ref _SizeColumn, value);
                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Size Column is " + _SizeColumn);
            }
        }

        private string _PriceColumn;
        public string PriceColumn
        {
            get { return _PriceColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.price_flag = (value != string.Empty) ? true : false;
                this.RaiseAndSetIfChanged(ref _PriceColumn, value);
                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Price Column is " + _PriceColumn);
            }
        }

        private string _QuantityColumn;
        public string QuantityColumn
        {
            get { return _QuantityColumn; }
            set {
                value = (value == "0") ? string.Empty : value;
                this.quantitiy_flag = (value != string.Empty) ? true : false;
                this.RaiseAndSetIfChanged(ref _QuantityColumn, value);
                this.RaisePropertyChanged("IsDataExtractEnabled");
                /* DEBUG */
                Debug.WriteLine("Quantity Column is " + _QuantityColumn);
            }
        }

        private bool code_flag, scientific_flag, common_flag, size_flag, price_flag, quantitiy_flag;

        public bool IsDataExtractEnabled { get => code_flag & scientific_flag & common_flag & size_flag & price_flag & quantitiy_flag;  }

        //public bool IsValidateDataEnabled { get => IsDataExtractEnabled; }
        public ViewModel()
        {

        }
    }
}
