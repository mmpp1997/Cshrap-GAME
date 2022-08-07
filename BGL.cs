using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Media;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */

        Letjelica letjelica;
        Zgrada sky,sky2,sky3,sky4,sky5;
        SoundPlayer let,ex;
        bool kontrola;
        Menu menu;
        double vrime;
        PowerUp zivot;
        /* Initialization */
        public delegate void Manje();
        public static event Manje _manje;

        public void SetupGame()
        {
            //1. setup stage
            SetStageTitle("Pass By");
            //setBackgroundColor(Color.WhiteSmoke);            
            setBackgroundPicture("backgrounds\\back.jpg");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");
            menu = new Menu();
            menu.ShowDialog();
            START = true;                      
            //2. add sprites
            sky = new Zgrada("Sprites\\sky1.png", 200, 325);
            sky.SetSize(10);
            sky.RotationStyle = "all around";
            sky.SetVisible(false);
            Game.AddSprite(sky);

            sky2 = new Zgrada("Sprites\\sky1.png", 400, 325);
            sky2.SetSize(10);
            sky2.RotationStyle = "all around";
            sky2.SetVisible(false);
            Game.AddSprite(sky2);

            sky3 = new Zgrada("Sprites\\sky1.png", 600, 325);
            sky3.SetSize(10);
            sky3.RotationStyle = "all around";
            sky3.SetVisible(false);
            Game.AddSprite(sky3);

            sky4 = new Zgrada("Sprites\\sky1.png", 800, 325);
            sky4.SetSize(10);
            sky4.RotationStyle = "all around";
            sky4.SetVisible(false);
            Game.AddSprite(sky4);
            
            sky5 = new Zgrada("Sprites\\sky1.png", 1000, 325);
            sky5.SetSize(10);
            sky5.RotationStyle = "all around";
            sky5.SetVisible(false);
            Game.AddSprite(sky5);

            zivot = new PowerUp("Sprites\\Powerup.png", 100, 100);
            zivot.SetSize(10);
            zivot.SetVisible(false);
            Game.AddSprite(zivot);

            if (menu.odabir.Text=="Ufo")
            {
                letjelica = new Ufo("Sprites\\ufo.png", 100, 100);
                letjelica.SetVisible(true);
                letjelica.SetSize(20);
                letjelica.GotoXY((GameOptions.RightEdge - letjelica.Width) / 2, (GameOptions.DownEdge + letjelica.Heigth) / 2 - 100);                                
                Game.AddSprite(letjelica);

                let = new SoundPlayer(@"Zvukovi\letjelica.wav");
                let.PlayLooping();
            }
            else if (menu.odabir.Text == "Zrakoplov")
            {
                letjelica = new Plane("Sprites\\plane.png", 100, 50);
                letjelica.SetVisible(true);
                letjelica.SetSize(35);
                letjelica.GotoXY((GameOptions.RightEdge - letjelica.Width) / 2, (GameOptions.DownEdge + letjelica.Heigth) / 2 -50);
                Game.AddSprite(letjelica);

                let = new SoundPlayer(@"Zvukovi\plane.wav");
                let.PlayLooping();
            }
            else
            {
                START = false;
            }
            if (menu.Srednje.Checked)
            {
                vrime = 0.015;
            }
            else if (menu.Tesko.Checked)
            {
                vrime = 0.01;
            }
            _manje += JedanManje;
            Game.StartScript(Ziv);
            Game.StartScript(LijevoDesno);
            Game.StartScript(Blize);      
        }
        /* Scripts */
        private int LijevoDesno()
        {            
            while (START) 
            {
                ISPIS = "Bodovi: "+letjelica.Bodovi+", Zivoti: "+letjelica.Zivot;
                if (kontrola) { }
                else if(sensing.KeyPressedTest==true && sensing.Key == "Left")
                {
                    letjelica.X -= letjelica.Brzina;     
                }
                else if (sensing.KeyPressedTest == true && sensing.Key == "Right")
                {
                    letjelica.X += letjelica.Brzina;             
                }
                else if (sensing.KeyPressedTest == true && sensing.Key == "Up")
                {
                    letjelica.Y -= letjelica.Brzina;
                }
                else if (sensing.KeyPressedTest == true && sensing.Key == "Down")
                {
                    letjelica.Y += letjelica.Brzina;
                }
                Wait(0.01);
            }
            return 0;
        }
        private int Ziv()
        {
            while (true)
            {
                if (letjelica.TouchingSprite(zivot) && zivot.Show==true)
                {
                    zivot.SetVisible(false);
                    letjelica.Zivot += 1;
                    Wait(0.01);
                }
            }
            return 0;
        }
        private int Blize()
        {
            Random r = new Random();
            int visina = sky.Heigth;
            int duljina = sky.Width; 
            while (START)
            { 
                if (sky.Heigth >= 374)
                {
                    letjelica.Bodovi += 10;
                    if (letjelica.TouchingSprite(sky) && sky.Show == true)
                    {
                        _manje.Invoke();                       
                    }
                    if (letjelica.TouchingSprite(sky2) && sky2.Show == true)
                    {
                        _manje.Invoke();
                    }
                    if (letjelica.TouchingSprite(sky3) && sky3.Show == true)
                    {
                        _manje.Invoke();                        
                    }
                    if (letjelica.TouchingSprite(sky4) && sky4.Show == true)
                    {
                        _manje.Invoke();                       
                    }
                    if (letjelica.TouchingSprite(sky5) && sky5.Show == true)
                    {
                        _manje.Invoke();                        
                    }                   
                    Wait(0.018);
                    Povratak(150, visina, duljina, sky);
                    Povratak(350, visina, duljina, sky2);
                    Povratak(550, visina, duljina, sky3);
                    Povratak(750, visina, duljina, sky4);
                    Povratak(950, visina, duljina, sky5);
                    int broj = r.Next(1, 6);
                    Gasi(broj);
                }
                PromjenaVelicine(sky);
                PromjenaVelicine(sky2);
                PromjenaVelicine(sky3);
                PromjenaVelicine(sky4);
                PromjenaVelicine(sky5);    
                Wait(vrime);   
            }
            return 0;
        }
        public void PromjenaVelicine(Zgrada objekt)
        {
            objekt.Width += 1;
            objekt.Heigth += 3;
            objekt.X -= 1;
            objekt.Y -= 3;
        }
        public void Povratak(int x,int v,int d, Zgrada objekt)
        {
            objekt.Width = d;
            objekt.Heigth = v;
            objekt.GotoXY(x, 325);
            objekt.SetVisible(true);
            PromjeniSkin(objekt);
            Wait(0.01);
        }
        public void Gasi(int broj)
        {
            if (broj == 1)
            {
                sky.SetVisible(false);
                Zivot(sky);
            }
            if (broj == 2)
            {
                sky2.SetVisible(false);
                Zivot(sky2);
            }
            if (broj == 3)
            {
                sky3.SetVisible(false);
                Zivot(sky3);
            }
            if (broj == 4)
            {
                sky4.SetVisible(false);
                Zivot(sky4);
            }
            if (broj == 5)
            {
                sky5.SetVisible(false);
                Zivot(sky5);
            }
        }
        public void Zivot(Zgrada obj)
        {
            zivot.SetVisible(false);
            if(letjelica.Bodovi%150==0)
            {
                Random r = new Random();
                zivot.SetVisible(true);
                zivot.GotoXY(obj.X-zivot.Width,r.Next(0,325));
            }            
        }
        public void PromjeniSkin(Zgrada lik)
        {
            Random r = new Random();
            int broj = r.Next(1, 6);
            if (broj == 1)
            {
                lik.AddCostumes("Sprites\\sky1.png");
                lik.NextCostume();
                lik.Costumes.RemoveAt(lik.CostumeIndex);  
            }
            if (broj == 2)
            {
                lik.AddCostumes("Sprites\\sky2.png");
                lik.NextCostume();
                lik.Costumes.RemoveAt(lik.CostumeIndex);
            }
            if (broj == 3)
            {
                lik.AddCostumes("Sprites\\sky3.png");
                lik.NextCostume();
                lik.Costumes.RemoveAt(lik.CostumeIndex);
            }
            if (broj == 4)
            {
                lik.AddCostumes("Sprites\\sky4.png");
                lik.NextCostume();
                lik.Costumes.RemoveAt(lik.CostumeIndex);
            }
            if (broj == 5)
            {
                lik.AddCostumes("Sprites\\sky5.png");
                lik.NextCostume();
                lik.Costumes.RemoveAt(lik.CostumeIndex);
            }            
        }       
        public void JedanManje()
        {           
            if (letjelica.Zivot > 1)
            {                
                kontrola = true;
                let.Stop();
                letjelica.AddCostumes("Sprites\\explo.png");
                letjelica.NextCostume();
                ex = new SoundPlayer(@"Zvukovi\Ex.wav");
                ex.Play();
                Wait(1);
                letjelica.Zivot -= 1;
                if (menu.odabir.Text == "Zrakoplov")
                {
                    letjelica.AddCostumes("Sprites\\plane.png");
                }
                else
                    letjelica.AddCostumes("Sprites\\ufo.png");
                letjelica.NextCostume();  
                kontrola = false;
                let.PlayLooping();
            }
            else
            {
                kontrola = true;
                let.Stop();
                letjelica.AddCostumes("Sprites\\explo.png");
                letjelica.NextCostume();
                ex = new SoundPlayer(@"Zvukovi\Ex.wav");
                ex.Play();
                Wait(0.7);
                letjelica.Zivot -= 1;
                letjelica.Show = false;
                Wait(0.01);
                START = false;
                ex.Stop();
                Wait(0.2);         
                MessageBox.Show("U prethodnoj igri si osvojio "+letjelica.Bodovi+" bodova!");
                Application.Exit();
            }     
        }       
        /* ------------ GAME CODE END ------------ */
    }
}
