﻿#include <iostream>
#include <string>
#include <vector>
#include <set>
#include <windows.h>

using namespace std;

set<string> validCommands = { "LD", "ST", "ADD", "SUB", "MUL", "DIV", "JMP", "CMP",
                                    "JE", "JNE", "JL", "JLE", "JG", "JGE", "JMP", "MV", "INC",
                                               "FADD", "FSUB", "FMUL", "FDIV", "FCMP" };

bool isRegsValidForCom(const string& cmd, const string& R1, const string& R2, const string& R3) {
    return cmd[0] == 'F' && R1[0] == 'F' && R2[0] == 'F' && R3[0] == 'F'
        || cmd[0] != 'F' && R1[0] == 'R' && R2[0] == 'R' && R3[0] == 'R';
    // проверяем принадлежность регистров выбранной команды (вещественным командам вещественные регистры).
}
bool isRegsValidForComCMP(const string& cmd, const string& R1, const string& R2) {
    return cmd[0] == 'F' && R1[0] == 'F' && R2[0] == 'F'
        || cmd[0] != 'F' && R1[0] == 'R' && R2[0] == 'R';
    // проверяем принадлежность регистров выбранной команды (вещественным командам вещественные регистры).
}

bool isValidCommand(const string& cmd) {
    return validCommands.find(cmd) != validCommands.end(); // Проверка корректности команды
}

// Проверка корректности адреса (например, [1000])
static bool isValidAddress(const string& addr) {
    return (addr.size() > 2 && addr.front() == '[' && addr.back() == ']');
}
void accurateSize(vector<vector<string>>& pipe, int& row) {
    int maxsize = 15;
    int sum = 0;
    while (sum < maxsize) {
        pipe[row][5] += " ";
        sum = (pipe[row][1] + pipe[row][2] + pipe[row][3] + pipe[row][4] + pipe[row][5]).size();
    }
}
void IF(const int& PC, int& ST, const bool& forwarding, int& sdvg,
    vector<vector<string>>& pipe, vector <pair<string, int>>& REGS, bool& stIF, const string& line)
{
    while (!stIF)
    {
        bool M = false; // занято ли ФУ памяти
        bool IF = false; // занято ли ФУ выборки инструкции
        bool stall = false;
        for (int i = 0; i < PC; i++)
        {
            if (pipe[i][ST] == "M*") { M = true; }
            if (pipe[i][ST] == "IF" || pipe[i][ST] == "if") { IF = true; }
            if (i == PC - 1 && pipe[i][ST] == "- ") { stall = true; }
        }

        if (!M && !IF && !stall) { pipe[PC][ST] = line; stIF = true; }
        else if (M) { pipe[PC][ST] = "- "; sdvg++; }
        else { pipe[PC][ST] = ". "; sdvg++; }
        ST++;
    }
}
void ID(const int& PC, int& ST, const bool& forwarding, int& sdvg,
    vector<vector<string>>& pipe, vector <pair<string, int>>& REGS, bool& stID, const string& line) {   
    //if (forwarding == true && !stID) { stID = true; pipe[PC][ST] = line; ST++; }
    bool exstID = false;
    while (!stID && !exstID) // besk cickl
    {
        bool stall = false;
        if (PC - 1 >= 0 && pipe[PC - 1][ST] == "- ") { // если ФУ ID занято
            int u = ST - 1;
            while (!stall) {
                if (pipe[PC - 1][u] == line) { stall = true;   }
                else if (pipe[PC - 1][u] == "- ") { u--; }
                else { break; }
            }
        }
        if(!forwarding){
            for (int i = 0; i < REGS.size(); i++) { // проверка на КФ данных
                if ((REGS[i].first == pipe[PC][2] || REGS[i].first == pipe[PC][3]) && pipe[PC][3] != "") {
                    while (ST < REGS[i].second) {
                        pipe[PC][ST] = "- ";
                        ST++; sdvg++;
                        if (line == "id" && pipe[PC][ST - 1] == "- ") { exstID = true; }
                    }
                }
            }
        }
        if (stall) { 
            pipe[PC][ST] = "- ";
            sdvg++;
            if (line == "id" && pipe[PC][ST - 1] == "- ") { exstID = true; } }
        else{
            pipe[PC][ST] = line; stID = true;
        }
        ST++;
    }
}
void EX(const int& PC, int& ST, const bool& forwarding, int& sdvg,
    vector<vector<string>>& pipe, vector <pair<string, int>>& REGS,
    bool& stEX, const string& line, int& waitEX, vector<pair<string, int>>& EXs, bool& debug) {
    string com = pipe[PC][1];
    if (com == "FSUB" || com == "FCMP") { com = "FADD"; }
    int _ST = ST + waitEX - 1;
    int tries = 0;
    if (forwarding == false || com == "ST" || com == "LD") {
        while (!stEX) {
            bool stall = false;
            for (int i = 0; i < EXs.size(); i++) {
                if (EXs[i].first == com) {
                    if (debug) { cout << "1. " << EXs[i].first << " vs " << com << endl; }
                    if (ST + 1 >= EXs[i].second)
                    {
                        EXs[i].first = "DELETED";
                    }
                    else {
                        stall = true; // тут ФУ ещё занято
                        cout << "f false, PC: " << PC << ". " << EXs[i].first << " " << EXs[i].second << ". com: " << com << endl;
                    }
                }
            }
            if (stall) { pipe[PC][ST] = "- "; sdvg++; if (line == "ex") { stEX = true; } }
            else { pipe[PC][ST] = line;  stEX = true; }
            ST++;
        }
    }
    while (!stEX)
    {
        bool stall = false;
        //bool isEXready = false;
        for (int i = 0; i < REGS.size(); i++) {
            if (REGS[i].first == pipe[PC][2] || REGS[i].first == pipe[PC][3]) {
                if (debug) { cout << "2. " << REGS[i].first << " vs " << pipe[PC][2] << " " << pipe[PC][3] << endl; }
                if (ST >= REGS[i].second)
                {
                    REGS[i].first = "DELETED";
                }
                else {
                    stall = true; // кф данных
                    //cout << "cf data" << ST << " vs " << REGS[i].second << endl;
                }
            }
        }

        for (int i = 0; i < EXs.size(); i++) {
            if (EXs[i].first == com) {
                if (debug) { cout << "3. " << EXs[i].first << " vs " << com << endl; }
                if (ST >= EXs[i].second)
                {
                    EXs[i].first = "DELETED";
                }
                else {
                    stall = true; // тут ФУ ещё занято
                    //cout << "cd FU PC: " << PC << ". " << EXs[i].first << " " << EXs[i].second << ". com: " << com << endl;
                }
            }
        }

        if (stall) { pipe[PC][ST] = "- "; if (line == "ex") { stEX = true; } } // убрал sdvg++;
        else { pipe[PC][ST] = line;  stEX = true; }
        ST++;
    }
    EXs.push_back({ com, _ST });
}
void MEM(const int& PC, int& ST, const bool& forwarding, int& sdvg,
    vector<vector<string>>& pipe, vector <pair<string, int>>& REGS,
    bool& stMEM, string& outR) {
    while (!stMEM) {
        bool M = false;
        for (int i = 0; i < PC; i++)
        {
            if (pipe[i][ST] == "M*" || pipe[i][ST] == "M ") { M = true; }
        }
        if (pipe[PC][1] == "LD" || pipe[PC][1] == "ST")
        {

            if (M) { pipe[PC][ST] = "- "; }
            else { pipe[PC][ST] = "M*"; stMEM = true; }


            outR = pipe[PC][3]; // у LD выходной регистр находится на второй позиции
            // и нам по барабану, что в массив кинется [1000], если будет ST, цикл на кф даты не примет и всё
            if (forwarding == true) { REGS.push_back({ outR, ST + 1 }); }
        }
        else if (pipe[PC][1] == "INC")
        {
            if (M) { pipe[PC][ST] = "- "; }
            else { pipe[PC][ST] = "M "; stMEM = true; }
            outR = pipe[PC][2]; // а у остальных команд выходной регистр на 3-ей позиции
            if (forwarding == true) { REGS.push_back({ outR, ST - 1 }); }
        }
        else
        {
            if (M) { pipe[PC][ST] = "- "; }
            else { pipe[PC][ST] = "M "; stMEM = true; }
            outR = pipe[PC][4]; // а у остальных команд выходной регистр на 3-ей позиции
            if (forwarding == true) { REGS.push_back({ outR, ST }); }
        }
        ST++;
    }
}

// функция которая генерирует стадии в pipe
void generate(const int& PC, int& ST, const bool& forwarding, int& sdvg,
    vector<vector<string>>& pipe, vector <pair<string, int>>& REGS,
    string& stAcc, string& jumpPrediction, bool& jumped, vector<pair<int, int>> cfg, vector<pair<string, int>>& EXs, bool& debug)
{
    if (debug) { cout << "Starting generating com " << pipe[PC][1] << "..." << endl; }
    int amountEX = 0, waitEX = 1;
    if (pipe[PC][1] == "FADD" || pipe[PC][1] == "FSUB" || pipe[PC][1] == "FCMP") {
        amountEX = cfg[0].first; // задержка (кол-во EX)
        waitEX = cfg[0].second; // через сколько можно снова запускать
    }
    else if (pipe[PC][1] == "FMUL") {
        amountEX = cfg[1].first; // задержка (кол-во EX)
        waitEX = cfg[1].second; // через сколько можно снова запускать
    }
    else if (pipe[PC][1] == "FDIV") {
        amountEX = cfg[2].first; // задержка (кол-во EX)
        waitEX = cfg[2].second; // через сколько можно снова запускать
    }
    bool stIF = false, stID = false, stEX = false, stMEM = false;

    int triesEX = 0;
    if (PC - 2 >= 0 && pipe[PC - 2][1] == "CMP")
    {
        //cout << pipe[PC][1] << ". jumped: " << jumped << ". " << endl;
        int end = 0;
        if (stAcc == "ID") { end = 1; }
        else if (stAcc == "MEM") { end = 3; }
        if (jumpPrediction == "N")
        { // ничего не предсказываем  
            for (int i = 0; i < end; i++)
            { // простои (кол-во в зависимости от ступени вычисления адреса перехода)
                pipe[PC][ST] = "- ";
                sdvg++;
                ST++;
            }
        }
        else if (jumpPrediction == "F") // предсказываем, что перехода не будет
        {
            if (jumped == false) // угадали
            {
                IF(PC, ST, forwarding, sdvg, pipe, REGS, stIF, "if");
                if (end == 3)
                {
                    int _ST = ST;
                    ID(PC, ST, forwarding, sdvg, pipe, REGS, stID, "id");
                    if (_ST + 1 == ST) {
                        EX(PC, ST, forwarding, sdvg, pipe, REGS, stEX, "ex", waitEX, EXs, debug);
                        triesEX++;
                        stEX = false;
                        if (pipe[PC][ST - 1] == "- ") { triesEX--; }
                    }
                }
            }
            else if (jumped == true) // не угадали
            {
                IF(PC, ST, forwarding, sdvg, pipe, REGS, stIF, "if");
                if (end == 3)
                {
                    int _ST = ST;
                    ID(PC, ST, forwarding, sdvg, pipe, REGS, stID, "id");
                    if (_ST + 1 == ST) {
                        EX(PC, ST, forwarding, sdvg, pipe, REGS, stEX, "ex", waitEX, EXs, debug);
                        triesEX++;
                        if (pipe[PC][ST - 1] == "- ") { triesEX--; }
                    }
                }
                stIF = false; stID = false; stEX = false; //sdvg = sdvg + 2 + amountEX;
            }
        }
        else if (jumpPrediction == "T") // предсказываем, что переход будет
        {
            if (jumped == true) // угадали
            {
                IF(PC, ST, forwarding, sdvg, pipe, REGS, stIF, "if");
                if (end == 3)
                {
                    int _ST = ST;
                    ID(PC, ST, forwarding, sdvg, pipe, REGS, stID, "id");
                    if (_ST + 1 == ST) {
                        EX(PC, ST, forwarding, sdvg, pipe, REGS, stEX, "ex", waitEX, EXs, debug);
                        triesEX++;
                        stEX = false;
                        if (pipe[PC][ST - 1] == "- ") { triesEX--; }
                    }
                }
            }
            else if (jumped == false) // не угадали
            {
                IF(PC, ST, forwarding, sdvg, pipe, REGS, stIF, "if");
                if (end == 3)
                {
                    int _ST = ST;
                    ID(PC, ST, forwarding, sdvg, pipe, REGS, stID, "id");
                    if (_ST + 1 == ST) {
                        EX(PC, ST, forwarding, sdvg, pipe, REGS, stEX, "ex", waitEX, EXs, debug);
                        triesEX++;
                        if (pipe[PC][ST - 1] == "- ") { triesEX--; }
                    }
                }
                stIF = false; stID = false; stEX = false; sdvg = sdvg + 2 + amountEX;
            }
        }
    }
    IF(PC, ST, forwarding, sdvg, pipe, REGS, stIF, "IF");
    if (debug) { cout << "IF done" << endl; }
    ID(PC, ST, forwarding, sdvg, pipe, REGS, stID, "ID");
    if (debug) { cout << "ID done" << endl; }
    while (triesEX <= amountEX) {
        EX(PC, ST, forwarding, sdvg, pipe, REGS, stEX, "EX", waitEX, EXs, debug);
        triesEX++;
        stEX = false;
    }
    string outR;
    if (debug) { cout << "EX done" << endl; }
    MEM(PC, ST, forwarding, sdvg, pipe, REGS, stMEM, outR);
    if (debug) { cout << "MEM done" << endl; }
    if (forwarding == false) {
        REGS.push_back({ outR, ST });
    }
    pipe[PC][ST] = "WB";
    if (debug) { cout << "WB done" << endl; }
}


int autoPipe(const string& input, const bool& forwarding, string& stageAccess, string& jumpPrediction, vector<pair<int, int>> cfg, bool& debug)
{
    vector<vector<string>> pipe; // 2D-вектор для хранения команд
    vector<string> words;        // Список слов для разбиения строки
    string part;


    // Разбиение входной строки по пробелам
    for (size_t c = 0; c <= input.size(); c++) {
        if (c == input.size() || input[c] == ' ') {
            if (!part.empty()) {
                words.push_back(part);
                part = "";
            }
        }
        else {
            part += input[c];
        }
    }
    pipe.resize(words.size() / 2, vector<string>(100, ". ")); // Создаём фиксированную таблицу команд
    // Подготовка двумерного вектора pipe (заполнение команд и регистров)
    int IP = 0;
    for (size_t i = 0; i < words.size();) {
        pipe[IP][0] = to_string(IP * 4);
        while (pipe[IP][0].size() < 3) { pipe[IP][0] += ' '; }
        if (!isValidCommand(words[i])) {
            cerr << "Ошибка: Неизвестная команда " << words[i] << endl;
            return 1;
        }
        pipe[IP][1] = words[i];
        if ((words[i] == "JE" || words[i] == "JNE" || words[i] == "JL" || words[i] == "JLE" || words[i] == "JG" || words[i] == "JGE" || words[i] == "JMP") && i + 1 < words.size())
        {
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = " ";
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 2;
        }
        else if (words[i] == "INC" && i + 1 < words.size()) {
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = " ";
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 2;
        }
        else if (words[i] == "LD" && i + 2 < words.size()) {
            if (!isValidAddress(words[i + 1])) {
                cerr << "Ошибка: Некорректный адрес ==> " << words[i + 1] << " <== в команде номер: " << IP << endl;
                return 1;
            }
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = words[i + 2];
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 3;
        }
        else if ((words[i] == "ST") && i + 2 < words.size()) {
            if (!isValidAddress(words[i + 2]))
            {
                cerr << "Ошибка: Некорректный адрес ==> " << words[i + 2] << " <== в команде номер: " << IP << endl;
                return 1;
            }
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = words[i + 2];
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 3;
        }
        else if (words[i] == "MV" && i + 2 < words.size()) {
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = words[i + 2];
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 3;
        }
        else if ((words[i] == "CMP" || words[i] == "FCMP") && i + 2 < words.size()) {
            if (!isRegsValidForComCMP(words[i], words[i + 1], words[i + 2])) {
                cerr << "Ошибка 11: Вы использовали неправильные регистры для команды " << words[i] << endl;
                return 1;
            }
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = words[i + 2];
            pipe[IP][4] = "";
            pipe[IP][5] = "";
            i += 3;
        }
        else if (i + 3 < words.size()) {
            if (!isRegsValidForCom(words[i], words[i + 1], words[i + 2], words[i + 3])) {
                cerr << "Ошибка 11: Вы использовали неправильные регистры для команды " << words[i] << endl;
                return 1;
            }
            pipe[IP][2] = words[i + 1];
            pipe[IP][3] = words[i + 2];
            pipe[IP][4] = words[i + 3];
            pipe[IP][5] = "";
            i += 4;
        }
        else {
            cerr << "Ошибка: Недостаточно аргументов для команды " << words[i] << " под номером: " << IP << endl;
            return 1;
        }
        accurateSize(pipe, IP);
        IP++;
    }


    vector<pair<string, float>> regs; // массив для подсчета значений регистров
    vector <pair<string, int>> dataCF; // массив, где 1- такт, в котором станет доступен регистр и 2- сам регистр, значение которого ожидается
    vector<pair<string, int>> EXs; // 1 - команда, которая заняла устройство ЕХ, 2 - такт, после которого устройство станет доступным

    dataCF.push_back({ "..", 0 });

    int sdvg = 0, tacts = 0;

    bool jumped=false;
    int E, G; // E - equal   G - greater
    string CMP = "";
    for (int PC = 0; PC < IP; PC++)
    {
        int ST = PC + 6 + sdvg;
        tacts = ST;

        if (debug) { cout << "Started com " << pipe[PC][1] << "..." << endl; }

        if (pipe[PC][1] == "LD")
        {
            cout << "Вы пытаетесь загрузить регистр " << pipe[PC][3] << ", введите его значение:" << endl;
            float R;
            cin >> R;
            regs.push_back({ pipe[PC][3], R });
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }


        else if (pipe[PC][1] == "ST")
        {
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }

        else if (pipe[PC][1] == "MV")
        {

            bool reg = false, r1 = false;
            float R1;
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == pipe[PC][2]) { R1 = regs[i].second; r1 = true; }
            }

            if (!r1)
            {
                cout << "Команда MV пытается использовать регистр " << pipe[PC][2] << ", укажите его значение:" << endl;
                cin >> R1;
                regs.push_back({ pipe[PC][2], R1 });
            }
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == pipe[PC][3]) { regs[i].second = R1; reg = true; }
            }
            if (reg == false) { regs.push_back({ pipe[PC][3], R1 }); }

            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);

        }

        else if (pipe[PC][1] == "ADD" || pipe[PC][1] == "SUB" || pipe[PC][1] == "MUL" || pipe[PC][1] == "DIV" || pipe[PC][1] == "INC" || pipe[PC][1] == "MV")
        {

            bool reg = false, r1 = false, r2 = false;
            if (pipe[PC][1] == "INC" || pipe[PC][1] == "MV") r2 = true;
            int R1, R2, newR;
            string outReg = pipe[PC][4];
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == pipe[PC][2]) { R1 = regs[i].second; r1 = true; }
                if (regs[i].first == pipe[PC][3] && !r2) { R2 = regs[i].second; r2 = true; }
            }
            if (!r1)
            {
                cout << "Команда " << pipe[PC][1] << " пытается использовать регистр " << pipe[PC][2] << ", укажите его значение : " << endl;
                cin >> R1;
                regs.push_back({ pipe[PC][2], R1 });
                if (pipe[PC][2] == pipe[PC][3]) { r2 = true; R2 = R1; }
            }
            if (!r2)
            {
                cout << "Команда " << pipe[PC][1] << " пытается использовать регистр " << pipe[PC][3] << ", укажите его значение:" << endl;
                cin >> R2;
                regs.push_back({ pipe[PC][3], R2 });
            }
            if (pipe[PC][1] == "ADD") { newR = R1 + R2; }
            else if (pipe[PC][1] == "SUB") { newR = R1 - R2; }
            else if (pipe[PC][1] == "MUL") { newR = R1 * R2; }
            else if (pipe[PC][1] == "DIV") {
                if (R2 == 0) {
                    if (R1 == 0) { newR = 0; }
                    else {
                        cout << "Внимание, вы пытаетесь поделить на 0, программа приостановлена!"; return 2;
                    }
                }
                else { newR = R1 / R2; }
            }
            else if (pipe[PC][1] == "INC") { newR = R1+1; outReg = pipe[PC][2]; }
            else if (pipe[PC][1] == "MV") { newR = R1; outReg = pipe[PC][2]; }
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == outReg) { regs[i].second = newR; reg = true; }
            }
            if (reg == false) { regs.push_back({ outReg, newR }); }

            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);

        }
        else if (pipe[PC][1] == "FADD" || pipe[PC][1] == "FSUB" || pipe[PC][1] == "FMUL" || pipe[PC][1] == "FDIV")
        {

            bool fReg = false, f1 = false, f2 = false;
            float F1, F2, newF;
            string outReg = pipe[PC][4];
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == pipe[PC][2]) { F1 = regs[i].second; f1 = true; }
                if (regs[i].first == pipe[PC][3] && !f2) { F2 = regs[i].second; f2 = true; }
            }
            if (!f1)
            {
                cout << "Команда " << pipe[PC][1] << " пытается использовать регистр " << pipe[PC][2] << ", укажите его значение : " << endl;
                cin >> F1;
                regs.push_back({ pipe[PC][2], F1 });
                if (pipe[PC][2] == pipe[PC][3]) { f2 = true; F2 = F1; }
            }
            if (!f2)
            {
                cout << "Команда " << pipe[PC][1] << " пытается использовать регистр " << pipe[PC][3] << ", укажите его значение:" << endl;
                cin >> F2;
                regs.push_back({ pipe[PC][3], F2 });
            }
            if (pipe[PC][1] == "FADD") { newF = F1 + F2; }
            else if (pipe[PC][1] == "FSUB") { newF = F1 - F2; }
            else if (pipe[PC][1] == "FMUL") { newF = F1 * F2; }
            else if (pipe[PC][1] == "FDIV") {
                if (F2 == 0) {
                    if (F1 == 0) { newF = 0; }
                    else {
                        cout << "Внимание, вы пытаетесь поделить на 0, программа приостановлена!"; return 2;
                    }
                }
                else { newF = F1 / F2; }
            }
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == outReg) { regs[i].second = newF; fReg = true; }
            }
            if (fReg == false) { regs.push_back({ outReg, newF }); }

            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);

        }
        else if (pipe[PC][1] == "CMP")
        {


            bool r1 = false, r2 = false;
            int R1, R2;
            for (int i = 0; i < regs.size(); i++)
            {
                if (regs[i].first == pipe[PC][2]) { R1 = regs[i].second; r1 = true; }
                if (regs[i].first == pipe[PC][3]) { R2 = regs[i].second; r2 = true; }
            }
            if (!r1)
            {
                cout << "Команда CMP пытается использовать регистр " << pipe[PC][2] << ", укажите его значение:" << endl;
                cin >> R1;
                regs.push_back({ pipe[PC][2], R1 });
                if (pipe[PC][2] == pipe[PC][3]) { r2 = true; R2 = R1; }
            }
            if (!r2)
            {
                cout << "Команда CMP пытается использовать регистр " << pipe[PC][3] << ", укажите его значение:" << endl;
                cin >> R2;
                regs.push_back({ pipe[PC][3], R2 });
            }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug); // generate CMP
            if (R1 == R2) { G = -1; E = 1; } // E
            if (R1 != R2) { G = -1; E = 0; } // NE
            if (R1 > R2) { G = 1; } // G
            if (R1 < R2) { G = 0; } // L
            //cout << "G = " << G << ".  E = " << E << endl;
            //cout << "R1 = " << R1 << ". R2 = " << R2 << endl;
            jumped = false;
        }
        else if (pipe[PC][1] == "JE")
        {
            if (E == 1) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JNE")
        {
            if (E == 0) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JG")
        {
            if (G == 1) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JGE")
        {
            if (G == 1 || E == 1) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF,  stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JL")
        {
            if (G == 0) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JLE")
        {
            if (G == 0 || E == 1) { jumped = true; }
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else if (pipe[PC][1] == "JMP")
        {
            generate(PC, ST, forwarding, sdvg, pipe, dataCF, stageAccess, jumpPrediction, jumped, cfg, EXs, debug);
        }
        else { cout << "Wrong com! PC: " << PC; return 1; }
        if (debug) { cout << "com finished" << endl; }
    }
    // 
    // ВЫВОД
    //
    tacts += 10;
    std::cout << endl;
    std::cout << "    Instructions        ";
    for (int i = 1; i < 10; i++)
    {
        std::cout << i << "  ";
    }
    for (int i = 10; i < tacts - 5; i++)
    {
        std::cout << i << ' ';
    }
    std::cout << endl << endl;
    for (int i = 0; i < IP; i++) {
        for (int j = 0; j < tacts; j++)
        {
            cout << pipe[i][j] << " ";
        }
        cout << endl;
    }

    cout << endl << "Карта памяти после работы программы:" << endl;
    cout << "---+---" << endl;
    for (int i = 0; i < regs.size(); i++)
    {
        cout << regs[i].first << " | " << regs[i].second << endl;
        cout << "---+---" << endl;
    }
}




//
// -------------------- MAIN ------------------------------------------------------
//

int main() {
    COORD coord;
    SetConsoleDisplayMode(GetStdHandle(STD_OUTPUT_HANDLE), CONSOLE_FULLSCREEN_MODE, &coord);
    //вариант 1
    keybd_event(VK_MENU, 0x38, 0, 0);
    keybd_event(VK_RETURN, 0x1c, 0, 0);
    keybd_event(VK_RETURN, 0x1c, KEYEVENTF_KEYUP, 0);
    setlocale(0, "");


    cout << "    ============ AutoPipe  v1.8 | By Mathew Ovchinnikov ============    " << endl;
    cout << "Добро пожаловать в АвтоКонвейер - лучший способ проверить свой конвейер " << endl << endl;


    cout << "    Если вам нужна помощь с использованием программы, введите /help     " << endl;
    cout << "Для начала работы введите набор команд в строчку, разделяя их пробелами " << endl;
    cout << "   Пример ввода: LD [1000] R0 ADD R0 R0 R1 DIV R0 R1 R2 ST R2 [1004]    " << endl << endl;

    bool forwarding = true;
    bool debug = false;
    string stageAccess = "ID"; // "ID"  "MEM"
    string jumpPrediction = "F"; // "N"  "T"  "F"
    vector<pair<int, int>> cfg = { {3,1},{6,1},{24,25} }; // задержка/период запуска команд 01 - ADD/CMP/SUB   23 - MUL   34 - DIV

    string input;//="LD [1000] R0 LD [1004] R1 ADD R0 R1 R2 DIV R0 R2 R3 ST R0 [1000] ST R3 [1004] MUL R0 R0 R2 ST R2 [1008]";
    string line, extraLine;

    bool saved = false;
    while (input != "*)(^****^^%(((%$#$#(($%($$$^&&&(&^^@*&*!_O(#!((^$^!)&(!%$(&%(!*!))!^$%!&!%$%89254(*&52987&*@%$%(@5929*$(@%&*($@(*&@%$*@^$!(*$(*%($%@(&@^(%(@%*&%") {

        getline(cin, input);
        if (input == "/help" || input == "/h")
        {
            cout << " =====   Руководство использования   =====" << endl;
            cout << " 1. /help - Помощь с командами" << endl;
            cout << " 2. /exit - Выйти из программы" << endl;
            cout << " 3. /run - Обработать текущий набор команд" << endl;
            cout << " 4. /settings - Настройки конвейера" << endl;
            cout << " 5. /commands - Показать текущий набор команд" << endl;
            cout << " 6. /back - Вернуться к предыдущему набору команд" << endl;
            cout << " 7. /debug true|false - Режим разработчика" << endl;
            cout << " =====   Дополнительная информация   =====" << endl;
            cout << " Каждая команда (например /settings) имеет свой сокращенный" << endl;
            cout << " вариант, который начинается с первой буквы команды - /s " << endl;
        }
        else if (input == "/run" || input == "/r")
        {
            if (line.empty()) { cout << "Ошибка! Вы ещё не ввели набор команд для обработки!" << endl; }
            else { autoPipe(line, forwarding, stageAccess, jumpPrediction, cfg, debug); }
        }
        else if (input == "/settings" || input == "/s")
        {
            string f = "Пересылка есть";
            if (forwarding == false) { f = "Пересылки нет"; }
            else { f = "Пересылка есть"; }
            cout << " =====  Текущие настройки конвейера  =====" << endl;
            cout << f << ". Чтобы изменить /forwarding true|false" << endl;
            cout << "Адрес перехода доступен после " << stageAccess << ". Чтобы изменить /jumpstage MEM|ID" << endl;
            string j = "не производится";
            if (jumpPrediction == "T") { j = "переход будет"; }
            else if (jumpPrediction == "F") { j = "перехода не будет"; }
            else if (jumpPrediction == "N") { j = "не производится"; }
            cout << "Предсказание перехода: " << j << ". Чтобы изменить /prediction N|T|F" << endl;
            cout << "Задержка/Период запуска команд: FADD|FSUB|FCMP " << cfg[0].first << "/" << cfg[0].second << ". FMUL " << cfg[1].first << "/" << cfg[1].second
                << ". FDIV " << cfg[2].first << "/" << cfg[2].second << endl;
            cout << "Чтобы изменить задержку и период запуска команды: /latency команда. " << endl;
        }
        else if (input == "/prediction N" || input == "/p N") { jumpPrediction = "N"; cout << "Настройки обновлены!" << endl; }
        else if (input == "/prediction T" || input == "/p T") { jumpPrediction = "T"; cout << "Настройки обновлены!" << endl; }
        else if (input == "/prediction F" || input == "/p F") { jumpPrediction = "F"; cout << "Настройки обновлены!" << endl; }

        else if (input == "/jumpstage MEM" || input == "/j MEM") { stageAccess = "MEM"; cout << "Настройки обновлены!" << endl; }
        else if (input == "/jumpstage ID" || input == "/j ID") { stageAccess = "ID"; cout << "Настройки обновлены!" << endl; }

        else if (input == "/debug true" || input == "/d true") { debug = true; cout << "Настройки обновлены!" << endl; }
        else if (input == "/debug false" || input == "/d false") { debug = false; cout << "Настройки обновлены!" << endl; }


        else if (input == "/forwarding true" || input == "/f true") { forwarding = true; cout << "Настройки обновлены!" << endl; }
        else if (input == "/forwarding false" || input == "/f false") { forwarding = false; cout << "Настройки обновлены!" << endl; }
        else if (input == "/latency ADD" || input == "/l ADD") {
            cout << "Введите задержку команды ADD|SUB|CMP: " << endl;
            int inp; cin >> inp;  cfg[0].first = inp;
            cout << "Введите период запуска команды ADD|SUB|CMP: " << endl;
            cin >> inp;  cfg[0].second = inp;
            cout << "Настройки обновлены!" << endl;
        }
        else if (input == "/latency MUL" || input == "/l MUL") {
            cout << "Введите задержку команды MUL: " << endl;
            int inp; cin >> inp;  cfg[1].first = inp;
            cout << "Введите период запуска команды MUL: " << endl;
            cin >> inp;  cfg[1].second = inp;
            cout << "Настройки обновлены!" << endl;
        }
        else if (input == "/latency DIV" || input == "/l DIV") {
            cout << "Введите задержку команды DIV: " << endl;
            int inp; cin >> inp;  cfg[2].first = inp;
            cout << "Введите период запуска команды DIV: " << endl;
            cin >> inp;  cfg[2].second = inp;
            cout << "Настройки обновлены!" << endl;
        }

        else if (input == "/commands" || input == "/c") { cout << line << endl; }
        else if (input == "/back" || input == "/b") { line = extraLine; }
        else if (input == "/exit" || input == "/e") { return 0; }
        else if (!input.empty()) {
            line = input;
            autoPipe(line, forwarding, stageAccess, jumpPrediction, cfg, debug);
            if (!saved) { extraLine = line; saved = true; }
            else { saved = false; }
        }
    }
}
