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
        public static List<BaseObject> _objs;
        public static int shipIndex = 0;
        public static int objID = 1;
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
            if (r.Next(1000) > 975) FireBullet();
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
            List<int> indexes = new List<int>();
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
                if ((obj is Bullet) && (obj.GetDir == new Point(0, 0)))
                {
                    indexes.Add(_objs.IndexOf(obj));
                }
            }
            ListRemove(indexes);
            CheckConnection();
        }
        // Далее мы рисуем count объектов, которые в дальнейшем будут перемещаться по полю
        public static void Load(int count)
        {
            _objs = new List<BaseObject>();
            for (int i = 0; i < count; i++)
            {
                int rnd = r.Next(300);
                if (rnd >= 1 && rnd < 180)
                {
                    _objs.Add(new Star(objID, new Point(r.Next(Width - 200) + 200, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0), new Size(12, 12)));
                    objID++;
                }
                else if (rnd >= 180 && rnd < 250)
                {
                    _objs.Add(new Star(objID, new Point(r.Next(Width - 200) + 200, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0), new Size(24, 24)));
                    objID++;
                }
                else
                {
                    _objs.Add(new ImgGalaxy(objID, new Point(r.Next(Width - 200) + 200, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0)));
                    objID++;
                }
            }
            _objs.Add(new PlayerShip(objID, new Point(20, Height / 2)));
            shipIndex = objID - 1;
            objID++;
        }
        private static void CheckConnection()
        {
            List<int> indexes = new List<int>();
            foreach (BaseObject obj in _objs)
            {
                if (_objs.IndexOf(obj) != shipIndex)
                    if (CheckCrash(_objs[shipIndex].GetPos, obj.GetPos, _objs[shipIndex].OSize, obj.OSize, new Point(0, 0)) == true)
                    {
                        Crash(_objs[shipIndex], obj);
                        indexes.Add(_objs.IndexOf(obj));
                    }
                if (obj is Bullet)
                {
                    foreach (BaseObject obj2 in _objs)
                    {
                        if ((CheckCrash(obj.GetPos, obj2.GetPos, obj.OSize, obj2.OSize, obj.GetDir) == true) && !(obj2 is Bullet))
                        {
                            Crash(obj, obj2);
                            indexes.Add(_objs.IndexOf(obj));
                            indexes.Add(_objs.IndexOf(obj2));
                        }
                    }
                }
            }
            ListRemove(indexes);
        }
        private static bool CheckCrash(Point pObj, Point pTarget, Size sObj, Size sTarget, Point spObj)
        {
            bool flag = false;
            /*
            if (((pTarget.X >= pObj.X) && (pTarget.X <= pObj.X + sObj.Width)) || ((pTarget.X + sTarget.Width >= pObj.X) && (pTarget.X + sTarget.Width <= pObj.X + sObj.Width)))
                if (((pTarget.Y >= pObj.Y) && (pTarget.Y <= pObj.Y + sObj.Height)) || ((pTarget.Y + sTarget.Height >= pObj.Y) && (pTarget.Y + sTarget.Height <= pObj.Y + sObj.Height)))
                    flag = true;
                    */
            /*
    Buffer.Graphics.DrawRectangle(Pens.Blue, pTarget.X,pTarget.Y, sTarget.Width, sTarget.Height);
    Buffer.Graphics.DrawRectangle(Pens.Red, pObj.X, pObj.Y, sObj.Width + spObj.X, sObj.Height + spObj.Y);
    Buffer.Render();
    */
            if ((((pTarget.X >= pObj.X) && (pTarget.X <= pObj.X + sObj.Width + spObj.X)) || ((pTarget.X + sTarget.Width >= pObj.X) && (pTarget.X + sTarget.Width <= pObj.X + sObj.Width + spObj.X))) ||
                ((pObj.X >= pTarget.X) && (pObj.X <= pTarget.X + sTarget.Width)) || ((pObj.X + sObj.Width + spObj.X >= pTarget.X) && (pObj.X + sObj.Width + spObj.X <= pTarget.X + sTarget.Width)))
                if ((((pTarget.Y >= pObj.Y) && (pTarget.Y <= pObj.Y + sObj.Height + spObj.Y)) || ((pTarget.Y + sTarget.Height >= pObj.Y) && (pTarget.Y + sTarget.Height <= pObj.Y + sObj.Height + spObj.Y))) ||
                ((pObj.Y >= pTarget.Y) && (pObj.Y <= pTarget.Y + sTarget.Height)) || ((pObj.Y + sObj.Height + spObj.Y >= pTarget.Y) && (pObj.Y + sObj.Height + spObj.Y <= pTarget.Y + sTarget.Height)))
                    flag = true;
            return flag;
        }
        private static void Crash(BaseObject obj1, BaseObject obj2)
        {
            Point pos = new Point();
            Image image = Image.FromFile("Resources/explosion.png");
            pos.X = obj1.GetPos.X + (obj1.OSize.Width - image.Width) / 2;
            pos.Y = obj1.GetPos.Y + (obj1.OSize.Height - image.Height) / 2;
            Buffer.Graphics.DrawImage(image, pos);
            Buffer.Render();
            // System.Threading.Thread.Sleep(1000);
        }
        public static void FireBullet()
        {
            // 24 32 40
            int yStart = _objs[shipIndex].GetPos.Y + 23 + r.Next(3) * 8;
            int xStart = _objs[shipIndex].GetPos.X + _objs[shipIndex].OSize.Width + 1;
            /*
            int yStart = _objs[shipIndex].GetPos.Y;
            int xStart = _objs[shipIndex].GetPos.X+100;
            */
            _objs.Add(new Bullet(objID, new Point(xStart, yStart), new Point(20, r.Next(5) - 2), new Size(20, 3)));
            objID++;
        }
        private static void ListRemove(List<int> indexes)
        {
            // Сортировка массива
            for (int i = 0; i < indexes.Count - 1; i++)
            {
                if (indexes[i] < indexes[i + 1])
                {
                    int x = indexes[i];
                    indexes[i] = indexes[i + 1];
                    indexes[i + 1] = x;
                    i = 0;
                }
            }
            // Удаление объектов из списка
            for (int i = 0; i < indexes.Count; i++)
            {
                _objs.RemoveAt(indexes[i]);
                if ((shipIndex > 0) && (indexes[i] < shipIndex)) shipIndex--;
            }
            if (_objs.Count == 1) GameOver();
        }
        private static void GameOver()
        {
            Application.Exit();
        }
    }
}
