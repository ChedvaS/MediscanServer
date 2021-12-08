using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class useresEntities
    {
        public string gmail { get; set; }
        public string fname { get; set; }
        public string pass { get; set; }


        //המרה מאוביקט מסוג מסד נתונים לאובייקט מסוג אנטיטיז
        public static useresEntities convertToEntities(USERStbl u)
        {
            return new useresEntities()
            {
                gmail = u.GMAIL,
                fname = u.FNAME,
                pass = u.PASS,
               

            };
        }
        //המרה מסוג אנטיטיז לסוג מסד נתונים
        public static USERStbl ConvertToDb(useresEntities u)
        {
            return new USERStbl()
            {
                GMAIL = u.gmail,
                FNAME = u.fname,
                PASS = u.pass,

            };
        }
        //המרה מסוג קשימת מסד נתונים לסוג רשמית אנטיטיז
        public static List<useresEntities> ConvertToListEntities(List<USERStbl> lu)
        {
            List<useresEntities> lu1 = new List<useresEntities>();
            foreach (var item in lu)
            {
                lu1.Add(convertToEntities(item));
            }
            return lu1;
        }
        //המרה מסוג רשימת אנטיטיז לרשימת מסד נתוניפ
        public static List<USERStbl> ConvertToListDb(List<useresEntities> lu)
        {
            List<USERStbl> lu1 = new List<USERStbl>();
            foreach (var item in lu)
            {
                lu1.Add(ConvertToDb(item));
            }
            return lu1;
        }
    }
}
