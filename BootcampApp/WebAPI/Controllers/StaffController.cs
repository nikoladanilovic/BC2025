
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BootcampApp.Model;
using BootcampApp.Service;
using BootcampaApp.Service.Common;
using WebAPI.RESTModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StaffModel>>> GetAll()
        {
            var staffList = await _staffService.GetAllAsync();
            List<StaffREST> staffRESTList = new List<StaffREST>();
            foreach (var staff in staffList)
            {
                staffRESTList.Add(new StaffREST(staff.Id, staff.Name, staff.Role, staff.HireDate, staff.Salary));
            }
            return Ok(staffRESTList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffModel>> GetById(Guid id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            if (staff == null)
                return NotFound();

            StaffREST staffREST = new StaffREST(staff.Id, staff.Name, staff.Role, staff.HireDate, staff.Salary);
            return Ok(staffREST);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StaffModel staff)
        {
            if (staff == null)
                return BadRequest("Staff data is required.");

            await _staffService.AddAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] StaffModel staff)
        {
            if (staff == null || staff.Id != id)
                return BadRequest("Staff ID mismatch.");

            var existing = await _staffService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _staffService.UpdateAsync(staff);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existing = await _staffService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _staffService.DeleteAsync(id);
            return NoContent();
        }
    }
}
