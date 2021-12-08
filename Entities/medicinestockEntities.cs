using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class medicinestockEntities
    {
        public short id { get; set; }
        public Nullable<short> idMedicne { get; set; }
        public Nullable<System.DateTime> insertDate { get; set; }
        public Nullable<System.DateTime> expiryDate { get; set; }



        //המרה מאוביקט מסוג מסד נתונים לאובייקט מסוג אנטיטיז
        public static medicinestockEntities convertToEntities(MEDICINESTOCKtbl m)
        {
            return new medicinestockEntities()
            {
                id = m.ID,
                idMedicne = m.IDMEDICINE,
                insertDate = m.INSERTDATE,
                expiryDate=m.EXPIRYDATE

            };
        }
        //המרה מסוג אנטיטיז לסוג מסד נתונים
        public static MEDICINESTOCKtbl ConvertToDb(medicinestockEntities m)
        {
            return new MEDICINESTOCKtbl()
            {
                ID = m.id,
                IDMEDICINE = m.idMedicne,
                INSERTDATE = m.insertDate,
                EXPIRYDATE=m.expiryDate
            };
        }
        //המרה מסוג קשימת מסד נתונים לסוג רשמית אנטיטיז
        public static List<medicinestockEntities> ConvertToListEntities(List<MEDICINESTOCKtbl> lm)
        {
            List<medicinestockEntities> lm1 = new List<medicinestockEntities>();
            foreach (var item in lm)
            {
                lm1.Add(convertToEntities(item));
            }
            return lm1;
        }
        //המרה מסוג רשימת אנטיטיז לרשימת מסד נתוניפ
        public static List<MEDICINESTOCKtbl> ConvertToListDb(List<medicinestockEntities> lm)
        {
            List<MEDICINESTOCKtbl> lm1 = new List<MEDICINESTOCKtbl>();
            foreach (var item in lm)
            {
                lm1.Add(ConvertToDb(item));
            }
            return lm1;
        }
    }
}
