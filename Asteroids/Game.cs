using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        public static List<BackgroundStar> background;
        public static Random r = new Random();

        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            // Вызываем рисование определенного количества объектов на поле
            Load(50);
            // Добавляем таймер обновления прорисовки объектов
            Timer timer = new Timer { Interval = 10 };
            timer.Start();
            timer.Tick += Timer_Tick;
            // Формируем список звезд для заднего фона
            background = new List<BackgroundStar>();
            background = SpaceCreate(Width * Height / 5000);
        }
        // Обработчик таймера
        public static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        // Создаем список "звезд" для заднего фона
        public static List<BackgroundStar> SpaceCreate(int count)
        {
            Random r = new Random();
            int x, y, size;
            List<BackgroundStar> background = new List<BackgroundStar>();
            for (int i = 0; i < count; i++)
            {
                x = r.Next(Width);
                y = r.Next(Height);
                size = r.Next(10);
                if (size > 7) size = 2;
                else size = 1;
                Color vColor = new Color();
                vColor = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                BackgroundStar temp = new BackgroundStar(new Point(x, y), new Size(size, size), vColor);
                background.Add(temp);
            }
            return background;

        }
        public static void Draw()
        {
            // Прорисовываем каждый объект
            Buffer.Graphics.Clear(Color.Black);
            BackScreenDraw();
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }
        // Рисуем задний фон
        public static void BackScreenDraw()
        {
            foreach (BackgroundStar bs in background)
            {
                Pen pen = new Pen(bs.GetColor);
                Buffer.Graphics.DrawRectangle(pen, bs.Position.X, bs.Position.Y, bs.GetSize.Width, bs.GetSize.Height);
            }
        }
        // Обновляем каждый объект по движению
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            CheckConnection();
        }
        // Далее мы рисуем count объектов, которые в дальнейшем будут перемещаться по полю
        public static void Load(int count)
        {
            _objs = new BaseObject[count + 1];
            for (int i = 0; i < _objs.Length - 1; i++)
            {
                int rnd = r.Next(300);
                if (rnd >= 1 && rnd < 180)
                    _objs[i] = new Star(new Point(r.Next(Width), r.Next(Height)), new Point(-1 * (r.Next(10) + 1), 0), new Size(12, 12));
                else if (rnd >= 180 && rnd < 250)
                    _objs[i] = new Star(new Point(r.Next(Width), r.Next(Height)), new Point(-1 * (r.Next(10) + 1), 0), new Size(24, 24));
                else
                {
                    _objs[i] = new ImgGalaxy(new Point(r.Next(Width), r.Next(Height)), new Point(-1 * (r.Next(10) + 1), 0));
                }
            }
            _objs[_objs.Length - 1] = new PlayerShip(new Point(20, Height / 2));
        }
        private static void CheckConnection()
        {
            foreach (BaseObject obj in _objs)
            {
                if (CheckCrash(_objs[_objs.Length - 1].GetPos, obj.GetPos, _objs[_objs.Length - 1].OSize, obj.OSize) == true)
                    Crash(_objs[_objs.Length - 1].GetPos, _objs[_objs.Length - 1].OSize);
            }
        }
        private static bool CheckCrash(Point pObj, Point pTarget, Size sObj, Size sTarget)
        {
            bool flag = false;
            if (((pTarget.X > pObj.X) && (pTarget.X < pObj.X + sObj.Width)) || ((pTarget.X + sTarget.Width > pObj.X) && (pTarget.X + sTarget.Width < pObj.X + sObj.Width)))
                if (((pTarget.Y > pObj.Y) && (pTarget.Y < pObj.Y + sObj.Height)) || ((pTarget.Y + sTarget.Height > pObj.Y) && (pTarget.Y + sTarget.Height < pObj.Y + sObj.Height)))
                    flag = true;
            return flag;
        }
        private static void Crash(Point pos, Size size)
        {
            Image image = Image.FromFile("Resources/explosion.png");
            pos.X = pos.X + (size.Width - image.Width) / 2;
            pos.Y = pos.Y + (size.Height - image.Height) / 2;
            Buffer.Graphics.DrawImage(image, pos);
            Buffer.Render();
        }
    }
}
