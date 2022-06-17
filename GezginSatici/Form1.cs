namespace GezginSatici
{
    public partial class Form1 : Form
    {
        public List<Location> location = new List<Location>();
        public int count = 0;
        public int s;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (count == s) { return; }
            listView1.Items.Add("Konum"+(count+1).ToString()+": X:"+e.X.ToString()+" Y:" + e.Y.ToString() );
            location.Add(new Location() { A = e.X, B = e.Y });
            Label namelabel = new Label();
            namelabel.Parent = pictureBox1;
            namelabel.Width = 0;
            namelabel.Height = 0;
            namelabel.AutoSize = true;
            namelabel.BackColor = System.Drawing.Color.Transparent;
            namelabel.Font = new Font("Arial", 20);
            if (count == 0)
            {
                namelabel.Text = "◼";
                namelabel.ForeColor = System.Drawing.Color.Yellow;

            }
            else if (count < s)
            {
                namelabel.Text = "●";
                namelabel.ForeColor = System.Drawing.Color.Red;
            }
            namelabel.Location = new Point(e.X - (namelabel.Width/2), e.Y - (namelabel.Width / 2));
            count++;
            if (count <= s-1) { return; }
            lines();
            
        }
        void lines()
        {
            var itemCount = 0;
            foreach ( var item in location)
            {
                for( var i = 0; i < location.Count; i++)
                {
                    if (i == itemCount)
                    {
                        continue;
                    }
                    Graphics myCanvas = pictureBox1.CreateGraphics();
                    Pen graph = new Pen(Color.Black, 4);
                    myCanvas.DrawLine(graph, new Point(item.A, item.B), new Point(location[i].A, location[i].B));

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             string sayi = textBox1.Text;
             int isayi=int.Parse(sayi);
                s = isayi;
            string mesaj = "Rota Sayısı Seçildi";
            MessageBox.Show(mesaj);
        } 
    }
}

public class Location
{
    public int A { get; set; }
    public int B { get; set; }
}


