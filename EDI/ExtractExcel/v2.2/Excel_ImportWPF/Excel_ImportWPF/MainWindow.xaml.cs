﻿using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Excel_ImportWPF
{

    // using https://github.com/ExcelDataReader/ExcelDataReader

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
                    label.Content = combobox.SelectedIndex+1;
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


                dtGrid.ItemsSource = null;

                MessageBox.Show("Import Excel Column");
                //Thread.Sleep(5000);
                btnHeader_Click(sender, e);

                btnValidate.IsEnabled = false;
                btnUpLoad.IsEnabled = false;
            }
            else
            {
                btnValidate.IsEnabled = false;
                btnUpLoad.IsEnabled = false;
            }
            lblMessage.Content = "Import Excel Column Completed !!!";
            //lblMessage.Height = 0;
        }

        /* https://stackoverflow.com/questions/27634477/using-exceldatareader-to-read-excel-data-starting-from-a-particular-cell */

        private void btnHeader_Click(object sender, RoutedEventArgs e)
        {
            if (file_fullpath == string.Empty)
            {
                /* DEBUG */
                //Debug.WriteLine("No File is selected !!!");
                MessageBox.Show("No File is selected !!!\nPlease try again");
                return;
            }

            //DataTable dt = new DataTable();
            List<string> columns = new List<string>();
            using (var stream = File.Open(file_fullpath, FileMode.Open, FileAccess.Read))
            {
                //IExcelDataReader excelReader;

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                    DataSet result = reader.AsDataSet();

                    //3. DataSet - Create column names from first row
                    //reader.IsFirstRowAsColumnNames = false;
                    //
                    DataTable dt = result.Tables[0];

                    // The result of each spreadsheet is in result.Tables
                    //var table = result.cu
                    Debug.WriteLine($"rows : { dt.Rows.Count }  coulumns : { dt.Columns.Count }");

                    int rowCount = dt.Rows.Count;
                    int colCount = dt.Columns.Count;

                    /* Retreive the Column Heading information*/
                    int emptyCell = 0;
                    //int start_row;
                    start_row = 0;
                    string strCellData, strData = "";
                    for (start_row = 1; start_row < rowCount; start_row++)
                    {
                        emptyCell = 0;
                        strData = "";
                        for (int col = 1; col < colCount; col++)
                        {
                            object value = dt.Rows[start_row][col];
                            //object value = temp.Value;

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
                        //Excel.Range temp = xlRange.Cells[start_row, col];
                        object value = dt.Rows[start_row][col];
                        string strColumn;
                        if (value.ToString() == "")
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
            }

            if (columns.Count() > 0)
            {
                //ComboBox[] comboboxs = { cbxCode, cbxScientific, cbxCommon, cbxSize, cbxPrice, cbxQuantity };
                //foreach ( var combobox in comboboxs)
                int count = comboboxs.Count();
                for (int i = 0; i < count; i++)
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
            //btnValidate.IsEnabled = false;
            MessageBox.Show("About to Extract Data.\nPlease wait patiently.");

            int count = labels.Count();
            List<int> col_index = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if (int.TryParse(labels[i].Content.ToString(), out int val))
                    col_index.Add(val);
            }

            DataTable dt = new DataTable();

            using (var stream = File.Open(file_fullpath, FileMode.Open, FileAccess.Read))
            {
                //IExcelDataReader excelReader;

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                    DataSet result = reader.AsDataSet();

                    //3. DataSet - Create column names from first row
                    //reader.IsFirstRowAsColumnNames = false;
                    //
                    DataTable excel_dt = result.Tables[0];


                    int rowCount = excel_dt.Rows.Count;
                    int colCount = excel_dt.Columns.Count;

                    for (int i = 0; i < col_index.Count(); i++)
                    {
                        int col = col_index[i];
                        //Excel.Range temp = xlRange.Cells[start_row, col];
                        //object value = temp.Value2;
                        //string strColumn = value.ToString();
                        object value = excel_dt.Rows[start_row][col];
                        dt.Columns.Add(value.ToString(), typeof(string));
                    }
                    dt.Columns.Add("MPI Name", typeof(string));
                    dt.Columns.Add("Status", typeof(string));


                    string strData, strCellData;
                    int emptyCell;

                    for (int row = start_row + 1; row < rowCount; row++)
                    {
                        bool isNotOK(string strValue)
                        {
                            int index;
                            int.TryParse(strValue, out index);
                            //Excel.Range temp_value = xlRange.Cells[row, index];
                            object value = excel_dt.Rows[row][index];

                            //object obj = temp_value.Value;
                            //if (obj == null || obj.ToString().Trim() == "")
                            //    return true;
                            //return false;

                            return value.ToString().Trim() == "" ? true : false;
                        }

                        ///* First check quantity field */
                        //Excel.Range temp_qty = xlRange.Cells[row, qty_index];
                        //object qty_obj = temp_qty.Value;
                        //if (qty_obj == null || qty_obj.ToString().Trim() == "" ) continue;

                        /* Check Code field */
                        if (isNotOK(lblCode.Content.ToString())) continue;

                        /* Check Scientific Name field */
                        if (isNotOK(lblScientific.Content.ToString())) continue;

                        /* Check Common Name field */
                        if (isNotOK(lblCommon.Content.ToString())) continue;

                        /* Check Size field */
                        if (isNotOK(lblSize.Content.ToString())) continue;

                        /* First check quantity field */
                        if (isNotOK(lblQuantity.Content.ToString())) continue;

                        strData = string.Empty;
                        emptyCell = 0;
                        for (int i = 0; i < col_index.Count(); i++)
                        {
                            int col = col_index[i];
                            //Excel.Range temp = xlRange.Cells[start_row, col];
                            //object value = temp.Value2;
                            //string strColumn = value.ToString();
                            object value = excel_dt.Rows[row][col];

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
                        //if (row == start_row + 50)
                        //{
                        //    Debug.WriteLine("Debug : Only check 50 lines");
                        //    break;
                        //}

                        //}
                    }
                }
            }
            dtGrid.ItemsSource = dt.DefaultView;

            lblMessage.Content = string.Empty;
            lblMessage.Height = 0;

            // 
            if (dt.Rows.Count > 1)
                btnValidate.IsEnabled = true;
            else
                btnValidate.IsEnabled = false;

            btnUpLoad.IsEnabled = false;
        }



        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
