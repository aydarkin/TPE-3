using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPE_3
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        void btnApply_Click(object sender, EventArgs e)
        {
            var v = (int)nudVar.Value;
            var thirdFactor = LatinSquare(v);
            CreateTable(thirdFactor);

            if (rbYouden.Checked)
            {
                // скрываем случайный столбец/строку
                var isRow = random.Next(2) > 0;
                var position = random.Next(v);

                if (isRow)
                    grid.Rows[position].Visible = false;
                else
                    grid.Columns[position].Visible = false;
            }
        }

        List<List<int>> LatinSquare(int v)
        {
            var model = new List<List<int>>();
            int c;
            for (int i = 0; i < v; i++)
            {
                model.Add(new List<int>());
                for (int j = 0; j < v; j++)
                {
                    c = (i + j) % v;
                    model[i].Add(c);
                }
            }

            return model;
        }

        double GetVaried(int elem, int full)
        {
            var part = 2.0 / (full - 1);
            return -1 + (part * elem);
        }

        void CreateTable(List<List<int>> model)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            string name;
            var v = model.Count;
            for (int col = 0; col < v; col++)
            {
                name = GetVaried(col, v).ToString("f2");
                grid.Columns.Add(name, name);

                for (int row = 0; row < model.Count; row++)
                {
                    if (col == 0)
                    {
                        grid.Rows.Add(new DataGridViewRow()
                        {
                            HeaderCell = new DataGridViewRowHeaderCell() { Value = GetVaried(row, v).ToString("f2") }
                        });
                    }

                    grid.Rows[row].Cells[col].Value = 
                        $"x3 = {GetVaried(model[row][col], v).ToString("f2")}{Environment.NewLine}y[{row+1},{col+1},{model[row][col]+1}]";
                }
            }

        }

    }
}
