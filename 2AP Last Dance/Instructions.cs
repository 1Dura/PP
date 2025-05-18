using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2AP_Last_Dance
{
    public class Instruction
    {
        public string Name;                     // Название инструкции
        public int ArgCount;                    // Кол-во аргументов
        public Func<string, bool>[] ArgValidators; // Валидаторы
        public int[] OutputIndices;             // Индексы выходных аргументов
    }


    internal class InstructionManager
    {
        public static Instruction GetInstruction(string name)
        {
            return InstructionList.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static List<Instruction> InstructionList = new List<Instruction>
        {
            new Instruction
            {
                Name = "ADD",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister, IsRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "FADD",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsFloatRegister, IsFloatRegister, IsFloatRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "SUB",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister, IsRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "FSUB",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsFloatRegister, IsFloatRegister, IsFloatRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "MUL",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister, IsRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "FMUL",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsFloatRegister, IsFloatRegister, IsFloatRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "DIV",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister, IsRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "FDIV",ArgCount = 3,ArgValidators = new Func<string, bool>[] { IsFloatRegister, IsFloatRegister, IsFloatRegister }, OutputIndices = new[] { 2 }
            },
            new Instruction
            {
                Name = "CMP",ArgCount = 2,ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "FCMP",ArgCount = 2,ArgValidators = new Func<string, bool>[] { IsFloatRegister, IsFloatRegister }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JE", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JNE", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JG", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JGE", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JL", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "JLE", ArgCount = 1, ArgValidators = new Func<string, bool>[] { IsJumpAdress }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "MV", ArgCount = 2, ArgValidators = new Func<string, bool>[] { IsRegister, IsRegister }, OutputIndices = new[] { 1 }
            },
            new Instruction
            {
                Name = "LD", ArgCount = 2, ArgValidators = new Func<string, bool>[] { IsAddress, IsRegister }, OutputIndices = new int[0]
            },
            new Instruction
            {
                Name = "ST", ArgCount = 2, ArgValidators = new Func<string, bool>[] { IsRegister, IsAddress }, OutputIndices = new int[0]
            }
        };
        static bool IsRegister(string token)
        {
            return token.StartsWith("R") && int.TryParse(token.Substring(1), out _);
        }
        static bool IsFloatRegister(string token)
        {
            return token.StartsWith("F") && int.TryParse(token.Substring(1), out _);
        }

        static bool IsAddress(string token)
        {
            return token.StartsWith("[") && token.EndsWith("]") &&
                   int.TryParse(token.Trim('[', ']'), out _);
        }
        static bool IsJumpAdress(string token)
        {
            return token.StartsWith("[") && token.EndsWith("]");
        }
        public static bool InputCheck(DataGridView grid)
        {
            string input = ComBuilder.PrepareComs(grid);
            string[] tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            int PC = 0;
            while (i < tokens.Length)
            {
                string token = tokens[i];
                var instr = InstructionList.FirstOrDefault(x => x.Name == token);


                if (instr == null)
                {
                    grid.Rows[PC].Cells[0].Style.BackColor = Color.Red;
                    MessageBox.Show("Вы неправильно указали инструкцию!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (i + instr.ArgCount >= tokens.Length)
                {
                    grid.Rows[PC].Cells[0].Style.BackColor = Color.Red;
                    MessageBox.Show("Вы указали неверное кол-во регистров для команды!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                for (int a = 0; a < instr.ArgCount; a++)
                {
                    string arg = tokens[i + 1 + a];
                    var isValid = instr.ArgValidators[a](arg);
                    if (!isValid)
                    {
                        grid.Rows[PC].Cells[0].Style.BackColor = Color.Red;
                        MessageBox.Show("Вы неправильно указали регистры!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                PC++;
                i += 1 + instr.ArgCount;
            }
            for(int IP=0; IP <PC; IP++)
            {
                grid.Rows[IP].Cells[0].Style.BackColor = Color.White;
            }
            return true;

        }
    }
}
