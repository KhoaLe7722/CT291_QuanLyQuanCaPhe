namespace QuanLyQuanCaPhe
{
    partial class fQuanLyMon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lbTim = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvMon = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTrangThaiNhap = new System.Windows.Forms.Label();
            this.lblTenAnhMonQL = new System.Windows.Forms.Label();
            this.picAnhMonQL = new System.Windows.Forms.PictureBox();
            this.cboLoaiMon = new System.Windows.Forms.ComboBox();
            this.lbGia = new System.Windows.Forms.Label();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.lbLoai = new System.Windows.Forms.Label();
            this.lbTenMon = new System.Windows.Forms.Label();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.lbMaMon = new System.Windows.Forms.Label();
            this.txtMaMon = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnLuuMoi = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhMonQL)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1 (Thanh công cụ phía trên: Nút Thêm món -> Tìm món -> Hoàn tác Ctrl+Z)
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(238, 240, 245);
            this.panel1.Controls.Add(this.btnUndo);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.lbTim);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1130, 62);
            this.panel1.TabIndex = 0;
            // 
            // button1 (Nút Thêm món mới nằm bên trái Tìm món)
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "➕ Thêm món mới";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // lbTim
            // 
            this.lbTim.AutoSize = true;
            this.lbTim.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lbTim.Location = new System.Drawing.Point(210, 22);
            this.lbTim.Name = "lbTim";
            this.lbTim.Size = new System.Drawing.Size(92, 18);
            this.lbTim.TabIndex = 1;
            this.lbTim.Text = "🔍 Tìm món:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Arial", 12F);
            this.txtTimKiem.Location = new System.Drawing.Point(310, 18);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(360, 26);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnUndo (Nút Hoàn tác Ctrl + Z)
            // 
            this.btnUndo.BackColor = System.Drawing.Color.FromArgb(243, 156, 18);
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnUndo.ForeColor = System.Drawing.Color.White;
            this.btnUndo.Location = new System.Drawing.Point(895, 10);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(220, 42);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "↩️ Hoàn tác (Ctrl+Z)";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvMon);
            this.panel2.Location = new System.Drawing.Point(10, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 595);
            this.panel2.TabIndex = 1;
            // 
            // dgvMon
            // 
            this.dgvMon.AllowUserToAddRows = false;
            this.dgvMon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMon.Font = new System.Drawing.Font("Arial", 11F);
            this.dgvMon.Location = new System.Drawing.Point(0, 0);
            this.dgvMon.Name = "dgvMon";
            this.dgvMon.ReadOnly = true;
            this.dgvMon.RowHeadersVisible = false;
            this.dgvMon.RowTemplate.Height = 32;
            this.dgvMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMon.Size = new System.Drawing.Size(500, 595);
            this.dgvMon.TabIndex = 0;
            this.dgvMon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMon_CellClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblTrangThaiNhap);
            this.panel3.Controls.Add(this.lblTenAnhMonQL);
            this.panel3.Controls.Add(this.picAnhMonQL);
            this.panel3.Controls.Add(this.cboLoaiMon);
            this.panel3.Controls.Add(this.lbGia);
            this.panel3.Controls.Add(this.txtGia);
            this.panel3.Controls.Add(this.lbLoai);
            this.panel3.Controls.Add(this.lbTenMon);
            this.panel3.Controls.Add(this.txtTenMon);
            this.panel3.Controls.Add(this.lbMaMon);
            this.panel3.Controls.Add(this.txtMaMon);
            this.panel3.Location = new System.Drawing.Point(525, 82);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(615, 480);
            this.panel3.TabIndex = 2;
            // 
            // lblTrangThaiNhap
            // 
            this.lblTrangThaiNhap.BackColor = System.Drawing.Color.FromArgb(235, 245, 255);
            this.lblTrangThaiNhap.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiNhap.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblTrangThaiNhap.Location = new System.Drawing.Point(15, 12);
            this.lblTrangThaiNhap.Name = "lblTrangThaiNhap";
            this.lblTrangThaiNhap.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblTrangThaiNhap.Size = new System.Drawing.Size(580, 36);
            this.lblTrangThaiNhap.TabIndex = 11;
            this.lblTrangThaiNhap.Text = "📝 CHẾ ĐỘ: XEM / SỬA MÓN ĐANG CHỌN";
            this.lblTrangThaiNhap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTenAnhMonQL
            // 
            this.lblTenAnhMonQL.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.lblTenAnhMonQL.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTenAnhMonQL.Location = new System.Drawing.Point(355, 345);
            this.lblTenAnhMonQL.Name = "lblTenAnhMonQL";
            this.lblTenAnhMonQL.Size = new System.Drawing.Size(240, 45);
            this.lblTenAnhMonQL.TabIndex = 10;
            this.lblTenAnhMonQL.Text = "Hình ảnh món";
            this.lblTenAnhMonQL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAnhMonQL
            // 
            this.picAnhMonQL.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.picAnhMonQL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnhMonQL.Location = new System.Drawing.Point(355, 70);
            this.picAnhMonQL.Name = "picAnhMonQL";
            this.picAnhMonQL.Size = new System.Drawing.Size(240, 260);
            this.picAnhMonQL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnhMonQL.TabIndex = 9;
            this.picAnhMonQL.TabStop = false;
            // 
            // lbMaMon
            // 
            this.lbMaMon.AutoSize = true;
            this.lbMaMon.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lbMaMon.Location = new System.Drawing.Point(25, 80);
            this.lbMaMon.Name = "lbMaMon";
            this.lbMaMon.Size = new System.Drawing.Size(75, 18);
            this.lbMaMon.TabIndex = 1;
            this.lbMaMon.Text = "Mã món:";
            // 
            // txtMaMon
            // 
            this.txtMaMon.Font = new System.Drawing.Font("Arial", 12F);
            this.txtMaMon.Location = new System.Drawing.Point(115, 76);
            this.txtMaMon.Name = "txtMaMon";
            this.txtMaMon.ReadOnly = true;
            this.txtMaMon.Size = new System.Drawing.Size(215, 26);
            this.txtMaMon.TabIndex = 0;
            // 
            // lbTenMon
            // 
            this.lbTenMon.AutoSize = true;
            this.lbTenMon.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lbTenMon.Location = new System.Drawing.Point(25, 160);
            this.lbTenMon.Name = "lbTenMon";
            this.lbTenMon.Size = new System.Drawing.Size(80, 18);
            this.lbTenMon.TabIndex = 3;
            this.lbTenMon.Text = "Tên món:";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Font = new System.Drawing.Font("Arial", 12F);
            this.txtTenMon.Location = new System.Drawing.Point(115, 156);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(215, 26);
            this.txtTenMon.TabIndex = 2;
            // 
            // lbLoai
            // 
            this.lbLoai.AutoSize = true;
            this.lbLoai.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lbLoai.Location = new System.Drawing.Point(25, 240);
            this.lbLoai.Name = "lbLoai";
            this.lbLoai.Size = new System.Drawing.Size(78, 18);
            this.lbLoai.TabIndex = 5;
            this.lbLoai.Text = "Loại món:";
            // 
            // cboLoaiMon
            // 
            this.cboLoaiMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiMon.Font = new System.Drawing.Font("Arial", 12F);
            this.cboLoaiMon.FormattingEnabled = true;
            this.cboLoaiMon.Location = new System.Drawing.Point(115, 236);
            this.cboLoaiMon.Name = "cboLoaiMon";
            this.cboLoaiMon.Size = new System.Drawing.Size(215, 26);
            this.cboLoaiMon.TabIndex = 8;
            // 
            // lbGia
            // 
            this.lbGia.AutoSize = true;
            this.lbGia.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lbGia.Location = new System.Drawing.Point(25, 320);
            this.lbGia.Name = "lbGia";
            this.lbGia.Size = new System.Drawing.Size(75, 18);
            this.lbGia.TabIndex = 7;
            this.lbGia.Text = "Giá bán:";
            // 
            // txtGia
            // 
            this.txtGia.Font = new System.Drawing.Font("Arial", 12F);
            this.txtGia.Location = new System.Drawing.Point(115, 316);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(215, 26);
            this.txtGia.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(238, 240, 245);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.btnLuuMoi);
            this.panel4.Location = new System.Drawing.Point(525, 577);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(615, 100);
            this.panel4.TabIndex = 3;
            // 
            // btnLuuMoi (Lưu món mới khi đang ở chế độ Thêm mới)
            // 
            this.btnLuuMoi.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnLuuMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuMoi.FlatAppearance.BorderSize = 0;
            this.btnLuuMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuMoi.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuMoi.ForeColor = System.Drawing.Color.White;
            this.btnLuuMoi.Location = new System.Drawing.Point(20, 25);
            this.btnLuuMoi.Name = "btnLuuMoi";
            this.btnLuuMoi.Size = new System.Drawing.Size(145, 48);
            this.btnLuuMoi.TabIndex = 0;
            this.btnLuuMoi.Text = "💾 Lưu món mới";
            this.btnLuuMoi.UseVisualStyleBackColor = false;
            this.btnLuuMoi.Click += new System.EventHandler(this.btnLuuMoi_Click);
            // 
            // button2 (Sửa món đang chọn)
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(180, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "✏️ Lưu sửa";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // button3 (Xóa món)
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(325, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 48);
            this.button3.TabIndex = 2;
            this.button3.Text = "🗑️ Xóa";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // button4 (Làm mới)
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(465, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 48);
            this.button4.TabIndex = 3;
            this.button4.Text = "🔄 Làm Mới";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // fQuanLyMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 690);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "fQuanLyMon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Món (Hỗ trợ Ctrl + Z hoàn tác)";
            this.Load += new System.EventHandler(this.fQuanLyMon_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhMonQL)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lbTim;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvMon;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtMaMon;
        private System.Windows.Forms.Label lbGia;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label lbLoai;
        private System.Windows.Forms.Label lbTenMon;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label lbMaMon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cboLoaiMon;
        private System.Windows.Forms.PictureBox picAnhMonQL;
        private System.Windows.Forms.Label lblTenAnhMonQL;
        private System.Windows.Forms.Label lblTrangThaiNhap;
        private System.Windows.Forms.Button btnLuuMoi;
        private System.Windows.Forms.Button btnUndo;
    }
}
