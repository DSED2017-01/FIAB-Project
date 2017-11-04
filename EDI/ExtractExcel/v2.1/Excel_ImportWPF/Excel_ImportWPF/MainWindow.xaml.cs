using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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
        private List<ComboBox> comboboxs = new List<ComboBox>();
        private List<Label> labels = new List<Label>();

        private int start_row;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();

            //ComboBox[] comboboxs = { cbxCode, cbxScientific, cbxCommon, cbxSize, cbxPrice, cbxQuantity };

            comboboxs.Add(cbxCode);
            comboboxs.Add(cbxScientific);
            comboboxs.Add(cbxCommon);
            comboboxs.Add(cbxSize);
            comboboxs.Add(cbxPrice);
            comboboxs.Add(cbxQuantity);

            //Label[] labels = { lblCode, lblScientific, lblCommon, lblSize, lblPrice, lblQuantity };
            labels.Add(lblCode);
            labels.Add(lblScientific);
            labels.Add(lblCommon);
            labels.Add(lblSize);
            labels.Add(lblPrice);
            labels.Add(lblQuantity);
            int count = comboboxs.Count();
            for (int i = 0; i < count; i++)
            {
                //labels[i].Content = string.Empty;

                ComboBox combobox = comboboxs[i];
                Label label = labels[i];
                combobox.SelectionChanged += delegate {
                    label.Content = combobox.SelectedIndex + 1;
                };
            }
            lblMessage.Content = string.Empty;
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
                lblMessage.Content = "Loading ..... ";
                lblMessage.Height = 50;
                file_fullpath = openfile.FileName;
                string[] text = file_fullpath.Split('\\');
                int last_index = text.Count() - 1;
                txtFilePath.Text = text[last_index];

                MessageBox.Show("Import Excel Column");
                //Thread.Sleep(5000);
                btnHeader_Click(sender, e);
            }
            lblMessage.Content = "Import Excel Column Completed !!!";
            //lblMessage.Height = 0;
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
                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column
                           + 2 ; // Need to at least 2 more column so as to detect some more Excel column 


            DataTable dt = new DataTable();
            List<string> columns = new List<string>();

            try {
                /* Retreive the Column Heading information*/
                int emptyCell = 0;
                //int start_row;
                start_row = 0;
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
                //ComboBox[] comboboxs = { cbxCode, cbxScientific, cbxCommon, cbxSize, cbxPrice, cbxQuantity };
                //foreach ( var combobox in comboboxs)
                int count = comboboxs.Count();
                for(int i= 0; i < count; i++)
                {
                    //labels[i].Content = string.Empty;
                    comboboxs[i].ItemsSource = columns;
                    comboboxs[i].SelectedIndex = -1;
                }

                lblMessage.Content = string.Empty;
                //MessageBox.Show("Import Execel Columns haved completed !!!");
            }
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            lblMessage.Content = "Extracting Data .....";
            lblMessage.Height = 50;

            MessageBox.Show("About to Extract Data.\nPlease wait patiently.");

            int count = labels.Count();
            List<int> col_index = new List<int>();
            for(int i = 0; i < count;i++)
            {
                if (int.TryParse(labels[i].Content.ToString(), out int val))
                    col_index.Add(val);
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@file_fullpath);
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            /* https://stackoverflow.com/questions/43353073/c-sharp-excel-correct-way-to-get-rows-and-columns-count  */

            int rowCount = xlWorksheet.Cells.Find("*", System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                           Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            int colCount = xlWorksheet.Cells.Find("*", System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                           Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious,
                           false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;

            /* DEBUG */
            Debug.WriteLine($"File @{txtFilePath.Text.ToString()} has {rowCount} rows and {colCount} columns");

            DataTable dt = new DataTable();

            try
            {
                /* Create the grid column header */
                for (int i = 0; i < col_index.Count(); i++)
                {
                    int col = col_index[i];
                    Excel.Range temp = xlRange.Cells[start_row, col];
                    object value = temp.Value2;
                    //string strColumn = value.ToString();
                    dt.Columns.Add(value.ToString(), typeof(string));
                }
                string strData, strCellData;
                int emptyCell;

                int qty_index ;
                int.TryParse(lblQuantity.Content.ToString(), out qty_index);

                for (int row = start_row + 1; row < rowCount; row++)
                {

                    ///* First check quantity field */
                    Excel.Range temp_qty = xlRange.Cells[row, qty_index];
                    object qty_obj = temp_qty.Value;

                    if (qty_obj == null || qty_obj.ToString().Trim() == "" ) continue;

                    strData = string.Empty;
                    emptyCell = 0;
                    for (int i = 0; i < col_index.Count(); i++)
                    {
                        int col = col_index[i];
                        Excel.Range temp = xlRange.Cells[row, col];

                        if (temp == null)
                            continue;

                        object value = temp.Value;

                        if (value == null)
                            strCellData = "";
                        else
                            strCellData = value.ToString();

                        if (strCellData == "") emptyCell++;

                        //strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        strData += strCellData + "|";
                    }
                    if (emptyCell < colCount / 2)
                    {
                        /* DEBUG */
                        Debug.WriteLine($"{row} : {strData}");
                        strData = strData.Remove(strData.Length - 1, 1);
                        dt.Rows.Add(strData.Split('|'));
                    }
                    /* Debug */
                    //if (row == start_row + 50) break;
                }
                dtGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                Console.Write("Error Encounter :" + ex.ToString());
            }
            finally
            {
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
            lblMessage.Content = string.Empty;
            lblMessage.Height = 0;
        }
    }
}
