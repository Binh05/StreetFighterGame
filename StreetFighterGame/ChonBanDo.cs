using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StreetFighterGame
{
    public partial class ChonBanDo : Form
    {
        public string SelectedMap { get; set; }

        public List<string> mapPaths;

        public ChonBanDo()
        {
            InitializeComponent();
            LoadMaps();
        }

        public void LoadMaps()
        {
            string mapFolder = @".\Map"; // Đường dẫn thư mục chứa các bản đồ

            // Lấy tất cả các file PNG trong thư mục Map
            mapPaths = Directory.GetFiles(mapFolder, "*.png").ToList();

            // Thêm tên các bản đồ vào ListBox (không bao gồm phần mở rộng)
            foreach (var mapPath in mapPaths)
            {
                listBoxMaps.Items.Add(Path.GetFileNameWithoutExtension(mapPath));
            }

            // Debug: Hiển thị thông tin các đường dẫn đã tải
            foreach (var mapPath in mapPaths)
            {
                Console.WriteLine(mapPath); // Hoặc dùng Debug.WriteLine(mapPath)
            }
        }

        private void ChonBanDo_Load(object sender, EventArgs e)
        {
            // Debug: Khi form được load
            Console.WriteLine("Form ChonBanDo loaded.");
        }

        public void buttonStart_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng đã chọn bản đồ
            if (listBoxMaps.SelectedIndex >= 0)
            {
                // Lấy đường dẫn bản đồ đã chọn từ danh sách
                SelectedMap = mapPaths[listBoxMaps.SelectedIndex];

                // Debug: Hiển thị thông tin về bản đồ đã chọn
                Console.WriteLine($"Đã chọn bản đồ: {SelectedMap}");

                // Đóng form và trả về kết quả
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Thông báo nếu người dùng chưa chọn bản đồ
                MessageBox.Show("Vui lòng chọn một bản đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có bản đồ nào được chọn không
            if (listBoxMaps.SelectedIndex >= 0)
            {
                // Hiển thị PictureBox
                pictureBoxMap.Show();

                // Lấy đường dẫn bản đồ đã chọn từ mapPaths
                string selectedMapPath = mapPaths[listBoxMaps.SelectedIndex];

                // Debug: Hiển thị thông tin về bản đồ đã chọn
                Console.WriteLine($"Đường dẫn bản đồ đã chọn: {selectedMapPath}");

                // Kiểm tra xem tệp có tồn tại không trước khi hiển thị
                if (File.Exists(selectedMapPath))
                {
                    // Tải hình ảnh từ tệp và hiển thị trong PictureBox
                    pictureBoxMap.Image = System.Drawing.Image.FromFile(selectedMapPath);
                }
                else
                {
                    // Thông báo nếu không tìm thấy tệp
                    MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Nếu không có lựa chọn nào, ẩn PictureBox
                pictureBoxMap.Hide();
            }
        }
    }
}
