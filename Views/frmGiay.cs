﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeStore.Controls;

namespace ShoeStore.Views
{
    public partial class frmGiay : Form
    {
        Status status = new Status();
        Giay giay;
        string idLoaiGiay = "0";
        public frmGiay()
        {
            InitializeComponent();
        }
        public frmGiay(string idLoaiGiay)
        {
            InitializeComponent();
            this.idLoaiGiay = idLoaiGiay;
        }
        private void frmGiay_Load(object sender, EventArgs e)
        {
            giay = new Giay(this.idLoaiGiay);
            LoadCBTenGiay();
            LoadListView();
        }
        public void LoadListView()
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Items.Clear();
            lv.Columns[1].Width = 133;

            string str;
            DataTable dt = giay.Giay_tb;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi;
                lvi = lv.Items.Add((i + 1).ToString());

                str = dt.Rows[i]["tenLoaiGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["tenHangGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["mauSac"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["size"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["soLuongGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["giaBan"].ToString();
                lvi.SubItems.Add(str);
            }
        }
        private void LoadCBTenGiay()
        {
            DataTable dt = giay.DanhMuc_tb;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboboxItem item = new ComboboxItem();
                string str = dt.Rows[i]["tenLoaiGiay"].ToString(); // + " - " + dt.Rows[i]["mauSac"].ToString() + " - " + dt.Rows[i]["size"].ToString();
                item.Text = str;
                item.Value = dt.Rows[i]["idLoaiGiay"].ToString();
                cbTenGiay.Items.Add(item);
            }
            if (cbTenGiay.Items.Count > 0)
            {
                cbTenGiay.SelectedIndex = 0;
            }
        }
        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedIndices.Count > 0)
            {
                string str = lv.Items[lv.SelectedIndices[0]].SubItems[1].Text;
                cbTenGiay.SelectedIndex = cbTenGiay.FindStringExact(str);
                cbMauSac.SelectedIndex = cbMauSac.FindStringExact(lv.Items[lv.SelectedIndices[0]].SubItems[3].Text);
                cbSize.SelectedIndex = cbSize.FindStringExact(lv.Items[lv.SelectedIndices[0]].SubItems[4].Text);
                txtSoLuong.Text = lv.Items[lv.SelectedIndices[0]].SubItems[5].Text;
                txtGiaBan.Text = lv.Items[lv.SelectedIndices[0]].SubItems[6].Text;
            }
        }

        private void btnTimKiemTen_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() != "")
            {
                giay.TimKiem(txtTimKiem.Text.Trim());
                LoadListView();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string soluong = txtSoLuong.Text.Trim();
            string giaban = txtGiaBan.Text.Trim();
            if (soluong != "" || giaban != "")
            {
                if (giay.CapNhat(lv.SelectedIndices[0], soluong, giaban) == status.Success)
                {
                    MessageBox.Show("Giày đã được cập nhật thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListView();
                    txtSoLuong.Text = "";
                    txtGiaBan.Text = "";
                }
                else
                {
                    MessageBox.Show("Giày bị trùng", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string idLoaiGiay = (cbTenGiay.SelectedItem as ComboboxItem).Value.ToString();
            string mausac = cbMauSac.SelectedItem.ToString();
            string size = cbSize.SelectedItem.ToString();
            if (giay.Them(idLoaiGiay, mausac, size) == status.Success)
            {
                MessageBox.Show("Giày đã được thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListView();
            }
            else
            {
                MessageBox.Show("Giày bị trùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
