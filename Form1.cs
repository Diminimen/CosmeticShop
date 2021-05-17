using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmeticShop
{
    public partial class Autorization : Form
    {
        DataBase db = new DataBase();

        int counter;

        public List<int> idList = new List<int>();

        List<PictureBox> mainImage = new List<PictureBox>();

        List<PictureBox> mainPanel = new List<PictureBox>();

        List<Label> name = new List<Label>();

        List<List<PictureBox>> checkList = new List<List<PictureBox>>();

        List<List<string>> allImageList = new List<List<string>>();


        public Autorization()
        {
            InitializeComponent();
            CreatePanel();

        }

        private void CreatePanel()
        {
            int stringNum = 0;
            int count = db.Product.Count();
            foreach (Product p in db.Product)
            {
                idList.Add(p.ID);
            }
            for (int f = 0; f < count; f++)
            {
                counter = 1;
                Product prd = db.Product.Find(idList[f]);

                List<string> imageList = new List<string>();
                foreach (ProductPhoto pp in db.ProductPhoto)
                {
                    if (pp.ProductID == idList[f])
                        imageList.Add(pp.PhotoPath);
                }
                allImageList.Add(imageList);
                counter += allImageList[f].Count;
                    

                if (f / (3 * (stringNum+1)) == 1)
                {
                    stringNum++;
                }
                PictureBox mainPn = new PictureBox()
                {
                    Size = new Size(200, 300),
                    Location = new Point(100 + (250 * (f - 3*stringNum)), 50 + (Size.Height * stringNum/5*3)),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };
                if (prd.IsActive == false)
                {
                    mainPn.BackColor = Color.LightGray;
                }
                
                mainPanel.Add(mainPn);
                panel2.Controls.Add(mainPanel[f]);

                
                Label text = new Label()
                {
                    Size = new Size(150, mainPanel[f].Height / 5),
                    Location = new Point(mainPanel[f].Location.X + ((mainPanel[f].Width - 150) / 2), (mainPanel[f].Location.Y + mainPanel[f].Height) - mainPanel[f].Height / 5 - 10),
                    Text = prd.Title,
                    BackColor = mainPanel[f].BackColor,
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Tahoma", 9f, FontStyle.Regular)
                };
                name.Add(text);
                panel2.Controls.Add(name[f]);
                name[f].BringToFront();

                PictureBox mainImg = new PictureBox()
                {
                    Size = new Size(150, 200),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackgroundImage = Image.FromFile($@"..\..\{prd.MainImagePath}"),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Location = new Point(mainPanel[f].Location.X + ((mainPanel[f].Width - 150) / 2), mainPanel[f].Location.Y + 10)
                };
                mainImage.Add(mainImg);
                panel2.Controls.Add(mainImage[f]);
                mainImage[f].BringToFront();

                List<PictureBox> pb = new List<PictureBox>();
                for (int i = 0; i < counter; i++)
                {
                    PictureBox rdP = new PictureBox
                    {
                        Size = new Size(15, 15),
                        BackColor = Color.FromArgb(225, 228, 255),
                        Location = new Point((mainImage[f].Location.X + ((mainImage[f].Width - (30 * counter / 2)) / 2) + (30 * i) / 2), mainImage[f].Location.Y + mainImage[f].Height)
                    };
                    if (i == 0)
                        rdP.BackColor = Color.FromArgb(255, 74, 109);
                    pb.Add(rdP);
                    panel2.Controls.Add(pb[i]);
                    pb[i].BringToFront();
                }
                checkList.Add(pb);
            }
        }

        //private void MainImage_MouseMove(object sender, MouseEventArgs e)
        //{
        //    double areaChange = mainImage[f].Width / counter;
        //    for (int i = 0; i < counter; i++)
        //    {
        //        if (MousePosition.X >= this.Left + mainImage[f].Location.X + areaChange * i - 1
        //            && MousePosition.X <= this.Location.X + mainImage.Location.X + areaChange * (i + 1) + 1)
        //        {
        //            if (i == 0)
        //            {
        //                mainImage.BackgroundImage = Image.FromFile($@"..\..\{productImage[i]}");
        //            }
        //            else
        //            {
        //                mainImage.BackgroundImage = Image.FromFile($@"..\..\{prdPh[i - 1].PhotoPath}");
        //            }

        //            mainImage.BackgroundImageLayout = ImageLayout.Stretch;
        //            for (int j = 0; j < counter; j++)
        //            {
        //                if (j == i)
        //                {
        //                    rd[j].BackColor = Color.FromArgb(255, 74, 109);
        //                }
        //                else
        //                {
        //                    rd[j].BackColor = BackColor = Color.FromArgb(225, 228, 255);
        //                }
        //            }
        //        }
        //    }
        //}

        

    }
}
