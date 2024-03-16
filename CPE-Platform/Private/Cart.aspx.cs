using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Web.UI.WebControls.WebParts;
using static System.Net.Mime.MediaTypeNames;

namespace CPE_Platform.Private
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ArrayList cart = (ArrayList)Session["Cart"];
                if (cart != null)
                {
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add("CPECode");
                    dt.Columns.Add("CPEName");
                    dt.Columns.Add("CPEPrice");

                    dr = dt.NewRow();

                    int sum = 0;
                    int count = 0;
                    foreach (string CPECode in (ArrayList)Session["Cart"])
                    {

                        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(connectionstring);
                        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CPE_Course where CPECode='" + CPECode + "'", con);
                        DataSet ds = new DataSet();

                        da.Fill(ds);


                        if (Session["Total"] != null)
                        {
                            string newTotal = Session["Total"].ToString();
                            String[] total = Session["Total"].ToString().Split(',');
                            total = total.Where(x => !string.IsNullOrEmpty(x)).ToArray();


                            if (count == total.Length)
                            {
                                dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());

                                newTotal = newTotal + "," + ds.Tables[0].Rows[0]["CPEPrice"].ToString();
                                Session["Total"] = newTotal;
                            }
                            else
                            {
                                dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());
                            }
                        }
                        else
                        {
                            dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());
                        }

                        //sum = sum + Convert.ToInt32(ds.Tables[0].Rows[0]["CPEPrice"].ToString());

                        //cartItemCards.DataSource = dt;
                        //cartItemCards.DataBind();

                        //cartItemCards.FooterRow.Cells[4].Text = sum.ToString();
                        //Session["Sum"] = sum.ToString();

                        //cartItemCards.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                        //cartItemCards.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;

                        count++;
                    }


                    //if (Session["Quantity"] != null)
                    //{
                    //    string newQuantity = Session["Quantity"].ToString();
                    //    String[] quantity = Session["Quantity"].ToString().Split(',');
                    //    quantity = quantity.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    //    int countQuantity = 0;
                    //    foreach (GridViewRow row in cartItemCards.Rows)
                    //    {
                    //        TextBox rowQuantity = (TextBox)row.FindControl("TextBox1");

                    //        if (countQuantity == quantity.Length)
                    //        {
                    //            rowQuantity.Text = "1";
                    //            newQuantity = newQuantity + "," + 1;
                    //        }
                    //        else
                    //        {
                    //            rowQuantity.Text = quantity[countQuantity++];
                    //        }
                    //    }

                    //    Session["Quantity"] = newQuantity;
                    //}

                    //if (Session["total"] != null)
                    //{
                    //    String[] total = Session["Total"].ToString().Split(',');
                    //    total = total.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    //    sum = 0;
                    //    for (int i = 0; i < total.Length; i++)
                    //    {
                    //        sum = sum + Convert.ToInt32(total[i]);
                    //    }

                    //    if (cartItemCards.DataSource != null)
                    //    {
                    //        cartItemCards.FooterRow.Cells[4].Text = sum.ToString();
                    //    }
                    //}


                }


                if (cartItemCards.DataSource == null)
                {
                    string emptyCart = "<div class=\"card w-100 my-5 p-3\">\r\n    <h2>No Items in Cart</h2>\r\n</div>";
                    Button2.Visible = false;
                    cartContainer.Controls.Add(new LiteralControl(emptyCart));
                }
            }
        }
        protected void removeItem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string CPECode = btn.CommandArgument.ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode='" + CPECode + "'", con);

            con.Open();
            SqlDataReader read = cmd.ExecuteReader();

            //if (read.Read())
            //{
            //    productID = read.GetString(0);
            //}
            //con.Close();
            //Session["Cart"] = Session["Cart"].ToString().Replace(productID, "");

            if (Session["Cart"] != null)
            {
                String[] cart = Session["Cart"].ToString().Split(',');

                string newCart = "";
                cart = cart.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Boolean checkFirst = true;
                for (int i = 0; i < cart.Length; i++)
                {
                    if (cart[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        if (checkFirst == true)
                        {
                            newCart = cart[i];
                            checkFirst = false;
                        }
                        else
                        {
                            newCart = newCart + "," + cart[i];
                        }
                    }
                }

                Session["Cart"] = newCart;
            }
            GridViewRow gridRow = (GridViewRow)((Button)sender).NamingContainer;
            int rowIndex = gridRow.RowIndex;


            if (Session["Quantity"] != null)
            {
                String[] quantity = Session["Quantity"].ToString().Split(',');
                System.Diagnostics.Debug.WriteLine(rowIndex + " row index");

                if (cartItemCards.Rows.Count == 1 || rowIndex == 1)
                {
                    quantity[rowIndex] = null;
                }
                else
                {
                    quantity[rowIndex + 1] = null;
                }

                string newQuantity = "";
                quantity = quantity.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Boolean checkFirst = true;
                for (int i = 0; i < quantity.Length; i++)
                {
                    if (quantity[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        if (checkFirst == true)
                        {
                            newQuantity = quantity[i];
                            checkFirst = false;
                        }
                        else
                        {
                            newQuantity = newQuantity + "," + quantity[i];
                        }
                    }
                }

                Session["Quantity"] = newQuantity;
                System.Diagnostics.Debug.WriteLine(Session["Quantity"].ToString() + " Total Session");
            }

            //if (Session["Total"] != null)
            //{
            //    String[] total = Session["Total"].ToString().Split(',');

            //    if (cartItemCards.Rows.Count == 1)
            //    {
            //        total[rowIndex] = null;
            //    }
            //    else
            //    {
            //        total[rowIndex + 1] = null;
            //    }

            //    string newTotal = "";
            //    total = total.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            //    Boolean checkFirst = true;
            //    for (int i = 0; i < total.Length; i++)
            //    {
            //        if (total[i] == null)
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            if (checkFirst == true)
            //            {
            //                newTotal = total[i];
            //                checkFirst = false;
            //            }
            //            else
            //            {
            //                newTotal = newTotal + "," + total[i];
            //            }
            //        }
            //    }

            //    Session["Total"] = newTotal;

            //    System.Diagnostics.Debug.WriteLine(Session["Total"].ToString() + " Total Session");
            //}

            Response.Redirect("Cart.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }

        protected void txt_OnTextChanged(object sender, EventArgs e)
        {

            TextBox quantityTB = (TextBox)sender;


            GridViewRow gridRow = (GridViewRow)((TextBox)sender).NamingContainer;
            int rowIndex = gridRow.RowIndex;

            int quantity = Convert.ToInt32(quantityTB.Text);


            GridViewRow rowTest = cartItemCards.Rows[rowIndex];

            //Restrict if more than inventory number

            string rowName = cartItemCards.Rows[rowIndex].Cells[1].Text;
            string productID = "";
            int currentQuantity = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand command = new SqlCommand("SELECT ProductID from Product where CPEName= '" + rowName + "'", con);

            con.Open();
            SqlDataReader read = command.ExecuteReader();
            if (read.Read())
            {
                productID = read.GetString(0);
            }
            con.Close();

            con.Open();
            SqlCommand commandSelect = new SqlCommand("Select ProductQuantity FROM Product Where ProductID= '" + productID + "'", con);
            SqlDataReader readQuantity = commandSelect.ExecuteReader();
            if (readQuantity.Read())
            {
                currentQuantity = readQuantity.GetInt32(0);
            }

            con.Close();

            if (currentQuantity < quantity)
            {

                quantityTB.Text = currentQuantity.ToString();
                quantity = currentQuantity;
            }

            //Change price when quantity change
            string rowPrice = cartItemCards.Rows[rowIndex].Cells[2].Text;

            int price = Convert.ToInt32(rowPrice);


            int total = 0;
            total = quantity * price;

            cartItemCards.Rows[rowIndex].Cells[4].Text = total.ToString();

            string totalPriceList = "";
            string quantityList = "";

            int sum = 0;
            foreach (GridViewRow row in cartItemCards.Rows)
            {
                TextBox rowQuantity = (TextBox)row.FindControl("TextBox1");

                quantityList = quantityList + "," + rowQuantity.Text;
                totalPriceList = totalPriceList + "," + row.Cells[4].Text;

                sum = sum + Convert.ToInt32(row.Cells[4].Text);
            }

            Session["Quantity"] = quantityList;
            Session["Total"] = totalPriceList;


            cartItemCards.FooterRow.Cells[4].Text = sum.ToString();

            Session["Sum"] = sum.ToString();

        }
    }
}