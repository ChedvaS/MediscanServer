using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Dal;

namespace Bl
{
    public class medicinestockBl
    {
        // שליפת רשימת מלאי התרופות לפי קוד
        public static medicinestockEntities Getmedicinestock(int idMedicine)
        {
            return medicinestockEntities.convertToEntities(medicinestockDal.Getall().FirstOrDefault(x => x.IDMEDICINE==idMedicine));
        }

    }
}
