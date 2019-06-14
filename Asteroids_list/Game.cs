using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Делегат логирования событий
        public delegate void MakeLog(string msg);
        // Таймер игры
        public static Timer _timer = new Timer { Interval = 7 };
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static List<BaseObject> _objs;
        public static int shipIndex = 0;
        public static int objID = 1;
        public static List<BackgroundStar> background;
        public static Random r = new Random();
        // Флаги управления кораблем
        public static bool flagUp = false;
        public static bool flagDown = false;
        public static bool flagLeft = false;
        public static bool flagRight = false;
        // Внутриигровые параметры
        public static int defaultShipEnergy = 500;
        public static int defaultStarEnergy = 5;
        public static int defaultGalaxyEnergy = 25;
        public static int score = 0; // ПОКА НЕ ИСПОЛЬЗУЕТСЯ
        public static int level = 1; // ПОКА НЕ ИСПОЛЬЗУЕТСЯ
        public static int targets = 50; // количество оставшихся целей на игровом поле, все цели без пуль, корабля и бонусов. Зависит от уровня, для первого уровня составляет 50 целей.
        public static int shipSpeed = 1; // максимальное значение - 3, шаг 1
        public static int maxShipSpeed = 4;
        public static int shipRapidFire = 25; // максимальное значение - 150, шаг 5
        public static int maxShipRapidFire = 400;
        // Делегаты для ведения логов игры
        public delegate void WriteLog(string s);
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
            // НЕОБХОДИМО РАЗОБРАТЬСЯ И ДОДЕЛАТЬ Обработка исключения в случае превышение размеров игрового поля
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            if ((resolution.Width < 1600) || (resolution.Height < 900)) throw new ArgumentOutOfRangeException("Form Init", "resolition too low");
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            // Вызываем рисование определенного количества объектов на поле
            Load(targets);
            // Добавляем таймер обновления прорисовки объектов
            //Timer timer = new Timer { Interval = 7 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            // Подписываемся на событие смерти корабля
            PlayerShip.MessageDie += GameOver;
            // Формируем список звезд для заднего фона
            background = new List<BackgroundStar>();
            background = SpaceCreate(Width * Height / 5000);
            // Проверяем файл логов для отлова эксепшенов, в случае его отсутствия - создаем
            CheckFiles();

        }
        // Обработчик таймера
        public static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            if (r.Next(1000) > (1000 - shipRapidFire)) FireBullet();
            if (r.Next(1000) > 995) AddBonus();
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
            Buffer.Graphics.DrawString($"Game Level: {level}   Score: {score}   Speed: {shipSpeed}   FireSpeed: {shipRapidFire / 5 - 4}   Targets remain: {targets}", new Font(FontFamily.GenericSansSerif, 20), new SolidBrush(Color.White), 20, 20);
        }
        // Обновляем каждый объект по движению
        public static void Update()
        {
            List<int> indexes = new List<int>();
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
                // Ниже идет отлов исключения, объект в неверными параметрами (например пуля с координатами -100, -100
                try
                {
                    if ((obj.GetPos.X < 0 - obj.OSize.Width) || (obj.GetPos.X > Width + obj.OSize.Width) || (obj.GetPos.Y < 0 - obj.OSize.Height) || (obj.GetPos.Y > Height + obj.OSize.Height))
                        throw new MyExceptions($"GameObjectException {obj}, coordinates {obj.GetPos.X},{obj.GetPos.Y}, size {obj.OSize.Width},{obj.OSize.Height}, coordinates too big", 1);
                }
                catch (MyExceptions e)
                {
                    e.ToFile();
                }
                // Окончание отлова исключания
                if (((obj is Bullet) || (obj is ShipBonus)) && (obj.ODir == new Point(-100, -100)))
                {
                    indexes.Add(_objs.IndexOf(obj));
                }
               if (_objs[shipIndex].OEnergy <= 0) _objs[shipIndex]?.Die();
            }
            ListRemove(indexes);
            CheckConnection();
        }
        // Далее мы рисуем count объектов, которые в дальнейшем будут перемещаться по полю
        public static void Load(int count)
        {
            _objs = new List<BaseObject>();
            AddObjects(targets);
            ObjectCreate("Player Ship", new Point(0,0));
        }
        // Обновленный метод создания объектов для отлова ошибок
        private static void ObjectCreate(string type, Point start)
        {
            switch (type)
            {
                case "Star Big": // энергия 50
                    _objs.Add(new Star(objID, new Point(r.Next(Width / 2) + Width / 2, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0), new Size(24, 24), defaultStarEnergy * (level)*5/2));
                    break;
                case "Star Small": // энергия 20
                    _objs.Add(new Star(objID, new Point(r.Next(Width / 2) + Width / 2, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0), new Size(12, 12), defaultStarEnergy * (level) * 5 / 2));
                    break;
                case "Bullet": // энергия 10 по умолчанию
                    _objs.Add(new Bullet(objID, start, new Point(20, r.Next(5) - 2), new Size(20, 3)));
                    break;
                case "Ship Bonus": // энергия 0 по умолчанию
                    _objs.Add(new ShipBonus(objID, start, new Point(-4, 0)));
                    break;
                case "Galaxy": // энергия 100
                    _objs.Add(new ImgGalaxy(objID, new Point(r.Next(Width / 2) + Width / 2, r.Next(Height)), new Point(-1 * (r.Next(5) + 1), 0), defaultGalaxyEnergy * (level) * 5 / 2));
                    break;
                case "Player Ship": // энергия 500
                    _objs.Add(new PlayerShip(objID, new Point(20, Height / 2), defaultShipEnergy));
                    shipIndex = objID - 1;
                    break;
                default:
                    break;
            }
            objID++;
            WriteLog e = LogNewObject;
            e($"{type}");
        }
        // Проверяем объекты на столкновение, на данный момент столкновение целей между собой невозможно
        private static void CheckConnection()
        {

            List<int> indexes = new List<int>();
            try
            {
                foreach (BaseObject obj in _objs)
                {
                    if (_objs.IndexOf(obj) != shipIndex)
                        if (CheckCrash(_objs[shipIndex].GetPos, obj.GetPos, _objs[shipIndex].OSize, obj.OSize, new Point(0, 0)) == true)
                        {
                            if (!(obj is ShipBonus)) Crash(_objs[shipIndex], obj);
                            if (obj.OEnergy <= 0) indexes.Add(_objs.IndexOf(obj));
                        }
                    if (obj is Bullet)
                    {
                        foreach (BaseObject obj2 in _objs)
                        {
                            if ((CheckCrash(obj.GetPos, obj2.GetPos, obj.OSize, obj2.OSize, obj.ODir) == true) && !(obj2 is Bullet) && !(obj2 is ShipBonus))
                            {
                                Crash(obj, obj2);
                                indexes.Add(_objs.IndexOf(obj));
                                if (obj2.OEnergy <= 0) indexes.Add(_objs.IndexOf(obj2));
                            }
                        }
                    }
                    if (obj is ShipBonus)
                    {
                        if (CheckCrash(obj.GetPos, _objs[shipIndex].GetPos, obj.OSize, _objs[shipIndex].OSize, obj.ODir) == true)
                        {
                            switch (obj.OBonType)
                            {
                                case 1:
                                    if (shipRapidFire < maxShipRapidFire) shipRapidFire = shipRapidFire + 5;
                                    break;
                                case 2:
                                    if (shipSpeed < maxShipSpeed) shipSpeed++;
                                    break;
                                case 3:
                                    _objs[shipIndex].OEnergy += 100;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                for (int i = 0; i < _objs.Count; i++)
                    if (_objs[i] is PlayerShip)
                    {
                        shipIndex = i;
                        break;
                    }
            }
            try
            {
                ListRemove(indexes);
            }
            catch (ArgumentOutOfRangeException)
            {
                MyExceptions e = new MyExceptions(@"doubled indexes CheckConnection", 2);
                e.ToFile();
            }
        }
        // Проверка наложения двух целей для определения столкновения, в случае успехва возвращается ture
        private static bool CheckCrash(Point pObj, Point pTarget, Size sObj, Size sTarget, Point spObj)
        {
            bool flag = false;
            if ((((pTarget.X >= pObj.X) && (pTarget.X <= pObj.X + sObj.Width + spObj.X)) || ((pTarget.X + sTarget.Width >= pObj.X) && (pTarget.X + sTarget.Width <= pObj.X + sObj.Width + spObj.X))) ||
                ((pObj.X >= pTarget.X) && (pObj.X <= pTarget.X + sTarget.Width)) || ((pObj.X + sObj.Width + spObj.X >= pTarget.X) && (pObj.X + sObj.Width + spObj.X <= pTarget.X + sTarget.Width)))
                if ((((pTarget.Y >= pObj.Y) && (pTarget.Y <= pObj.Y + sObj.Height + spObj.Y)) || ((pTarget.Y + sTarget.Height >= pObj.Y) && (pTarget.Y + sTarget.Height <= pObj.Y + sObj.Height + spObj.Y))) ||
                ((pObj.Y >= pTarget.Y) && (pObj.Y <= pTarget.Y + sTarget.Height)) || ((pObj.Y + sObj.Height + spObj.Y >= pTarget.Y) && (pObj.Y + sObj.Height + spObj.Y <= pTarget.Y + sTarget.Height)))
                    flag = true;
            return flag;
        }
        // Отрисовка столкновения двух объектов
        private static void Crash(BaseObject obj1, BaseObject obj2)
        {
            Point pos = new Point();
            Image image = Image.FromFile("Resources/explosion.png");
            pos.X = obj1.GetPos.X + (obj1.OSize.Width - image.Width) / 2;
            pos.Y = obj1.GetPos.Y + (obj1.OSize.Height - image.Height) / 2;
            // Применяем к обеим целям урон от столкновения
            int obj1Damage = obj2.OEnergy;
            int obj2Damage = obj1.OEnergy;
            obj1.EnergyLow(obj1Damage);
            obj2.EnergyLow(obj2Damage);
            Buffer.Graphics.DrawImage(image, pos);
            Buffer.Render();
            // System.Threading.Thread.Sleep(1000); // заготовка для slow motion
        }
        // Выстрел из пушки корабля
        public static void FireBullet()
        {
            // Отступы от координаты расположение корабля для выстрелов из пушек - 23 31 39
            int yStart = _objs[shipIndex].GetPos.Y + 23 + r.Next(3) * 8;
            int xStart = _objs[shipIndex].GetPos.X + _objs[shipIndex].OSize.Width + 1;
            ObjectCreate("Bullet", new Point(xStart, yStart));
            // _objs.Add(new Bullet(objID, new Point(xStart, yStart), new Point(20, r.Next(5) - 2), new Size(20, 3)));
            // objID++;
        }
        // Добавляем на игровое поле бонус
        public static void AddBonus()
        {
            int yStart = r.Next(Height);
            ObjectCreate("Ship Bonus", new Point(Width, yStart));
            WriteLog e = LogEvent;
            e("Ship Bonus created");
        }
        // Удаление объектов с игрового поля с помощью списка индексов объектов для удаления
        private static void ListRemove(List<int> indexes)
        {
            // Отладочный коэффициент для проверки отсутствия дубляжа
            int ind = indexes.Count;
            // Сортировка массива
            for (int j = 1; j < indexes.Count - 1; j++)
            {
                bool flag = false;
                for (int i = 0; i < indexes.Count - 1 - j; i++)
                {
                    if (indexes[i] < indexes[i + 1])
                    {
                        int x = indexes[i];
                        indexes[i] = indexes[i + 1];
                        indexes[i + 1] = x;
                        flag = true;
                    }
                }
                if (flag == false) break;
            }
            // Удаление объектов из списка
            for (int i = 0; i < indexes.Count; i++)
            {
                if ((_objs[indexes[i]] is Star) || (_objs[indexes[i]] is ImgGalaxy))
                {
                    targets--;
                    score++;
                }
                _objs.RemoveAt(indexes[i]);
                if ((shipIndex > 0) && (indexes[i] < shipIndex)) shipIndex--;
            }
            // if (_objs.Count == 1) GameOver();
            if (targets <= 0) NextLevel();
        }
        // ПОКА НЕ РАБОТАЕТ Конец игры, тут необходимо добавить сообщение "Конец игры", набранные очки и после этого предложить пользователю выйти или начать новую игру
        private static void GameOver()
        {
            WriteLog e = LogEvent;
            _timer.Stop();
            e("PlayerShip destroyed");
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.OrangeRed, (Width/2-120), (Height/2-30));
            Buffer.Render();
        }
        private static void AddObjects(int count)
        {
            Point start = new Point(0, 0); // Так как начальное формирование объектов происходит на случайных позициях, в качестве аргумента в метод создания отправляется константа (0,0)
            for (int i = 0; i < count; i++)
            {
                int rnd = r.Next(300);
                if (rnd >= 1 && rnd < 180)
                    ObjectCreate("Star Small", start);
                else if (rnd >= 180 && rnd < 250)
                    ObjectCreate("Star Big", start);
                else
                    ObjectCreate("Galaxy", start);
            }
        }
        private static void NextLevel()
        {
            level++;
            targets = 50 + (level - 1) * 10;
            AddObjects(targets);
        }
        public static void CheckFiles()
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            }
            if (!File.Exists(@"Logs\logs.txt")) using (File.Create(@"Logs\logs.txt")) { }
            if (!File.Exists(@"Logs\gamelogs.txt")) using (File.Create(@"Logs\eventslog.txt")) { }
            if (!File.Exists(@"Logs\gamelogs.txt")) using (File.Create(@"Logs\newobjectslog.txt")) { }
        }
        public static void LogEvent(string s)
        {
            using (StreamWriter sw = new StreamWriter(@"Logs\eventslog.txt", true))
                sw.WriteLine($"{DateTime.Now} произошло событие {s}");
        }
        public static void LogNewObject(string s)
        {
            using (StreamWriter sw = new StreamWriter(@"Logs\newobjectslog.txt", true))
                sw.WriteLine($"{DateTime.Now} создан новый объект {s}");
        }
    }
}
