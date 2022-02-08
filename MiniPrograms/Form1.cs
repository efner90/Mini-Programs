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
        char[] spec_chars = new char[] { '!', '@', '#', '$', '%', '^', '&', '*','~','?','*' };
        Dictionary<string, double> metrica; //создаём словарь

        string[] lengthName = new string[] { "mm", "cm", "dm", "m", "km", "mile" }; //длина
        double[] lengthValue = new double[] { 1, 10, 100, 1000, 1000000, 1609344 };

        string[] weightName = new string[] { "g", "kg", "t", "lb/'funt'", "oz/'uncia'" };
        double[] weightValue = new double[] { 1, 1000, 1000000, 453.6, 28.3 };


        public fMainForm()
        {
            InitializeComponent();
            metrica = new Dictionary<string, double>();
            for (int i = 0; i < lengthName.Length; i++) //заполняем словарик длины
            {
                metrica.Add(lengthName[i], lengthValue[i]);
            }

            //for (int i = 0; i < weightName.Length; i++) //заполняем словарик веса
            //{
            //    metrica.Add(weightName[i], weightValue[i]);
            //}

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

        private void tsmInsertDate_Click(object sender, EventArgs e) //добавляем текущую дату
        {
            rtbBloknot.AppendText(DateTime.Now.ToShortDateString() + "\n");
            
        }

        private void tsmInsertTime_Click(object sender, EventArgs e)//текущее время
        {
            rtbBloknot.AppendText(DateTime.Now.ToShortTimeString() + "\n");
        }
        /// <summary>
        ///Загрузка файла Ноутпад
        /// </summary>
        void LoadNotepad()
        {
            try
            {
                rtbBloknot.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке");
            }
        }
        private void tsmSave_Click(object sender, EventArgs e) //сохраняем файл
        {
            try
            {
                rtbBloknot.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении");
            }
        }

        private void tsmLoad_Click(object sender, EventArgs e) //загружаем файл
        {                   //важно отметить, что сохранение и загрузка
                            //осуществляется в одном конкретном файле                      
            LoadNotepad();
        }

        private void fMainForm_Load(object sender, EventArgs e) //загружается при открытии фолрмы
        {
            LoadNotepad();
            clbPassword.SetItemChecked(0, true); //выбираем сразу пару чекбоксов при загрузен формы
            clbPassword.SetItemChecked(1, true);

        }

        private void btnCreatePassword_Click(object sender, EventArgs e) //создаём пасс
        {
            if (clbPassword.CheckedItems.Count == 0) return; //ничего не выбрано - ничего не делаем, возвращаем
            string password = ""; //создаём пустое
            for (int i = 0; i<nudPassLength.Value; i++) //проходим по длине включенных чекбоксов
            {
                int n=rnd.Next(0, clbPassword.CheckedItems.Count); //рандом от нуля до вклю боксов
                string s= clbPassword.CheckedItems[n].ToString(); //сохраняем сгенерированный выше
                switch (s)
                {
                    case "Цифры": password +=rnd.Next(10).ToString(); 
                        break; //0-9
                    case "Прописные буквы": password += Convert.ToChar(rnd.Next(65, 88)); 
                        break;
                    //делаем рандом символов по таблице
                    case "Строчные буквы": password += Convert.ToChar(rnd.Next(97, 122)); 
                        break;
                    default:
                        password += spec_chars[rnd.Next(spec_chars.Length)];
                        break;
                }
                tbPasswordField.Text = password;
                //Clipboard.SetText(password); сразу в буфер пихает
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text]; //берём Из
            double m2 = metrica[cbTo.Text]; //берём В
            double conv = Convert.ToDouble(tbFrom.Text); //берём нужную сумму
            tbTo.Text = (conv*m1/m2).ToString(); //конвертируем
        }

        private void btnSwap_Click(object sender, EventArgs e) //меняем величины местами
        {
            string t = cbFrom.Text; 
            cbFrom.Text = cbTo.Text;
            cbTo.Text = t;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            metrica.Clear();
            //cbFrom.Items.Clear();
            //cbTo.Items.Clear();            
            switch (cbMetric.Text)
            {
                case "Длина":
                    metrica.Clear();
                    cbFrom.Items.Clear();
                    cbTo.Items.Clear();
                    for (int i = 0;i< lengthName.Length;i++)
                    {
                        metrica.Add(lengthName[i], lengthValue[i]);
                        cbFrom.Items.Add(lengthName[i]);
                        cbTo.Items.Add(lengthName[i]);
                    }
                    cbFrom.Text = lengthName[0];
                    cbTo.Text = lengthName[0];
                    break;

                case "Вес":
                    //metrica.Clear();
                    cbFrom.Items.Clear();
                    cbTo.Items.Clear();
                    for (int i = 0;i<weightName.Length;i++)
                    {
                        metrica.Add(weightName[i], weightValue[i]);
                        cbFrom.Items.Add(weightName [i]);
                        cbTo.Items.Add(weightName[i]);
                    }
                    cbFrom.Text = weightName[0];
                    cbTo.Text = weightName[0];
                    break;
                default:
                    break;
            }
        }
    }
}
