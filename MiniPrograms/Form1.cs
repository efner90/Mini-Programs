using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPrograms
{
    public partial class fMainForm : Form
    {
        public int count = 0;
        Random rnd = new Random();
        
        public fMainForm()
        {
            InitializeComponent();
        }

        private void tsmExit_Click(object sender, EventArgs e) //кнопка выход
        {
            this.Close();
        }

        private void tsmAbout_Click(object sender, EventArgs e) //кнопка о программе
        {
            MessageBox.Show("Программа 'Universe utilites' служит для практики с языком С#, чтобы показать " +
                "как работают основы языка и FindowsForm. \nАвторство Мацков А.Е.");
        }

        private void btnPlus_Click(object sender, EventArgs e) //кнопка плюс
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e) //кнопка минус
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e) //сборс
        {
            count = 0;
            lblCount.Text =Convert.ToString(count); //ещё способ конвертирования инт в стр
        }

        private void btnGeneric_Click(object sender, EventArgs e) //сгенерироть рандом
        {
            int n;
            n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            lblRandom.Text = n.ToString();
            if (chbRepeat.Checked)
            {
                int count = 0;
                while (tbRandom.Text.IndexOf(n.ToString()) != -1)//исключаем повторения
                {
                    n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
                    count++;
                    lblRandom.Text = n.ToString();
                    if (count > 1000) break;
                }
                    if (count <=1000)
                    tbRandom.AppendText(n + "\r\n");
                
            }
            else tbRandom.AppendText(n + "\r\n");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text); //сохраняем в буфер обмена
        }
    }
}
