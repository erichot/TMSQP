using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Odbc;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string queryString = "SELECT * FROM BANK64";
            OdbcCommand command = new OdbcCommand(queryString);

            string connectionStg = "PROVIDER=MSDASQL;DSN=QPAY_TT2023_EMP64;UID=admin;PWD=;";
            //OdbcConnection connection = new OdbcConnection(connectionStg);


            using (OdbcConnection connection = new OdbcConnection(connectionStg))
            {
                command.Connection = connection;
                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();

                int fCount = reader.FieldCount;
                while (reader.Read())
                {
                    for (int i = 0; i < fCount; i++)
                    {
                        var dataval = reader.GetValue(i).ToString();
                        Console.WriteLine(dataval);
                    }
                }

                reader.Close();
                command.Dispose();
            }
        }
    }
}
