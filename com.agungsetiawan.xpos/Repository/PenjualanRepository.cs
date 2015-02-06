﻿using com.agungsetiawan.xpos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.agungsetiawan.xpos.Repository
{
    public class PenjualanRepository : GenericRepository<Penjualan>
    {
        public String[] GetKodeTransaksiTerakhir()
        {
            return this.db.Penjualans.OrderByDescending(x => x.Id).Select(p => p.KodeTransaksi).ToArray();
        }
    }
}