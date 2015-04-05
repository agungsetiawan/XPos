﻿using com.agungsetiawan.xpos.Model;
using com.agungsetiawan.xpos.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.agungsetiawan.xpos.View.VLaporan
{
    public partial class LaporanPenjualan : Form
    {
        private static LaporanPenjualan form;
        private KategoriService kategoriService;
        private BarangService barangService;
        private LaporanService laporanService;
        private LaporanPenjualan()
        {
            InitializeComponent();
            kategoriService = new KategoriService();
            barangService = new BarangService();
            laporanService = new LaporanService();

            comboBoxJenis.Items.Add("Kategori");
            comboBoxJenis.Items.Add("Barang");
            comboBoxJenis.SelectedItem = "Kategori";

            comboBoxKatOrBarang.DataSource = kategoriService.Get();
            comboBoxKatOrBarang.DisplayMember = "NamaKategori";
            comboBoxKatOrBarang.ValueMember = "Id";

            chartPenjualan.ChartAreas[0].AxisY.Interval = 1;
            chartPenjualan.ChartAreas[0].AxisX.Interval = 1;

            //chartPenjualan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chartPenjualan.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            LaporanSummary();
        }

        private void LaporanSummary()
        {

            chartPenjualan.Series.Clear();
            List<Laporan> data = laporanService.GetLaporan();

            Series series = new Series("Jumlah Transaksi Penjualan");

            foreach (var d in data)
            {
                series.Points.AddXY(d.Tanggal, d.Jumlah);
            }

            chartPenjualan.Series.Add(series);
        }

        public static LaporanPenjualan GetForm()
        {
            if(form==null || form.IsDisposed)
            {
                form = new LaporanPenjualan();
            }

            return form;
        }

        private void comboBoxJenis_SelectedValueChanged(object sender, EventArgs e)
        {
            var value = comboBoxJenis.SelectedItem.ToString();

            if(value.Equals("Barang"))
            {
                labelKatOrBarang.Text = "Barang";
                comboBoxKatOrBarang.DataSource = barangService.Get();
                comboBoxKatOrBarang.DisplayMember = "NamaBarang";
                comboBoxKatOrBarang.ValueMember = "Id";
            } else if(value.Equals("Kategori"))
            {
                labelKatOrBarang.Text = "Kategori";
                comboBoxKatOrBarang.DataSource = kategoriService.Get();
                comboBoxKatOrBarang.DisplayMember = "NamaKategori";
                comboBoxKatOrBarang.ValueMember = "Id";
            }
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            var value = comboBoxJenis.SelectedItem.ToString();

            if (value.Equals("Barang"))
            {
                
            }
            else if (value.Equals("Kategori"))
            {
                
                var idKategori = int.Parse(comboBoxKatOrBarang.SelectedValue.ToString());
                var kategori=kategoriService.Get(idKategori);
                List<Laporan> data = laporanService.GetLaporanKategori(idKategori);

                chartPenjualan.Series.Clear();
                var namaChart=string.Format("Jumlah Barang Kategori {0} Terjual",kategori.NamaKategori);
                Series series = new Series(namaChart);

                foreach (var d in data)
                {
                    series.Points.AddXY(d.Tanggal, d.Jumlah);
                }

                chartPenjualan.Series.Add(series);
                chartPenjualan.Series[namaChart]["PixelPointWidth"] = "5";
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            LaporanSummary();
        }
    }
}
