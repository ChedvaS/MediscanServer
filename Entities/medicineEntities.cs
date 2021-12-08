using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class medicineEntities
    {
        public short id { get; set; }
        public string  nameMedicine { get; set; }
        public string userName { get; set; }



        //המרה מאוביקט מסוג מסד נתונים לאובייקט מסוג אנטיטיז
        public static medicineEntities convertToEntities(MEDICINEtbl m)
        {
            return new medicineEntities()
            {
                id = m.ID,
                nameMedicine = m.NAMEMEDICINE,
                userName = m.USERNAME
            };
        }
        //המרה מסוג אנטיטיז לסוג מסד נתונים
        public static MEDICINEtbl ConvertToDb(medicineEntities m)
        {
            return new MEDICINEtbl()
            {
                ID = m.id,
                NAMEMEDICINE = m.nameMedicine,
                USERNAME=m.userName


            };
        }
        //המרה מסוג קשימת מסד נתונים לסוג רשמית אנטיטיז
        public static List<medicineEntities> ConvertToListEntities(List<MEDICINEtbl> lm)
        {
            List<medicineEntities> lm1 = new List<medicineEntities>();
            foreach (var item in lm)
            {
                lm1.Add(convertToEntities(item));
            }
            return lm1;
        }
        //המרה מסוג רשימת אנטיטיז לרשימת מסד נתוניפ
        public static List<MEDICINEtbl> ConvertToListDb(List<medicineEntities> lm)
        {
            List<MEDICINEtbl> lm1 = new List<MEDICINEtbl>();
            foreach (var item in lm)
            {
                lm1.Add(ConvertToDb(item));
            }
            return lm1;
        }

    }
}
