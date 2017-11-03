using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Excel_ImportWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string file_fullpath = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();
            if (browsefile == true)
            {
                file_fullpath = openfile.FileName;
                string[] text = file_fullpath.Split('\\');
                int last_index = text.Count() - 1;
                txtFilePath.Text = text[last_index];
            }
        }

        private void btnHeader_Click(object sender, RoutedEventArgs e)
        {
            if(file_fullpath == string.Empty) {
                /* DEBUG */
                //Debug.WriteLine("No File is selected !!!");
                MessageBox.Show("No File is selected !!!\nPlease try again");
                return;
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@file_fullpath);
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlWorksheet.Cells.Find("*", System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                           Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            int colCount = xlWorksheet.Cells.Find("*", System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                           Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious,
                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;

            DataTable dt = new DataTable();
            List<string> columns = new List<string>();

            try {
                /* Retreive the Column Heading information*/
                int emptyCell = 0;
                int start_row;
                string strCellData, strData="";
                for (start_row = 1; start_row < rowCount; start_row++)
                {
                    emptyCell = 0;
                    strData = "";
                    for (int col = 1; col < colCount; col++)
                    {
                        Excel.Range temp = xlRange.Cells[start_row, col];

                        if (temp == null)
                        {
                            emptyCell++;
                            continue;
                        }

                        object value = temp.Value;

                        if (value == null)
                        {
                            strCellData = "";
                        }
                        else
                            strCellData = value.ToString();

                        if (strCellData == "") emptyCell++;

                        strData += strCellData + "|";
                    }

                    if (emptyCell < colCount / 2) break;
                }
                Debug.WriteLine($"Header String {strData}");

                for (int col = 1; col < colCount; col++)
                {
                    Excel.Range temp = xlRange.Cells[start_row, col];
                    object value = temp.Value;
                    string strColumn;
                    if (value == null)
                    {
                        strColumn = "Col" + col;
                    }
                    else
                    {
                        strColumn = value.ToString();
                    }
                    dt.Columns.Add(strColumn, typeof(string));
                    columns.Add(strColumn);
                }

            }
            catch (Exception ex) {
                Console.Write("Error Encounter :" + ex.ToString());
            }
            finally {
                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close(false);
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            if(columns.Count() > 0)
            {
                ComboBox[] comboboxs = { lbxCode, lbxScientific, lbxCommon, lbxSize, lbxPrice, lbxQuantity };
                foreach( var combobox in comboboxs)
                    combobox.ItemsSource = columns;
            }
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
