using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Model;
using PharmacyManagementSystem.Repository;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private IAdmin _adminRepository;
        private IDrug _drugRepository;
        private ILogger<AdminController> logger;
        private readonly string drugName;

        public AdminController(IAdmin adminRepository, IDrug _drugRepository)
        {
            _adminRepository = adminRepository;
            this._drugRepository = _drugRepository;


        }
        [HttpPost("Add Drug")]
        public IActionResult AddDrug(Drug drug)
        {
            _drugRepository.Add(drug);
            return Ok("Drug Added Successfully.");
        }
        [HttpGet("Get Drug")]
        public IActionResult DrugList()
        {
            return Ok(_drugRepository.GetDrugList());
        }

        [HttpPut("Update Drug")]
        public IActionResult UpdateDrug(string drugName, Drug drug)
        {
            var existingDrug = _drugRepository.GetDrug(drugName);
            if (existingDrug == null)
            {
                return NotFound();
            }

            existingDrug.DrugName = drug.DrugName;
            existingDrug.Description = drug.Description;
            existingDrug.ExpiryDate = drug.ExpiryDate;
            existingDrug.Quantity = drug.Quantity;
            existingDrug.Price = drug.Price;

            _drugRepository.Update(existingDrug);
            return Ok("Updated Successfully");
        }
        [HttpDelete("Delete Drug")]
        public IActionResult DeleteDrug(int id)
        {
            var drug = _drugRepository.GetDrug(drugName);
            if (drug == null)
            {
                return NotFound();
            }

            _drugRepository.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
