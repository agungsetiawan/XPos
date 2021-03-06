﻿using com.agungsetiawan.xpos.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.agungsetiawan.xpos.View.VTransaksiInternal
{
    public partial class ListTransaksiInternal : Form
    {
        private TransaksiInternalService transaksiInternalService;
        private static ListTransaksiInternal form;
        private ListTransaksiInternal()
        {
            InitializeComponent();
            transaksiInternalService = new TransaksiInternalService();

            dataGridViewTransaksiInternal.DataSource = transaksiInternalService.Get();
            dataGridViewTransaksiInternal.Columns[0].Visible = false;
        }

        public static ListTransaksiInternal GetForm()
        {
            if(form==null || form.IsDisposed)
            {
                form = new ListTransaksiInternal();
            }

            return form;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            TambahTransaksiInternal form = new TambahTransaksiInternal();
            form.ParentForm = this;
            form.ShowDialog();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            UbahTransaksiInternal form = new UbahTransaksiInternal();
            form.ParentForm = this;
            form.PopulateData();
            form.ShowDialog();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridViewTransaksiInternal.SelectedRows[0].Cells[0].Value.ToString());
            var transaksiInternal = transaksiInternalService.Get(id);

            DialogResult result = MessageBox.Show("Hapus data " + transaksiInternal.Keterangan + " ?", "Hapus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                transaksiInternalService.Delete(transaksiInternal);
                dataGridViewTransaksiInternal.DataSource = transaksiInternalService.Get();
            }
        }
    }
}
