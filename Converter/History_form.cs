using System;
using System.Windows.Forms;

namespace Converter
{
    public partial class History_form : Form
    {
        public History_form()
        {
            InitializeComponent();
            History.FillData(dataGridViewHistory);
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

        // Очистка истории
        private void buttonClear_Click(object sender, EventArgs e)
        {
            History.EraseHistory();
            dataGridViewHistory.Rows.Clear();
        }
    }
}
