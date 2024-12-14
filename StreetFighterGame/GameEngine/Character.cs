using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Media;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;


namespace StreetFighterGame.GameEngine
{
    public enum ActionState
    {
        Standing,
        WalkingFront,
        WalkingBack,
        Defending,
        Jumping,
        AttackingJ,
        AttackingK,
        AttackingL,
        AttackingI,
        hit
    }
    public struct ChiSoSucManh
    {
        public float mauHienTai, mauToiDa;
        public float nangLuongHienTai, nangLuonToiDa;
        public float dame;
        public ChiSoSucManh(int mauTd, int d)
        {
            mauHienTai = mauTd;
            mauToiDa = mauTd;
            nangLuongHienTai = 100;
            nangLuonToiDa = 100;
            dame = d;
        }
    }
    public abstract class Character
    {
        public bool IsFacingLeft { get; set; }
        public bool IsMovingLeft { get; set; }
        public string Name { get; set; }
        public float Energy { get; set; }
        public float Health { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int BaseY { get; set; }
        public float ScaleFactor { get; set; }
        public Image CurrentImage { get; protected set; }
        public Image Avatar { get; set; }  // Ảnh đại diện cho nhân vật
        public int charWidth { get; set; }
        public int charHeight { get; set; }

        protected Dictionary<ActionState, List<Image>> Animations { get; set; }
        public ActionState CurrentState { get; private set; } = ActionState.Standing;

        private int currentFrame = 0;
        private int frameDelay = 0;
        private int frameCounter = 0;
        private int lastFrameOfAttackAnimation;
        private float jumpSpeed = 0f;
        private const float Gravity = 16f;
        private int DefendYOffset = 25;
        public bool isDefense, triggerAttack; // biến để kiểm tra có đang trong trạng thái attack hay ko
        public bool isAttacking, isJumpping, isHit;

        ChiSoSucManh cssm;

        private Timer frameTimer; // Timer để thay đổi frame
        private int frameInterval = 100; // Khoảng thời gian giữa các frame (ms)

        private Timer hitTimer;
        private int hitDuration = 500;

        private List<Hitbox> hitboxes = new List<Hitbox>();
        public void createHitbox()
        {
            int hitboxWidth = charWidth, hitboxHeight = 50, hitboxLifetime = 50;
            
            int hitboxX = IsFacingLeft ? PositionX - hitboxWidth : PositionX; // Tọa độ X dựa trên hướng
            int hitboxY = PositionY + (int)(CurrentImage.Height / 3);

            // Tạo hitbox mới
            Hitbox newHitbox = new Hitbox(hitboxX, hitboxY, hitboxWidth, hitboxHeight, hitboxLifetime);
            hitboxes.Add(newHitbox);
        }
        public void Draw(Graphics g)
        {
            if (CurrentImage != null)
            {
                g.DrawImage(CurrentImage, PositionX, PositionY, CurrentImage.Width * ScaleFactor, CurrentImage.Height * ScaleFactor);
            }
        }
        public void DrawHitbox(Graphics g)
        {
            foreach (var hitbox in hitboxes)
            {
                hitbox.Draw(g, isCircle: false); // Chọn vẽ hình tròn hoặc vuông
            }
        }
        public void mele(Graphics g)
        {
            createHitbox(); // Tạo hitbox cho đòn tấn công
            DrawHitbox(g);
        }
        public void Attack(ActionState attackType)
        {
            ChangeState(attackType);
            isAttacking = true;
            triggerAttack = true;
        }
        public string soundPath = ".\\sound\\PunchHit1.wav";
        private SoundPlayer soundPlayer;
        protected Character(int startX, int startY, float scaleFactor, int mau, int d)
        {
            PositionX = startX;
            BaseY = startY;
            PositionY = startY;
            ScaleFactor = scaleFactor;
            Animations = new Dictionary<ActionState, List<Image>>();
            isAttacking = isJumpping = isDefense = triggerAttack = isHit = false;

            frameTimer = new Timer { Interval = frameInterval };
            frameTimer.Tick += OnFrameTimerTick;
            frameTimer.Start();

            hitTimer = new Timer { Interval = hitDuration };
            hitTimer.Tick += OnHitTimerTick;

            SetChiSoSucManh(mau, d);
            LoadHitSound();
        }
        public void LoadHitSound()
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, soundPath);
            if (File.Exists(fullPath))
            {
                soundPlayer = new SoundPlayer(fullPath);
            }
            else
            {
                Console.WriteLine("Sound file not found: " + fullPath);
            }
        }
        public void PlayHitSound()
        {
            soundPlayer?.Play(); // Phát âm thanh nếu đã nạp
        }
        private void OnFrameTimerTick(object sender, EventArgs e)
        {
            // Chỉ xử lý nếu trạng thái hiện tại có animation
            if (Animations.ContainsKey(CurrentState) && Animations[CurrentState].Count > 0)
            {
                var frames = Animations[CurrentState];
                currentFrame = (currentFrame + 1) % frames.Count;
                CurrentImage = frames[currentFrame];

                // Nếu là trạng thái tấn công, kiểm tra nếu đến frame cuối thì ngừng tấn công
                if (IsAttackAction(CurrentState) && currentFrame == lastFrameOfAttackAnimation)
                {
                    isAttacking = triggerAttack = false;
                    StopAttacking();
                }
            }
        }
        private void OnHitTimerTick(object sender, EventArgs e)
        {

            isHit = false;
            ChangeState(ActionState.Standing);
            hitTimer.Stop();
        }
        public float PhanTramMauHienTai()
        {
            return cssm.mauHienTai / cssm.mauToiDa;
        }
        public float PhanTramNangLuongHienTai()
        {

            //Console.WriteLine(cssm.nangLuongHienTai / cssm.nangLuonToiDa);
            return (float)(cssm.nangLuongHienTai) / (float)(cssm.nangLuonToiDa);
        }
        public void SetChiSoSucManh(int mau, int d)
        {
            cssm = new ChiSoSucManh(mau, d);
        }
        public void TruMau(float dame)
        {
            Console.WriteLine(dame);
            Console.WriteLine(cssm.mauHienTai);
            Console.WriteLine(cssm.mauToiDa);
            Console.WriteLine(cssm.mauHienTai / cssm.mauToiDa);
            cssm.mauHienTai -= dame;
        }
        public float Dame
        {
            get { return cssm.dame; }
        }
        protected void LoadAnimations(string folderPath, Dictionary<ActionState, string> stateToPrefix, Dictionary<ActionState, int> stateToFrameCount)
        {
            foreach (var state in stateToPrefix)
            {
                // Lấy số lượng khung hình từ dictionary stateToFrameCount
                int frameCount = stateToFrameCount.ContainsKey(state.Key) ? stateToFrameCount[state.Key] : 1;
                Animations[state.Key] = LoadImages(folderPath, frameCount, state.Value);
            }

            if (Animations.ContainsKey(ActionState.Standing))
            {
                CurrentImage = Animations[ActionState.Standing][0];
            }
        }

        private List<Image> LoadImages(string folderPath, int count, string filePrefix)
        {
            var images = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                string filePath = Path.Combine(folderPath, $"{filePrefix}-{i}.png");
                if (File.Exists(filePath))
                {
                    images.Add(Image.FromFile(filePath));
                }
            }
            return images;
        }

        public virtual void Update()
        {
            charWidth = (int)(CurrentImage.Width * ScaleFactor);
            charHeight = (int)(CurrentImage.Height * ScaleFactor);
            if (isHit) return;
            if (isJumpping) HandleJumping();
            hitboxes.RemoveAll(h => !h.isActive); // Xóa hitbox đã hết thời gian tồn tại

            // UpdateFrame();
        }
        public bool DangDanhDungKo()
        {
            bool b = isAttacking && triggerAttack;
            triggerAttack = false;
            return b;
        }
        public void XuLiKhiBiDanh()
        {
            if (cssm.mauHienTai <= 0) return;

            ChangeState(ActionState.hit); // Chuyển trạng thái sang Hit

            // Bắt đầu đếm ngược 1 giây
            hitTimer.Start();
            isHit = true;
        }
        /*private void UpdateFrame()
        {
            frameCounter++;
            if (frameCounter >= frameDelay)
            {
                frameCounter = 0;

                var frames = Animations[CurrentState];
                if (CurrentState == ActionState.Defending)
                {
                    // Dừng ở khung hình cuối khi phòng thủ
                    currentFrame = frames.Count - 1;
                }
                else
                {
                    // Tiếp tục lặp cho các trạng thái khác
                    currentFrame = (currentFrame + 1) % frames.Count;
                }
                CurrentImage = frames[currentFrame];
                if (lastFrameOfAttackAnimation == currentFrame && isAttacking)
                {
                    isAttacking = triggerAttack = false;
                    StopAttacking();
                }
            }

        }*/

        private void HandleJumping()
        {
            PositionY -= (int)jumpSpeed;
            if (isJumpping) jumpSpeed -= Gravity;

            if (PositionY > BaseY - charHeight)  // Đảm bảo không đi qua mặt đất
            {
                PositionY = BaseY - charHeight;
                jumpSpeed = 0;
                isJumpping = false;
                ChangeState(ActionState.Standing);
            }
        }

        public void ChangeState(ActionState newState)
        {
            if (CurrentState == newState || isAttacking || isHit) return;

            CurrentState = newState;
            frameDelay = newState == ActionState.WalkingFront || newState == ActionState.WalkingBack ? 1 : 1;
            currentFrame = 0;

            if (Animations.ContainsKey(newState))
            {
                CurrentImage = Animations[newState][0];
            }

            // Đặt khoảng thời gian giữa các frame dựa trên hành động
            frameInterval = GetFrameIntervalForState(newState);
            frameTimer.Interval = frameInterval; // Cập nhật tốc độ Timer

            /// neu state la tan cong
            if (IsAttackAction(newState))
            {
                lastFrameOfAttackAnimation = Animations[newState].Count - 1; // lấy frame để kiểm tra tấn công
            }
        }
        private int GetFrameIntervalForState(ActionState state)
        {
            switch (state)
            {
                case ActionState.Standing: return 50;
                case ActionState.WalkingBack: return 50;
                case ActionState.WalkingFront: return 50;
                case ActionState.Jumping: return 50;
                case ActionState.AttackingJ: return 50;
                case ActionState.AttackingI: return 50;
                default: return 32;
            }
        }
        public bool IsAttackAction(ActionState ac)
        {
            return ac == ActionState.AttackingJ || ac == ActionState.AttackingK || ac == ActionState.AttackingL || ac == ActionState.AttackingI;
        }
        public bool IsOnGround() // kiem tra co tren mat dat ko
        {
            //Console.WriteLine("vua goi IsOnGround");
            //if (PositionY == BaseY) Console.WriteLine("dang tren dat");
            return jumpSpeed == 0 && !isJumpping;
        }
        public void MoveRight()
        {
            if (isDefense) return;
            //if (CurrentState == ActionState.Jumping) return; // nếu đang nhảy mà retun thì nhân vật ko thể di chuyển khi nhảy
            //ChangeState(ActionState.WalkingFront);
            //PositionX = Math.Min(PositionX + 12, 1000);  // Kiểm tra vị trí để tránh đi ra ngoài
            /*** code sữa ****/
            if (IsOnGround()) ChangeState(ActionState.WalkingFront);
            PositionX = Math.Min(PositionX + 15, 900);  // Kiểm tra vị trí để tránh đi ra ngoài
        }

        public void MoveLeft()
        {
            if (isDefense) return;
            if (IsOnGround()) ChangeState(ActionState.WalkingBack);
            PositionX = Math.Max(PositionX - 15, 0);  // Kiểm tra vị trí để tránh đi ra ngoài
        }
        public void StopMoving()
        {
            if (IsOnGround()) // chỉ chuyển sang standing khi ngừng, và ở trên đất
            {
                ChangeState(ActionState.Standing);
            }
        }

        public void Jump()
        {
            if (IsOnGround()) // Kiểm tra nếu đang ở mặt đất
            {
                jumpSpeed = 70f;
                ChangeState(ActionState.Jumping);
                isJumpping = true;
            }
        }
        public void Defend(bool isDefen)
        {
            //ChangeState(ActionState.Defending);
            //PositionY = BaseY + DefendYOffset;
            /**** code sữa *****/
            if (IsOnGround() && isDefen) // chỉ ở trên đất mới được phòng thủ
            {
                ChangeState(ActionState.Defending);
                //PositionY = BaseY + DefendYOffset;
                isDefense = true;
            }
            else if (IsOnGround())
            {
                PositionY = BaseY;
                ChangeState(ActionState.Standing);
                isDefense = false;
            }
        }


        public void StopAttacking()
        {
            if (!isAttacking) ChangeState(ActionState.Standing);
        }
        protected void LoadAvatar(string avatarPath)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, avatarPath);
            Console.WriteLine($"Loading avatar from: {fullPath}"); // In đường dẫn

            if (File.Exists(fullPath))
            {
                Avatar = Image.FromFile(fullPath);
            }
            else
            {
                Console.WriteLine("Avatar file not found!");
                Avatar = new Bitmap(50, 50); // Ảnh mặc định
                using (Graphics g = Graphics.FromImage(Avatar))
                {
                    g.Clear(Color.Gray);
                    g.DrawString("?", new Font("Arial", 24), Brushes.White, 10, 10);
                }
            }
        }

    }
}
