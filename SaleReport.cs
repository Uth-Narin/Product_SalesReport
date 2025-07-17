using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ProductSalesReportingTool
{
    public partial class SaleReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SaleReport(List<SaleDto> sales)
        {
            InitializeComponent();

            // Set the data source directly
            this.DataSource = sales;

            // This ensures labels use field names from SaleDto
            this.DataMember = ""; // Make sure it's empty
        }
    }

}
