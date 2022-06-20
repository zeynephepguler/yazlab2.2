using System.Drawing.Drawing2D;

namespace GezginSatici
{
    public partial class Form1 : Form
    {
        //// this tracks the transformation applied to the PictureBox's Graphics
        //private Matrix transform = new Matrix();
        //public static float s_dScrollValue = 1.01f; // zoom factor
        public List<Location> location = new List<Location>();
        public List<Location> location2 = new List<Location>();
        public List<int> uzaklık = new List<int>();

        float zoomratio = 1.059f;
        public int count = 0;
        public int s;
        int sayac = -1;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (count == s) { return; }
            listView1.Items.Add("Konum"+(count+1).ToString()+": X:"+e.X.ToString()+" Y:" + e.Y.ToString() );
            location.Add(new Location() { A = e.X, B = e.Y });
            location2.Add(new Location() { A = e.X, B = e.Y });
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
        //private void pictureBox1_Canvas_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    g.Transform = transform;
        //}
        //protected override void OnMouseWheel(MouseEventArgs mea)
        //{
        //    pictureBox1.Focus();
        //    if (pictureBox1.Focused == true && mea.Delta != 0)
        //    {
        //        ZoomScroll(mea.Location, mea.Delta > 0);
        //    }
        //}
        //private void ZoomScroll(Point location, bool zoomIn)
        //{
        //    // make zoom-point (cursor location) our origin
        //    transform.Translate(-location.X, -location.Y);

        //    // perform zoom (at origin)
        //    if (zoomIn)
        //        transform.Scale(s_dScrollValue, s_dScrollValue);
        //    else
        //        transform.Scale(1 / s_dScrollValue, 1 / s_dScrollValue);

        //    // translate origin back to cursor
        //    transform.Translate(location.X, location.Y);

        //    pictureBox1.Invalidate();
        //}
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                if (e.Delta <= 0)
                {
                    //set minimum size to zoom
                    if (pictureBox1.Width < 50)
                        // lbl_Zoom.Text = pictureBox1.Image.Size; 
                        return;
                }
                else
                {
                    //set maximum size to zoom
                    if (pictureBox1.Width > 10000000)
                        return;
                }
                pictureBox1.Width += Convert.ToInt32(pictureBox1.Width * e.Delta / 1000);
                pictureBox1.Height += Convert.ToInt32(pictureBox1.Height * e.Delta / 1000);
                //pictureBox1.Width = (int)(pictureBox1.Width * zoomratio);
                //pictureBox1.Height = (int)(pictureBox1.Height * zoomratio);
                pictureBox1.Top = (int)(e.Y - zoomratio * (e.Y - pictureBox1.Top));
                pictureBox1.Left = (int)(e.X - zoomratio * (e.X - pictureBox1.Left));
                
            }

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
            //int maliyet1 = 0;
            //foreach (var item in location)
            //{
            //    for (var j=1;j< location.Count; j++)
            //    {
            //        maliyet1 = ((Math.Abs(item.A - location[j].A) ^ 2 + Math.Abs(item.B - location[j].B) ^ 2) ^ 1 / 2);
            //        uzaklık.Add(maliyet1);
            //    }
            //    uzaklık.Sort();
            //    for (var k = 1; k < location.Count; k++)
            //    {
            //        maliyet1 = ((Math.Abs(item.A - location[k].A) ^ 2 + Math.Abs(item.B - location[k].B) ^ 2) ^ 1 / 2);
            //        if (maliyet1== uzaklık[0])
            //        {
            //            Graphics myCanvas = pictureBox1.CreateGraphics();
            //            Pen graph = new Pen(Color.Blue, 4);
            //            myCanvas.DrawLine(graph, new Point(item.A, location[k].B), new Point(item.A, location[k].B));
            //        }
            //    }

            //}








            var sabit = 0;
            int maliyet1 = 0;
            int maliyet2 = 0;




            foreach (var item in location)
            {
                sayac++;
                
                for (var i = 1; i < location.Count; i++)
                {
                    //var eski = sabit;
                    if (item.A == location[i].A & item.B == location[i].B)
                    {
                        continue;
                    }

                    maliyet1 = ((Math.Abs(item.A - location[i].A) ^ 2
                        + Math.Abs(item.B - location[i].B) ^ 2) ^ 1 / 2);

                  

                    uzaklık.Add(maliyet1);
                    Console.WriteLine(uzaklık);

                    string maliyet = maliyet1.ToString();
                    //listView2.Items.Add(maliyet+" "+item.A+ " "+ location[i].A);
                    
                    //        //if (maliyet1 < eski)
                    //        //{
                    //        //    Graphics myCanvas = pictureBox1.CreateGraphics();
                    //        //    Pen graph = new Pen(Color.Blue, 4);
                    //        //    myCanvas.DrawLine(graph, new Point(item.A, item.B), new Point(location[i].A, location[i].B));

                    //        //}
                    //        //else
                    //        //{
                    //        //    Graphics myCanvas = pictureBox1.CreateGraphics();
                    //        //    Pen graph = new Pen(Color.Red, 4);
                    //        //    myCanvas.DrawLine(graph, new Point(item.A, item.B), new Point(item2.A, item2.B));
                    //        //}

                    //        //location2.Remove(item);
                       }
                
                    
            for (int i = 1; i < location.Count; i++)
            {
                    if (item.A == location[i].A & item.B == location[i].B)
                    {
                        continue;
                    }

                    maliyet2 = ((Math.Abs(item.A - location[i].A) ^ 2 + Math.Abs(item.B - location[i].B) ^ 2) ^ 1 / 2);
                Console.WriteLine(maliyet2);
                    Console.WriteLine(uzaklık.Min());
                    Console.WriteLine(uzaklık.Max());
                    if (maliyet2 == uzaklık.Min())
                {
                    Graphics myCanvas = pictureBox1.CreateGraphics();
                    Pen graph = new Pen(Color.Blue, 4);
                    myCanvas.DrawLine(graph, new Point(item.A, item.B), new Point(location[i].A, location[i].B));

                        uzaklık.Clear();
                        
                        Console.WriteLine(location);
                     break;

                    }
                   
            }
               

        }

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

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
           
        }
    }
}

public class Location
{
    public int A { get; set; }
    public int B { get; set; }
}


