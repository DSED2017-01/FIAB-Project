using ExcelDataReader;
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

namespace CheckApprovedSpecies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string file_name = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        /* 
         * When the user click the 'Open File' */
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";

            dataGrid.ItemsSource = null;

            /* show an Open File Dialog Box for user to select the Excel file name */
            if (openfile.ShowDialog() == true)
            {
                file_name = openfile.FileName;
                RetreiveExcelColumnHeader();
            }
            else
                file_name = string.Empty;

            txtFilePath.Text = file_name; // Display the file name onto text box
        }

        /* function to open the Excel file
           and retrive the column header */

        static int header_row;

        private void RetreiveExcelColumnHeader()
        {
            if (file_name == string.Empty)
            {
                /* DEBUG */
                //Debug.WriteLine("No File is selected !!!");
                MessageBox.Show("No File is selected !!!\nPlease try again");
                return;
            }

            

            /* Open the Excel file to reade*/
            using (var stream = File.Open(file_name, FileMode.Open, FileAccess.Read))
            {
                /*  using ExcelDataReader libary to process excel file */

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Use DataSet method - The result of each spreadsheet will be created in the result.Tables
                    DataSet result = reader.AsDataSet();

                    DataTable dt = result.Tables[0]; // use 1st spreadsheet by default

                    /* retreive total number of rows and column in the spreadsheet */
                    int rowCount = dt.Rows.Count;
                    int colCount = dt.Columns.Count;

                    /* TDD Debuging */
                    Debug.WriteLine($"rows : { rowCount }  coulumns : { colCount }");

                    /* Retreive the Row and Cell information*/
                    string cellData, rowData = "";

                    rowData = ExtractHeader(dt);

                    if (header_row == -1)
                        Debug.WriteLine($"Error encounted !!! row == {header_row}");

                    /* Populate the combo box with the column header */
                    /* split the row data into individual column */
                    string[] colData = rowData.Split('|');

                    /* variable to store excel column header*/
                    List<string> header = new List<string>();

                    /* re-use the variable strCellData */
                    for (int col = 0; col < colCount; col++)
                    {
                        object value = dt.Rows[header_row][col];
                        string no = (col + 1).ToString();
                        cellData = "Col " +
                                        "".PadRight(2 - no.Length, '0') + no + " : " +
                                        value.ToString();
                        header.Add(cellData);
                    }

                    cbxScientific.ItemsSource = header;
                }
            }
        }

        private static string ExtractHeader(DataTable dt)
        {
            string[] check_list_array = { "qty", "box", "science", "scientific", "common", "price", "order", "size" };
            List<string> check_list; // = check_list_array.ToList();

            /* retreive total number of rows and column in the spreadsheet */
            int rowCount = dt.Rows.Count;
            int colCount = dt.Columns.Count;

            header_row = -1;
            string rowData="", cellData;

            for (int row = 0; row < rowCount; row++)
            {
                /* set empty column counter to zero for each row */
                int emptyCell = 0;  
                int field_count = 0;
                /* set row data to empty for each row */
                rowData = "";       
                /* reset check list each time for each row */
                check_list = check_list_array.ToList();

                /* Additional checking */
                /* check each column whether contains word from the check list*/
                bool validate_check_list()
                {
                    bool flag = false;
                    string temp = "";
                    for (int i = 0; i < check_list.Count; i++)
                    {
                        temp = check_list[i];
                        if (cellData.ToLower().Contains(temp))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        check_list.Remove(temp);

                    return flag;
                }

                for (int col = 0; col < colCount; col++)
                {
                    object value = dt.Rows[row][col];
                    cellData = value.ToString();
                    /* Count number of empty cell column in a row*/
                    if (cellData == "") emptyCell++;

                    rowData += cellData.Trim() + "|";
                    if (validate_check_list()) field_count++;

                }
                /* If tthe programm find the first row which staisfy the following condition
                 *   the number of empty cell is less than half of the total columns in a row
                 *   ==> that must be column header !!! */
                if (field_count >= 4 || emptyCell < (colCount / 2))
                {
                    header_row = row; // set pointer to column header row
                    break;
                }
            }
            /* TDD Debuging */
            Debug.WriteLine($"[Header][row:{header_row}]:{rowData}");

            return rowData;
        }

        private void btnImportExcelData_Click(object sender, RoutedEventArgs e)
        {
            /* Check whether the user has selected a excel file */
            if (file_name == string.Empty)
            {
                /* DEBUG */
                //Debug.WriteLine("No File is selected !!!");
                MessageBox.Show("No File is selected !!!\nPlease try again");
                return;
            }

            /* Check whether the user has selected one supplier column field as the supplier's species information */
            int col_selected = cbxScientific.SelectedIndex;
            if ( col_selected < 0)
            {
                MessageBox.Show("Please select a column to extract supplier's species information.\nPlease try again");
                return;
            }

            DataTable dt_Species = new DataTable(); // will store supplier's species information

            /* Open the Excel file to reade*/
            using (var stream = File.Open(file_name, FileMode.Open, FileAccess.Read))
            {
                /*  using ExcelDataReader libary to process excel file */

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Use DataSet method - The result of each spreadsheet will be created in the result.Tables
                    DataSet result = reader.AsDataSet();

                    DataTable dt = result.Tables[0]; // use 1st spreadsheet by default

                    /* retreive total number of rows and column in the spreadsheet */
                    int rowCount = dt.Rows.Count;
                    int colCount = dt.Columns.Count;

                    /* TDD Debuging */
                    Debug.WriteLine($"rows : { rowCount }  coulumns : { colCount }");

                    /* Retreive the Row and Cell information*/
                    string celData="", rowData = "";

                    rowData = ExtractHeader(dt);

                    /* setup the datagrid column */
                    dt_Species.Columns.Add("Row Number", typeof(string));
                    dt_Species.Columns.Add("Supplier Species Information", typeof(string));
                    dt_Species.Columns.Add("MPI Species Name", typeof(string));
                    dt_Species.Columns.Add("Status", typeof(string));

                    for(int row = header_row + 1; row < rowCount; row++)
                    {
                        object value = dt.Rows[row][col_selected];

                        if (value.ToString().Trim() == "") continue;

                        celData = "" + row + "|";
                        celData += value.ToString().Trim();
                        rowData = celData + "||";
                        dt_Species.Rows.Add(rowData.Split('|'));
                    }
                }
                
            }
            dataGrid.ItemsSource = dt_Species.DefaultView;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
