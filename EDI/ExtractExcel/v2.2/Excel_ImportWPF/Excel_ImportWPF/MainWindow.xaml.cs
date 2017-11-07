using Excel_ImportWPF.DAO;
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
using System.Windows.Controls.Primitives;
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
                //lblMessage.Content = "Loading ..... ";
                //lblMessage.Height = 50;
                file_fullpath = openfile.FileName;
                string[] text = file_fullpath.Split('\\');
                int last_index = text.Count() - 1;
                txtFilePath.Text = text[last_index];

                dtGrid.ItemsSource = null;

                //MessageBox.Show("Import Excel Column");
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
            //lblMessage.Content = "Import Excel Column Completed !!!";
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

                    for (int col = 0; col < colCount; col++)
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
                        //dt.Columns.Add(strColumn, typeof(string));
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

                //lblMessage.Content = string.Empty;
                //MessageBox.Show("Import Execel Columns haved completed !!!");
            }
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            //lblMessage.Content = "Extracting Data .....";
            //lblMessage.Height = 50;
            //btnValidate.IsEnabled = false;
            //MessageBox.Show("About to Extract Data.\nPlease wait patiently.");

            int count = labels.Count();
            List<int> col_index = new List<int>();
            for (int i = 0; i < count; i++)
            {
                if(labels[i].Content == null)
                    col_index.Add(-1);
                else
                {
                    if (int.TryParse(labels[i].Content.ToString(), out int val))
                        col_index.Add(val-1);
                }

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

                        /* Note : some supplier do not provide size nor code
                         code - index 0
                         size - index 3
                         */
                        if( col < 0)
                        {
                            //string value = "Col " + i;
                            string value;
                            switch(i)
                            {
                                case 0: value = "code"; break;
                                case 2: value = "common"; break;

                                case 3: value = "size"; break;
                                default: value = "Col " + i; break;
                            }

                            dt.Columns.Add(value.ToString(), typeof(string));
                        }
                        else
                        {
                            object value = excel_dt.Rows[start_row][col];
                            string[] temp = value.ToString().Split('/');

                            if (dt.Columns.Contains(temp[0]))
                                dt.Columns.Add("Duplicate " + (col+1), typeof(string));
                            else
                            {
                                
                                dt.Columns.Add(temp[0], typeof(string));
                            }
                                
                        }
                        
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
                            object value = excel_dt.Rows[row][index - 1];

                            return value.ToString().Trim() == "" ? true : false;
                        }

                        ///* First check quantity field */

                        ///* Check Code field */
                        ////if (isNotOK(lblCode.Content.ToString())) continue;

                        ///* Check Scientific Name field */
                        //if (isNotOK(lblScientific.Content.ToString())) continue;

                        ///* Check Common Name field */
                        //if (isNotOK(lblCommon.Content.ToString())) continue;

                        ///* Check Size field */
                        ////if (isNotOK(lblSize.Content.ToString())) continue;

                        /* First check quantity field */
                        if (isNotOK(lblQuantity.Content.ToString())) continue;

                        strData = string.Empty;
                        emptyCell = 0;
                        //object value;
                        for (int i = 0; i < col_index.Count(); i++)
                        {
                            int col = col_index[i];

                            /* Note : some supplier do not provide size nor code
                             code - index 0
                             common - index 2
                             size - index 3
                             */
                            //strCellData = col != -1 ? excel_dt.Rows[row][col].ToString() : "" ;
                            
                            strCellData = "";
                            if (col != -1)
                            {
                                object value = excel_dt.Rows[row][col];
                                strCellData = value.ToString();
                            }


                            if (strCellData == "") emptyCell++;

                            strData += strCellData + "|";
                        }
                        /* DEBUG */
                        Debug.WriteLine($"{row} : {strData}");
                        //if (emptyCell < colCount / 2)
                        //{

                            //strData = strData.Remove(strData.Length - 1, 1);
                            dt.Rows.Add(strData.Split('|'));
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
            btnUpLoad.IsEnabled = false;

            // https://social.msdn.microsoft.com/Forums/en-US/290d3c67-440e-4037-86b6-cf668990b5da/how-to-loop-through-all-the-the-cells-in-datagrid?forum=wpf

            for (int row = 0; row < dtGrid.Items.Count; row++)
            {
                //for(int column = 0; column < dtGrid.Columns.Count; column++)
                //{
                //    DataGridCell cell = GetCell(row, column);
                //}

                DataGridCell cell = GetCell(row, 1);
                TextBlock text = cell.Content as TextBlock;
                string[] array = text.Text.Split(' ');
                string species = array[0] + ' ' + array[1];
                Debug.WriteLine(species);
                //dtGrid.Items[row].Cells[dtGrid.Columns.Count-1].Text = SpeciesDataHelper.GetIDByScientificName(species).ToString();

                string id, name;
                (id, name) = SpeciesDataHelper.CheckScientificName(species);

                //cell = GetCell(row, dtGrid.Columns.Count - 1);
                //cell = GetCell(row, 1);
                if (id.Trim() != "-1" && species.Length != name.Length)
                    id = "-1";

                /* Display the status */
                cell = GetCell(row, dtGrid.Columns.Count - 1);
                ((TextBlock)cell.Content).Text = id.ToString();
                /* Display MPI Name */
                cell = GetCell(row, dtGrid.Columns.Count - 2);
                ((TextBlock)cell.Content).Text = name.ToString();
            }
        }

        private DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dtGrid.ScrollIntoView(rowContainer, dtGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        private DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)dtGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dtGrid.UpdateLayout();
                dtGrid.ScrollIntoView(dtGrid.Items[index]);
                row = (DataGridRow)dtGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
