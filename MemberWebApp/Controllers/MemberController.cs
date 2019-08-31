using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MemberWebApp.Models;

namespace MemberWebApp.Controllers
{
    public class MemberController : ApiController
    {
        private CRUDEntities db = new CRUDEntities();

        public IQueryable<Member> GetMembers()
        {
            return db.Members;
        }

        [ResponseType(typeof(Member))]
        public IHttpActionResult GetMember(int id)
        {
            Member member = db.Members.Find(id);
            if(member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutMember(int id, Member member)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != member.MemberId)
            {
                return BadRequest();
            }

            db.Entry(member).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Member))]
        public IHttpActionResult PostMember(Member member)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Members.Add(member);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = member.MemberId }, member);
        }

        [ResponseType(typeof(Member))]
        public IHttpActionResult DeleteMember(int id)
        {
            Member member = db.Members.Find(id);
            if(member == null)
            {
                return NotFound();
            }

            db.Members.Remove(member);
            db.SaveChanges();

            return Ok(member);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberExists(int id)
        {
            return db.Members.Count(e => e.MemberId == id) > 0;
        }
    }
}