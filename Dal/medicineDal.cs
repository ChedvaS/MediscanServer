using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  public  class medicineDal
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

       
    }
}
