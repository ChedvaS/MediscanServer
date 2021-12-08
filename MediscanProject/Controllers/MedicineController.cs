using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MediscanProject.Controllers
{
    [RoutePrefix("api/medicine")]
    public class MedicineController : ApiController
    {

            //שליפת רשימת התרופות
            [HttpGet]
            [Route("GetMedicineList")]
            public IHttpActionResult GetMedicineList()
            {
                return Ok(medic.GetCategoryList());
            }

            //שליפת קטגוריה לפי קוד
            [HttpGet]
            [Route("GetCategoryById/{idCategory}")]
            public IHttpActionResult GetCategoryById(int idCategory)
            {
                return Ok(CategoryBl.GetCategoryById(idCategory));
            }

            //הוספה לרשימה
            [HttpPut]
            [Route("addCategory")]
            public IHttpActionResult addCategory(CategoryEntities category)
            {
                try
                {
                    CategoryBl.addCategory(category);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }
            }

            //עדכון קטגוריה ברשימה
            [HttpPost]
            [Route("Category")]
            public IHttpActionResult updateCategory(CategoryEntities category)
            {
                try
                {
                    CategoryBl.updateCategory(category);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }

            }

            //הסרת קטגוריה מהרשימה
            [HttpDelete]
            [Route("deleteCategory/{id}")]
            public IHttpActionResult deleteCategory(int id)
            {
                try
                {
                    CategoryBl.deleteCategory(id);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }
            }
    }
}
