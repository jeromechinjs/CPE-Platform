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
                if ((ArrayList)Session["Cart"] != null)
                {
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add("CPECode");
                    dt.Columns.Add("CPEName");
                    dt.Columns.Add("CPEPrice");

                    dr = dt.NewRow(); // create a new row for each item in cart

                    int sum = 0;
                    int count = 0;
                    foreach (String CPECode in (ArrayList)Session["Cart"])
                    {

                        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(connectionstring);
                        con.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode='" + CPECode + "'", con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //DataSet ds = new DataSet();

                        da.Fill(dt);

                        cartItemCards.DataSource = dt;
                        cartItemCards.DataBind();


                        //if (Session["Total"] != null)
                        //{
                        //    string newTotal = Session["Total"].ToString();
                        //    String[] total = Session["Total"].ToString().Split(',');
                        //    total = total.Where(x => !string.IsNullOrEmpty(x)).ToArray();


                        //    if (count == total.Length)
                        //    {
                        //        dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());

                        //        newTotal = newTotal + "," + ds.Tables[0].Rows[0]["CPEPrice"].ToString();
                        //        Session["Total"] = newTotal;
                        //    }
                        //    else
                        //    {
                        //        dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());
                        //    }
                        //}
                        //else
                        //{
                        //    dt.Rows.Add(ds.Tables[0].Rows[0]["CPECode"].ToString(), ds.Tables[0].Rows[0]["CPEName"].ToString(), ds.Tables[0].Rows[0]["CPEPrice"].ToString());
                        //}

                        //sum = sum + Convert.ToInt32(ds.Tables[0].Rows[0]["CPEPrice"].ToString());

                        //cartItemCards.DataSource = dt;
                        //cartItemCards.DataBind();

                        //cartItemCards.FooterRow.Cells[4].Text = sum.ToString();
                        //Session["Sum"] = sum.ToString();

                        //cartItemCards.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                        //cartItemCards.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;

                        //count++;
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
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                CPECode = dataReader.GetString(0);
            }
            con.Close();
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
            Response.Redirect("Billing.aspx");
        }

    }
}