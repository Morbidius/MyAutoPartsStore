Autoparts Web Shop

Web application for hosting dealers and users who can buy and sell items and an administrator who can track/edit/delete every item on the platform.

This project is made with ASP.NET Core 5. Parts of the design is taken from the catalog part of[InterCars](https://bg.intercars.eu/) for educational purposes!

## 🛠 Built with:

- ASP.NET Core MVC
- MS SQL Server
- AutoMapper
- Bootstrap
- Font-awesome
- xUnit
- Moq

## Permissions:
Permission | Guest | Logged User | Dealer | Admin
-- | ---- | ---- | ---
Index page | ✅ | ✅ | ✅ | ✅
View Product Details | ✅ | ✅ | ✅ | ✅
Admin Dashboard | ❌| ❌ | ❌ | ✅
Become Dealer | ❌ | ✅ | ❌ | ✅
Buy Product |❌ | ✅ | ❌ | ❌
Add Product | ❌ | ❌ | ✅ | ✅
Edit Product | ❌ | ❌ | ✅ | ✅
Delete Product | ❌ | ❌ | ✅ | ✅

Seeded users
Admin 	pesho@mapi.com 	root123

## Pages:

### Public Pages:

**Home Page**

This is the Intex page of the application, here the user can do various things like product search by name or category, become a dealer or buy a product if registered and logged in.
![image](https://user-images.githubusercontent.com/34027947/129605096-d3641db3-4d51-44e2-b053-c33ab3756d70.png)

### Pages for Logged Users:

**Dealer Panel**

In this page, the dealer tracks his products, edits them and deletes if needed.
![image](https://user-images.githubusercontent.com/34027947/129605154-710fc085-a052-4512-ab7e-05a1763cc2b5.png)

**Admin Panel**

In this page, the admin tracks all of the products from all the dealers on the website, approves or dissaproves them depending on the content of the product and the accuracy of it and can also edit or delete if needed.
![image](https://user-images.githubusercontent.com/34027947/129605035-08173a8b-2f22-4e18-bf40-44682aca9ca6.png)

