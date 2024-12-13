using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreetFighterGame.QuanLiTaikhoan;

namespace StreetFighterGame
{
    public partial class ChonNhanVat : Form
    {
        private string tenNhanVatDuocChon1, tenNhanVatDuocChon2;
        private int idChar1, idChar2;
        private int soLanChonNhanVat;
        private Dictionary<string, Image> characterImages;
        private List<string> mapPaths;
        private int currentInDexMapSelect;
        public ChonNhanVat()
        {
            InitializeComponent();
            buttonFight.Visible = true;
            labelName.Visible = false;
            pictureBoxMap.Visible = false;
            buttonNextMap.Visible = buttonPreMap.Visible =false;
            this.Controls.Add(pictureBoxMap);
            // Lấy kích thước của Form và pictureBoxMap
            int x = (this.ClientSize.Width - pictureBoxMap.Width) / 2;
            int y = (this.ClientSize.Height - pictureBoxMap.Height) / 2;

            // Đặt vị trí của pictureBoxMap vào giữa Form
            pictureBoxMap.Location = new Point(x, y);
            LoadMapInFo();
            InitializeCharacterImages();
        }
        private void LoadMapInFo()
        {
            currentInDexMapSelect = 0;
            string mapFolder = @".\Map"; // Đường dẫn thư mục chứa các bản đồ
            // Lấy tất cả các file PNG trong thư mục Map
            mapPaths = Directory.GetFiles(mapFolder, "*.png").ToList();
        }
        private void InitializeCharacterImages()
        {
            characterImages = new Dictionary<string, Image>();

            characterImages["chunli"] = Image.FromFile(".\\Chunli\\ChunLi_0-0.png");
            characterImages["goku"] = Image.FromFile(".\\GokuMUI\\GokuMUI_0-0.png");
            characterImages["kyo"] = Image.FromFile(".\\Kyo\\Kyo_0-0.png");
            characterImages["king"] = Image.FromFile(".\\King\\King_0-0.png");
            characterImages["ryu"] = Image.FromFile(".\\Ryu\\Ryu_0-0.png");
            characterImages["vegeto"] = Image.FromFile(".\\Vegeto\\Vegeto_0-0.png");
            characterImages["zenitsu"] = Image.FromFile(".\\Zenitsu\\Zenitsu_0-1.png");
            pictureBoxChar1.Visible = pictureBoxChar2.Visible = false;

        }
        public void buttonFight_Click(object sender, EventArgs e)
        {
            // Kiểm tra số lần chọn nhân vật
            if (soLanChonNhanVat == 1)
            {
                buttonFight.Text = "Start"; // Đổi chữ của button khi đã chọn đủ 2 nhân vật
                panel1.Visible = false;
                labelName.Visible =false;
                //
                pictureBoxMap.Visible = true;
                buttonNextMap.Visible = buttonPreMap.Visible = true;
                ShowCurrentInDexMapOnPictureBox();
            }
            else if (soLanChonNhanVat == 2)
            {
                StreetFighterGame game = new StreetFighterGame(tenNhanVatDuocChon1, tenNhanVatDuocChon2, idChar1, idChar2, mapPaths[currentInDexMapSelect]);
                game.Show(); // Mở game
                this.Close(); // Đóng form hiện tại
                //
                // Tạo đối tượng chọn bản đồ
                //ChonBanDo formChonBanDo = new ChonBanDo();

                //// Mở form chọn bản đồ và chờ người chơi chọn
                //if (formChonBanDo.ShowDialog() == DialogResult.OK)
                //{
                //    // Kiểm tra và gán đường dẫn bản đồ cho game
                //    string selectedMap = formChonBanDo.SelectedMap;
                //    if (!string.IsNullOrEmpty(selectedMap) && File.Exists(selectedMap))
                //    {
                //        // Khởi tạo trò chơi với thông tin nhân vật và bản đồ đã chọn
                //        StreetFighterGame game = new StreetFighterGame(tenNhanVatDuocChon1, tenNhanVatDuocChon2, idChar1, idChar2, selectedMap);
                //        game.Show(); // Mở game
                //        this.Close(); // Đóng form hiện tại
                //    }
                //    else
                //    {
                //        MessageBox.Show("Vui lòng chọn bản đồ hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
                //else
                //{
                //    // Nếu không chọn bản đồ (thoát ra mà không chọn gì)
                //    MessageBox.Show("Bạn chưa chọn bản đồ! Vui lòng chọn lại bản đồ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    // Reset lại để có thể chọn lại bản đồ
                //    soLanChonNhanVat = 0; // Reset lại số lần chọn nhân vật để quay lại bước chọn nhân vật
                //    labelName.Visible = false;
                //    buttonFight.Visible = false;
                //}
            }

            // Ẩn/Hiện các control khi chọn nhân vật
            if (soLanChonNhanVat < 1) buttonFight.Visible = false;  // Ẩn nút Fight khi người chơi đã chọn xong
            else buttonFight.Visible = true;
            labelName.Visible = false;
            // Tăng số lần chọn nhân vật để chuyển sang bước tiếp theo
            soLanChonNhanVat++;
        }
        private void ShowCurrentInDexMapOnPictureBox()
        {
            if (File.Exists(mapPaths[currentInDexMapSelect]))
            {
                // Tải hình ảnh từ tệp và hiển thị trong PictureBox
                pictureBoxMap.BackgroundImage = Image.FromFile(mapPaths[currentInDexMapSelect]);
            }
            else
            {
                // Thông báo nếu không tìm thấy tệp
                MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ChonNhanVat_Load(object sender, EventArgs e)
        {
            tenNhanVatDuocChon1 = tenNhanVatDuocChon2 = "";
            soLanChonNhanVat = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HandlePictureBoxClick(string nameNV)
        {

            if (soLanChonNhanVat == 0)
            {
                pictureBoxChar1.Visible = true;
                tenNhanVatDuocChon1 = nameNV;
                idChar1 = QuanLiTaiKhoan.GetNhanVatIdByName(nameNV);
                pictureBoxChar1.BackgroundImage = characterImages[nameNV.ToLower()];
                pictureBoxChar2.Visible = false;
            }
            else if (soLanChonNhanVat == 1)
            {
                pictureBoxChar2.Visible = true;
                tenNhanVatDuocChon2 = nameNV;
                idChar2 = QuanLiTaiKhoan.GetNhanVatIdByName(nameNV);
                pictureBoxChar2.BackgroundImage = characterImages[nameNV.ToLower()];
            }
            labelName.Visible = true;
            labelName.Text = nameNV;
            buttonFight.Visible = true;
        }
        private void LoadCharacterImage(string characterName)
        {
            if (characterImages.ContainsKey(characterName.ToLower()))
            {
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("chunli");

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("goku");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("king");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("kyo");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("ryu");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("vegeto");
        }

        private void buttonNextMap_Click(object sender, EventArgs e)
        {
            currentInDexMapSelect++;
            if (currentInDexMapSelect > mapPaths.Count - 1) currentInDexMapSelect = 0;
            ShowCurrentInDexMapOnPictureBox();
        }

        private void buttonPreMap_Click(object sender, EventArgs e)
        {
            currentInDexMapSelect--;
            if (currentInDexMapSelect < 0) currentInDexMapSelect = mapPaths.Count - 1;
            ShowCurrentInDexMapOnPictureBox();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }

        private void labelName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("zenitsu");
        }
    }
}
