using System;
using System.Windows.Forms;

namespace Converter
{
    public partial class Main : Form
    {
        // Флаг, определяющий, содержит ли число десятичную точку
        private bool hasDot = false;

        public Main()
        {
            InitializeComponent();

            // Отображаем дефолтные системы счисления
            comboBoxScaleP1.Text = comboBoxScaleP1.Items[0].ToString(); // двоичная
            comboBoxScaleP2.Text = comboBoxScaleP2.Items[8].ToString(); // десятичная
        }

        // Закрытие приложения
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Сворачивание приложения
        private void buttonTurn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Перетаскивание окна
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            panel2.Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        // Преобразование числа из одной системы в другую
        private void buttonConversion_Click(object sender, EventArgs e)
        {
            if (labelNumberInput.Text == "")
            {
                labelError.Visible = true;
                labelError.Text = "Введите исходное число для конвертации!";
            }
            else
            {
                labelError.Visible = false;
                byte p1 = Convert.ToByte(comboBoxScaleP1.Text);
                byte p2 = Convert.ToByte(comboBoxScaleP2.Text);

                try
                {
                    // Если последний элемент строки точка, то поставить ноль
                    if (labelNumberInput.Text[labelNumberInput.Text.Length - 1].ToString() == ".")
                        labelNumberInput.Text += "0";

                    labelNumberResult.Text = Control.DoConversion(labelNumberInput.Text, p1, p2);
                    History.AddToHistory(labelNumberInput.Text, p1, p2, labelNumberResult.Text);
                    History.AddToFile();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Ошибка при конвертации");
                }
            }
        }

        // Кнопка BC - backspace. Удаление крайнего символа
        private void buttonBC_DoBackspace(object sender, EventArgs e)
        {
            if (labelNumberInput.Text != string.Empty) // Если строка не пуста
            {
                if (labelNumberInput.Text[labelNumberInput.Text.Length - 1] == '.')
                    hasDot = false;                
                labelNumberInput.Text = labelNumberInput.Text.Remove(labelNumberInput.Text.Length - 1);
                if (labelNumberInput.Text == "-")
                    labelNumberInput.Text = "";
            }
        }

        // Кнопка CE. Очистка поля ввода
        private void buttonCE_DoClean(object sender, EventArgs e)
        {
            hasDot = false;
            labelNumberInput.Text = "";
            labelNumberResult.Text = "";
        }

        // Кнопка справки
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help form = new Help();
            form.ShowDialog();
        }

        // Кнопка истории
        private void buttonHistory_Click(object sender, EventArgs e)
        {
            History_form form = new History_form();
            form.ShowDialog();
        }

        // Изменение системы счисления в первом comboBox -> Меняем видимость кнопок
        private void comboBoxScaleP1_Change(object sender, EventArgs e)
        {
            int scale = Convert.ToInt32(comboBoxScaleP1.SelectedItem); // выбранное число

            for (int i = 0; i < scale; i++)
                panelButtons.Controls[i].Enabled = true;
            for (int i = scale; i <= comboBoxScaleP1.Items.Count; i++)
                panelButtons.Controls[i].Enabled = false;
            buttonCE_DoClean(null, null);
        }

        // Добавление числа в поле ввода, в зависимости от кнопки
        public void button_AddDigit(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            // Первым ноль ставить нельзя. Ставим ноль только в случае, если поле не пустое
            if (button.Text != "0" || labelNumberInput.Text != "")
                labelNumberInput.Text += button.Text;
        }

        // Добавление точки в поле ввода
        private void buttonDot_AddDot(object sender, EventArgs e)
        {
            if (labelNumberInput.Text == "")
                labelNumberInput.Text += "0";
            if (!hasDot)
            {
                labelNumberInput.Text += ".";
                hasDot = true;
            }
        }

        // Смена знака в поле ввода
        private void buttonMinusPlus_ChangeSign(object sender, EventArgs e)
        {
            string str = labelNumberInput.Text;
            if (labelNumberInput.Text != "")
            {
                if (labelNumberInput.Text[0] == '-')
                    labelNumberInput.Text = labelNumberInput.Text.Substring(1, labelNumberInput.Text.Length - 1);
                else
                    labelNumberInput.Text = "-" + str;
            }
        }
    }
}
