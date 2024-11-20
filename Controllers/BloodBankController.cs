using System.Net.Http.Headers;
using BloodBankManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodBankController : Controller
    {
        static List<BloodBank> DonorList = new List<BloodBank>
        {
            new BloodBank{Id=1,DonorName="Nithisha",Age=19,BloodType="A-",ContactInfo=9989219890,Quantity="80ml",CollectionDate=new DateTime(2024, 12, 13),ExpirationDate= new DateTime(2025, 10, 13),Status="Available"},
            new BloodBank{Id=2,DonorName="Vignesh",Age=24,BloodType="A+",ContactInfo=7569421889,Quantity="100ml",CollectionDate=new DateTime(2024-01-13),ExpirationDate=new DateTime(2026-11-13),Status="Requested"},
            new BloodBank{Id=3,DonorName="Rizwana",Age=24,BloodType="B+",ContactInfo=7569465267,Quantity="10ml",CollectionDate=new DateTime(2023-01-13),ExpirationDate=new DateTime(2026-12-13),Status="Expired"},
            new BloodBank{Id=4,DonorName="Nith",Age=19,BloodType="A-",ContactInfo=9989278478,Quantity="80ml",CollectionDate=new DateTime(2022-11-13),ExpirationDate=new DateTime(2025-11-13),Status="Available"},
            new BloodBank{Id=5,DonorName="Vig",Age=24,BloodType="B+",ContactInfo=7569657889,Quantity="200ml",CollectionDate=new DateTime(2024-01-19),ExpirationDate=new DateTime(2026-10-13),Status="Requested"},
            new BloodBank{Id=6,DonorName="Riz",Age=24,BloodType="A+",ContactInfo=7569484909,Quantity="100ml",CollectionDate=new DateTime(2023-11-17),ExpirationDate=new DateTime(2026-12-14),Status="Expired"},
        };
        // Post method 
        [HttpPost]
        public ActionResult<BloodBank> AddDonor(BloodBank b)
        {
            if (b.ContactInfo.ToString().Length != 10 || !b.ContactInfo.ToString().All(char.IsDigit))
            {
                return BadRequest("ContactInfo must be a 10-digit number");
            }
            if (string.IsNullOrWhiteSpace(b.Quantity) || !int.TryParse(new string(b.Quantity.Where(char.IsDigit).ToArray()), out int quantityValue) || quantityValue <= 0)
            {
                return BadRequest("Quantity must be a positive number.");
            }
            var validStatuses = new[] { "Available", "Expired", "Requested" };
            if (!validStatuses.Contains(b.Status, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Status must be one of 'Available', 'Expired', or 'Requested'.");
            }
            var validBloodTypes = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            if (!validBloodTypes.Contains(b.BloodType, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("BloodType must be a valid blood type (A+, A-, B+, B-, AB+, AB-, O+, O-).");
            }
            b.Id = DonorList.Any() ? DonorList.Max(i => i.Id) + 1 : 1;
            DonorList.Add(b);
            return CreatedAtAction(nameof(GetById), new { id = b.Id }, b);
        }
        // Getting all Donors
        [HttpGet]
        public ActionResult<IEnumerable<BloodBank>> GetAllDonors()
        {
            if (DonorList.Any())
            {
                return Ok(DonorList);
            }
            else
            {
                return NotFound("No Entries found");
            }
        }
        //Getting all Donors using id
        [HttpGet("{id}")]
        public ActionResult<BloodBank> GetById(int id)
        {
            var donor = DonorList.Find(i => i.Id == id);
            if (donor == null)
            {
                return NotFound($"No Entry Found with id {id}");
            }
            return donor;
        }

        //Update the Donors - with id
        [HttpPut("{id}")]
        public IActionResult UpdateDonorList(int id, string status)
        {
            var Donor = DonorList.Find(i => i.Id == id);
            if (Donor == null)
            {
                return NotFound($"No Entry Found with id {id}");
            }
            var validStatuses = new[] { "Available", "Expired", "Requested" };
            if (!validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Status must be one of 'Available', 'Expired', or 'Requested'.");
            }
            Donor.Status = status;
            return NoContent();

        }
        //Delete the Donors - with id
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var donor = DonorList.Find(x => x.Id == id);
                if (donor == null)
                {
                    return NotFound($"No donor found with ID {id}");
                }

                DonorList.Remove(donor);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An unexpected error occurred while deleting the donor.");
            }
        }

        //pagination
        [HttpGet("page")]
        public ActionResult<IEnumerable<BloodBank>> GetPage(int page = 1, int size = 3)
        {
            if (page <= 0 || size <= 0)
            {
                return BadRequest("Page number and size number must be greater than zero");
            }
            var res = DonorList.Skip((page - 1) * size).Take(size).ToList();
            if (!res.Any())
            {
                return NotFound($"No entries found for page {page} with size {size} ");
            }
            return res;
        }


        // Searching based on the bloodType
        [HttpGet("ByBloodType")]

        public ActionResult<IEnumerable<BloodBank>> SearchBloodType(string bloodType)
        {
            var donors = DonorList.Where(x => x.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase)).ToList();
            var validBloodTypes = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            if (!validBloodTypes.Contains(bloodType, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("BloodType must be a valid blood type (A+, A-, B+, B-, AB+, AB-, O+, O-).");
            }
            if (!donors.Any())
            {
                return NotFound($"No donors found with the specfied blood type {bloodType}");
            }
            else
            {
                return donors;
            }
        }
        //Searching based on Status
        [HttpGet("Bystatus")]
        public ActionResult<IEnumerable<BloodBank>> SearchStatus(string status)
        {
            var donors = DonorList.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            var validStatuses = new[] { "Available", "Expired", "Requested" };
            if (!validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Status must be one of 'Available', 'Expired', or 'Requested'.");
            }
            if (!donors.Any())
            {
                return NotFound($"No entries found with the status {status}");
            }
            return donors;

        }
        //search by donorName
        [HttpGet("BydonorName")]
        public ActionResult<IEnumerable<BloodBank>> SearchDonorName(string Name)
        {
            var donors = DonorList.Where(x => x.DonorName.Equals(Name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!donors.Any())
            {
                return NotFound($"No entries found with the specified name {Name}");
            }
            return donors;

        }
        //Sorting by CollecionDate
        [HttpGet("SortbyCollectionDate")]
        public ActionResult<IEnumerable<BloodBank>> sortByCollectionDate(string sortOrder ="asc")
        {
            if (string.IsNullOrWhiteSpace(sortOrder) || (sortOrder.ToLower() != "asc" && sortOrder.ToLower() != "desc"))
            {
                return BadRequest("Invalid sort order,Please use asc or desc");
            }
          
            var res =sortOrder.ToLower()=="asc"? DonorList.OrderBy(i => i.CollectionDate).ToList():DonorList.OrderByDescending(i => i.CollectionDate).ToList();
            if (!res.Any())
            {
                return NotFound("No Entries found to sort");
            }
            return res;
        }

        //multipleSearching functionality using bloodType and status

        [HttpGet("SearchbybloodTypeAndStatus")]
        public ActionResult<IEnumerable<BloodBank>> SearchbybloodTypeAndStatus(string bloodType,string status)
        {
            if (string.IsNullOrWhiteSpace(bloodType) || string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("Blood type and status are required fields.");
            }
            var res = DonorList.Where(i => i.BloodType.Equals(bloodType, StringComparison.OrdinalIgnoreCase) &&
                   i.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!res.Any())
            {
                return NotFound($"No donors found with blood type {bloodType} and status {status}");
            }

            return res;

        }

    }
}
