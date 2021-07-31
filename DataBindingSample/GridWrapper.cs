using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataBindingSample
{
    public class GridWrapper<T>
    {

        private WeakReference<DataGridView> _GridReference;
        private ConditionalWeakTable<DataGridViewRow, object> _Table;
        private Func<T> _MakeDefaultFunc;
        private Func<T, IEnumerable<string>> _ColTextFunc;
        private int _ColNum;
        

        public GridWrapper(DataGridView grid, Func<T> makeDefaultFunc, Func<T, IEnumerable<string>> colTextFunc)
        {
            _GridReference = new WeakReference<DataGridView>(grid);
            _Table = new ConditionalWeakTable<DataGridViewRow, object>();
            _MakeDefaultFunc = makeDefaultFunc;
            _ColTextFunc = colTextFunc;
            _ColNum = _ColTextFunc(_MakeDefaultFunc()).Count();

        }

        public void Add(T item)
        {
            if (!_GridReference.TryGetTarget(out DataGridView grid)) return;

            if (grid.ColumnCount < _ColNum)
            {
                grid.ColumnCount = _ColNum;
            }

            var index = grid.Rows.Add(_ColTextFunc(item).ToArray());
            _Table.Add(grid.Rows[index], item);
        }

        public void SetData(int index, T item)
        {
            if (!_GridReference.TryGetTarget(out DataGridView grid)) return;
            if (index < 0 || grid.Rows.Count - 1 < index) return;

            if (_Table.TryGetValue(grid.Rows[index], out _))
            {
                _Table.Remove(grid.Rows[index]);
            }

            if (item != null)
            {
                _Table.Add(grid.Rows[index], item);
                var colTexts = _ColTextFunc(item).ToArray();
                for (int i = 0; i < grid.Rows[index].Cells.Count; i++)
                {
                    grid.Rows[index].Cells[i].Value = colTexts[i];
                }
            }
        }

        public IEnumerable<T> GetSelectedItems()
        {
            if (!_GridReference.TryGetTarget(out DataGridView grid)) yield break;

            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                if (_Table.TryGetValue(row, out object data))
                {
                    yield return (T)data;
                }
            }
        }

        public void Remove(T item)
        {
            if (!_GridReference.TryGetTarget(out DataGridView grid)) return;

            DataGridViewRow deleteRow = null;

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (_Table.TryGetValue(row, out object data) && data.Equals(item))
                {
                    deleteRow = row;
                    break;
                }
            }

            if (deleteRow != null)
            {
                _Table.Remove(deleteRow);
                grid.Rows.Remove(deleteRow);
            }
        }
    }
}