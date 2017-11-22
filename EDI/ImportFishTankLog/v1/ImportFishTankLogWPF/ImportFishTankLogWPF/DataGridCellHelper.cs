using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ImportFishTankLogWPF
{
    // https://social.msdn.microsoft.com/Forums/en-US/290d3c67-440e-4037-86b6-cf668990b5da/how-to-loop-through-all-the-the-cells-in-datagrid?forum=wpf

    public class DataGridCellHelper
    {
        private DataGrid _dataGrid;
        public DataGridCellHelper(DataGrid dtGrid)
        {
            _dataGrid = dtGrid;
        }

        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    _dataGrid.ScrollIntoView(rowContainer, _dataGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        private DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)_dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                _dataGrid.UpdateLayout();
                _dataGrid.ScrollIntoView(_dataGrid.Items[index]);
                row = (DataGridRow)_dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
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
