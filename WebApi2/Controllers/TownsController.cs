using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    /*
    Для класса WebApiConfig может понадобиться внесение дополнительных изменений, чтобы добавить маршрут в этот контроллер. Объедините эти инструкции в методе Register класса WebApiConfig соответствующим образом. Обратите внимание, что в URL-адресах OData учитывается регистр символов.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApi2.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Town>("Towns");
    builder.EntitySet<Student>("Students"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TownsController : ODataController
    {
        private WebApi2Context db = new WebApi2Context();

        // GET: odata/Towns
        [EnableQuery]
        public IQueryable<Town> GetTowns()
        {
            return db.Towns;
        }

        // GET: odata/Towns(5)
        [EnableQuery]
        public SingleResult<Town> GetTown([FromODataUri] int key)
        {
            return SingleResult.Create(db.Towns.Where(town => town.Id == key));
        }

        // PUT: odata/Towns(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Town> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Town town = db.Towns.Find(key);
            if (town == null)
            {
                return NotFound();
            }

            patch.Put(town);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(town);
        }

        // POST: odata/Towns
        public IHttpActionResult Post(Town town)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Towns.Add(town);
            db.SaveChanges();

            return Created(town);
        }

        // PATCH: odata/Towns(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Town> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Town town = db.Towns.Find(key);
            if (town == null)
            {
                return NotFound();
            }

            patch.Patch(town);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(town);
        }

        // DELETE: odata/Towns(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Town town = db.Towns.Find(key);
            if (town == null)
            {
                return NotFound();
            }

            db.Towns.Remove(town);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Towns(5)/Students
        [EnableQuery]
        public IQueryable<Student> GetStudents([FromODataUri] int key)
        {
            return db.Towns.Where(m => m.Id == key).SelectMany(m => m.Students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TownExists(int key)
        {
            return db.Towns.Count(e => e.Id == key) > 0;
        }
    }
}
