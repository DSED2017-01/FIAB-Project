using ExcelDataReader;
using ImportFishTankLogWPF.DAO;
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
using Technewlogic.WpfDialogManagement;

namespace ImportFishTankLogWPF
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

        /// <summary>
        /// point to the file with file path and full file name)
        /// </summary>
        private string file_fullpath = string.Empty;

        /// <summary>
        ///  open a dialog box for user to open excel file. 
        /// </summary>
        private void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            var browsefile = openfile.ShowDialog();

            /* Only do something when the user actually select a file */
            if (browsefile == true)
            {
                file_fullpath = openfile.FileName;
                string[] text = file_fullpath.Split('\\');
                int last_index = text.Count() - 1;
                txtFilePath.Text = text[last_index];

                dtGrid.ItemsSource = null;
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            /* Clear the content of following variables */
            txtFilePath.Text = string.Empty;
            file_fullpath = string.Empty;

            cbxNo.Items.Clear();
            dtGrid.ItemsSource = null;

            /* Prompt the user to open a file */
            OpenFile();

            /* If user has selected a file, the following variables will contains something.
             txtFilePath.Text and file_fullpath;            
             */
            if (file_fullpath != string.Empty)
            {

            }
            else
            {
                //MessageBox.Show("Sorry no file is selected.\nPlease try again!!");
                string text = "Sorry no file is selected.\nPlease try again!!";
                var dialogManager = new DialogManager(this, Dispatcher);
                dialogManager
                    .CreateMessageDialog(text, "Error", DialogMode.Ok)
                    .Show();
                return;
                /* Since no file is being selected, do not proceed from here */
            }

            using (var stream = File.Open(file_fullpath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int sheetCount = reader.ResultsCount;

                    cbxNo.Items.Clear();
                    for (int i=0; i < sheetCount;i++)
                    {
                        cbxNo.Items.Add(i + 1);
                    }
                }
            }
            cbxNo.SelectedIndex = 0;
            ExtractExcelData(0);
        }

        private void ExtractExcelData(int sheet_index)
        {
            /* Extract Excel File */
            /* create a new datatable*/
            DataTable dt = new DataTable();

            using (var stream = File.Open(file_fullpath, FileMode.Open, FileAccess.Read))
            {
                /* store the header columns */
                List<string> header_columns = new List<string>();

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet();
                    DataTable excel_dt = result.Tables[sheet_index];

                    /* get total row and total columns. */
                    int rowCount = excel_dt.Rows.Count;
                    int colCount = excel_dt.Columns.Count;

                    int sheetCount = reader.ResultsCount;

                    /* Debug */
                    Debug.WriteLine($"Total Number of Sheet: {sheetCount}");
                    Debug.WriteLine($"rows : { rowCount }  coulumns : { colCount }");

                    #region Retrieve excel column header
                    /* Retreive the Column Heading information*/
                    int emptyCell = 0;
                    //int start_row;
                    int start_row = 0;
                    string strCellData, strData = "";

                    /* Quick - Fix !!!! */


                    bool CheckHeaderText(string text)
                    {
                        string[] header_text = {
                        "code", "name", "vietnamese","size", "price","total",
                        "tank", "Description", "qty", "price", "amount"};
                        bool flag = false;

                        if (text.Trim() == string.Empty) return flag;

                        for (int n = 0; n < header_text.Length; n++)
                        {
                            //flag = header_text[n].Contains(text.ToLower());
                            flag = text.ToLower().Contains(header_text[n]);
                            if (flag) break;
                        }

                        return flag;
                    }


                    int cellCount = 0;

                    for (start_row = 1; start_row < rowCount; start_row++)
                    {
                        cellCount = 0;
                        strData = "";
                        for (int col = 0; col < colCount; col++)
                        {
                            object value = excel_dt.Rows[start_row][col];
                            //object value = temp.Value;

                            if (value == null)
                            {
                                strCellData = "";
                            }
                            else
                                strCellData = value.ToString().Trim();

                            if (strCellData != "" && CheckHeaderText(strCellData)) //emptyCell++;
                                cellCount++;

                            strData += strCellData + "|";
                        }
                        if (cellCount > 5) break;
                        //Debug.WriteLine($"row {start_row} : {strData}");
                    }
                    #endregion
                    //strData = "Tank ID" + "|" + strData;
                    Debug.WriteLine($"Header String {strData}");

                    #region (NOT USING) Displays all Excel header columns
                    //string[] columns = strData.Split('|');
                    ////int cellCount = 0;
                    ////cellCount = 0;
                    //for(int i = 0; i < columns.Length; i++)
                    //{
                    //    string text = columns[i];

                    //    if(text != string.Empty)
                    //    {
                    //        dt.Columns.Add(text, typeof(string));
                    //        //cellCount++;
                    //    }
                    //    else
                    //    {
                    //        if(i == cellCount)
                    //            break;
                    //        else
                    //        {
                    //            dt.Columns.Add("Col" + i, typeof(string));
                    //        }
                    //    }
                    //}
                    #endregion

                    /* Create a 'standard' header column*/
                    string strColumnHeader = "TANK_FK|SPECIES_TEXT_2|SPECIES_FK|COMMON|SIZE_FK|QTY";
                    dt.Columns.Add("Tank", typeof(string));  // TANK_FK
                    dt.Columns.Add("Code", typeof(string));     // SPECIES_TEXT_2
                    dt.Columns.Add("Scientific", typeof(string));// SPECIES_FK
                    dt.Columns.Add("Common", typeof(string));    // SPECIES_TEXT
                    dt.Columns.Add("Size", typeof(string)); // SIZE
                    dt.Columns.Add("Quantity", typeof(string));  // QTY
                    dt.Columns.Add("Result", typeof(string));  // ==> for validate data status

                    #region Link excel column to table column
                    /* process the strData to link the excel column to table column */
                    /* retreive the table column name */
                    string[] strTableColumn = strColumnHeader.Split('|');
                    string scientific_field = "description vietnamese latin name ";
                    string[] strMatchingText = { "tank","code",
                        scientific_field, "", // process Common Name when dealing with Scientific Name
                        "size", "qty tot'pc" };
                    string[] strColumnName = strData.Split('|');
                    string[] strScientific = scientific_field.Split(' ');
                    List<int> excel_col_index = new List<int>();
                    /* */
                    int table_col_index = 0;
                    string table_col = "";

                    strData = "";
                    for (int col = 0; col < colCount; col++)
                    {
                        string text = strColumnName[col].ToLower();
                        string matching_text = strMatchingText[table_col_index];
                        //bool col_found = false;

                        bool IsMatchingText()
                        {
                            string[] subText = text.Split(' ');
                            for (int sub_index = 0; sub_index < subText.Length; sub_index++)
                            {
                                if (matching_text.Contains(subText[sub_index])) return true;
                            }
                            return false;
                        }


                        /*If there is match up between excel column and table column, 
                           do the following and proceed to the next table column */
                        if (text != "" && matching_text != "" &&
                             IsMatchingText())
                        //text.Contains(matching_text))
                        {
                            /* save the actual column (index) */
                            excel_col_index.Add(col);
                            strData += text + "|";
                           

                            switch (table_col_index)
                            {
                                case 0:
                                case 1:
                                case 4:
                                case 5:

                                    break;
                                /* Handle Scientific and Common Name together */
                                case 2:
                                    if( text.Contains(strScientific[0]) )
                                        excel_col_index.Add(col + 1);
                                    else if( text.Contains(strScientific[1]))
                                        excel_col_index.Add(col-1);
                                    else
                                        excel_col_index.Add(col);
                                        
                                    strData += text + "|";

                                    table_col_index++;
                                    break;
                                //case 3: break;
                                default: throw new Exception($"Error : table index {table_col_index} not defined !!!");
                            }

                            table_col_index++;
                        }

                        /* Terminate the loop once processed sthe last table column*/
                        if (table_col_index == strMatchingText.Length) break;
                    }
                    Debug.WriteLine($"Matching Column:{strData}");

                    #endregion

                    /* Need to format Scientific Name before validating*/


                    #region Display excel records onto screen (data grid)
                    bool addData = false;
                    for (int row = start_row + 1; row < rowCount; row++)
                    {
                        strData = "";

                        object value = excel_dt.Rows[row][0];
                        strCellData = value.ToString();
                        /* Remove any row that has no tank number allocated to it.
                            Also remove invalid record ..... */
                        if (strCellData == string.Empty) continue;

                        for (int i = 0; i < excel_col_index.Count; i++)
                        {
                            int col = excel_col_index[i];
                            value = excel_dt.Rows[row][col];
                            strCellData = value.ToString();

                            switch(i)
                            {
                                case 2: /* Foremat Scientific Name */
                                    string temp = value.ToString();
                                    string[] scientific = temp.Split(' ');
                                    strCellData = scientific[0] + ( scientific.Length > 1 ? ' ' + scientific[1]: "");
                                    break;
                                default: strCellData = value.ToString(); break;
                            }
                            strData += (strData != "" ? "|" : "") + strCellData;

                            //
                        }
                        dt.Rows.Add(strData.Split('|'));
                        //Debug.WriteLine($"{row}:{strData}");

                    }
                    #endregion
                }
            }
            /* update the data grid */
            dtGrid.ItemsSource = dt.DefaultView;
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void RefreshData(object sender, SelectionChangedEventArgs e)
        {
            ComboBox obj = sender as ComboBox;
            int selected = obj.SelectedIndex;
            if ( selected != -1)
            {
                ExtractExcelData(selected);
            }
        }

        /// <summary>
        /// This function verify excel record before data update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {

            /*1. Check Scientific Name.*/
            /* Helper class to extract data from data grid cell. */
            DataGridCellHelper cell_helper = new DataGridCellHelper(dtGrid);

            int col = 2; // Column that store Scientific Name
            for (int row = 0; row < dtGrid.Items.Count; row++)
            {

                DataGridCell cell = cell_helper.GetCell(row, col);
                TextBlock text = cell.Content as TextBlock;
                

                string species = text.Text;
                string id, name;

                (id, name) = SpeciesDataHelper.CheckScientificName(species);
                //Debug.WriteLine($"row {row + 1}:{text.Text} => {name}");

                /* Get the status data grid cell field */
                cell = cell_helper.GetCell(row, dtGrid.Columns.Count - 1);
                ((TextBlock)cell.Content).Text = id == "-1" ? id : name ;

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
