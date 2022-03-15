using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class remindersEntities
    {
        public short id { get; set; }
        public Nullable<short> idDetail { get; set; }
        public Nullable<System.DateTime> dateTake { get; set; }
        public Nullable<System.DateTime> hourTake { get; set; }
        public string gmail { get; set; }


        //המרה מאוביקט מסוג מסד נתונים לאובייקט מסוג אנטיטיז
        public static remindersEntities convertToEntities(REMINDERStbl r)
        {
            return new remindersEntities()
            {
                id = r.ID,
                idDetail = r.IDDETAIL,
                dateTake = r.DATETAKE,
                hourTake = r.HOURTAKE,
                gmail = r.GMAIL
            };
        }
        //המרה מסוג אנטיטיז לסוג מסד נתונים
        public static REMINDERStbl ConvertToDb(remindersEntities r)
        {
            return new REMINDERStbl()
            {
                ID = r.id,
                IDDETAIL = r.idDetail,
                DATETAKE = r.dateTake,
                HOURTAKE = r.hourTake,
                GMAIL = r.gmail
            };
        }
        //המרה מסוג קשימת מסד נתונים לסוג רשמית אנטיטיז
        public static List<remindersEntities> ConvertToListEntities(List<REMINDERStbl> lr)
        {
            List<remindersEntities> lr1 = new List<remindersEntities>();
            foreach (var item in lr)
            {
                lr1.Add(convertToEntities(item));
            }
            return lr1;
        }
        //המרה מסוג רשימת אנטיטיז לרשימת מסד נתוניפ
        public static List<REMINDERStbl> ConvertToListDb(List<remindersEntities> lr)
        {
            List<REMINDERStbl> lr1 = new List<REMINDERStbl>();
            foreach (var item in lr)
            {
                lr1.Add(ConvertToDb(item));
            }
            return lr1;
        }
        
    }
}
