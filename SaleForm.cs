using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ProductSalesReportingTool
{
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
        
        }
        public List<SaleDto> GetSales(DateTime startDate, DateTime endDate)
        {
            var sales = new List<SaleDto>();
            string connectionString = @"Server=NARIN\SQLEXPRESS;Database=ProductSaleReport;Trusted_Connection=True;";

            string query = @"
                SELECT 
                    PRODUCTCODE,
                    PRODUCTNAME,
                    QUANTITY,
                    UNITPRICE,
                    (QUANTITY * UNITPRICE) AS Total,
                    SALEDATE
                FROM ProductSales
                WHERE SALEDATE BETWEEN @StartDate AND @EndDate";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sales.Add(new SaleDto
                            {
                                ProductCode = reader["PRODUCTCODE"]?.ToString(),
                                ProductName = reader["PRODUCTNAME"]?.ToString(),
                                Quantity = reader["QUANTITY"] != DBNull.Value ? Convert.ToInt32(reader["QUANTITY"]) : 0,
                                UnitPrice = reader["UNITPRICE"] != DBNull.Value ? Convert.ToDecimal(reader["UNITPRICE"]) : 0,
                                Total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : 0,
                                SaleDate = reader["SALEDATE"] != DBNull.Value ? Convert.ToDateTime(reader["SALEDATE"]) : DateTime.MinValue
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Directory.CreateDirectory("logs");
                    File.AppendAllText("logs/errors.txt", $"{DateTime.Now}: {ex.Message}{Environment.NewLine}");
                }
                catch
                {
                    MessageBox.Show("Failed to write to log file.", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("An error occurred while retrieving sales data.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sales;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStartDate.Value.Date;
            DateTime end = dtpEndDate.Value.Date.AddDays(1).AddTicks(-1);

            var sales = GetSales(start, end);

            if (sales == null || sales.Count == 0)
            {
                MessageBox.Show("No sales found in selected range.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var report = new SaleReport(sales);
            new DevExpress.XtraReports.UI.ReportPrintTool(report).ShowPreviewDialog();

        }
    }
}
