using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class FRM_URUN : Form
    {
        public FRM_URUN()
        {
            InitializeComponent();
        }
        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from r in db.TBLURUN
                                        select new
                                        {
                                            r.URUNID,
                                            r.URUNAD,
                                            r.MARKA,
                                            r.STOK,
                                            r.FIYAT,
                                            r.KATEGORİ,
                                            r.TBLKATEGORI.AD,
                                            r.DURUM
                                        }).ToList();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TBLURUN t = new TBLURUN();
            t.URUNAD = TxtUrunAdi.Text;
            t.MARKA = txtMarka.Text;
            t.STOK = short.Parse(txtStok.Text);
            t.KATEGORİ = int.Parse(comboBox1.SelectedValue.ToString());
            t.FIYAT = decimal.Parse(txtFiyat.Text);
            t.DURUM = true;
            db.TBLURUN.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün Sisteme Kaydedildi.");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtId.Text);
            var urun = db.TBLURUN.Find(x);
            db.TBLURUN.Remove(urun);
            db.SaveChanges();
            MessageBox.Show("Ürün Silindi.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtId.Text);
            var urun = db.TBLURUN.Find(x);
            urun.URUNAD = TxtUrunAdi.Text;
            urun.STOK = short.Parse(txtStok.Text);
            urun.MARKA = txtMarka.Text;
            db.SaveChanges();
            MessageBox.Show("Ürün Güncellendi");
        }

        private void FRM_URUN_Load(object sender, EventArgs e)
        {
            var kategoriler = (from x in db.TBLKATEGORI
                               select new
                               {
                                   x.ID,
                                   x.AD
                               }
                               ).ToList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "AD";
            comboBox1.DataSource = kategoriler;
        }
    }
}
