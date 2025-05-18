using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2AP_Last_Dance
{
    internal class ComBuilder
    {
        public static string PrepareComs(DataGridView grid)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (!row.IsNewRow)
                {
                    object value = row.Cells[0].Value;
                    if (value != null)
                    {
                        sb.Append(value.ToString());
                        sb.Append(" ");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
