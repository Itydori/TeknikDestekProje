# Technical Support Management System

## 🧩 About the Project

This project is a web-based management system designed for companies that provide technical support services. It allows easy tracking of **customer records**, **service requests**, and **work orders**.

- Built with ASP.NET MVC architecture  
- Data operations are handled using Entity Framework  
- Follows a multi-layered architecture (Entities, DataAccess, Business, Web)  

---

## 🔧 Main Features

- Create and manage customer records
- Add new work orders
- List and view details of service requests
- Track work orders based on customers
- Admin panel access to manage operations
- Clean code structure with layered architecture

---

## 📂 Project Structure

### 1. Entities Layer

- `Musteri.cs`: Holds customer details (Name, Phone, Address, etc.)
- `Isemri.cs`: Contains service request information (Device, Description, Status, etc.)



![image](https://github.com/user-attachments/assets/088db209-4e75-49a4-a7a9-03b7c74edcb7)
![image](https://github.com/user-attachments/assets/db146b12-013b-4d27-98e0-c2e91073b2e6)
![image](https://github.com/user-attachments/assets/d8219a90-1113-453f-99a2-5854e86ade87)

---

### 2. Business Layer (`Services`)

- `IsemriService.cs`: Includes methods for adding, listing, deleting, and updating work orders
- `MusteriService.cs`: Handles customer creation and listing

  ![image](https://github.com/user-attachments/assets/1b6055f1-ddde-4ebb-a413-3531ce118f77)

---


### 3. Controllers

- `HomeController.cs`:
  - `IsemriMusteri()`: Lists all work orders of a customer
  - `YeniIsEmri()`: Displays the form to create a new work order
  - `IsEmriDetay(int id)`: Shows detailed info about a selected work order

![image](https://github.com/user-attachments/assets/b31d794f-04fe-45a7-83a6-b10e986f9c05)
![image](https://github.com/user-attachments/assets/16c1536a-8960-42c8-a1da-a64ce787e704)

---

### 4. Views (`Views/Home`)

- `IsemriMusteri.cshtml`: Displays the list of work orders
- `YeniIsEmri.cshtml`: A form to add a new work order
- `IsEmriDetay.cshtml`: Shows detailed information of a selected work order

![image](https://github.com/user-attachments/assets/1a4e972c-717e-472e-bd93-787458d0df53)
![image](https://github.com/user-attachments/assets/67aa6f16-1786-4e02-a43b-2e602793b219)
![image](https://github.com/user-attachments/assets/c7c045a0-54b3-4fca-bb66-444279de7d31)
![image](https://github.com/user-attachments/assets/faa2c246-995e-4048-83f9-1073b6833433)
![image](https://github.com/user-attachments/assets/1a7aa07d-4487-4562-9c4b-8b86db449cfb)
![image](https://github.com/user-attachments/assets/1157b9c7-7d56-45ac-9829-17a3cd8578d2)
![image](https://github.com/user-attachments/assets/60e0303e-ce7b-47d4-9297-b0822cec6029)

---

## 🚀 Demo Scenario (How to Use)

1. Open the homepage
2. Navigate to the "New Work Order" page
3. Fill in device, description, and customer details to create a request
4. Go to the "Work Orders" page to view created entries
5. Click the details button to view the full order info


---
