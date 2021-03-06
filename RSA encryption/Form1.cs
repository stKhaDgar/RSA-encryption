﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace RSA_encryption
{

    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Проверка на наличие каталога
        public Form1()
        {
            InitializeComponent();
            bool di = Directory.Exists("Folder");
            if (di == true)
            {
                create_Catalog.Text = "Открыть каталог";
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                button_Encrypt.Visible = true;
                button_Encode.Visible = true;
                button_Reload.Visible = true;
                textBox_p.Visible = true;
                textBox_q.Visible = true;
                textBox_d.Visible = true;
                textBox_n.Visible = true;
                richTextBox1.Visible = true;
                richTextBox2.Visible = true;
                richTextBox3.Visible = true;
            }
            else
            {
                create_Catalog.Text = "Создать каталог";
            }
        }

        // Алфавит, используемый в программе
        char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
          'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
          'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
          'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
          '8', '9', '0', '\n', '\r'};

       
        // Вычисление наименьшего общего делителя 'd' (должно быть взаимно простым с параметром m)
        private long Calculate_d(long m)
        {
            long d = m - 1;
            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }
            return d;
        }

        // Функция для вычисление 'e'
        private long Calculate_e(long d, long m)
        {
            long e = 10;
            while (true)
            {   
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
            return e;
        }
        
        // Кнопка "Зашифровать"
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox_p.Text.Length > 0) && (textBox_q.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox_p.Text);
                long q = Convert.ToInt64(textBox_q.Text);

                
                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                    richTextBox3.Clear();
                    string s = "";
                    
                    Encoding enc = Encoding.GetEncoding(1251);
                    StreamReader sr = new StreamReader("Folder/in.txt" , enc);
                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }
                    sr.Close();
                    s = s.ToUpper();
                    Console.WriteLine(s);
                    
                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long d = Calculate_d(m);
                    long e_ = Calculate_e(d, m);
                    List<string> result = RSA_Endoce(s, e_, n);
                    StreamWriter sw = new StreamWriter("Folder/out1.txt");
                    foreach (string item in result)
                    {
                        sw.WriteLine(item);
                        richTextBox3.Text += item + '\n';
                    }
                        
                    sw.Close();
                    textBox_d.Text = d.ToString();
                    textBox_n.Text = n.ToString();
                    
                }
                else
                    MessageBox.Show("p или q - не простые числа!");
            }
            else
                MessageBox.Show("Введите p и q!");
        }

        // Кнопка "Расшифровать"
        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox_d.Text.Length > 0) && (textBox_n.Text.Length > 0))
            {
                long d = Convert.ToInt64(textBox_d.Text);
                long n = Convert.ToInt64(textBox_n.Text);

                List<string> input = new List<string>();

                StreamReader sr = new StreamReader("Folder/out1.txt");

                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }

                sr.Close();

                string result = RSA_Dedoce(input, d, n);

                StreamWriter sw = new StreamWriter("Folder/out2.txt");
                sw.WriteLine(result);
                richTextBox2.Text = result;
                sw.Close();
                
            }
            else
                MessageBox.Show("Введите секретный ключ!");
        }

        // Проверяет простое ли число
        private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }   

        // Функция для шифрования строки при помощи алгоритма RSA
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;
                result.Add(bi.ToString());
            }

            return result;
        }

        // Функция для расшифрования строки при помощи алгоритма RSA
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        // Кнопка для создания каталога с необходимыми файлами
        private void create_Catalog_Click(object sender, EventArgs e)
        {
            bool di = Directory.Exists("Folder");
            if (di == true)
            {
                Process.Start(new ProcessStartInfo("Folder"));
                Console.WriteLine("Directory exists.");
            }
            else
            {
                MessageBox.Show("Каталог создан");
                Directory.CreateDirectory("Folder");
                create_Catalog.Text = "Открыть каталог";
                Console.WriteLine("Directory created.");

                // Отображение всех элементов формы
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                button_Encrypt.Visible = true;
                button_Encode.Visible = true;
                button_Reload.Visible = true;
                textBox_p.Visible = true;
                textBox_q.Visible = true;
                textBox_d.Visible = true;
                textBox_n.Visible = true;
                richTextBox1.Visible = true;
                richTextBox2.Visible = true;
                richTextBox3.Visible = true;

                // Создание необходимых файлов
                File.Create("Folder/in.txt").Close();
                File.Create("Folder/out1.txt").Close();
                File.Create("Folder/out2.txt").Close();
            }
        }

        // Удаление каталога при завершении программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool di = Directory.Exists("Folder");
            if (di == true)
            {
                DirectoryInfo dirInfo = new DirectoryInfo("Folder");
                Directory.Delete("Folder", true);
            }
        }

        private void button_Reload_Click(object sender, EventArgs e)
        {
            string s = "";
            Encoding enc = Encoding.GetEncoding(1251);
            StreamReader sr = new StreamReader("Folder/in.txt", enc);
            while (!sr.EndOfStream)
            {
                s += sr.ReadLine();
                s += "\n";
                Console.WriteLine(s);
            }
            sr.Close();
            richTextBox1.Text = s.ToString();
        }
    }
}
