namespace _04.AddNewProduct
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public class AddNewProduct
    {
        private SqlConnection dbConnection;

        public static void Main()
        {
            AddNewProduct products = new AddNewProduct();

            try
            {
                products.ConnectToDB();

                int newProductId = products.InsertProduct("Something very special", 2, 3, null, null, null, null, null, true);

                Console.WriteLine("Inserted new product with ProductID = {0}", newProductId);
            }
            finally
            {
                products.DisconnectFromDB();
            }
        }

        private void ConnectToDB()
        {
            // Go to app.config to change the connection string
            ConnectionStringSettings dBConnectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            this.dbConnection = new SqlConnection(dBConnectionString.ConnectionString);
            this.dbConnection.Open();
        }

        private void DisconnectFromDB()
        {
            if (this.dbConnection != null)
            {
                this.dbConnection.Close();
            }
        }

        private void ConvertToDBNull(SqlParameter sqlCmd, object value)
        {
            if (value == null)
            {
                sqlCmd.Value = DBNull.Value;
            }
        }

        private int InsertProduct(
                                  string productName,
                                  int? supplierID,
                                  int? categoryID,
                                  string quantity,
                                  decimal? price,
                                  int? unitsInStock,
                                  int? unitsOnOrder,
                                  int? reorderLevel,
                                  bool discontinued)
        {
            SqlCommand cmdInsertProduct = new SqlCommand(
                "INSERT INTO Products(ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) " +
                "VALUES(@productName, @supplierID, @categoryID, @quantity, @price, @unitsInStock, @unitsOnOrder, @reorderLevel, @discontinued)",
                this.dbConnection);

            cmdInsertProduct.Parameters.AddWithValue("@productName", productName);

            SqlParameter sqlParameterSupplierID = new SqlParameter("@supplierID", supplierID);
            this.ConvertToDBNull(sqlParameterSupplierID, supplierID);
            cmdInsertProduct.Parameters.Add(sqlParameterSupplierID);

            SqlParameter sqlParameterCategoryID = new SqlParameter("@categoryID", categoryID);
            this.ConvertToDBNull(sqlParameterCategoryID, categoryID);
            cmdInsertProduct.Parameters.Add(sqlParameterCategoryID);

            SqlParameter sqlParameterQuantity = new SqlParameter("@quantity", quantity);
            this.ConvertToDBNull(sqlParameterQuantity, quantity);
            cmdInsertProduct.Parameters.Add(sqlParameterQuantity);

            SqlParameter sqlParameterPrice = new SqlParameter("@price", price);
            this.ConvertToDBNull(sqlParameterPrice, price);
            cmdInsertProduct.Parameters.Add(sqlParameterPrice);

            SqlParameter sqlParameterUnitsInStock = new SqlParameter("@unitsInStock", unitsInStock);
            this.ConvertToDBNull(sqlParameterUnitsInStock, unitsInStock);
            cmdInsertProduct.Parameters.Add(sqlParameterUnitsInStock);

            SqlParameter sqlParameterUnitsInOrder = new SqlParameter("@unitsOnOrder", unitsOnOrder);
            this.ConvertToDBNull(sqlParameterUnitsInOrder, unitsOnOrder);
            cmdInsertProduct.Parameters.Add(sqlParameterUnitsInOrder);

            SqlParameter sqlParameterReorderLevel = new SqlParameter("@reorderLevel", reorderLevel);
            this.ConvertToDBNull(sqlParameterReorderLevel, reorderLevel);
            cmdInsertProduct.Parameters.Add(sqlParameterReorderLevel);

            cmdInsertProduct.Parameters.AddWithValue("@discontinued", discontinued);

            var rowsAffectedCount = cmdInsertProduct.ExecuteNonQuery();
            cmdInsertProduct.Parameters.Clear();
            Console.WriteLine("({0} row(s) affected)", rowsAffectedCount);

            SqlCommand cmdSelectIdentity = new SqlCommand("SELECT @@Identity", this.dbConnection);
            int insertedProductId = (int)(decimal)cmdSelectIdentity.ExecuteScalar();

            return insertedProductId;
        }
    }
}
