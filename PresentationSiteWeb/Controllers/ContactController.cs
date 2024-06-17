using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PresentationSiteWeb.Models;
using System.Data.Common;
using ToolsBoxADO;

namespace PresentationSiteWeb.Controllers
{
    public class ContactController : Controller
    {
       // private readonly DbConnection _connection;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult GererContact()
        {
            List<Contact> contacts = new List<Contact>();
            using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Contact ";
                    command.CommandType = System.Data.CommandType.Text;
                  
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(
                                new Contact
                                {
                                    Id = (int)reader["Id"],
                                    Nom = (string)(reader["Nom"]),
                                    Prenom = (string)(reader["Prenom"]),
                                    Email = (string)(reader["Email"])
                                });
                        }
                    }
                }
            }
            return View(contacts);
        }


        public IActionResult DetailContact(int id)
        {
            Contact? contact;
            using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
            {
                connection.Open();
                contact = (Contact)connection.ExecuteReader("SELECT * FROM Contact WHERE id = @id"
                   , detail => new Contact{
                    Id = (int)detail["Id"],
                    Nom = (string)(detail["Nom"]),
                    Prenom = (string)(detail["Prenom"]),
                       Email = (string)(detail["Email"])
                   }, false, new { id }).SingleOrDefault();
            }

            return View(contact);
        }

        [HttpGet]
        public IActionResult EnregistreContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnregistreContact(CreateContactForm contact)
        {

            if (!ModelState.IsValid)
            {
                return View();
            } 
            else
            {
                using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
                {
                    connection.Open();
                    connection.ExecuteNonQuery("CreateContact", true, new { contact.Prenom, contact.Nom, contact.Email });
                    return RedirectToAction("GererContact");
                }
            } 
        }

        public IActionResult deleteContact(int id)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
            {
                connection.Open();
                connection.ExecuteNonQuery("DELETE FROM Contact WHERE id = @id", false, new { id });
                return RedirectToAction("GererContact");
            }
        }

        [HttpGet]
        public IActionResult EnregistreContact(int id)
        {

            using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
            {
                connection.Open();
                CreateContactForm? contact;
                using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
                {
                    connection.Open();
                    contact = (CreateContactForm)connection.ExecuteReader("SELECT * FROM Contact WHERE id = @id"
                       , detail => new CreateContactForm
                       {
                           Nom = (string)(detail["Nom"]),
                           Prenom = (string)(detail["Prenom"]),
                           Email = (string)(detail["Email"])
                       }, false, new { id }).SingleOrDefault();
                }
                return RedirectToAction("GererContact");
            }
            return View();
        }


        public IActionResult ModifierContact(int id, CreateContactForm contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            } 
            else
            {
                using (SqlConnection connection = new SqlConnection(@"Server=GOS-VDI201\TFTIC;Database=ASP;Trusted_Connection=True;TrustServerCertificate=true"))
                {
                    connection.Open();
                    connection.ExecuteNonQuery("UPDATE Contact SET Nom = @Nom, Prenom = @Prenom, Email = @Email WHERE ID = @Id;", false, new { contact.Nom, contact.Prenom, contact.Email, id });
                    return RedirectToAction("GererContact");
                }
            }

        }
    }

}
