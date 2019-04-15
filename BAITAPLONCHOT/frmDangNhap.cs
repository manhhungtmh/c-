using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace BAITAPLONCHOT
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
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
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            check_dangnhap(txtTenDangNhap.ToString(), txtMatKhau.ToString());
        }
            
        private void check_dangnhap(string taikhoan, string matkhau)
        {
            check();
            string tk = null, quyen = "1", manv = null;
            if (txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không được để trống !");
                return;
            }
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_checkdangnhap";
                command.Connection = conn;
                command.Parameters.Add("@tentaikhoan", txtTenDangNhap.Text);
                command.Parameters.Add("@matkhau", txtMatKhau.Text);
                SqlDataReader sdr = command.ExecuteReader();
                if (sdr.Read())
                {
                    //lấy tên đăng nhập và quyên của họ lưu vào tblss
                    string key = txtTenDangNhap.Text;
                    int a = 2;
                    //Nếu quyền == 1
                    if (sdr.GetBoolean(2))
                    {
                        a = 1;
                    }
                    //Nếu quyền == 0
                    else
                    {
                        a = 0;
                    }
                    this.Hide();
                    sdr.Close();
                    luu_ss(key, a);
                    
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng !!!");
                    sdr.Close();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void luu_ss(string ma, int quyen)
        {
            check();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_session";
            command.Connection = conn;
            command.Parameters.Add("@ma", ma);
            command.Parameters.Add("@quyen", quyen);
            int ret = command.ExecuteNonQuery();
            if (ret > 0)
            {
                frmGiaoDienHeThong frmHT = new frmGiaoDienHeThong();
                frmHT.Show();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            conn.Close();
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {

                check_dangnhap(txtTenDangNhap.ToString(), txtMatKhau.ToString());

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát chương trình không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                check_dangnhap(txtTenDangNhap.ToString(), txtMatKhau.ToString());
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {

                checkBox1.Checked = true;

            }
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (Application.OpenForms.Count == 2|| Application.OpenForms.Count == 1)
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

        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
