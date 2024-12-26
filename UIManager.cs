using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public static class UIManager
{
    // Gán hình nền từ đường dẫn trong thư mục Resources
    public static void SetFormBackground(Form form, string imageName)
    {
        // Tạo đường dẫn tới thư mục Resources (cùng cấp với .exe khi build)
        string imagePath = Path.Combine(Application.StartupPath, "Resources", imageName);

        if (File.Exists(imagePath))
        {
            form.BackgroundImage = Image.FromFile(imagePath);
            form.BackgroundImageLayout = ImageLayout.Stretch;  // Căng hình vừa form
        }
        else
        {
            MessageBox.Show($"Không tìm thấy ảnh: {imagePath}", "Lỗi Tải Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Tạo nút với hình ảnh từ Resources
    public static Button CreateImageButton(string normalImage, string hoverImage, int width, int height)
    {
        string resourcePath = Path.Combine(Application.StartupPath, "Resources");
        string normalPath = Path.Combine(resourcePath, normalImage);
        string hoverPath = Path.Combine(resourcePath, hoverImage);

        Button button = new Button
        {
            Size = new Size(width, height),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            FlatAppearance = { BorderSize = 0 }
        };

        if (File.Exists(normalPath))
        {
            button.BackgroundImage = Image.FromFile(normalPath);
            button.BackgroundImageLayout = ImageLayout.Stretch;
        }
        else
        {
            MessageBox.Show($"Không tìm thấy ảnh: {normalPath}", "Lỗi Tải Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        button.MouseEnter += (s, e) =>
        {
            if (File.Exists(hoverPath))
            {
                button.BackgroundImage = Image.FromFile(hoverPath);
            }
        };

        button.MouseLeave += (s, e) =>
        {
            if (File.Exists(normalPath))
            {
                button.BackgroundImage = Image.FromFile(normalPath);
            }
        };

        return button;
    }
}
