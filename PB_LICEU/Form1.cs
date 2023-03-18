using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace PB_LICEU
{
    public partial class Form1 : Form
    {
        Queue<Tuple<int,int>> q = new Queue<Tuple<int,int>>();
        static int[] di = { 1, -1, 0, 0 };
        static int[] dj = { 0, 0, 1, -1 };

        static int n, s, t, m1, m2, s1, s2, s3, s4, cnt, prec, result, contor, ppp, px, py, portalek, changed;

        private void button4_Click(object sender, EventArgs e)
        {
            if (result == 0 || changed > 0)
            {
                t = (int)numericUpDown2.Value;
                r1 = new int[405];
                r2 = new int[405];
                bordare();
                lee(cm, 1, 1);
                lee(cc, m1, m2);
                s = cm[n, n];///direct in clasa
                s1 = cm[m1, m2];///pana la magazin
                s2 = cc[n, n];///de la magazin pana la clasa
                if (cm[m1, m2] > 0 && s1 + s2 - 1 <= t)
                {
                    result = s1 + s2 - 1;
                    rec2(cm, m1, m2);
                    rec1(cc, n, n);
                    textBox1.Text = "baiatul trece si pe la magazin si ajunge in clasa in " + result + " secunde";
                }
                else
                {
                    result = s;
                    rec2(cm, n, n);
                    textBox1.Text = "baiatul nu reuseste sa treaca si pe la magazin in cele " + t + " secunde, insa reuseste sa ajunga inapoi in clasa in " + result + " secunde";
                }
            }
            if (px == n && py == n)
            {
                btt[n, n].BackColor = System.Drawing.Color.White;
                ppp = 0;
                px = 0;
            }
            changed = contor = cnt = 0;
            timer1.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form myForm = new Form();
            myForm.WindowState = FormWindowState.Maximized;
            myForm.Show();
            WebBrowser cerinta = new WebBrowser();
            cerinta.Url = new Uri(Application.StartupPath + "/enunt.html");
            cerinta.Size = myForm.Size;
            myForm.Controls.Add(cerinta);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string txtClipboard;
            StreamReader fin = new StreamReader("sursa.txt");
            txtClipboard = fin.ReadToEnd();
            Clipboard.SetText(txtClipboard);
            MessageBox.Show("SURSA COPIATA CU SUCCES IN CLIPBOARD!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            m1 = 0;
            cnt = contor = result = 0;
            flowLayoutPanel1.Controls.Clear();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "Text files|*.txt";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader fin = new StreamReader(openFileDialog1.FileName);
                int z, p, z1, z2;
                string[] s;
                s = fin.ReadLine().Split();
                n = int.Parse(s[0]);
                z = int.Parse(s[1]);
                p = int.Parse(s[2]);
                t = int.Parse(s[3]);
                cnt = p;
                x = new int[n + 5, n + 5];
                a = new int[n + 5, n + 5];
                b = new int[n + 5, n + 5];
                cm = new int[n + 5, n + 5];
                cc = new int[n + 5, n + 5];
                numericUpDown1.Value = n;
                numericUpDown2.Value = t;
                btt = new Button[n + 5, n + 5];
                flowLayoutPanel1.Height = n * 50;
                flowLayoutPanel1.Width = n * 50;
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                    {
                        btt[i, j] = new Button();
                        btt[i, j].Name = i + " " + j;
                        if (i == 1 && j == 1)
                            btt[i, j].Text = "S";
                        else if (i == n && j == n)
                            btt[i, j].Text = "F";
                        else
                            btt[i, j].Text = i + "," + j;
                        btt[i, j].Width = 40;
                        btt[i, j].Height = 40;
                        btt[i, j].BackColor = Color.White;
                        btt[i, j].ContextMenuStrip = contextMenuStrip1;
                        flowLayoutPanel1.Controls.Add(btt[i, j]);
                        btt[i, j].Click += new EventHandler(button_Click);
                    }
                for (int i = 1; i <= z; ++i)
                {
                    s = fin.ReadLine().Split();
                    z1 = int.Parse(s[0]);
                    z2 = int.Parse(s[1]);
                    x[z1, z2] = 1;
                    btt[z1, z2].Text = "X";
                    btt[z1, z2].BackColor = Color.Red;
                }
                for (int i = 1; i <= p; ++i)
                {
                    s = fin.ReadLine().Split();
                    s1 = int.Parse(s[0]);
                    s2 = int.Parse(s[1]);
                    s3 = int.Parse(s[2]);
                    s4 = int.Parse(s[3]);
                    a[s1,s2] = s3;
                    b[s1,s2] = s4;
                    a[s3,s4] = s1;
                    b[s3,s4] = s2;
                    btt[s1, s2].BackColor = Color.BlueViolet;
                    btt[s3, s4].BackColor = Color.BlueViolet;
                    btt[s1, s2].Text = "P" + i;
                    btt[s3, s4].Text = "P" + i;
                }

                s = fin.ReadLine().Split();
                m1 = int.Parse(s[0]);
                m2 = int.Parse(s[1]);
                btt[m1, m2].BackColor = Color.Gold;
                btt[m1, m2].Text = "M";

                fin.Close();
                    
                r1 = new int[405];
                r2 = new int[405];
                bordare();
                lee(cm, 1, 1);
                lee(cc, m1, m2);
                int ss;
                ss = cm[n, n];///direct in clasa
                s1 = cm[m1, m2];///pana la magazin
                s2 = cc[n, n];///de la magazin pana la clasa
                textBox1.Top = flowLayoutPanel1.Height + flowLayoutPanel1.Top + 10 - 3 * n;
                if (n > 7)
                    this.Height = flowLayoutPanel1.Height + flowLayoutPanel1.Top + 140 - 3 * n;
                if (cm[m1, m2] > 0 && s1 + s2 - 1 <= t)
                {
                    result = s1 + s2 - 1;
                    rec2(cm, m1, m2);
                    rec1(cc, n, n);
                    textBox1.Text = "baiatul trece si pe la magazin si ajunge in clasa in " + result + " secunde";
                }
                else
                {
                    result = ss;
                    rec2(cm, n, n);
                    textBox1.Text = "baiatul nu reuseste sa treaca si pe la magazin in cele " + t + " secunde, insa reuseste sa ajunga inapoi in clasa in " + result + " secunde";
                }
            }
            changed = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            n = (int)numericUpDown1.Value;
            changed = 1;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            t = (int)numericUpDown2.Value;
            changed = 1;
        }

        Bitmap Save_Frame()
        {
            Point start = new Point();
            start = flowLayoutPanel1.PointToScreen(Point.Empty);
            Size size = new Size(btt[n, n].Left + 40 + 10, btt[n, n].Top + 40 + 10);
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics grph = Graphics.FromImage(bitmap);
            grph.CopyFromScreen(start, Point.Empty, size);
            return bitmap;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (n > 0)
            {
                Bitmap bitmap = Save_Frame();
                bitmap.Save(result + ".png", ImageFormat.Png);
                MessageBox.Show("S-a salvat imaginea finala.");
            }
            else
                MessageBox.Show("Matricea nu este creata inca.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int[,] pus;
            pus = new int[n + 5, n + 5];
            for (int i = 1; i <= n; ++i)
                for (int j = 1; j <= n; ++j)
                    pus[i, j] = 0; ;
            int z = 0;
            for (int i = 1; i <= n; ++i)
                for (int j = 1; j <= n; ++j)
                    if (a[i, j] > 0)
                        ++s;
            s /= 2;
                    for (int i = 1; i <= n; ++i)
                for (int j = 1; j <= n; ++j)
                    if (x[i, j] == 1)
                        ++z;
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "Fisiere text|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter fout = new StreamWriter(saveFileDialog1.FileName);
                fout.Write(n + " " + z + " " + s + " " + t + '\n');
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                        if (x[i, j] == 1)
                            fout.Write(i + " " + j + '\n');
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                        if (a[i, j] > 0 && pus[i, j] == 0)
                        {
                            fout.Write(a[i, j] + " " + b[i, j] + " " + a[a[i, j], b[i, j]] + " " + b[a[i, j], b[i, j]] + '\n');
                            pus[a[i, j], b[i, j]] = 1;
                        }
                fout.Write(m1 + " " + m2);
                fout.Close();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value * 100;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=GAtb5M3kvMQ");
        }

        private void plaseazaMagazinulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem; if (item == null) return;
            ContextMenuStrip context = item.Owner as ContextMenuStrip; if (context == null) return;
            Button button = context.SourceControl as Button;
            changed = 1;
            if (m1 == 0)
            {
                button.BackColor = Color.Gold;
                button.Text = "M";
                string[] s = button.Name.Split();
                m1 = int.Parse(s[0]);
                m2 = int.Parse(s[1]);
                x[m1, m2] = 0;
            }
            else
            {
                btt[m1, m2].BackColor = Color.White;
                btt[m1, m2].Text = String.Format("{0},{1}", m1, m2);
                button.BackColor = Color.Gold;
                button.Text = "M";
                string[] s = button.Name.Split();
                m1 = int.Parse(s[0]);
                m2 = int.Parse(s[1]);
                x[m1, m2] = 0;
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            r1 = new int[405];
            r2 = new int[405];
            bordare();
            lee(cm, 1, 1);
            lee(cc, m1, m2);
            s = cm[n, n];///direct in clasa
            s1 = cm[m1, m2];///pana la magazin
            s2 = cc[n, n];///de la magazin pana la clasa
            if (cm[m1, m2] > 0 && s1 + s2 - 1 <= t)
            {
                result = s1 + s2 - 1;
                rec2(cm, m1, m2);
                rec1(cc, n, n);
                textBox1.Text = "baiatul trece si pe la magazin si ajunge in clasa in " + result + " secunde";
            }
            else
            {
                result = s;
                rec2(cm, n, n);
                textBox1.Text = "baiatul nu reuseste sa treaca si pe la magazin in cele " + t + " secunde, insa reuseste sa ajunga inapoi in clasa in " + result + " secunde";
            }
        }

        int[,] x, a, b, cm, cc;
        int[] r1, r2;
        Button[,] btt;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (px > 0)
            {
                if (btt[px, py].Text == "M")
                    btt[px, py].BackColor = Color.Gold;
                else if(btt[px, py].Text[0] == 'P')
                    btt[px, py].BackColor = Color.BlueViolet;
                else
                    btt[px, py].BackColor = Color.White;
            }
            if (px != n || py != n)
            {
                px = r1[++ppp];
                py = r2[ppp];
            }
            else
                timer1.Stop();
            btt[px, py].BackColor = Color.Green;
        }
        private void bordare()
        {
            for (int i = 1; i <= n; ++i)
                x[i, 0] = x[0, i] = x[i, n + 1] = x[n + 1, i] = 1;
        }
        private void lee(int[,] c, int Is, int js)
        {
            c[Is, js] = 1;
            q.Enqueue(new Tuple<int, int>(Is, js));
            while (q.Count > 0)
            {
                int i = q.Peek().Item1;
                int j = q.Peek().Item2;
                q.Dequeue();
                for (int d = 0; d < 4; ++d)
                {
                    int ii = i + di[d];
                    int jj = j + dj[d];
                    if (x[ii, jj] == 0 && c[ii, jj] == 0)
                    {
                        c[ii, jj] = c[i, j] + 1;
                        q.Enqueue(new Tuple<int, int>(ii, jj));
                    }
                }
                if (a[i, j] > 0)
                {
                    int ii = a[i, j];
                    int jj = b[i, j];
                    if (c[ii, jj] == 0)
                    {
                        c[ii, jj] = c[i, j] + 1;
                        q.Enqueue(new Tuple<int, int>(ii, jj));
                    }
                }
            }
        }
        void rec1(int[,] c, int i, int j)
        {
            if (i != m1 || j != m2)
            {
                bool ok = true;
                for (int d = 0; d < 4; ++d)
                {
                    int ii = i + di[d];
                    int jj = j + dj[d];
                    if (c[ii, jj] == c[i, j] - 1)
                    {
                        rec1(c, ii, jj);
                        ok = false;
                        break;
                    }
                }
                if (a[i, j] > 0 && ok == true)
                {
                    int ii = a[i, j];
                    int jj = b[i, j];
                    if (c[ii, jj] == c[i, j] - 1)
                        rec1(c, ii, jj);
                }
            }
            r1[++contor] = i;
            r2[contor] = j;
        }
        void rec2(int[,] c, int i, int j)
        {
            if (i != 1 || j != 1)
            {
                bool ok = true;
                for (int d = 0; d < 4; ++d)
                {
                    int ii = i + di[d];
                    int jj = j + dj[d];
                    if (c[ii, jj] == c[i, j] - 1)
                    {
                        rec2(c, ii, jj);
                        ok = false;
                        break;
                    }
                }
                if (a[i, j] > 0 && ok == true)
                {
                    int ii = a[i, j];
                    int jj = b[i, j];
                    if (c[ii, jj] == c[i, j] - 1)
                        rec2(c, ii, jj);
                }
            }
            if (i != m1 || j != m2)
            {
                r1[++contor] = i;
                r2[contor] = j;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (portalek == 0)
            {
                button3.BackColor = Color.DarkCyan;
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                    {
                        btt[i, j].Click -= button_Click;
                        btt[i, j].Click += button_ClickPort;
                    }
                textBox1.Text = "click stanga-portal";
                portalek = 1;
            }
            else
            {
                portalek = 0;
                button3.BackColor = SystemColors.ButtonFace;
                button3.UseVisualStyleBackColor = true;
                for (int i = 1; i <= n; ++i)
                    for (int j = 1; j <= n; ++j)
                    {
                        btt[i, j].Click -= button_ClickPort;
                        btt[i, j].Click += button_Click;
                    }
                textBox1.Text = "click stanga-zid | " + "click dreapta-magazin" + "(doar un singur magazin)";
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m1 = 0;
            cnt = contor = result = 0;
            flowLayoutPanel1.Controls.Clear();
            x = new int[n + 5, n + 5];
            a = new int[n + 5, n + 5];
            b = new int[n + 5, n + 5];
            cm = new int[n + 5, n + 5];
            cc = new int[n + 5, n + 5];
            btt = new Button[n + 5, n + 5];
            flowLayoutPanel1.Height = n * 50;
            flowLayoutPanel1.Width = n * 50;
            textBox1.Top = flowLayoutPanel1.Height + flowLayoutPanel1.Top + 10 - 3 * n;
            if(n>7)
                this.Height = flowLayoutPanel1.Height + flowLayoutPanel1.Top + 140 - 3 * n;
            for (int i = 1; i <= n; ++i)
                for (int j = 1; j <= n; ++j)
                {
                    btt[i, j] = new Button();
                    btt[i, j].Name = i + " " + j;
                    if (i == 1 && j == 1)
                        btt[i, j].Text = "S";
                    else if (i == n && j == n)
                        btt[i, j].Text = "F";
                    else
                        btt[i, j].Text = i + "," + j;
                    btt[i, j].Width = 40;
                    btt[i, j].Height = 40;
                    btt[i, j].BackColor = Color.White;
                    btt[i, j].ContextMenuStrip = contextMenuStrip1;
                    flowLayoutPanel1.Controls.Add(btt[i, j]);
                    btt[i, j].Click += new EventHandler(button_Click);
                }
        }
        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "X")
            {
                button.BackColor = Color.White;
                string[] s = button.Name.Split();
                int x1, x2;
                x1 = int.Parse(s[0]);
                x2 = int.Parse(s[1]);
                button.Text = x1 + "," + x2;
                x[x1, x2] = 0;
            }
            else
            {
                if (button.Text == "M")
                    m1 = 0;
                button.Text = "X";
                button.BackColor = Color.Red;
                string[] s = button.Name.Split();
                x[int.Parse(s[0]), int.Parse(s[1])] = 1;
            }
            changed = 1;
        }
        protected void button_ClickPort(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] s = button.Name.Split();
            if(prec==0)
            {
                s1 = int.Parse(s[0]);
                s2 = int.Parse(s[1]);
                button.BackColor = Color.BlueViolet;
                button.Text = "P" + ++cnt;
                prec = 1;
            }
            else
            {
                s3 = int.Parse(s[0]);
                s4 = int.Parse(s[1]);
                button.BackColor = Color.BlueViolet;
                button.Text = "P" + cnt;
                prec = 0;
                a[s1, s2] = s3;
                b[s1, s2] = s4;
                a[s3, s4] = s1;
                b[s3, s4] = s2;
            }
            changed = 1;
        }
    }
}