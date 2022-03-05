using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x => x.Durum == true).ToList(); //sadece durumu true olanlari getirecek
            return View(urunler);
        }
        public ActionResult UrunEkle()
        {
            List<SelectListItem> deger = (from x in c.Kategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.KategoriAd, //bizim gorecegimiz deger
                                              Value = x.KategoriID.ToString() //kulanicinin gorecegi deger
                                          }).ToList();
            ViewBag.deger = deger;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun u)
        {
            c.Uruns.Add(u);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        //burada iliskili tablolarda silme islemi tehlikeli oldugu icin silmek yerine durumunu true'dan false'a cevirdik
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger = (from x in c.Kategoris.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.KategoriAd, //bizim gorecegimiz deger
                                              Value = x.KategoriID.ToString() //kulanicinin gorecegi deger
                                          }).ToList();
            ViewBag.deger = deger;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun u)
        {
            var uruns = c.Uruns.Find(u.UrunID);
            uruns.AlisFiyat = u.AlisFiyat;
            uruns.Durum = u.Durum;
            uruns.Kategoriid = u.Kategoriid;
            uruns.Marka = u.Marka;
            uruns.SatisFiyat = u.SatisFiyat;
            uruns.Stok = u.Stok;
            uruns.UrunAd = u.UrunAd;
            uruns.UrunGorsel = u.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}