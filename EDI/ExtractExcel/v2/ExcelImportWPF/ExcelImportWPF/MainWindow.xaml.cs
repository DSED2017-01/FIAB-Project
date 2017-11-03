using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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


using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelImportWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                txtFilePath.Text = openfile.FileName;

                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@txtFilePath.Text);
                Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1] ;
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

                string strData = string.Empty, strCellData;

                try
                {
                    DataTable dt = new DataTable();

                    /* Retreive the Column Heading
                     */
                    int emptyCell = 0;
                    int start_row;
                    for(start_row=1; start_row < rowCount ; start_row++)
                    {
                        emptyCell = 0;
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
                                emptyCell++;
                            }
                            else
                            {
                                strCellData = value.ToString();
                                
                            }
                            //if (strCellData == "") emptyCell++;

                        }

                        if (emptyCell < colCount / 2) break;
                    }

                    for (int col=1; col < colCount; col++)
                    {
                        Excel.Range temp = xlRange.Cells[start_row, col];
                        object value = temp.Value2;
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
                    }

                    for (int row = start_row+1; row < rowCount; row++)
                    {
                        strData = string.Empty;

                        emptyCell = 0;
                        for (int col = 1; col < colCount; col++)
                        {

                            /* 
                             * Get the Default datatype for excel column
                             * https://stackoverflow.com/questions/2672956/get-the-default-datatype-for-excel-column-using-vsto  */

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
                                               
                        /* DEBUG */
                        if(emptyCell < colCount/2)
                        {
                            Debug.WriteLine($"{row} : {strData}");
                            strData = strData.Remove(strData.Length - 1, 1);
                            dt.Rows.Add(strData.Split('|'));
                        }

                        /* Debug */
                        if (row == start_row+50) break;
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

                Debug.WriteLine($"End of File {txtFilePath.Text.ToString()}");
            }


            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
