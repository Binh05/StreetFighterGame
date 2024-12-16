using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreetFighterGame.GameEngine;
using StreetFighterGame.Scenes;
using StreetFighterGame.UI;
using StreetFighterGame.QuanLiTaikhoan;


namespace StreetFighterGame
{
    public partial class StreetFighterGame : Form
    {
        private Timer timer;
        private bool endGame;
        private LogicGame logicGame;
        private Image background;
        private List<string> backgroundImages = new List<string>();
        private int idChar1, idChar2;
        private Timer animationTimer;

        // Thêm các biến kiểm tra trạng thái các phím của người chơi
        private bool player1MoveLeft, player1MoveRight, player1Jump, player1AttackJ, player1AttackK, player1AttackL, player1AttackI;
        private bool player2MoveLeft, player2MoveRight, player2Jump, player2AttackJ, player2AttackK, player2AttackL, player2AttackI;

        private void labelWiner_Click(object sender, EventArgs e)
        {

        }

        public StreetFighterGame(string s1, string s2, int idChar1, int idChar2, string mappath)
        {
            InitializeComponent();
            SetUpForm(s1, s2, idChar1, idChar2, mappath);

        }

        private void SetUpForm(string s1, string s2, int _idChar1, int _idChar2, string mappath)
        {
            labelWiner.Visible = false;
            endGame = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            // Load background images
            // Kiểm tra đường dẫn bản đồ và tải hình ảnh
            if (!string.IsNullOrEmpty(mappath) && File.Exists(mappath))
            {
                background = Image.FromFile(mappath); // Assign to the background field
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }


            // Initialize the game logic
            logicGame = new LogicGame(this.ClientSize.Width, this.ClientSize.Height);

            // Initialize the characters for both players
            string player1Choice = s1;  // Default character for player 1
            string player2Choice = s2;  // Default character for player 2
            idChar1 = _idChar1;
            idChar2 = _idChar2;
            logicGame.InitializeCharacterSelection(player1Choice, player2Choice);
            // Initialize Timer for game animation
            animationTimer = new Timer
            {
                Interval = 32
            };
            animationTimer.Tick += new EventHandler(OnAnimationTick);
            animationTimer.Start();

            // Key events and drawing events
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            // Update game logic based on input
            if (!endGame/* && logicGame.Player1.CurrentState != ActionState.Standing*/)
            {
                UpdatePlayerInput();

                // Update the game logic (character positions, states)
                logicGame.Update();
            }

            // Redraw the game screen
            this.Invalidate();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Handle key down events for Player 1
            if (!logicGame.Player1.isHit && !logicGame.Player1.DangDanhDungKo())
            {
                if (e.KeyCode == Keys.A) player1MoveLeft = true;
                if (e.KeyCode == Keys.D)
                {
                    player1MoveRight = true;
                    if (player1MoveLeft) player1MoveLeft = false;// đảm bảo chỉ có 1 move là true, vì khi 2 true sẽ gặp bug logic vì có 2state được change
                }

                if (e.KeyCode == Keys.W) player1Jump = true;
                if (e.KeyCode == Keys.S) logicGame.Player1.Defend(true);
                if (e.KeyCode == Keys.J) player1AttackJ = true;
                if (e.KeyCode == Keys.K) player1AttackK = true;
                if (e.KeyCode == Keys.L) player1AttackL = true;
                if (e.KeyCode == Keys.I) player1AttackI = true;
                if (e.KeyCode == Keys.N) logicGame.Player1.PlayHitSound();
            }

            if (logicGame.Player2.isHit || logicGame.Player2.DangDanhDungKo()) return;

            // Handle key down events for Player 2
            if (e.KeyCode == Keys.Left) player2MoveLeft = true;
            if (e.KeyCode == Keys.Right)
            {
                player2MoveRight = true;
                if (player2MoveLeft) player2MoveLeft = false;
            }
            if (e.KeyCode == Keys.Up) player2Jump = true;
            if (e.KeyCode == Keys.Down) logicGame.Player2.Defend(true);
            if (e.KeyCode == Keys.NumPad1) player2AttackJ = true;
            if (e.KeyCode == Keys.NumPad2) player2AttackK = true;
            if (e.KeyCode == Keys.NumPad3) player2AttackL = true;
            if (e.KeyCode == Keys.NumPad5) player2AttackI = true;
            if (e.KeyCode == Keys.M) Console.WriteLine(logicGame.Player2.IsFacingLeft ? "left" : "Right");
        }

        private void StreetFighterGame_Load_1(object sender, EventArgs e)
        {

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Handle key up events for Player 1
            if (e.KeyCode == Keys.A || logicGame.Player1.DangDanhDungKo()) player1MoveLeft = false; logicGame.Player1.StopMoving();
            if (e.KeyCode == Keys.D || logicGame.Player1.DangDanhDungKo()) player1MoveRight = false; logicGame.Player1.StopMoving();
            if (e.KeyCode == Keys.W) player1Jump = false;
            if (e.KeyCode == Keys.S) logicGame.Player1.Defend(false);
            if (e.KeyCode == Keys.J) player1AttackJ = false;
            if (e.KeyCode == Keys.K) player1AttackK = false;
            if (e.KeyCode == Keys.L) player1AttackL = false;
            if (e.KeyCode == Keys.I) player1AttackI = false;
            // Handle key up events for Player 2
            if (e.KeyCode == Keys.Left || logicGame.Player2.DangDanhDungKo()) player2MoveLeft = false; logicGame.Player2.StopMoving();
            if (e.KeyCode == Keys.Right || logicGame.Player2.DangDanhDungKo()) player2MoveRight = false; logicGame.Player2.StopMoving();
            if (e.KeyCode == Keys.Up) player2Jump = false;
            if (e.KeyCode == Keys.Down) logicGame.Player2.Defend(false);
            if (e.KeyCode == Keys.NumPad1) player2AttackJ = false;
            if (e.KeyCode == Keys.NumPad2) player2AttackK = false;
            if (e.KeyCode == Keys.NumPad3) player2AttackL = false;
            if (e.KeyCode == Keys.NumPad5) player2AttackI = false;
        }

        private void UpdatePlayerInput()
        {
            // Cập nhật di chuyển cho Player 1
            if (player1MoveLeft)
            {
                if (!logicGame.Player1.IsFacingLeft) logicGame.Player1.PositionX += logicGame.Player1.charWidth;
                logicGame.Player1.MoveLeft();
                logicGame.Player1.IsMovingLeft = true;
                logicGame.Player1.IsFacingLeft = true;
            }

            if (player1MoveRight)
            {
                if (logicGame.Player1.IsFacingLeft) logicGame.Player1.PositionX -= logicGame.Player1.charWidth;
                logicGame.Player1.MoveRight();
                logicGame.Player1.IsMovingLeft = false;
                logicGame.Player1.IsFacingLeft = false;
            }

            // Chỉ cập nhật hướng đối mặt nếu cả hai người chơi không di chuyển
            if (!player1MoveLeft && !player1MoveRight)
            {
                if (!logicGame.Player1.IsFacingLeft && logicGame.Player1.PositionX > logicGame.Player2.PositionX)
                {
                    logicGame.Player1.IsFacingLeft = true;
                    logicGame.Player1.PositionX += logicGame.Player1.charWidth;
                }
            }

            // Các hành động khác cho Player 1
            if (player1Jump) logicGame.Player1.Jump();
            if (player1AttackJ) logicGame.Player1.Attack(attackType: ActionState.AttackingJ, logicGame.Player1, logicGame.Player2);
            if (player1AttackK) logicGame.Player1.Attack(attackType: ActionState.AttackingK, logicGame.Player1, logicGame.Player2);
            if (player1AttackL) logicGame.Player1.Attack(attackType: ActionState.AttackingL, logicGame.Player1, logicGame.Player2);
            if (player1AttackI)
            {
                logicGame.Player1.Attack(attackType: ActionState.AttackingI, logicGame.Player1, logicGame.Player2);
                logicGame.Player1.startDrawHitbox();
            }

                // Cập nhật di chuyển cho Player 2
                if (player2MoveLeft)
            {
                if (!logicGame.Player2.IsFacingLeft) logicGame.Player2.PositionX += logicGame.Player2.charWidth;
                logicGame.Player2.MoveLeft();
                logicGame.Player2.IsMovingLeft = true;
                logicGame.Player2.IsFacingLeft = true;
            }

            if (player2MoveRight)
            {
                if (logicGame.Player2.IsFacingLeft) logicGame.Player2.PositionX -= logicGame.Player2.charWidth;
                logicGame.Player2.MoveRight();
                logicGame.Player2.IsMovingLeft = false;
                logicGame.Player2.IsFacingLeft = false;
            }

            if (!player2MoveLeft && !player2MoveRight)
            {
                if (!logicGame.Player2.IsFacingLeft && logicGame.Player2.PositionX > logicGame.Player1.PositionX) {
                    logicGame.Player2.IsFacingLeft = true;
                    logicGame.Player2.PositionX += logicGame.Player2.charWidth;
                }
            }

            // Các hành động khác cho Player 2
            if (player2Jump) logicGame.Player2.Jump();
            if (player2AttackJ) logicGame.Player2.Attack(attackType: ActionState.AttackingJ, logicGame.Player1, logicGame.Player2);
            if (player2AttackK) logicGame.Player2.Attack(attackType: ActionState.AttackingK, logicGame.Player1, logicGame.Player2);
            if (player2AttackL) logicGame.Player2.Attack(attackType: ActionState.AttackingL, logicGame.Player1, logicGame.Player2);
            if (player2AttackI)
            {
                logicGame.Player2.Attack(attackType: ActionState.AttackingI, logicGame.Player1, logicGame.Player2);
                logicGame.Player2.startDrawHitbox();
            }
        }

        private void DrawCharacter(PaintEventArgs e, Character character, bool flip)
        {
            if (character == null) return;

            // Sử dụng `IsMovingLeft` để quyết định có flip hình ảnh hay không
            bool shouldFlip = character.IsMovingLeft || (!character.IsMovingLeft && character.IsFacingLeft);
            character.charWidth = (int)(character.CurrentImage.Width * character.ScaleFactor);
            character.charHeight = (int)(character.CurrentImage.Height * character.ScaleFactor);
            if (!character.isJumpping) character.PositionY = Math.Min(character.BaseY - character.charHeight, this.ClientSize.Height - character.charHeight);

            // Lưu trạng thái đồ họa hiện tại
            GraphicsState state = e.Graphics.Save();
            
            // Nếu cần flip, thực hiện flip
            if (shouldFlip)
            {

                character.IsFacingLeft = true;
                e.Graphics.TranslateTransform(character.PositionX, 0);
                e.Graphics.ScaleTransform(-1, 1); // Lật trên trục X
                character.rectangle = new Rectangle(character.PositionX + character.charWidth, character.PositionY, character.charWidth, character.charHeight);
                e.Graphics.DrawImage(character.CurrentImage, new Rectangle(0, character.PositionY, character.charWidth, character.charHeight));
            }
            else
            {
                character.rectangle = new Rectangle(character.PositionX, character.PositionY, character.charWidth, character.charHeight);
                e.Graphics.DrawImage(character.CurrentImage, character.rectangle);
            }

            // Khôi phục trạng thái đồ họa
            e.Graphics.Restore(state);
        }
        private void DrawHitbox(PaintEventArgs e, ActionState attackType, Character character, bool flip, Character character2)
        {

            GraphicsState state = e.Graphics.Save();
            if (character.IsFacingLeft)
            {
                e.Graphics.TranslateTransform(character.PositionX, 0);
                e.Graphics.ScaleTransform(-1, 1);
                if (character.CurrentHitboxImage == null)
                {
                    Rectangle rectangleHitbox = new Rectangle(0, character.PositionY + (character.charHeight / 2 - 50), character.charWidth, 100);
                    using (SolidBrush redBrush = new SolidBrush(Color.Red))
                    {
                        Graphics g = e.Graphics;
                        g.FillRectangle(redBrush, rectangleHitbox); // Tô đầy hitbox
                        g.FillRectangle(redBrush, character2.rectangle);
                    }
                    CollisionHandler.KiemTra2ThangDanhNhau(character, character2, rectangleHitbox, character2.rectangle);
                }
                else
                {
                    Rectangle rectangleHitbox = new Rectangle(character.charWidth, character.PositionY + (character.charHeight / 2 - character.CurrentHitboxImage.Height) + character.CurrentHitboxImage.Height / 2, (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height));
                    e.Graphics.DrawImage(character.CurrentHitboxImage, rectangleHitbox);
                    CollisionHandler.KiemTra2ThangDanhNhau(character, character2, rectangleHitbox, character2.rectangle);
                }
            }
            else
            {
                if (character.CurrentHitboxImage == null)
                {
                    Rectangle rectangleHitbox = new Rectangle(character.PositionX, character.PositionY + (character.charHeight / 2 - 50), character.charWidth, 100);
                    using (SolidBrush redBrush = new SolidBrush(Color.Red))
                    {
                        Graphics g = e.Graphics;
                        g.FillRectangle(redBrush, character.PositionX, character.PositionY + (character.charHeight / 2 - 50), character.charWidth, 100); // Tô đầy hitbox
                        g.FillRectangle(redBrush, character2.rectangle);
                    }
                    CollisionHandler.KiemTra2ThangDanhNhau(character, character2, rectangleHitbox, character2.rectangle);
                }
                else
                {
                    Rectangle rectangleHitbox = new Rectangle(character.PositionX + character.charWidth, character.PositionY + (character.charHeight / 2 - character.CurrentHitboxImage.Height / 2), (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height));
                    e.Graphics.DrawImage(character.CurrentHitboxImage, new Rectangle(character.PositionX + character.charWidth, character.PositionY + (character.charHeight / 2 - character.CurrentHitboxImage.Height / 2), (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height)));
                    CollisionHandler.KiemTra2ThangDanhNhau(character, character2, rectangleHitbox, character2.rectangle);
                }
            }
            e.Graphics.Restore(state);
            
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (background != null && background.Width > 0 && background.Height > 0)
            {
                // Vẽ nền với tỷ lệ phù hợp
                float scaleX = (float)this.ClientSize.Width / background.Width;
                float scaleY = (float)this.ClientSize.Height / background.Height;
                float scale = Math.Min(scaleX, scaleY);

                e.Graphics.DrawImage(background, 0, 0, background.Width * scale, background.Height * scale);
            }
            else
            {
                // Nếu không có nền hợp lệ, vẽ nền mặc định
                e.Graphics.Clear(Color.Gray); // Nền màu xám mặc định
            }

            // Vẽ thanh sức khỏe và năng lượng cho cả hai người chơi
            if (logicGame.Player1 != null)
            {
                // Vẽ thanh cho Player1 ở góc trái
                if (DrawHealthAndEnergy(e, logicGame.Player1, 100, 20, flip: false) <= 0f)
                {
                    //QuanLiTaiKhoan.LuuTranDau(idChar1, idChar2, "Lose");
                    SetTextWinner(logicGame.Player2.Name);
                }
                DrawCharacter(e, logicGame.Player1, flip: false);  // Vẽ nhân vật Player1
            }

            if (logicGame.Player2 != null)
            {
                // Vẽ thanh cho Player2 ở góc phải
                if (DrawHealthAndEnergy(e, logicGame.Player2, this.ClientSize.Width - 300, 20, flip: true) <= 0f)
                {
                    //QuanLiTaiKhoan.LuuTranDau(idChar1, idChar2, "Win");
                    SetTextWinner(logicGame.Player1.Name);
                };
                DrawCharacter(e, logicGame.Player2, flip: true);  // Vẽ nhân vật Player2
            }
            
            if (logicGame.Player1.DangDanhDungKo()) DrawHitbox(e, attackType: ActionState.AttackingI, logicGame.Player1, flip: false, logicGame.Player2);
            if (logicGame.Player2.DangDanhDungKo())  DrawHitbox(e, attackType: ActionState.AttackingI, logicGame.Player2, flip: false, logicGame.Player1);
        }
        private void SetTextWinner(string nameWinner)
        {
            if (endGame) return; // Đảm bảo chỉ xử lý khi game chưa kết thúc

            endGame = true; // Đánh dấu trận đấu đã kết thúc
            labelWiner.Visible = true;
            labelWiner.Text = nameWinner + "  " + "WIN";

            // Lưu lịch sử trận đấu
            QuanLiTaiKhoan.LuuTranDau(
                idChar1,
                idChar2,
                nameWinner == logicGame.Player1.Name ? "Win" : "Lose"
            );

            // Hiển thị FormStart
            // Dừng và giải phóng Timer cũ (nếu có)
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            // Tạo Timer mới
            timer = new Timer
            {
                Interval = 4000
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
            FormStart mhs = new FormStart();
            mhs.Show();
            this.Dispose(); // Giải phóng form hiện tại
        }


        private float DrawHealthAndEnergy(PaintEventArgs e, Character character, int x, int y, bool flip)
        {
            if (character == null) return 0f;

            // Vẽ tên nhân vật
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            {
                if (flip)
                {
                    e.Graphics.DrawString(character.Name, font, Brushes.White, x + 160, y - 20);
                }
                else
                {
                    e.Graphics.DrawString(character.Name, font, Brushes.White, x, y - 20);
                }
            }

            // Vẽ ảnh đại diện (nếu có)
            if (character.Avatar != null)
            {
                if (flip)
                {
                    // Lật ảnh đại diện cho Player 2
                    GraphicsState state = e.Graphics.Save(); // Lưu trạng thái gốc
                    e.Graphics.ScaleTransform(-1, 1); // Lật trên trục X
                    e.Graphics.DrawImage(character.Avatar, -(x + 200 + 1), y, -60, 50); // Vẽ ảnh đã lật
                    e.Graphics.Restore(state); // Khôi phục trạng thái gốc
                }
                else
                {
                    e.Graphics.DrawImage(character.Avatar, x - 60, y, 60, 50); // Vẽ bình thường
                }
            }

            // Vẽ thanh sức khỏe
            float healthPercentage = character.PhanTramMauHienTai();
            if (flip)
            {
                // Lật thanh máu từ phải sang trái
                e.Graphics.FillRectangle(Brushes.Green, x + (1 - healthPercentage) * 200, y, healthPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y, 200, 20);
            }
            else
            {
                // Vẽ bình thường
                e.Graphics.FillRectangle(Brushes.Green, x, y, healthPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y, 200, 20);
            }

            // Vẽ thanh năng lượng
            float energyPercentage = character.PhanTramNangLuongHienTai();
            if (flip)
            {
                // Lật thanh năng lượng từ phải sang trái
                e.Graphics.FillRectangle(Brushes.Blue, x + (1 - energyPercentage) * 200, y + 30, energyPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y + 30, 200, 20);
            }
            else
            {
                // Vẽ bình thường
                e.Graphics.FillRectangle(Brushes.Blue, x, y + 30, energyPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y + 30, 200, 20);
            }
            return healthPercentage;
        }

        private void StreetFighterGame_Load(object sender, EventArgs e)
        {
            // Empty, currently no special loading logic needed
        }
    }
}
