using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class medicineDal
    {
        static MedicineProjectEntities db = new MedicineProjectEntities();

        //שליפה
        public static List<MEDICINEtbl> Getall()
        {
            return db.MEDICINEtbl.ToList();
        }
        //הוספה
        public static void AddMedicine(MEDICINEtbl m)
        {
            db.MEDICINEtbl.Add(m);
            db.SaveChanges();
        }
        //מחיקה
        public static void delete(int id)
        {
            db.MEDICINEtbl.Remove(db.MEDICINEtbl.FirstOrDefault(k => k.ID == id));
            db.SaveChanges();
        }
        //עידכון
        public static void update(MEDICINEtbl m)
        {
            db.MEDICINEtbl.FirstOrDefault(x => x.ID == m.ID).NAMEMEDICINE = m.NAMEMEDICINE;
            db.MEDICINEtbl.FirstOrDefault(x => x.ID == m.ID).USERNAME = m.USERNAME;
            db.SaveChanges();
        }
        //שליפה לפי מייל
        public static List<ListMedicine> GetMedicineListByGmail(string gmail)
        {
            List<ListMedicine> listMedi = new List<ListMedicine>();
           var listM1 = db.MEDICINESTOCKtbl.Where(x => x.MEDICINEtbl.USERNAME == gmail).Select(x => new { namemed = x.MEDICINEtbl.NAMEMEDICINE, expirydate = x.EXPIRYDATE, insertdate = x.INSERTDATE });
            foreach (var item in listM1)
            {
                listMedi.Add(new ListMedicine { NameMedicine = item.namemed, ExpiryDate = item.expirydate, InserDate = item.insertdate });
            }
            return listMedi;
        }

    }
    public class ListMedicine
    {
        public string NameMedicine { get; set; }
        public Nullable<System.DateTime> ExpiryDate  { get; set; }
        public Nullable<System.DateTime> InserDate { get; set; }
    }

}
