namespace _2_lab_computer_graphics
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.отменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cHARlieTakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.точечныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инверсияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.черноБелыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сепияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.яркостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.яркостьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.медианныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.максимумToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переносToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поворотToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.волныВертикальныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.волныГоризонтальныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стеклоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.минимумToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.матричныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размытиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размытиеПоГауссуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.собельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.резкостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.резкостьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.размытиеВДвиженииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.комбинированныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тиснениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тиснениецветнойToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.светящиесяКраяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.щарраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прюиттаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лютоеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шахматыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.Stop = new System.Windows.Forms.Button();
            this.Dpicture = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменаToolStripMenuItem,
            this.файлToolStripMenuItem,
            this.фильтрыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1054, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // отменаToolStripMenuItem
            // 
            this.отменаToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.отменаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.отменаToolStripMenuItem.Name = "отменаToolStripMenuItem";
            this.отменаToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.отменаToolStripMenuItem.Text = "Назад";
            this.отменаToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.cHARlieTakeToolStripMenuItem});
            this.файлToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.открытьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlText;
            this.сохранитьКакToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // cHARlieTakeToolStripMenuItem
            // 
            this.cHARlieTakeToolStripMenuItem.Name = "cHARlieTakeToolStripMenuItem";
            this.cHARlieTakeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.cHARlieTakeToolStripMenuItem.Text = "CHARlie take";
            this.cHARlieTakeToolStripMenuItem.Click += new System.EventHandler(this.cHARlieTakeToolStripMenuItem_Click);
            // 
            // фильтрыToolStripMenuItem
            // 
            this.фильтрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.точечныеToolStripMenuItem,
            this.матричныеToolStripMenuItem,
            this.комбинированныеToolStripMenuItem,
            this.операторыToolStripMenuItem,
            this.лютоеToolStripMenuItem});
            this.фильтрыToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            this.фильтрыToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // точечныеToolStripMenuItem
            // 
            this.точечныеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.точечныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инверсияToolStripMenuItem,
            this.черноБелыйToolStripMenuItem,
            this.сепияToolStripMenuItem,
            this.яркостьToolStripMenuItem,
            this.яркостьToolStripMenuItem1,
            this.медианныйToolStripMenuItem,
            this.максимумToolStripMenuItem,
            this.переносToolStripMenuItem,
            this.поворотToolStripMenuItem,
            this.волныВертикальныеToolStripMenuItem,
            this.волныГоризонтальныеToolStripMenuItem,
            this.стеклоToolStripMenuItem,
            this.минимумToolStripMenuItem});
            this.точечныеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.точечныеToolStripMenuItem.Name = "точечныеToolStripMenuItem";
            this.точечныеToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.точечныеToolStripMenuItem.Text = "Точечные";
            // 
            // инверсияToolStripMenuItem
            // 
            this.инверсияToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuText;
            this.инверсияToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.инверсияToolStripMenuItem.Name = "инверсияToolStripMenuItem";
            this.инверсияToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.инверсияToolStripMenuItem.Text = "Инверсия";
            this.инверсияToolStripMenuItem.Click += new System.EventHandler(this.инверсияToolStripMenuItem_Click);
            // 
            // черноБелыйToolStripMenuItem
            // 
            this.черноБелыйToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuText;
            this.черноБелыйToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.черноБелыйToolStripMenuItem.Name = "черноБелыйToolStripMenuItem";
            this.черноБелыйToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.черноБелыйToolStripMenuItem.Text = "Черно белый";
            this.черноБелыйToolStripMenuItem.Click += new System.EventHandler(this.черноБелыйToolStripMenuItem_Click);
            // 
            // сепияToolStripMenuItem
            // 
            this.сепияToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuText;
            this.сепияToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.сепияToolStripMenuItem.Name = "сепияToolStripMenuItem";
            this.сепияToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.сепияToolStripMenuItem.Text = "Сепия";
            this.сепияToolStripMenuItem.Click += new System.EventHandler(this.сепияToolStripMenuItem_Click);
            // 
            // яркостьToolStripMenuItem
            // 
            this.яркостьToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuText;
            this.яркостьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.яркостьToolStripMenuItem.Name = "яркостьToolStripMenuItem";
            this.яркостьToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.яркостьToolStripMenuItem.Text = "Яркость+";
            this.яркостьToolStripMenuItem.Click += new System.EventHandler(this.яркостьToolStripMenuItem_Click);
            // 
            // яркостьToolStripMenuItem1
            // 
            this.яркостьToolStripMenuItem1.BackColor = System.Drawing.SystemColors.MenuText;
            this.яркостьToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.яркостьToolStripMenuItem1.Name = "яркостьToolStripMenuItem1";
            this.яркостьToolStripMenuItem1.Size = new System.Drawing.Size(259, 26);
            this.яркостьToolStripMenuItem1.Text = "Яркость-";
            this.яркостьToolStripMenuItem1.Click += new System.EventHandler(this.яркостьToolStripMenuItem1_Click);
            // 
            // медианныйToolStripMenuItem
            // 
            this.медианныйToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.медианныйToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.медианныйToolStripMenuItem.Name = "медианныйToolStripMenuItem";
            this.медианныйToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.медианныйToolStripMenuItem.Text = "Медианный";
            this.медианныйToolStripMenuItem.Click += new System.EventHandler(this.медианныйToolStripMenuItem_Click);
            // 
            // максимумToolStripMenuItem
            // 
            this.максимумToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.максимумToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.максимумToolStripMenuItem.Name = "максимумToolStripMenuItem";
            this.максимумToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.максимумToolStripMenuItem.Text = "Максимум";
            this.максимумToolStripMenuItem.Click += new System.EventHandler(this.максимумToolStripMenuItem_Click);
            // 
            // переносToolStripMenuItem
            // 
            this.переносToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.переносToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.переносToolStripMenuItem.Name = "переносToolStripMenuItem";
            this.переносToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.переносToolStripMenuItem.Text = "Перенос";
            this.переносToolStripMenuItem.Click += new System.EventHandler(this.переносToolStripMenuItem_Click);
            // 
            // поворотToolStripMenuItem
            // 
            this.поворотToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.поворотToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.поворотToolStripMenuItem.Name = "поворотToolStripMenuItem";
            this.поворотToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.поворотToolStripMenuItem.Text = "Поворот";
            this.поворотToolStripMenuItem.Click += new System.EventHandler(this.поворотToolStripMenuItem_Click);
            // 
            // волныВертикальныеToolStripMenuItem
            // 
            this.волныВертикальныеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.волныВертикальныеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.волныВертикальныеToolStripMenuItem.Name = "волныВертикальныеToolStripMenuItem";
            this.волныВертикальныеToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.волныВертикальныеToolStripMenuItem.Text = "Волны Вертикальные";
            this.волныВертикальныеToolStripMenuItem.Click += new System.EventHandler(this.волныВертикальныеToolStripMenuItem_Click);
            // 
            // волныГоризонтальныеToolStripMenuItem
            // 
            this.волныГоризонтальныеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.волныГоризонтальныеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.волныГоризонтальныеToolStripMenuItem.Name = "волныГоризонтальныеToolStripMenuItem";
            this.волныГоризонтальныеToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.волныГоризонтальныеToolStripMenuItem.Text = "Волны Горизонтальные";
            this.волныГоризонтальныеToolStripMenuItem.Click += new System.EventHandler(this.волныГоризонтальныеToolStripMenuItem_Click);
            // 
            // стеклоToolStripMenuItem
            // 
            this.стеклоToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.стеклоToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.стеклоToolStripMenuItem.Name = "стеклоToolStripMenuItem";
            this.стеклоToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.стеклоToolStripMenuItem.Text = "Стекло";
            this.стеклоToolStripMenuItem.Click += new System.EventHandler(this.стеклоToolStripMenuItem_Click);
            // 
            // минимумToolStripMenuItem
            // 
            this.минимумToolStripMenuItem.Name = "минимумToolStripMenuItem";
            this.минимумToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.минимумToolStripMenuItem.Text = "Минимум";
            this.минимумToolStripMenuItem.Click += new System.EventHandler(this.минимумToolStripMenuItem_Click);
            // 
            // матричныеToolStripMenuItem
            // 
            this.матричныеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.матричныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.размытиеToolStripMenuItem,
            this.размытиеПоГауссуToolStripMenuItem,
            this.собельToolStripMenuItem,
            this.резкостьToolStripMenuItem,
            this.резкостьToolStripMenuItem1,
            this.размытиеВДвиженииToolStripMenuItem});
            this.матричныеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.матричныеToolStripMenuItem.Name = "матричныеToolStripMenuItem";
            this.матричныеToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.матричныеToolStripMenuItem.Text = "Матричные";
            // 
            // размытиеToolStripMenuItem
            // 
            this.размытиеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.размытиеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.размытиеToolStripMenuItem.Name = "размытиеToolStripMenuItem";
            this.размытиеToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.размытиеToolStripMenuItem.Text = "Размытие";
            this.размытиеToolStripMenuItem.Click += new System.EventHandler(this.размытиеToolStripMenuItem_Click);
            // 
            // размытиеПоГауссуToolStripMenuItem
            // 
            this.размытиеПоГауссуToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.размытиеПоГауссуToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.размытиеПоГауссуToolStripMenuItem.Name = "размытиеПоГауссуToolStripMenuItem";
            this.размытиеПоГауссуToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.размытиеПоГауссуToolStripMenuItem.Text = "Размытие по Гауссу";
            this.размытиеПоГауссуToolStripMenuItem.Click += new System.EventHandler(this.размытиеПоГауссуToolStripMenuItem_Click);
            // 
            // собельToolStripMenuItem
            // 
            this.собельToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.собельToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.собельToolStripMenuItem.Name = "собельToolStripMenuItem";
            this.собельToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.собельToolStripMenuItem.Text = "Собель";
            this.собельToolStripMenuItem.Click += new System.EventHandler(this.собельToolStripMenuItem_Click);
            // 
            // резкостьToolStripMenuItem
            // 
            this.резкостьToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.резкостьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.резкостьToolStripMenuItem.Name = "резкостьToolStripMenuItem";
            this.резкостьToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.резкостьToolStripMenuItem.Text = "Резкость+";
            this.резкостьToolStripMenuItem.Click += new System.EventHandler(this.резкостьToolStripMenuItem_Click);
            // 
            // резкостьToolStripMenuItem1
            // 
            this.резкостьToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ControlText;
            this.резкостьToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.резкостьToolStripMenuItem1.Name = "резкостьToolStripMenuItem1";
            this.резкостьToolStripMenuItem1.Size = new System.Drawing.Size(251, 26);
            this.резкостьToolStripMenuItem1.Text = "Резкость++";
            this.резкостьToolStripMenuItem1.Click += new System.EventHandler(this.резкостьToolStripMenuItem1_Click);
            // 
            // размытиеВДвиженииToolStripMenuItem
            // 
            this.размытиеВДвиженииToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.размытиеВДвиженииToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.размытиеВДвиженииToolStripMenuItem.Name = "размытиеВДвиженииToolStripMenuItem";
            this.размытиеВДвиженииToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.размытиеВДвиженииToolStripMenuItem.Text = "Размытие в движении ";
            this.размытиеВДвиженииToolStripMenuItem.Click += new System.EventHandler(this.размытиеВДвиженииToolStripMenuItem_Click);
            // 
            // комбинированныеToolStripMenuItem
            // 
            this.комбинированныеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.комбинированныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.тиснениеToolStripMenuItem,
            this.тиснениецветнойToolStripMenuItem,
            this.светящиесяКраяToolStripMenuItem});
            this.комбинированныеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.комбинированныеToolStripMenuItem.Name = "комбинированныеToolStripMenuItem";
            this.комбинированныеToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.комбинированныеToolStripMenuItem.Text = "Комбинированные";
            // 
            // тиснениеToolStripMenuItem
            // 
            this.тиснениеToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.тиснениеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.тиснениеToolStripMenuItem.Name = "тиснениеToolStripMenuItem";
            this.тиснениеToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.тиснениеToolStripMenuItem.Text = "Тиснение (серый)";
            this.тиснениеToolStripMenuItem.Click += new System.EventHandler(this.тиснениеToolStripMenuItem_Click);
            // 
            // тиснениецветнойToolStripMenuItem
            // 
            this.тиснениецветнойToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.тиснениецветнойToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.тиснениецветнойToolStripMenuItem.Name = "тиснениецветнойToolStripMenuItem";
            this.тиснениецветнойToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.тиснениецветнойToolStripMenuItem.Text = "Тиснение (цветной)";
            this.тиснениецветнойToolStripMenuItem.Click += new System.EventHandler(this.тиснениецветнойToolStripMenuItem_Click);
            // 
            // светящиесяКраяToolStripMenuItem
            // 
            this.светящиесяКраяToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.светящиесяКраяToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.светящиесяКраяToolStripMenuItem.Name = "светящиесяКраяToolStripMenuItem";
            this.светящиесяКраяToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.светящиесяКраяToolStripMenuItem.Text = "Светящиеся края";
            this.светящиесяКраяToolStripMenuItem.Click += new System.EventHandler(this.светящиесяКраяToolStripMenuItem_Click);
            // 
            // операторыToolStripMenuItem
            // 
            this.операторыToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlText;
            this.операторыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.щарраToolStripMenuItem,
            this.прюиттаToolStripMenuItem});
            this.операторыToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.операторыToolStripMenuItem.Name = "операторыToolStripMenuItem";
            this.операторыToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.операторыToolStripMenuItem.Text = "Операторы";
            // 
            // щарраToolStripMenuItem
            // 
            this.щарраToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.щарраToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.щарраToolStripMenuItem.Name = "щарраToolStripMenuItem";
            this.щарраToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.щарраToolStripMenuItem.Text = "Щарра";
            this.щарраToolStripMenuItem.Click += new System.EventHandler(this.щарраToolStripMenuItem_Click);
            // 
            // прюиттаToolStripMenuItem
            // 
            this.прюиттаToolStripMenuItem.BackColor = System.Drawing.SystemColors.Desktop;
            this.прюиттаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.прюиттаToolStripMenuItem.Name = "прюиттаToolStripMenuItem";
            this.прюиттаToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.прюиттаToolStripMenuItem.Text = "Прюитта";
            this.прюиттаToolStripMenuItem.Click += new System.EventHandler(this.прюиттаToolStripMenuItem_Click);
            // 
            // лютоеToolStripMenuItem
            // 
            this.лютоеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.шахматыToolStripMenuItem});
            this.лютоеToolStripMenuItem.Name = "лютоеToolStripMenuItem";
            this.лютоеToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.лютоеToolStripMenuItem.Text = "Лютое";
            // 
            // шахматыToolStripMenuItem
            // 
            this.шахматыToolStripMenuItem.Name = "шахматыToolStripMenuItem";
            this.шахматыToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.шахматыToolStripMenuItem.Text = "Шахматы";
            this.шахматыToolStripMenuItem.Click += new System.EventHandler(this.шахматыToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1054, 642);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 690);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(816, 48);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.UseWaitCursor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Stop
            // 
            this.Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop.BackColor = System.Drawing.SystemColors.MenuText;
            this.Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Stop.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Stop.Location = new System.Drawing.Point(859, 690);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(183, 48);
            this.Stop.TabIndex = 4;
            this.Stop.Text = "Отмена";
            this.Stop.UseVisualStyleBackColor = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Dpicture
            // 
            this.Dpicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dpicture.BackColor = System.Drawing.Color.Transparent;
            this.Dpicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Dpicture.ContextMenuStrip = this.contextMenuStrip1;
            this.Dpicture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Dpicture.ErrorImage = null;
            this.Dpicture.Image = ((System.Drawing.Image)(resources.GetObject("Dpicture.Image")));
            this.Dpicture.InitialImage = null;
            this.Dpicture.Location = new System.Drawing.Point(117, 168);
            this.Dpicture.Name = "Dpicture";
            this.Dpicture.Size = new System.Drawing.Size(801, 381);
            this.Dpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Dpicture.TabIndex = 6;
            this.Dpicture.TabStop = false;
            this.Dpicture.Click += new System.EventHandler(this.Dpicture_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1054, 753);
            this.Controls.Add(this.Dpicture);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1024, 800);
            this.Name = "Form1";
            this.Text = "Photo Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фильтрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem точечныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem инверсияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem матричныеToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button Stop;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.ToolStripMenuItem размытиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размытиеПоГауссуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem черноБелыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сепияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem яркостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem собельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem резкостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem яркостьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem комбинированныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тиснениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тиснениецветнойToolStripMenuItem;
        private System.Windows.Forms.PictureBox Dpicture;
        private System.Windows.Forms.ToolStripMenuItem медианныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem максимумToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem светящиесяКраяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переносToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поворотToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem волныВертикальныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem волныГоризонтальныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem стеклоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размытиеВДвиженииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem резкостьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem операторыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem щарраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прюиттаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem минимумToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem лютоеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шахматыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cHARlieTakeToolStripMenuItem;
    }
}

