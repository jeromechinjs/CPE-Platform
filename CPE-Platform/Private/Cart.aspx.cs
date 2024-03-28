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
                loadCartItems();
            }
        }
        protected void removeItem(object sender, CommandEventArgs e)
        {
            string CPECode = e.CommandArgument.ToString();
            int numOfItems = Convert.ToInt32(Session["numOfItems"]); // for cart number badge

            if ((ArrayList)Session["Cart"] != null)
            {
                ArrayList temp_cart = new ArrayList(); // temporary cart to be temporary clone of Session["Cart"]
                foreach (String course in (ArrayList)Session["Cart"])
                {
                    temp_cart.Add(course); // copy all items inside session["cart"] into temp_cart, clone all things
                }
                temp_cart.Remove(CPECode); // remove the selected course
                Session["Cart"] = temp_cart; // update latest cart into session["cart"]
            }

            numOfItems--;
            Session["numOfItems"] = numOfItems; // update numOfItems session
            updateCartNumber(numOfItems); // reflect changes into navbar

            loadCartItems();
        }

        protected void loadCartItems()
        {
            if ((ArrayList)Session["Cart"] != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CPECode");
                dt.Columns.Add("CPEName");
                dt.Columns.Add("CPEPrice");

                foreach (String CPECode in (ArrayList)Session["Cart"])
                {

                    string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionstring);
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode='" + CPECode + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    cartItemCards.DataSource = dt;
                    cartItemCards.DataBind();
                }
            }
            if (cartItemCards.DataSource == null) // when cart is empty
            {
                string emptyCart = "<div class=\"card w-100 my-5 p-3\">No Items in Cart</div>";
                Button2.Visible = false;
                cartContainer.Controls.Add(new LiteralControl(emptyCart));
            }
        }
        private void updateCartNumber(int numOfItems)
        {
            System.Web.UI.WebControls.Label cartBadge = (System.Web.UI.WebControls.Label)Master.FindControl("cartBadge");
            if (numOfItems > 0)
            {
                // access element form Master page
                cartBadge.Text = "" + numOfItems;
            } else
            {
                cartBadge.Text = "" + 0;
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Billing.aspx");
        }

    }
}