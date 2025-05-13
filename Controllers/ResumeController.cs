using HireCraft.Backend.Data;
using HireCraft.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System.Threading.Tasks;

namespace HireCraft.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ResumeController(ApplicationDbContext db) => _db = db;

        // GET api/resume
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.Resumes
                .Include(r => r.Educations)
                .Include(r => r.Experiences)
                .Include(r => r.Skills)
                .ToListAsync());

        // GET api/resume/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var resume = await _db.Resumes
                .Include(r => r.Educations)
                .Include(r => r.Experiences)
                .Include(r => r.Skills)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resume == null) return NotFound();
            return Ok(resume);
        }

        // POST api/resume
        [HttpPost]
        public async Task<IActionResult> Create(Resume resume)
        {
            resume.Id = Guid.NewGuid();
            _db.Resumes.Add(resume);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = resume.Id }, resume);
        }

        // PUT api/resume/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Resume updated)
        {
            if (id != updated.Id) return BadRequest();
            _db.Entry(updated).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/resume/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resume = await _db.Resumes.FindAsync(id);
            if (resume == null) return NotFound();
            _db.Resumes.Remove(resume);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // GET api/resume/{id}/pdf
        [HttpGet("{id:guid}/pdf")]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var resume = await _db.Resumes
                .Include(r => r.Educations)
                .Include(r => r.Experiences)
                .Include(r => r.Skills)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (resume == null) return NotFound();

            // Generate PDF
            using var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 20);

            gfx.DrawString(resume.FullName, font, XBrushes.Black, new XPoint(40, 50));

            // ...you’d add more text for summary, education, etc.

            using var ms = new MemoryStream();
            doc.Save(ms);
            ms.Position = 0;
            return File(ms.ToArray(), "application/pdf", $"{resume.FullName}.pdf");
        }
    }
}
