namespace QuanLyQuanCaPhe
{
    partial class fMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýMónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bánHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblBannerSub = new System.Windows.Forms.Label();
            this.lblBannerTitle = new System.Windows.Forms.Label();
            this.panelCard1 = new System.Windows.Forms.Panel();
            this.lblSoBanTrong = new System.Windows.Forms.Label();
            this.lblSoBanPhucVu = new System.Windows.Forms.Label();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.panelCard2 = new System.Windows.Forms.Panel();
            this.lblCard2Sub = new System.Windows.Forms.Label();
            this.lblSoHoaDonHomNay = new System.Windows.Forms.Label();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.panelCard3 = new System.Windows.Forms.Panel();
            this.lblCard3Sub = new System.Windows.Forms.Label();
            this.lblDoanhThuHomNay = new System.Windows.Forms.Label();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.lblQuickTitle = new System.Windows.Forms.Label();
            this.btnQuickBanHang = new System.Windows.Forms.Button();
            this.btnQuickHoaDon = new System.Windows.Forms.Button();
            this.btnQuickQuanLyMon = new System.Windows.Forms.Button();
            this.btnQuickThongKe = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblFooterRight = new System.Windows.Forms.Label();
            this.lblFooterLeft = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelCard1.SuspendLayout();
            this.panelCard2.SuspendLayout();
            this.panelCard3.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(238, 240, 245);
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýMónToolStripMenuItem,
            this.bánHàngToolStripMenuItem,
            this.hóaĐơnToolStripMenuItem,
            this.thốngKêToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quảnLýMónToolStripMenuItem
            // 
            this.quảnLýMónToolStripMenuItem.Name = "quảnLýMónToolStripMenuItem";
            this.quảnLýMónToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.quảnLýMónToolStripMenuItem.Text = "🍰 Quản lý món";
            this.quảnLýMónToolStripMenuItem.Click += new System.EventHandler(this.quảnLýMónToolStripMenuItem_Click);
            // 
            // bánHàngToolStripMenuItem
            // 
            this.bánHàngToolStripMenuItem.Name = "bánHàngToolStripMenuItem";
            this.bánHàngToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.bánHàngToolStripMenuItem.Text = "☕ Bán hàng";
            this.bánHàngToolStripMenuItem.Click += new System.EventHandler(this.bánHàngToolStripMenuItem_Click);
            // 
            // hóaĐơnToolStripMenuItem
            // 
            this.hóaĐơnToolStripMenuItem.Name = "hóaĐơnToolStripMenuItem";
            this.hóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.hóaĐơnToolStripMenuItem.Text = "🧾 Hóa đơn";
            this.hóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.hóaĐơnToolStripMenuItem_Click);
            // 
            // thốngKêToolStripMenuItem
            // 
            this.thốngKêToolStripMenuItem.Name = "thốngKêToolStripMenuItem";
            this.thốngKêToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.thốngKêToolStripMenuItem.Text = "📊 Thống kê";
            this.thốngKêToolStripMenuItem.Click += new System.EventHandler(this.thốngKêToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.đăngXuấtToolStripMenuItem.Text = "🚪 Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(44, 30, 22);
            this.panelHeader.Controls.Add(this.lblClock);
            this.panelHeader.Controls.Add(this.lblBannerSub);
            this.panelHeader.Controls.Add(this.lblBannerTitle);
            this.panelHeader.Location = new System.Drawing.Point(0, 28);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1280, 120);
            this.panelHeader.TabIndex = 1;
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblClock.Location = new System.Drawing.Point(850, 65);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(400, 25);
            this.lblClock.TabIndex = 2;
            this.lblClock.Text = "🕒 00:00:00 - 01/01/2026";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBannerSub
            // 
            this.lblBannerSub.AutoSize = true;
            this.lblBannerSub.Font = new System.Drawing.Font("Arial", 11F);
            this.lblBannerSub.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.lblBannerSub.Location = new System.Drawing.Point(30, 68);
            this.lblBannerSub.Name = "lblBannerSub";
            this.lblBannerSub.Size = new System.Drawing.Size(300, 18);
            this.lblBannerSub.TabIndex = 1;
            this.lblBannerSub.Text = "Chào mừng bạn đến với hệ thống quản lý!";
            // 
            // lblBannerTitle
            // 
            this.lblBannerTitle.AutoSize = true;
            this.lblBannerTitle.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.lblBannerTitle.ForeColor = System.Drawing.Color.White;
            this.lblBannerTitle.Location = new System.Drawing.Point(26, 20);
            this.lblBannerTitle.Name = "lblBannerTitle";
            this.lblBannerTitle.Size = new System.Drawing.Size(600, 36);
            this.lblBannerTitle.TabIndex = 0;
            this.lblBannerTitle.Text = "☕ HỆ THỐNG QUẢN LÝ QUÁN CÀ PHÊ";
            // 
            // panelCard1
            // 
            this.panelCard1.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.panelCard1.Controls.Add(this.lblSoBanTrong);
            this.panelCard1.Controls.Add(this.lblSoBanPhucVu);
            this.panelCard1.Controls.Add(this.lblCard1Title);
            this.panelCard1.Location = new System.Drawing.Point(30, 165);
            this.panelCard1.Name = "panelCard1";
            this.panelCard1.Size = new System.Drawing.Size(370, 140);
            this.panelCard1.TabIndex = 2;
            // 
            // lblSoBanTrong
            // 
            this.lblSoBanTrong.AutoSize = true;
            this.lblSoBanTrong.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.lblSoBanTrong.ForeColor = System.Drawing.Color.FromArgb(255, 235, 235);
            this.lblSoBanTrong.Location = new System.Drawing.Point(15, 100);
            this.lblSoBanTrong.Name = "lblSoBanTrong";
            this.lblSoBanTrong.Size = new System.Drawing.Size(120, 16);
            this.lblSoBanTrong.TabIndex = 2;
            this.lblSoBanTrong.Text = "0 bàn còn trống";
            // 
            // lblSoBanPhucVu
            // 
            this.lblSoBanPhucVu.AutoSize = true;
            this.lblSoBanPhucVu.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.lblSoBanPhucVu.ForeColor = System.Drawing.Color.White;
            this.lblSoBanPhucVu.Location = new System.Drawing.Point(12, 48);
            this.lblSoBanPhucVu.Name = "lblSoBanPhucVu";
            this.lblSoBanPhucVu.Size = new System.Drawing.Size(180, 38);
            this.lblSoBanPhucVu.TabIndex = 1;
            this.lblSoBanPhucVu.Text = "0 / 10 bàn";
            // 
            // lblCard1Title
            // 
            this.lblCard1Title.AutoSize = true;
            this.lblCard1Title.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblCard1Title.ForeColor = System.Drawing.Color.White;
            this.lblCard1Title.Location = new System.Drawing.Point(15, 12);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(200, 18);
            this.lblCard1Title.TabIndex = 0;
            this.lblCard1Title.Text = "🪑 BÀN ĐANG PHỤC VỤ";
            // 
            // panelCard2
            // 
            this.panelCard2.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.panelCard2.Controls.Add(this.lblCard2Sub);
            this.panelCard2.Controls.Add(this.lblSoHoaDonHomNay);
            this.panelCard2.Controls.Add(this.lblCard2Title);
            this.panelCard2.Location = new System.Drawing.Point(455, 165);
            this.panelCard2.Name = "panelCard2";
            this.panelCard2.Size = new System.Drawing.Size(370, 140);
            this.panelCard2.TabIndex = 3;
            // 
            // lblCard2Sub
            // 
            this.lblCard2Sub.AutoSize = true;
            this.lblCard2Sub.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.lblCard2Sub.ForeColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.lblCard2Sub.Location = new System.Drawing.Point(15, 100);
            this.lblCard2Sub.Name = "lblCard2Sub";
            this.lblCard2Sub.Size = new System.Drawing.Size(160, 16);
            this.lblCard2Sub.TabIndex = 2;
            this.lblCard2Sub.Text = "Đã hoàn tất thanh toán";
            // 
            // lblSoHoaDonHomNay
            // 
            this.lblSoHoaDonHomNay.AutoSize = true;
            this.lblSoHoaDonHomNay.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.lblSoHoaDonHomNay.ForeColor = System.Drawing.Color.White;
            this.lblSoHoaDonHomNay.Location = new System.Drawing.Point(12, 48);
            this.lblSoHoaDonHomNay.Name = "lblSoHoaDonHomNay";
            this.lblSoHoaDonHomNay.Size = new System.Drawing.Size(170, 38);
            this.lblSoHoaDonHomNay.TabIndex = 1;
            this.lblSoHoaDonHomNay.Text = "0 Hóa đơn";
            // 
            // lblCard2Title
            // 
            this.lblCard2Title.AutoSize = true;
            this.lblCard2Title.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblCard2Title.ForeColor = System.Drawing.Color.White;
            this.lblCard2Title.Location = new System.Drawing.Point(15, 12);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(200, 18);
            this.lblCard2Title.TabIndex = 0;
            this.lblCard2Title.Text = "🧾 HÓA ĐƠN HÔM NAY";
            // 
            // panelCard3
            // 
            this.panelCard3.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.panelCard3.Controls.Add(this.lblCard3Sub);
            this.panelCard3.Controls.Add(this.lblDoanhThuHomNay);
            this.panelCard3.Controls.Add(this.lblCard3Title);
            this.panelCard3.Location = new System.Drawing.Point(880, 165);
            this.panelCard3.Name = "panelCard3";
            this.panelCard3.Size = new System.Drawing.Size(370, 140);
            this.panelCard3.TabIndex = 4;
            // 
            // lblCard3Sub
            // 
            this.lblCard3Sub.AutoSize = true;
            this.lblCard3Sub.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.lblCard3Sub.ForeColor = System.Drawing.Color.FromArgb(235, 255, 240);
            this.lblCard3Sub.Location = new System.Drawing.Point(15, 100);
            this.lblCard3Sub.Name = "lblCard3Sub";
            this.lblCard3Sub.Size = new System.Drawing.Size(145, 16);
            this.lblCard3Sub.TabIndex = 2;
            this.lblCard3Sub.Text = "Cập nhật trực tuyến";
            // 
            // lblDoanhThuHomNay
            // 
            this.lblDoanhThuHomNay.AutoSize = true;
            this.lblDoanhThuHomNay.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuHomNay.ForeColor = System.Drawing.Color.White;
            this.lblDoanhThuHomNay.Location = new System.Drawing.Point(12, 50);
            this.lblDoanhThuHomNay.Name = "lblDoanhThuHomNay";
            this.lblDoanhThuHomNay.Size = new System.Drawing.Size(100, 36);
            this.lblDoanhThuHomNay.TabIndex = 1;
            this.lblDoanhThuHomNay.Text = "0 VNĐ";
            // 
            // lblCard3Title
            // 
            this.lblCard3Title.AutoSize = true;
            this.lblCard3Title.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblCard3Title.ForeColor = System.Drawing.Color.White;
            this.lblCard3Title.Location = new System.Drawing.Point(15, 12);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(220, 18);
            this.lblCard3Title.TabIndex = 0;
            this.lblCard3Title.Text = "💰 DOANH THU HÔM NAY";
            // 
            // lblQuickTitle
            // 
            this.lblQuickTitle.AutoSize = true;
            this.lblQuickTitle.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            this.lblQuickTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblQuickTitle.Location = new System.Drawing.Point(28, 325);
            this.lblQuickTitle.Name = "lblQuickTitle";
            this.lblQuickTitle.Size = new System.Drawing.Size(450, 21);
            this.lblQuickTitle.TabIndex = 5;
            this.lblQuickTitle.Text = "CÁC CHỨC NĂNG CHÍNH:";
            // 
            // btnQuickBanHang
            // 
            this.btnQuickBanHang.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnQuickBanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickBanHang.FlatAppearance.BorderSize = 0;
            this.btnQuickBanHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickBanHang.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickBanHang.ForeColor = System.Drawing.Color.White;
            this.btnQuickBanHang.Location = new System.Drawing.Point(30, 360);
            this.btnQuickBanHang.Name = "btnQuickBanHang";
            this.btnQuickBanHang.Size = new System.Drawing.Size(275, 130);
            this.btnQuickBanHang.TabIndex = 6;
            this.btnQuickBanHang.Text = "☕ BÁN HÀNG\r\n\r\n(Chọn bàn & Gọi món)";
            this.btnQuickBanHang.UseVisualStyleBackColor = false;
            this.btnQuickBanHang.Click += new System.EventHandler(this.bánHàngToolStripMenuItem_Click);
            // 
            // btnQuickHoaDon
            // 
            this.btnQuickHoaDon.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnQuickHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickHoaDon.FlatAppearance.BorderSize = 0;
            this.btnQuickHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickHoaDon.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnQuickHoaDon.Location = new System.Drawing.Point(335, 360);
            this.btnQuickHoaDon.Name = "btnQuickHoaDon";
            this.btnQuickHoaDon.Size = new System.Drawing.Size(275, 130);
            this.btnQuickHoaDon.TabIndex = 7;
            this.btnQuickHoaDon.Text = "🧾 HÓA ĐƠN\r\n\r\n(Lịch sử & In phiếu)";
            this.btnQuickHoaDon.UseVisualStyleBackColor = false;
            this.btnQuickHoaDon.Click += new System.EventHandler(this.hóaĐơnToolStripMenuItem_Click);
            // 
            // btnQuickQuanLyMon
            // 
            this.btnQuickQuanLyMon.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnQuickQuanLyMon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickQuanLyMon.FlatAppearance.BorderSize = 0;
            this.btnQuickQuanLyMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickQuanLyMon.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickQuanLyMon.ForeColor = System.Drawing.Color.White;
            this.btnQuickQuanLyMon.Location = new System.Drawing.Point(640, 360);
            this.btnQuickQuanLyMon.Name = "btnQuickQuanLyMon";
            this.btnQuickQuanLyMon.Size = new System.Drawing.Size(275, 130);
            this.btnQuickQuanLyMon.TabIndex = 8;
            this.btnQuickQuanLyMon.Text = "🍰 QUẢN LÝ MÓN\r\n\r\n(Thêm / Sửa / Xóa)";
            this.btnQuickQuanLyMon.UseVisualStyleBackColor = false;
            this.btnQuickQuanLyMon.Click += new System.EventHandler(this.quảnLýMónToolStripMenuItem_Click);
            // 
            // btnQuickThongKe
            // 
            this.btnQuickThongKe.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            this.btnQuickThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickThongKe.FlatAppearance.BorderSize = 0;
            this.btnQuickThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickThongKe.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuickThongKe.ForeColor = System.Drawing.Color.White;
            this.btnQuickThongKe.Location = new System.Drawing.Point(945, 360);
            this.btnQuickThongKe.Name = "btnQuickThongKe";
            this.btnQuickThongKe.Size = new System.Drawing.Size(275, 130);
            this.btnQuickThongKe.TabIndex = 9;
            this.btnQuickThongKe.Text = "📊 THỐNG KÊ\r\n\r\n(Báo cáo doanh thu)";
            this.btnQuickThongKe.UseVisualStyleBackColor = false;
            this.btnQuickThongKe.Click += new System.EventHandler(this.thốngKêToolStripMenuItem_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(238, 240, 245);
            this.panelFooter.Controls.Add(this.lblFooterRight);
            this.panelFooter.Controls.Add(this.lblFooterLeft);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 670);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1280, 50);
            this.panelFooter.TabIndex = 10;
            // 
            // lblFooterRight
            // 
            this.lblFooterRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooterRight.AutoSize = true;
            this.lblFooterRight.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblFooterRight.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.lblFooterRight.Location = new System.Drawing.Point(850, 16);
            this.lblFooterRight.Name = "lblFooterRight";
            this.lblFooterRight.Size = new System.Drawing.Size(300, 16);
            this.lblFooterRight.TabIndex = 1;
            this.lblFooterRight.Text = "";
            // 
            // lblFooterLeft
            // 
            this.lblFooterLeft.AutoSize = true;
            this.lblFooterLeft.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFooterLeft.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblFooterLeft.Location = new System.Drawing.Point(28, 16);
            this.lblFooterLeft.Name = "lblFooterLeft";
            this.lblFooterLeft.Size = new System.Drawing.Size(450, 16);
            this.lblFooterLeft.TabIndex = 0;
            this.lblFooterLeft.Text = "Quán Cà Phê Nhóm 2 - Học phần CT291 - Lập trình ứng dụng";
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.btnQuickThongKe);
            this.Controls.Add(this.btnQuickQuanLyMon);
            this.Controls.Add(this.btnQuickHoaDon);
            this.Controls.Add(this.btnQuickBanHang);
            this.Controls.Add(this.lblQuickTitle);
            this.Controls.Add(this.panelCard3);
            this.Controls.Add(this.panelCard2);
            this.Controls.Add(this.panelCard1);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Quán Cà Phê";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelCard1.ResumeLayout(false);
            this.panelCard1.PerformLayout();
            this.panelCard2.ResumeLayout(false);
            this.panelCard2.PerformLayout();
            this.panelCard3.ResumeLayout(false);
            this.panelCard3.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýMónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bánHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblBannerTitle;
        private System.Windows.Forms.Label lblBannerSub;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Panel panelCard1;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblSoBanPhucVu;
        private System.Windows.Forms.Label lblSoBanTrong;
        private System.Windows.Forms.Panel panelCard2;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblSoHoaDonHomNay;
        private System.Windows.Forms.Label lblCard2Sub;
        private System.Windows.Forms.Panel panelCard3;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblDoanhThuHomNay;
        private System.Windows.Forms.Label lblCard3Sub;
        private System.Windows.Forms.Label lblQuickTitle;
        private System.Windows.Forms.Button btnQuickBanHang;
        private System.Windows.Forms.Button btnQuickHoaDon;
        private System.Windows.Forms.Button btnQuickQuanLyMon;
        private System.Windows.Forms.Button btnQuickThongKe;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblFooterLeft;
        private System.Windows.Forms.Label lblFooterRight;
        private System.Windows.Forms.Timer timerClock;
    }
}
