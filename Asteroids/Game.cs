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
            // Вызываем рисование объектов на поле
            Load();
            // Добавляем таймер обновления прорисовки объектов
            Timer timer = new Timer { Interval = 20 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        // Обработчик таймера
        public static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Draw()
        {
            // Проверяем вывод графики
            /*
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.White, new Rectangle(100, 100, 200, 200));
            Buffer.Render();
            */
            // Прорисовываем каждый объект
            Buffer.Graphics.Clear(Color.Black);
            BackScreenDraw();
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }
        public static void BackScreenDraw()
        {
            Random r = new Random();
            int count = (Width * Height) / 5000;
            for (int i = 0; i < count; i++)
            {
                Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(new Point(r.Next(Width), r.Next(Height)), new Size(1, 1)));
            }
        }
        // Обновляем каждый объект по движению
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
        // Далее мы рисуем 30 объектов, которые в дальнейшем будут перемещаться по полю
        /*
        public static void Load()
        {
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
        }
        */
        public static void Load()
        {
            Random r = new Random();
            _objs = new BaseObject[100];
            for (int i = 0; i < _objs.Length; i++)
            {
                int rnd = r.Next(3);
                if (rnd == 1)
                    // _objs[i] = new Star(new Point(i * 20, i * 20), new Point(r.Next(20) - 10, r.Next(10) - 5), new Size(12, 12));
                    _objs[i] = new Star(new Point(Width / 2, Height / 2), new Point(r.Next(20) - 10, r.Next(10) - 5), new Size(12, 12));
                else if (rnd == 2)
                    // _objs[i] = new Star(new Point(i * 20, i * 20), new Point(r.Next(20) - 10, r.Next(20) - 10), new Size(24, 24));
                    _objs[i] = new Star(new Point(Width / 2, Height / 2), new Point(r.Next(20) - 10, r.Next(20) - 10), new Size(24, 24));
                else
                    // _objs[i] = new ImgGalaxy(new Point(i * 20, i * 20), new Point(r.Next(20) - 10, r.Next(20) - 10));
                    _objs[i] = new ImgGalaxy(new Point(Width / 2, Height / 2), new Point(r.Next(20) - 10, r.Next(20) - 10));
            }
        }
    }
}
