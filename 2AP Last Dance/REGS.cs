using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _2AP_Last_Dance
{
    public class Register
    {
        public string Name;                 // Имя регистра, например "R1", "F2"
        public bool CanEdit;   // true = входной регистр (можно ввести значение), false = выходной (нельзя)
        public float Value;

    }
    internal class REGS
    {
        public static void ShowRegs(DataGridView grid, List<Register> regs)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            grid.Columns.Add("colName", "Регистр");
            DataGridViewTextBoxColumn valCol = new DataGridViewTextBoxColumn();
            valCol.Name = "colValue";
            valCol.HeaderText = "Значение";
            grid.Columns.Add(valCol);

            for (int i = 0; i < regs.Count; i++)
            {
                int index = grid.Rows.Add();
                grid.Rows[index].Cells[0].Value = regs[i].Name;
                grid.Rows[index].Cells[1].ReadOnly = !regs[i].CanEdit;
                grid.Rows[index].Cells[1].Style.BackColor =
                    regs[i].CanEdit ? Color.White : Color.LightGray;
            }
        }
        public static List<Register> FindRegsInOut(DataGridView grid)
        {
            Dictionary<string, bool> регистры = new Dictionary<string, bool>();
            Regex regex = new Regex(@"^[RF]\d+$", RegexOptions.IgnoreCase);

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                var row = grid.Rows[i];
                if (row.IsNewRow) continue;

                object value = row.Cells[0].Value;
                if (value == null) continue;

                string[] токены = value.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (токены.Length < 2) continue;

                string команда = токены[0].ToUpper();
                string[] args = токены.Skip(1).ToArray();

                var instr = InstructionManager.GetInstruction(команда);
                if (instr == null) continue;

                for (int j = 0; j < args.Length; j++)
                {
                    string reg = args[j].ToUpper();
                    if (!regex.IsMatch(reg)) continue;

                    bool этоВыход = instr.OutputIndices.Contains(j);

                    if (!регистры.ContainsKey(reg))
                    {
                        регистры[reg] = !этоВыход; // true = вход
                    }
                    // иначе не трогаем — пусть остаётся входом, если им был

                }
            }

            List<Register> список = new List<Register>();
            foreach (var kv in регистры)
            {
                список.Add(new Register
                {
                    Name = kv.Key,
                    CanEdit = kv.Value
                });
            }

            return список.OrderBy(r => r.Name).ToList();
        }
        public static List<object[]> GetRegistersFromGrid(DataGridView grid)
        {
            List<object[]> registers = new List<object[]>();

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                DataGridViewRow row = grid.Rows[i];

                object nameObj = row.Cells[0].Value;
                object valueObj = row.Cells[1].Value;

                if (nameObj == null || valueObj == null)
                    continue;

                string name = nameObj.ToString().ToUpper();
                double value;

                if (!double.TryParse(valueObj.ToString(), out value))
                    continue;

                registers.Add(new object[] { name, value });
            }

            return registers;
        }

    }
}
