#  Product Sales Report - DevExpress (.NET WinForms)

##  Setup Instructions

1. **Clone this repository:**
   ```bash
   git clone https://github.com/Uth-Narin/Product_SalesReport.git
   ```

2. **Open the solution in Visual Studio.**

3. **Run the SQL script** located at:
   ```
   /SQL/CreateAndSeed_ProductSales.sql
   ```
   This will create and populate the `ProductSales` table.

4. **Check the connection string** in the app — it should be set to use **LocalDB** or your own SQL Server instance.

---

## 🔌 Connection String Info

If you're using `App.config` or `Settings.cs`, your connection string should look like this:

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductSalesDB;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

>  You can change `Data Source` to match your local SQL Server (e.g., `localhost`, `SQLExpress`, etc.) if you're not using LocalDB.

---

##  Report Features

- Grouped by: `PRODUCTCODE`
- Totals per group:
  -  Total Quantity Sold
  -  Total Revenue (Quantity × UnitPrice)

---
## ⚙️ Tech Stack

- .NET WinForms
- DevExpress Reporting
- SQL Server / LocalDB
- ADO.NET for data access
