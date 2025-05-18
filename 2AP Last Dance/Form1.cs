using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _2AP_Last_Dance
{
    
    public partial class AutoPipe : Form
    {
        public int countRows = 0;
        private int _rowIndexFromMouseDown;
        private DataGridViewRow _draggedRow;
        private int _lastHighlightedRow = -1;

        bool forwarding=false;
        string stageAccess = "ID";
        string prediction = "N";

        public AutoPipe()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 51;
            dataGridView1.Columns[0].Name = "Инструкция";
            for(int i = 1; i < 51; i++)
            {
                dataGridView1.Columns[i].Name = i.ToString();
            }

            // Фиксируем первый столбец
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.MouseDown += new MouseEventHandler(dataGridView1_MouseDown);
            dataGridView1.DragOver += new DragEventHandler(dataGridView1_DragOver);
            dataGridView1.DragDrop += new DragEventHandler(dataGridView1_DragDrop);
            dataGridView1.MouseUp += dataGridView1_MouseUp;
            dataGridView1.Font = new Font("Russo One", 10, FontStyle.Regular);

            dataGridViewRegs.ColumnCount = 2;
            dataGridViewRegs.Columns[0].Name = "Регистр";
            dataGridViewRegs.Columns[1].Name = "Значение";
            dataGridViewRegs.Font = new Font("Russo One", 10, FontStyle.Regular);
        }
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_lastHighlightedRow >= 0 && _lastHighlightedRow < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[_lastHighlightedRow].DefaultCellStyle.BackColor = Color.White;
                _lastHighlightedRow = -1;
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            _rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (_rowIndexFromMouseDown != -1)
            {
                _draggedRow = dataGridView1.Rows[_rowIndexFromMouseDown];
                dataGridView1.DoDragDrop(_draggedRow, DragDropEffects.Move);
            }
        }
        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            int rowIndex = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndex >= 0 && rowIndex != _lastHighlightedRow)
            {
                // Очистить предыдущую подсветку
                if (_lastHighlightedRow >= 0 && _lastHighlightedRow < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[_lastHighlightedRow].DefaultCellStyle.BackColor = Color.White;
                }

                // Подсветить новую строку
                dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                _lastHighlightedRow = rowIndex;
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            int targetIndex = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (targetIndex < 0 || _draggedRow == null || targetIndex == _rowIndexFromMouseDown)
                return;

            DataGridViewRow newRow = (DataGridViewRow)_draggedRow.Clone();
            for (int i = 0; i < _draggedRow.Cells.Count; i++)
            {
                newRow.Cells[i].Value = _draggedRow.Cells[i].Value;
            }

            dataGridView1.Rows.RemoveAt(_rowIndexFromMouseDown);
            dataGridView1.Rows.Insert(targetIndex, newRow);
            // Убрать подсветку
            if (_lastHighlightedRow >= 0 && _lastHighlightedRow < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[_lastHighlightedRow].DefaultCellStyle.BackColor = Color.White;
                _lastHighlightedRow = -1;
            }

        }
        

        private void Simulate()
        {
            if (InstructionManager.InputCheck(dataGridView1))
            {
                StageAccess();
                Prediction();
                string input = ComBuilder.PrepareComs(dataGridView1);
                if (input == "")
                {
                    MessageBox.Show(input, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // IP — число команд
                    int ip = dataGridView1.RowCount;

                    string cfgS = "316171";

                    List<object[]> regs = REGS.GetRegistersFromGrid(dataGridViewRegs);

                    // Сбор JSON
                    var jsonObj = new
                    {
                        input = input,
                        forwarding = forwarding,
                        stageAccess = stageAccess,
                        jumpPrediction = prediction,
                        cfgS = cfgS,
                        ip = ip,
                        regs = regs
                    };
                    string json = JsonConvert.SerializeObject(jsonObj);

                    // Запуск exe
                    var proc = new Process();
                    proc.StartInfo.FileName = "sim2AP_GUI.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.CreateNoWindow = true;

                    proc.Start();
                    proc.StandardInput.WriteLine(json);
                    proc.StandardInput.Close();

                    List<string[]> result = new List<string[]>();
                    string line;
                    while ((line = proc.StandardOutput.ReadLine()) != null)
                    {
                        result.Add(line.Split(','));
                    }
                    proc.WaitForExit();

                    // Отобразить результат в таблице
                    int existingCols = dataGridView1.Columns.Count;
                    int resultCols = result.Max(r => r.Length);

                    // Добавляем только если ещё не добавлены
                    for (int i = 0; i < resultCols; i++)
                    {
                        string name = $"Result{i + 1}";
                        if (!dataGridView1.Columns.Contains(name))
                            dataGridView1.Columns.Add(name, $"R{i + 1}");
                    }


                    for (int i = 0; i < result.Count; i++)
                    {
                        // Убедимся, что строка существует
                        if (i >= dataGridView1.Rows.Count)
                            dataGridView1.Rows.Add();

                        for (int j = 0; j < result[i].Length; j++)
                        {
                            // j+1, потому что 0-й столбец — input
                            dataGridView1.Rows[i].Cells[j + 1].Value = result[i][j];
                        }
                    }
                }
                
            }
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            Simulate();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            // 1. Найти регистры с учётом вход/выход
            List<Register> regs = REGS.FindRegsInOut(dataGridView1);

            // 2. Отобразить в другом гриде
            REGS.ShowRegs(dataGridViewRegs, regs);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void checkBoxForwarding_CheckedChanged(object sender, EventArgs e)
        {
            forwarding = checkBoxForwarding.Checked;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Simulate();
        }
        private void StageAccess()
        {
            if (radioButtonID.Checked)
            {
                stageAccess = "ID";
            }
            else if (radioButtonMEM.Checked)
            {
                stageAccess = "MEM";
            }
        }
        private void Prediction()
        {
            if (radioButtonN.Checked)
            {
                prediction = "N";
            }
            else if (radioButtonT.Checked)
            {
                prediction = "T";
            }
            else if (radioButtonF.Checked)
            {
                prediction = "F";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            dataGridViewRegs.Rows.Clear();

        }

        private void dataGridView1_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 1. Найти регистры с учётом вход/выход
            List<Register> regs = REGS.FindRegsInOut(dataGridView1);

            // 2. Отобразить в другом гриде
            REGS.ShowRegs(dataGridViewRegs, regs);
        }
    }
}
