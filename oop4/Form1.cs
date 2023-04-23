using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace oop4
{
    public partial class Form1 : Form
    {
        private List<CCircle> FCircles = new List<CCircle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CCircle Circle in FCircles)
            {
                Circle.drawCircle(e.Graphics);//Рисует все круги из списка
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (CB1.Checked == false)//не нажат ctrl
            {
                foreach (CCircle Circle1 in FCircles)
                {
                    Circle1.setColor("Black");//снимает выделение со всех объектов
                }
                CCircle Circle = new CCircle(e.X, e.Y, 20);//создает новый объект с выделением
                FCircles.Add(Circle);
            }
            if (CB1.Checked == true) // удерживается ctrl
            {
                foreach (CCircle Circle1 in FCircles)
                {
                    if (Circle1.checkCircle(e) == true && CB2.Checked == true)//проверка на мас-совое выделение

                    {
                        break;
                    }
                }
                Refresh();
            }
            Refresh();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FCircles.Count(); i++)
            {
                if (FCircles[i].getColor() == "Red")//проверка выделения объектов
                {
                    FCircles.RemoveAt(i);//удаление выделенных объектов
                    i--;
                }
            }
            Refresh();
        }
        public class CCircle//описание объекта круга
        {
            private int x, y, rad;//координаты и радиус
            private string color = "Red";//цвет выделения
            private static bool check_ctrl = false;
            public CCircle(int xp, int yp, int radp)//конструктор с параметрами
            {
                x = xp;
                y = yp;
                rad = radp;

            }
            public void drawCircle(Graphics Canvas)//метод отрисовки круга
            {
                if (color == "Red")
                {
                    Canvas.DrawEllipse(new Pen(Color.Red), x - rad, y - rad, rad * 2, rad * 2);
                }
                else
                {
                    Canvas.DrawEllipse(new Pen(Color.Black), x - rad, y - rad, rad * 2, rad * 2);
                }
            }
            public void setColor(string Color)//сеттер цвета круга
            {
                color = Color;
            }
            public string getColor()//геттер цвета круга
            {
                return color;
            }
            public bool checkCircle(MouseEventArgs e)//проверка на попадание курсора мыши во внутрь круга

            {
                if (check_ctrl)
                {
                    if (Math.Pow(e.X - x, 2) + Math.Pow(e.Y - y, 2) <= Math.Pow(rad, 2) && color != "Red")
                    {
                        color = "Red";
                        return true;
                    }
                }
                return false;
            }
            static public void set_ctrl(bool check)
            {
                check_ctrl = check;
            }
        }

        private void btn1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                CCircle.set_ctrl(true);
                CB1.Checked = true;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            CCircle.set_ctrl(false);
            CB1.Checked = false;
        }
    }
}
