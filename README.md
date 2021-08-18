# InterCars

![image](https://user-images.githubusercontent.com/34027947/129862406-d78a4dbb-2817-4b21-a11f-90752406e678.jpg)

Web application for hosting dealers and users who can buy and sell items and an administrator who can track/edit/delete every item on the platform.

This project is made with ASP.NET Core 5. Parts of the design is taken from the catalog part of [InterCars](https://bg.intercars.eu/) for educational purposes!

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
-- | --- | --- | --- | ---
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

Here, the dealer tracks each order that is linked to his items.
![image](https://user-images.githubusercontent.com/34027947/129956754-a51ed460-8ec8-4afe-b24f-aaa12585a75c.png)

And here, the dealer can see the details of each order that is linked to his items.
![image](https://user-images.githubusercontent.com/34027947/129956886-8bfbd4ab-687f-4446-a1f7-5bfc6ca1985d.png)

**Admin Panel**

In this page, the admin tracks all of the products from all the dealers on the website, approves or dissaproves them depending on the content of the product and the accuracy of it and can also edit or delete if needed.
![image](https://user-images.githubusercontent.com/34027947/129605035-08173a8b-2f22-4e18-bf40-44682aca9ca6.png)


**Shopping Cart**
In this page, the user can see their items in the cart and change the quantity or remove an item altogether.
The price gets calculated dynamically according to the items quantities.
![image](https://user-images.githubusercontent.com/34027947/129956377-609dedab-b1d5-41e7-a51e-1482a2af2d86.png)
