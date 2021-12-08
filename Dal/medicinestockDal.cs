using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class medicinestockDal
    {

        static MedicineProjectEntities db = new MedicineProjectEntities();

        //שליפה
        public static List<MEDICINESTOCKtbl> Getall()
        {
            return db.MEDICINESTOCKtbl.ToList();
        }
        //הוספה
        public static void AddMedicineStock(MEDICINESTOCKtbl m)
        {
            db.MEDICINESTOCKtbl.Add(m);
            db.SaveChanges();
        }
        //מחיקה
        public static void delete(int id)
        {
            db.MEDICINESTOCKtbl.Remove(db.MEDICINESTOCKtbl.FirstOrDefault(k => k.ID == id));
            db.SaveChanges();
        }
        //עידכון
        public static void update(MEDICINESTOCKtbl m)
        {
            db.MEDICINESTOCKtbl.FirstOrDefault(x => x.ID == m.ID).IDMEDICINE = m.IDMEDICINE;
            db.MEDICINESTOCKtbl.FirstOrDefault(x => x.ID == m.ID).INSERTDATE = m.INSERTDATE;
            db.MEDICINESTOCKtbl.FirstOrDefault(x => x.ID == m.ID).EXPIRYDATE = m.EXPIRYDATE;
            db.SaveChanges();
        }
    }
}
