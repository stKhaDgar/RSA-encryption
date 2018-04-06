using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_encryption
{

    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Алфавит, используемый в программе
        char[] characters = new char[] 
        { '$', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
          'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
          'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
          'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
          '8', '9', '0' };

        public Form1()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // Кнопка "Зашифровать"
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
