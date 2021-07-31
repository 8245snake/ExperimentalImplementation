using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBindingSample
{
    public partial class MainForm : Form
    {
        private GridWrapper<MyData> _Grid;

        public MainForm()
        {
            InitializeComponent();
            _Grid = new GridWrapper<MyData>(this.gridMain, () => new MyData(), dispCols);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            _Grid.Add(new MyData(){Name = "あああ", Number = 1});
            _Grid.Add(new MyData(){Name = "あああ", Number = 2});
            _Grid.Add(new MyData(){Name = "あああ", Number = 3});
            _Grid.Add(new MyData(){Name = "あああ", Number = 4});

            _Grid.SetData(3, new MyData() { Name = "いいい", Number = 9 });
        }

        private IEnumerable<string> dispCols(MyData arg)
        {
            yield return arg.Name;
            yield return arg.Number.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (var item in _Grid.GetSelectedItems())
            {
                _Grid.Remove(item);
            }
        }
    }

    class MyData
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
