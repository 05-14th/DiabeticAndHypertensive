using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class RecycleBin : Form
    {
        string connectionString = "Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;";
        public RecycleBin()
        {
            InitializeComponent();
        }
    }
}
