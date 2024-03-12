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
using System.Web.Optimization;
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
                allCourses.SelectCommand = "SELECT * FROM CPE_Course";
                allCourses.DataBind();

                courseCards.DataBind();

                // Dropdown course type selection filter
                courseTypes.SelectCommand = "SELECT DISTINCT CPEType FROM [CPE_Course]";
                courseTypes.DataBind();

            }

            // Filter CPE Course Types
            if (dropdown_courseTypes.SelectedValue == "-1")
            {
                allCourses.SelectCommand = "SELECT * FROM CPE_Course";
            }
            else
            {
                allCourses.SelectCommand = "SELECT * FROM CPE_Course";
            }
        }
        protected void view_course_info(object sender, CommandEventArgs e)
        {
            string CPECode = e.CommandArgument.ToString();
            Session["currentCPECode"] = CPECode;

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode=@CPECode", con);
                cmd.Parameters.AddWithValue("@CPECode", CPECode);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) // returns true if have more rows to read, else false
                {
                    txtCPECode.Text = dataReader["CPECode"].ToString();
                    txtCPEDesc.Text = dataReader["CPEDesc"].ToString();
                    txtCPESeat.Text = dataReader["CPESeatAmount"].ToString();
                    txtCPEPrice.Text = dataReader["CPEPrice"].ToString();
                    dllStartDate.Text = dataReader["CPEStartDate"].ToString().Trim();
                    dllEndDate.Text = dataReader["CPEEndDate"].ToString().Trim();
                }
                dataReader.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#courseDetailsModal').modal('show');", true);
        }

        protected void CartBtn_Click(object sender, CommandEventArgs e)
        {
            ArrayList cart = new ArrayList(); // array list to be assigned to the cart session for payment module
            int seatsLeft = 0; // need to initialized to a value
            String CPECode = Session["currentCPECode"].ToString();
           
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CPESeatAmount FROM CPE_Course where CPECode='" + CPECode + "'", con);
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                seatsLeft = dataReader.GetInt32(0); // retrieve seats left for currently selected CPE Course
            }

            if (seatsLeft == 0)
            {
                Response.Write("<script>alert('No more seats left');</script>");
            }
            else
            {
                if (Session["Cart"] != null)
                {
                    foreach (String course in cart)
                    {
                        if (course == CPECode)
                        {
                            Response.Write("<script>alert('Course already added in cart');</script>");
                        } 
                        else
                        {
                            cart.Add(CPECode);
                            Session["Cart"] = cart;
                            Response.Write("<script>alert('Item added to Cart');</script>");
                        }
                    }

                }
                else
                {
                    // cart is empty
                    cart.Add(CPECode);
                    Session["Cart"] = cart;
                    Response.Write("<script>alert('Item added to Cart');</script>");
                }
            }

            // cart test
            String allItems = "Courses added: ";
            foreach (String course in cart)
            {
                allItems = allItems + "," + course;
            }


            testlbl.Text = allItems; 

        }
    }
}