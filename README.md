# BloodBankManagement
This project is a simple ASP.NET Core Web API for managing blood bank Entries. It includes functionality for adding, retrieving, updating, and deleting blood bank entry information, as well as searching, sorting, and filtering based on specific criteria.

Features
  Add, update, delete donor information.
  Retrieve all donors or specific donors by ID.
  Search donors by blood type, status, or name.
  Sort donors by collection date.
  Filter donors using multiple criteria (e.g., blood type and status).
  Pagination support for large datasets.
  
This assignment involves creating a RESTful API for a Blood Bank Management system using
C# and ASP.NET Core.I have used single model for the data interactions and perform
operations on data using an in-memory list as the database. 

I have created an API with one model and one controller.I used that API to cover all CRUD operations,pagination,  search functionalities and Sorting functionalities  as well .


Model Description:
I have included the following  attributes in BloodBank Model:
• Id: A unique identifier for the entry (auto-generated).
• DonorName: Name of the donor.
• Age: Donor's age.
• BloodType: Blood group of the donor (e.g., A+, O-, B+).
• ContactInfo: Contact details (phone number or email).
• Quantity: Quantity of blood donated (in ml).
• CollectionDate: Date when the blood was collected.
• ExpirationDate: Expiration date for the blood unit.
• Status: Status of the blood entry (e.g., "Available", "Requested", "Expired").


Testing the Endpoints:
I used Postman  tool to test the endpoints.

Example POST Request
  URL: https://localhost:7023/api/BloodBank 
  //This URL used to post the information in BloodBank Entries as in the below format
  Body(Json)
  {
    "DonorName": "Nithisha",
    "Age": 30,
    "BloodType": "O+",
    "ContactInfo": 9876543210,
    "Quantity": "500ml",
    "CollectionDate": "2024-01-01",
    "ExpirationDate": "2025-01-01",
    "Status": "Available"
}

Example of GET Request to retrieve all the items
URL: https://localhost:7023/api/BloodBank
//This Api is used to retrieve all the BloodBAnk Entries


Example of GET request to retrieve the data with the id
URL: https://localhost:7023/api/BloodBank/2

Example of Put request to update the status with the id  
URL: https://localhost:7023/api/BloodBank/1?status=Available


Example of Put request to delete the status with the id  
URL: https://localhost:7023/api/BloodBank/6

Example of  pagination using pages and size
URL: https://localhost:7023/api/BloodBank/page?page=1&size=3

Example of  retrieving the BloodBankEnties using bloodType
URL: https://localhost:7023/api/BloodBank/ByBloodType?bloodType=A-

Example of  retrieving the BloodBankEnties using status
https://localhost:7023/api/BloodBank/Bystatus?status=Available

Example of  retrieving the BloodBankEnties using DonorName
https://localhost:7023/api/BloodBank/BydonorName?Name=nithisha

Example of  retrieving the BloodBankEnties by sorting the collectionDate
https://localhost:7023/api/BloodBank/SortbyCollectionDate?sortOrder=asc

Example of  retrieving the BloodBankEnties by using the bloodType and status
https://localhost:7023/api/BloodBank/SearchbybloodTypeAndStatus?bloodType=A-&status=Available


These are the endpoints I have used in my project 


I have Added Swagger support to my project for easy API Testing.And I have Used Postman to manually test each API endpoint.





