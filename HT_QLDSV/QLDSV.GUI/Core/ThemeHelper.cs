using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QLDSV.GUI
{
    public static class ThemeHelper
    {
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;

            // ===== LOẠI TRỪ HOÀN TOÀN frmDangNhap =====
            // Form đăng nhập giữ nguyên style gốc từ designer, không áp dụng theme
            if (form is frmDangNhap) return;

            // Nền Form con chuẩn xám nhạt #F0F2F5 (trừ Shell chính)
            if (!(form is frmMain))
            {
                form.BackColor = Color.FromArgb(240, 242, 245);
            }

            ApplyControlTheme(form);
        }

        private static bool IsInSidebar(Control control)
        {
            Control current = control;
            while (current != null)
            {
                if (current.Name.ToLower().Contains("sidebar"))
                    return true;
                current = current.Parent;
            }
            return false;
        }

        private static string StripHtml(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            char[] array = new char[input.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < input.Length; i++)
            {
                char let = input[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex).Trim();
        }

        private static void ApplyControlTheme(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Bảo vệ thanh Sidebar - chỉ áp dụng màu chữ trắng, không thay đổi kích thước
                if (IsInSidebar(control))
                {
                    if (control is Label lblSidebar)
                    {
                        // Giữ nguyên font size của sidebar, chỉ đổi màu
                        lblSidebar.Font = new Font("Segoe UI", lblSidebar.Font.Size, lblSidebar.Font.Style);
                        lblSidebar.ForeColor = Color.White;
                    }
                    else if (control is Guna2HtmlLabel lblHtmlSidebar)
                    {
                        // Sidebar HtmlLabel: giữ kích thước, chỉ đổi màu
                        Size origSz = lblHtmlSidebar.Size;
                        bool origAuto = lblHtmlSidebar.AutoSize;
                        lblHtmlSidebar.AutoSize = false;
                        lblHtmlSidebar.Font = new Font("Segoe UI", lblHtmlSidebar.Font.Size, lblHtmlSidebar.Font.Style);
                        lblHtmlSidebar.ForeColor = Color.White;
                        lblHtmlSidebar.Size = origSz;
                        if (origAuto) lblHtmlSidebar.AutoSize = true;
                    }

                    if (control.Controls.Count > 0)
                    {
                        ApplyControlTheme(control);
                    }
                    continue;
                }

                if (control is Guna2Button btnGuna)
                {
                    ApplyGunaButtonTheme(btnGuna);
                }
                else if (control is Button btnStd)
                {
                    ApplyButtonTheme(btnStd);
                }
                
                else if (control is Guna2ComboBox cboGuna)
                {
                    // CRITICAL: Guna2ComboBox cũng có thể collapse khi set Font
                    Size cboOrigSize = cboGuna.Size;

                    cboGuna.BorderRadius = 5;
                    cboGuna.FillColor = Color.White;
                    cboGuna.ForeColor = Color.Black;
                    cboGuna.DropDownStyle = ComboBoxStyle.DropDownList;
                    float cboOrigFontSize = cboGuna.Font.Size;
                    cboGuna.Font = new Font("Segoe UI", cboOrigFontSize <= 10.5f ? cboOrigFontSize : 10.5f);

                    // Khôi phục kích thước gốc SAU khi thay đổi thuộc tính
                    cboGuna.Size = cboOrigSize;
                }
                else if (control is ComboBox cboStd)
                {
                    cboStd.DropDownStyle = ComboBoxStyle.DropDownList;
                    float origSize = cboStd.Font.Size;
                    cboStd.Font = new Font("Segoe UI", origSize <= 10.5f ? origSize : 10.5f);
                    cboStd.ForeColor = Color.Black;
                }
                else if (control is Guna2DataGridView gridGuna)
                {
                    ConfigureDataGridView(gridGuna);
                }
                else if (control is DataGridView gridStd)
                {
                    ConfigureDataGridView(gridStd);
                }
                else if (control is Guna2Panel pnlGuna)
                {
                    string pnlName = pnlGuna.Name.ToLower();
                    if (pnlName.Contains("filter") || pnlName.Contains("timkiem") || pnlName.Contains("loc"))
                    {
                        pnlGuna.FillColor = Color.FromArgb(180, 200, 223);
                    }
                    else if (pnlName.Contains("detail") || pnlName.Contains("chitiet") || pnlName.Contains("card"))
                    {
                        pnlGuna.FillColor = Color.White;
                    }
                }
                else if (control is Panel pnlStd)
                {
                    string pnlName = pnlStd.Name.ToLower();
                    if (pnlName.Contains("filter") || pnlName.Contains("timkiem") || pnlName.Contains("loc"))
                    {
                        pnlStd.BackColor = Color.FromArgb(180, 200, 223);
                    }
                    else if (pnlName.Contains("detail") || pnlName.Contains("chitiet") || pnlName.Contains("card"))
                    {
                        pnlStd.BackColor = Color.White;
                    }
                }
                else if (control is Guna2GroupBox grpGuna)
                {
                    grpGuna.FillColor = Color.White;
                    grpGuna.Font = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);
                    grpGuna.ForeColor = Color.FromArgb(31, 41, 55); // #1F2937
                    grpGuna.CustomBorderColor = Color.FromArgb(220, 225, 235);
                }
                else if (control is GroupBox grpBox)
                {
                    grpBox.BackColor = Color.White;
                    // Giữ font size gốc của GroupBox để tránh layout bị vỡ
                    float grpOrigSize = grpBox.Font.Size;
                    grpBox.Font = new Font("Segoe UI", grpOrigSize <= 10.5f ? grpOrigSize : 10f, FontStyle.Bold | FontStyle.Italic);
                    grpBox.ForeColor = Color.FromArgb(31, 41, 55); // #1F2937
                }
                else if (control is RadioButton radStd)
                {
                    radStd.Font = new Font("Segoe UI", radStd.Font.Size <= 10.5f ? radStd.Font.Size : 10f, radStd.Font.Style);
                    radStd.ForeColor = Color.Black;
                    radStd.BackColor = Color.Transparent;
                }
                else if (control is Guna2CheckBox chkGuna)
                {
                    chkGuna.Font = new Font("Segoe UI", chkGuna.Font.Size <= 10.5f ? chkGuna.Font.Size : 10f, chkGuna.Font.Style);
                    chkGuna.ForeColor = Color.Black;
                }
                else if (control is CheckBox chkStd)
                {
                    chkStd.Font = new Font("Segoe UI", chkStd.Font.Size <= 10.5f ? chkStd.Font.Size : 10f, chkStd.Font.Style);
                    chkStd.ForeColor = Color.Black;
                    chkStd.BackColor = Color.Transparent;
                }
                else if (control is Guna2HtmlLabel lblHtml)
                {
                    ApplyHtmlLabelStyle(lblHtml);
                }
                else if (control is Label lblStd)
                {
                    ApplyLabelStyle(lblStd);
                }

                if (control.Controls.Count > 0)
                {
                    ApplyControlTheme(control);
                }
            }
        }

        private static void ApplyButtonTheme(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;

            // Loại bỏ emoji và ký tự đặc biệt để so sánh text chính xác hơn
            string rawText = btn.Text != null ? btn.Text : "";
            string text = System.Text.RegularExpressions.Regex.Replace(rawText, @"[^\p{L}\p{N}\s]", "").ToLower().Trim();
            Color origColor = btn.BackColor;

            // Ưu tiên 1: Nhận diện qua text (bỏ emoji, giữ chữ tiếng Việt)
            if (text.Contains("xóa") || text.Contains("xoa") || text.Contains("từ chối") || text.Contains("tu choi") || text.Contains("hủy") || text.Contains("huy"))
            {
                btn.BackColor = Color.FromArgb(198, 40, 40); // Premium Red
            }
            else if (text.Contains("duyệt") || text.Contains("duyet") || text.Contains("lưu") || text.Contains("luu") || text.Contains("làm mới") || text.Contains("lam moi"))
            {
                btn.BackColor = Color.FromArgb(46, 125, 50); // Premium Green
            }
            else if (text.Contains("thêm") || text.Contains("them") || text.Contains("tạo") || text.Contains("tao"))
            {
                btn.BackColor = Color.FromArgb(21, 101, 192); // Royal Blue
            }
            // Ưu tiên 2: Nếu designer đã đặt màu rõ ràng (không phải màu hệ thống), giữ và premium hóa
            else if (origColor.A > 0 && origColor != SystemColors.Control && origColor != Color.Transparent
                     && origColor != SystemColors.GradientActiveCaption && origColor != SystemColors.GradientInactiveCaption
                     && origColor != Color.White && origColor.R + origColor.G + origColor.B < 700)
            {
                if (origColor.R > 150 && origColor.G < 100)
                    btn.BackColor = Color.FromArgb(198, 40, 40);
                else if (origColor.G > 80 && origColor.R < 100 && origColor.B < 100)
                    btn.BackColor = Color.FromArgb(46, 125, 50);
                else if (origColor.B > 120 && origColor.R < 50)
                    btn.BackColor = Color.FromArgb(21, 101, 192);
                else if (origColor.R > 100 && origColor.G > 50 && origColor.B < 50) // Orange
                    btn.BackColor = Color.FromArgb(230, 81, 0);
                else
                    btn.BackColor = Color.FromArgb(79, 93, 117);
            }
            else
            {
                btn.BackColor = Color.FromArgb(79, 93, 117); // Slate Blue mặc định
            }

            // Tự động điều chỉnh kích cỡ font tương thích kích thước nút để tránh tràn chữ
            float fontSize;
            if (btn.Width < 65 || btn.Height < 25)
                fontSize = 8f;   // Nút rất nhỏ (icon-style)
            else if (btn.Width < 85 || btn.Height < 30)
                fontSize = 8.5f; // Nút nhỏ (Width~68, Height~27 như frmGiangVien)
            else if (btn.Width < 110 || btn.Height < 35)
                fontSize = 9.25f; // Nút nhỏ vừa
            else if (btn.Width < 130 || btn.Height < 38)
                fontSize = 9.75f; // Nút vừa
            else
                fontSize = 10.5f; // Nút đủ lớn

            btn.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);

            // Gán sự kiện hover an toàn
            btn.MouseEnter -= Button_MouseEnter;
            btn.MouseEnter += Button_MouseEnter;
            btn.MouseLeave -= Button_MouseLeave;
            btn.MouseLeave += Button_MouseLeave;
        }

        private static void ApplyGunaButtonTheme(Guna2Button btn)
        {
            // Kiểm tra xem đây có phải nút sidebar không (FillColor = Transparent)
            bool isSidebarNav = btn.FillColor == Color.Transparent ||
                                (btn.Parent != null && IsInSidebar(btn.Parent));
            if (isSidebarNav)
            {
                // Nút sidebar: không thay đổi màu, chỉ đảm bảo font đẹp
                btn.ForeColor = Color.White;
                return;
            }

            btn.BorderRadius = 5;
            btn.ForeColor = Color.White;

            // Phân loại màu theo text của nút (ưu tiên cao nhất)
            string text = btn.Text != null ? btn.Text.ToLower().Trim() : "";
            Color origColor = btn.FillColor;

            if (text.Contains("xóa") || text.Contains("xoa") || text.Contains("hủy") || text.Contains("huy") || text.Contains("từ chối"))
            {
                btn.FillColor = Color.FromArgb(198, 40, 40);
                btn.HoverState.FillColor = Color.FromArgb(170, 30, 30);
            }
            else if (text.Contains("lưu") || text.Contains("luu") || text.Contains("duyệt") || text.Contains("làm mới"))
            {
                btn.FillColor = Color.FromArgb(46, 125, 50);
                btn.HoverState.FillColor = Color.FromArgb(35, 100, 40);
            }
            else if (text.Contains("thêm") || text.Contains("them") || text.Contains("tạo"))
            {
                btn.FillColor = Color.FromArgb(21, 101, 192);
                btn.HoverState.FillColor = Color.FromArgb(15, 80, 160);
            }
            else if (origColor.A > 0 && origColor != SystemColors.Control && origColor != Color.Transparent
                     && origColor != Color.FromArgb(94, 148, 255))
            {
                if (origColor.R > 150 && origColor.G < 100)
                {
                    btn.FillColor = Color.FromArgb(198, 40, 40);
                    btn.HoverState.FillColor = Color.FromArgb(170, 30, 30);
                }
                else if (origColor.G > 120 && origColor.R < 100)
                {
                    btn.FillColor = Color.FromArgb(46, 125, 50);
                    btn.HoverState.FillColor = Color.FromArgb(35, 100, 40);
                }
                else if (origColor.B > 150 && origColor.R < 100)
                {
                    btn.FillColor = Color.FromArgb(21, 101, 192);
                    btn.HoverState.FillColor = Color.FromArgb(15, 80, 160);
                }
                else
                {
                    btn.FillColor = Color.FromArgb(79, 93, 117);
                    btn.HoverState.FillColor = Color.FromArgb(62, 78, 104);
                }
            }
            else
            {
                btn.FillColor = Color.FromArgb(79, 93, 117);
                btn.HoverState.FillColor = Color.FromArgb(62, 78, 104);
            }

            // Tự động điều chỉnh font theo kích thước nút
            float fontSize;
            if (btn.Width < 65 || btn.Height < 25)
                fontSize = 8f;
            else if (btn.Width < 85 || btn.Height < 30)
                fontSize = 8.5f;
            else if (btn.Width < 110 || btn.Height < 35)
                fontSize = 9.25f;
            else if (btn.Width < 130 || btn.Height < 38)
                fontSize = 9.75f;
            else
                fontSize = 10.5f;

            btn.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);

            btn.DisabledState.FillColor = Color.FromArgb(189, 189, 189);
            btn.DisabledState.ForeColor = Color.FromArgb(100, 100, 100);
        }

        private static void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                Color current = btn.BackColor;
                btn.BackColor = Color.FromArgb(
                    Math.Max(0, current.R - 20),
                    Math.Max(0, current.G - 20),
                    Math.Max(0, current.B - 20)
                );
            }
        }

        private static void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string text = btn.Text != null ? btn.Text.ToLower() : "";
                if (text.Contains("xóa") || text.Contains("xoa") || text.Contains("hủy") || text.Contains("huy") || text.Contains("từ chối"))
                    btn.BackColor = Color.FromArgb(198, 40, 40);
                else if (text.Contains("lưu") || text.Contains("luu") || text.Contains("duyệt") || text.Contains("làm mới"))
                    btn.BackColor = Color.FromArgb(46, 125, 50);
                else if (text.Contains("thêm") || text.Contains("them") || text.Contains("tạo"))
                    btn.BackColor = Color.FromArgb(21, 101, 192);
                else
                    btn.BackColor = Color.FromArgb(79, 93, 117);
            }
        }

        private static void ConfigureDataGridView(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.GridColor = Color.FromArgb(231, 229, 255);
            grid.RowTemplate.Height = 35;
            grid.EnableHeadersVisualStyles = false;
            grid.BorderStyle = BorderStyle.None;

            grid.ColumnHeadersHeight = 40;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(100, 88, 255); // Indigo #6458FF
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 88, 255);
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 216, 242); // Sky Blue #AED8F2
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            if (grid is Guna2DataGridView gunaGrid)
            {
                gunaGrid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
                gunaGrid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
                gunaGrid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                gunaGrid.ThemeStyle.RowsStyle.BackColor = Color.White;
                gunaGrid.ThemeStyle.RowsStyle.ForeColor = Color.Black;
                gunaGrid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(174, 216, 242);
                gunaGrid.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
                gunaGrid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                gunaGrid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
                gunaGrid.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Black;
                gunaGrid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(174, 216, 242);
                gunaGrid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Black;
                gunaGrid.ThemeStyle.AlternatingRowsStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            }
        }

        private static bool IsInsideTableLayout(Label lbl)
        {
            // Kiểm tra xem label có nằm trong TableLayoutPanel / ShadowPanel (thường là stats card) không
            Control p = lbl.Parent;
            int depth = 0;
            while (p != null && depth < 5)
            {
                string pn = p.Name.ToLower();
                if (p is TableLayoutPanel || pn.Contains("shadow") || pn.Contains("tablelay"))
                    return true;
                p = p.Parent;
                depth++;
            }
            return false;
        }

        private static void ApplyLabelStyle(Label lbl)
        {
            string name = lbl.Name.ToLower();
            string text = lbl.Text != null ? lbl.Text.Trim() : "";
            Font origFont = lbl.Font;
            Size origSize = lbl.Size;
            bool origAutoSize = lbl.AutoSize;

            // ===== NHẬN DIỆN NHÃN STATS CARD =====
            // Nhận diện qua: tên (val/stat), parent (card/stat), hoặc nằm trong TableLayoutPanel
            bool isStatControl = name.Contains("val") || name.Contains("stat") ||
                                 (lbl.Parent != null &&
                                  (lbl.Parent.Name.ToLower().Contains("card") ||
                                   lbl.Parent.Name.ToLower().Contains("stat") ||
                                   lbl.Parent is TableLayoutPanel ||
                                   (lbl.Parent.Parent != null &&
                                    (lbl.Parent.Parent.Name.ToLower().Contains("card") ||
                                     lbl.Parent.Parent.Name.ToLower().Contains("stat") ||
                                     lbl.Parent.Parent is TableLayoutPanel))));

            // Bổ sung: nếu label nằm trong ShadowPanel (Guna2ShadowPanel), cũng là stat card
            if (!isStatControl && IsInsideTableLayout(lbl))
                isStatControl = true;

            if (isStatControl)
            {
                // Giữ nguyên kích thước & autosize ban đầu cho Stats Card
                bool wasAutoSize = lbl.AutoSize;
                lbl.AutoSize = false;
                lbl.Font = new Font("Segoe UI", origFont.Size, origFont.Style);
                lbl.Size = origSize;
                if (wasAutoSize) lbl.AutoSize = true;

                // Giữ nguyên màu designer nếu không phải màu đen/hệ thống
                if (lbl.ForeColor == Color.Black || lbl.ForeColor == SystemColors.ControlText)
                    lbl.ForeColor = Color.FromArgb(31, 41, 55);
                // else: giữ màu designer (navy, xanh, đỏ...)
                return;
            }

            // ===== NHẬN DIỆN TIÊU ĐỀ FORM CHÍNH =====
            // Chỉ nhận diện là title nếu: tên chứa "title"/"header" HOẶC text ALL CAPS
            // VÀ không nằm trong TableLayout/ShadowPanel (để tránh nhận diện sai stats card)
            bool isTitle = (name.Contains("title") || name.Contains("header")) &&
                           !name.Contains("detail") && !name.Contains("group") &&
                           !name.Contains("khung") && !name.Contains("card");

            // Nhận diện qua text in hoa CHỈ KHI font size gốc đủ lớn (>=11pt) - stats card label thường nhỏ hơn
            if (!isTitle && text.Length > 8 && text == text.ToUpper() && !text.Contains(":")
                && origFont.Size >= 11f)
                isTitle = true;

            // ===== NHẬN DIỆN TIÊU ĐỀ PHỤ =====
            bool isSubtitle = name.Contains("sub") || name.Contains("desc") || name.Contains("mota");
            if (!isSubtitle && text.Length > 20 && !text.Contains(":") &&
                (text.StartsWith("Quản lý ") || text.StartsWith("Quản lí ") ||
                 text.StartsWith("Danh sách ") || text.StartsWith("Trang tổng quan ") ||
                 text.StartsWith("Báo cáo ") || text.StartsWith("Hệ thống ") ||
                 text.StartsWith("Thông tin ")))
                isSubtitle = true;

            // ===== ÁP DỤNG STYLE =====
            if (isTitle)
            {
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", Math.Max(14f, origFont.Size), FontStyle.Bold);
                lbl.ForeColor = Color.FromArgb(31, 41, 55); // #1F2937
            }
            else if (isSubtitle)
            {
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", Math.Max(9.5f, origFont.Size), FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(75, 85, 99); // #4B5563
            }
            else
            {
                // Nhãn trường thông tin thông thường (Field Labels)
                lbl.AutoSize = false;

                float safeSize = Math.Min(origFont.Size, 10.5f);
                lbl.Font = new Font("Segoe UI", safeSize, origFont.Style);

                // Giữ màu designer nếu không phải màu mặc định (ví dụ navy 0,0,64 của frmBaoCaoThongKe)
                if (lbl.ForeColor == Color.Black || lbl.ForeColor == SystemColors.ControlText ||
                    lbl.ForeColor == SystemColors.WindowText)
                    lbl.ForeColor = Color.Black;
                // else: giữ nguyên ForeColor designer

                lbl.Size = origSize;
                if (origAutoSize)
                    lbl.AutoSize = true;
            }
        }

        private static void ApplyHtmlLabelStyle(Guna2HtmlLabel lbl)
        {
            string name = lbl.Name.ToLower();
            string text = lbl.Text != null ? StripHtml(lbl.Text) : "";
            Font origFont = lbl.Font;

            // ===== CRITICAL: Lưu kích thước và AutoSize gốc TRƯỚC KHI thay đổi bất cứ thứ gì =====
            // Guna2HtmlLabel có lỗi: khi thay Font, nó tự kích hoạt HTML parser và collapse width
            Size origSize = lbl.Size;
            bool origAutoSize = lbl.AutoSize;

            // Tạm thời disable autosize trước khi thay font
            lbl.AutoSize = false;

            // ===== NHẬN DIỆN NHÃN STATS CARD =====
            bool isStatControl = name.Contains("val") || name.Contains("stat") ||
                                 (lbl.Parent != null &&
                                  (lbl.Parent.Name.ToLower().Contains("card") ||
                                   lbl.Parent.Name.ToLower().Contains("stat") ||
                                   (lbl.Parent.Parent != null &&
                                    (lbl.Parent.Parent.Name.ToLower().Contains("card") ||
                                     lbl.Parent.Parent.Name.ToLower().Contains("stat")))));

            if (isStatControl)
            {
                lbl.Font = new Font("Segoe UI", origFont.Size, origFont.Style);
                lbl.Size = origSize; // Phục hồi kích thước

                if (name.Contains("val") || name.Contains("value") ||
                    (text.Length > 0 && char.IsDigit(text[0])))
                {
                    if (lbl.ForeColor != Color.Black && lbl.ForeColor != SystemColors.ControlText && lbl.ForeColor.A > 0)
                    { /* Giữ màu */ }
                    else
                        lbl.ForeColor = Color.FromArgb(31, 41, 55);
                }
                else
                {
                    lbl.ForeColor = Color.FromArgb(100, 110, 120);
                }

                // Khôi phục AutoSize sau cùng
                if (origAutoSize) lbl.AutoSize = true;
                return;
            }

            // ===== NHẬN DIỆN TIÊU ĐỀ FORM CHÍNH =====
            bool isTitle = (name.Contains("title") || name.Contains("header")) &&
                           !name.Contains("detail") && !name.Contains("group") &&
                           !name.Contains("khung") && !name.Contains("card");
            if (!isTitle && text.Length > 5 && text == text.ToUpper() && !text.Contains(":"))
                isTitle = true;

            // ===== NHẬN DIỆN TIÊU ĐỀ PHỤ =====
            bool isSubtitle = name.Contains("sub") || name.Contains("desc") || name.Contains("mota");
            if (!isSubtitle && text.Length > 20 && !text.Contains(":") &&
                (text.StartsWith("Quản lý ") || text.StartsWith("Quản lí ") ||
                 text.StartsWith("Danh sách ") || text.StartsWith("Trang tổng quan ") ||
                 text.StartsWith("Báo cáo ") || text.StartsWith("Hệ thống ") ||
                 text.StartsWith("Thông tin ")))
                isSubtitle = true;

            // ===== ÁP DỤNG STYLE =====
            if (isTitle)
            {
                lbl.Font = new Font("Segoe UI", Math.Max(16f, origFont.Size), FontStyle.Bold);
                lbl.ForeColor = Color.FromArgb(31, 41, 55);
                // Title: khôi phục autosize để text render đúng
                lbl.Size = origSize;
                if (origAutoSize) lbl.AutoSize = true;
            }
            else if (isSubtitle)
            {
                lbl.Font = new Font("Segoe UI", Math.Max(9.5f, origFont.Size), FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(75, 85, 99);
                lbl.Size = origSize;
                if (origAutoSize) lbl.AutoSize = true;
            }
            else
            {
                // Field Labels và các nhãn thông thường khác
                // CRITICAL: Dùng font size nhỏ an toàn, LUÔN khôi phục kích thước gốc
                float safeSize = Math.Min(origFont.Size, 10.5f);
                lbl.Font = new Font("Segoe UI", safeSize, origFont.Style);
                lbl.ForeColor = Color.Black;

                // Luôn khôi phục kích thước gốc sau khi thay font để tránh collapse
                lbl.Size = origSize;

                // Khôi phục AutoSize sau cùng (phải làm SAU khi set Size)
                if (origAutoSize) lbl.AutoSize = true;
            }
        }
    }
}
