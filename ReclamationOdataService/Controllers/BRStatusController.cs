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
using ReclamationOdataService.Models;

namespace ReclamationOdataService.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using ReclamationOdataService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Recl_BRstatus>("BRStatus");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RclBRstatussController : ODataController
    {
        private ReferenceDataEntities db = new ReferenceDataEntities();

        // GET odata/BRStatus
        [Queryable]
        public IQueryable<RclBRstatus> GetBRStatus()
        {
            return db.RclBRstatus;
        }

        [EnableQuery]
        public IQueryable<RclBRstatus> Get()
        {
            return db.RclBRstatus;
        }


        // GET odata/BRStatus(5)
        [Queryable]
        public SingleResult<RclBRstatus> Get([FromODataUri] string key)
        {
            return SingleResult.Create(db.RclBRstatus.Where(recl_brstatus => recl_brstatus.BlockID == key));
        }

        // PUT odata/BRStatus(5)
        public IHttpActionResult Put([FromODataUri] string key, RclBRstatus recl_brstatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != recl_brstatus.BlockID)
            {
                return BadRequest();
            }

            db.Entry(recl_brstatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Recl_BRstatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(recl_brstatus);
        }

        // POST odata/BRStatus
        public IHttpActionResult Post(RclBRstatus recl_brstatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RclBRstatus.Add(recl_brstatus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Recl_BRstatusExists(recl_brstatus.BlockID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(recl_brstatus);
        }

        // PATCH odata/BRStatus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<RclBRstatus> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RclBRstatus recl_brstatus = db.RclBRstatus.Find(key);
            if (recl_brstatus == null)
            {
                return NotFound();
            }

            patch.Patch(recl_brstatus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Recl_BRstatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(recl_brstatus);
        }

        // DELETE odata/BRStatus(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            RclBRstatus recl_brstatus = db.RclBRstatus.Find(key);
            if (recl_brstatus == null)
            {
                return NotFound();
            }

            db.RclBRstatus.Remove(recl_brstatus);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Recl_BRstatusExists(string key)
        {
            return db.RclBRstatus.Count(e => e.BlockID == key) > 0;
        }
    }
}
