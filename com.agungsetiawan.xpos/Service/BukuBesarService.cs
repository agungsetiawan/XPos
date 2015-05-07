﻿using com.agungsetiawan.xpos.Model;
using com.agungsetiawan.xpos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.agungsetiawan.xpos.Service
{
    public class BukuBesarService
    {
        private BukuBesarRepository bukuBesarRepository;
        private PenjualanRepository penjualanRepository;
        private TransaksiInternalRepository transaksiInternalRepository;

        public BukuBesarService()
        {
            bukuBesarRepository=new BukuBesarRepository();
            penjualanRepository = new PenjualanRepository();
            transaksiInternalRepository = new TransaksiInternalRepository();
        }

        public List<BukuBesar> GetByTanggal(DateTime tanggal)
        {
            var penjualans = penjualanRepository.FindByTanggal(tanggal);

            var bukuBesars = (from p in penjualans select new BukuBesar {
                                 Tanggal=p.Tanggal,
                                 Keterangan="Penjualan Pakaian",
                                 Debet=p.TotalHargaJual,
                                 Kredit=0
                            }).ToList();

            var transaksiInternals = transaksiInternalRepository.FindByTanggal(tanggal);

            BukuBesar bukuBesar;
            foreach(var t in transaksiInternals)
            {
                bukuBesar = new BukuBesar();

                bukuBesar.Tanggal = t.Tanggal;
                bukuBesar.Keterangan=t.Keterangan;

                if(t.Jenis.Equals("Debet"))
                {
                    bukuBesar.Debet = t.Jumlah;
                    bukuBesar.Kredit = 0;
                }
                else if(t.Jenis.Equals("Kredit"))
                {
                    bukuBesar.Debet = 0;
                    bukuBesar.Kredit = t.Jumlah;
                }

                bukuBesars.Add(bukuBesar);
            }

            var result = from b in bukuBesars orderby b.Tanggal select b;

            return result.ToList();
        }
    }
}
