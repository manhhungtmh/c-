using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BAITAPLONCHOT
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        string strConn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        SqlConnection conn = null;
        public void check()
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            hienthidanhsach();
        }
        private void hienthidanhsach()
        {
            check();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_nhanvien";
            command.Connection = conn;
            command.Parameters.Add("action", "selectall");
            DataTable dtb = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtb);
            foreach (DataRow row in dtb.Rows)
            {
                ListViewItem item = new ListViewItem(row["sMaNV"].ToString());
                item.SubItems.Add(row["sTenNV"].ToString());
                item.SubItems.Add(row["dNgaySinh"].ToString());
                item.SubItems.Add(row["sDiaChi"].ToString());
                item.SubItems.Add(row["sGioiTinh"].ToString());
                item.SubItems.Add(row["sSDT"].ToString());
                item.SubItems.Add(row["sChucVu"].ToString());
                item.SubItems.Add(row["fHSL"].ToString());
                item.SubItems.Add((bool)(row["bTrangThai"]) == true ? "Đang đi làm" : "Đã nghỉ");
                lvNhanVien.Items.Add(item);
            }
            cbChucVu.Items.Add("Quản lý");
            cbChucVu.Items.Add("Nhân viên");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void frmNhanVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 2)
            {
                if (MessageBox.Show("Bạn có muốn thoát không?",
                               "Thông báo",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Hide();
                    Environment.Exit(1);
                }
                else
                    e.Cancel = true;
            }
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            if (OpenAForm(frm))
            {
                frm.Show();
            }
        }

        private void báocaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKe frm = new frmThongKe();
            if (OpenAForm(frm))
            {
                frm.Show();
            }
        }

        private void thongtinnhanvienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformation frm = new frmInformation();
            if (OpenAForm(frm))
            {
                frm.Show();
            }
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            if (OpenAForm(frm))
            {
                frm.Show();
            }
        }

        private Boolean OpenAForm(Form form)
        {
            try
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {

                    Form n = Application.OpenForms[i];
                    if (n.Name == form.Name)
                    {
                        n.BringToFront();
                        return false;
                    }
                }
            }
            catch
            {
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        

        

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvNhanVien.SelectedItems[0];
                string manv = lvi.SubItems[0].Text;
                txtMaNV.Text  = lvi.SubItems[0].Text;
                txtTenNV.Text = lvi.SubItems[1].Text;
                mtbNgaySinh.Text = lvi.SubItems[2].Text;
                txtDiaChi.Text = lvi.SubItems[3].Text;
                if (lvi.SubItems[4].Text == "Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                txtSDT.Text = lvi.SubItems[5].Text;
                if (lvi.SubItems[6].Text == "Quản Lý")
                {
                    cbChucVu.SelectedIndex = 0;
                }
                else
                {
                    cbChucVu.SelectedIndex = 1;
                }
            }
            
        }

        private void rbNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_mamoinhanvien";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtMaNV.Text = reader.GetString(0);
            }
            reader.Close();
            txtDiaChi.Clear();
            txtTenNV.Clear();
            txtSDT.Clear();
            mtbNgaySinh.Clear();
            cbChucVu.SelectedIndex = 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //check();
            //SqlCommand command = new SqlCommand();
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "sp_nhanvien";
            //command.Connection = conn;
            //command.Parameters.Add("@action", "insert");
            //command.Parameters.Add("@tennv", txtTenNV.Text);
            //command.Parameters.Add("@ngaysinh", mtbNgaySinh.Text);
            //command.Parameters.Add("@diachi", txtDiaChi.Text);
            //if (rdNam.Checked)
            //{
            //    command.Parameters.Add("@gioitinh", "Nam");
            //}
            //else
            //{
            //    command.Parameters.Add("@gioitinh", "Nữ");
            //}
            //command.Parameters.Add("@sdt", txtSDT.Text);

        }
    }
}
