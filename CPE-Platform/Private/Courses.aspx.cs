using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;



namespace CPE_Platform.Private
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM CPE_Course";
                SqlDataSource1.DataBind();
                DataList1.DataBind();
            }
            

            //if (DropDownList2.SelectedValue == "-1")
            //{
            //    SqlDataSource1.SelectCommand = "SELECT * FROM Product";
            //}
            //else
            //{
            //    SqlDataSource1.SelectCommand = "SELECT * FROM [Product] inner join ProductCategory on Product.CategoryID=ProductCategory.CategoryID and ProductCategory.CategoryID=@CategoryID";
            //}

        }
        protected void CartBtn_Click(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;

            //string productID = btn.CommandArgument.ToString();

            //int currentQuantity = 0;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //con.Open();
            //SqlCommand commandSelect = new SqlCommand("Select ProductQuantity FROM Product Where ProductID= '" + productID + "'", con);
            //SqlDataReader readQuantity = commandSelect.ExecuteReader();
            //if (readQuantity.Read())
            //{
            //    currentQuantity = readQuantity.GetInt32(0);
            //}

            //con.Close();

            //if (currentQuantity == 0)
            //{
            //    Response.Write("<script>alert('Item out of stock');</script>");
            //}
            //else
            //{

            //    if (Session["Cart"] != null)
            //    {
            //        if (Session["Cart"].ToString().Contains(productID))
            //        {
            //            Response.Write("<script>alert('Item already added in cart');</script>");
            //        }
            //        else
            //        {
            //            Session["Cart"] = Session["Cart"] + "," + productID;
            //        }
            //    }
            //    else
            //    {
            //        Session["Cart"] = productID;
            //    }
            //}

        }
        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}