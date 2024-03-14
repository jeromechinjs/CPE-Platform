﻿using Newtonsoft.Json.Linq;
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

                Session["Cart"] = new ArrayList();
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
            Session["currentCPECode"] = CPECode; // saving it for later use for cart

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
                    txtCPEName.Text = dataReader["CPEName"].ToString();
                    txtCPEDesc.Text = dataReader["CPEDesc"].ToString();
                    txtCPEVenue.Text = dataReader["CPEVenue"].ToString();
                    txtCPETrainer.Text = dataReader["CPETrainer"].ToString();
                    txtCPESeat.Text = dataReader["CPESeatAmount"].ToString();
                    txtCPEPrice.Text = dataReader["CPEPrice"].ToString();
                    txtContact.Text = dataReader["CPEContact"].ToString();
                    txtEmail.Text = dataReader["CPEEmail"].ToString();
                    txtStartTime.Text = dataReader["CPEStartTime"].ToString();
                    txtEndTime.Text = dataReader["CPEEndTime"].ToString();
                    dllStartDate.Text = dataReader["CPEStartDate"].ToString().Trim();
                    dllEndDate.Text = dataReader["CPEEndDate"].ToString().Trim();
                }
                dataReader.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#courseDetailsModal').modal('show');", true);
        }

        protected void CartBtn_Click(object sender, CommandEventArgs e)
        {
            int seatsLeft = 0; // need to initialized to a value
            String CPECode = Session["currentCPECode"].ToString();
            Boolean itemInsideCart = false;

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
                if ((ArrayList)Session["Cart"] != null)
                {
                    // check if CPECode existed in cart
                    foreach (String course in (ArrayList)Session["Cart"])
                    {
                        if (course == CPECode)
                        {
                            itemInsideCart = true;
                            break;
                        } 
                        else
                        {
                            itemInsideCart = false;
                        }
                    }
                    // item not in cart, proceed to add new item to cart
                    if (itemInsideCart)
                    {
                        Response.Write("<script>alert('Course already added in cart');</script>");
                    } else
                    {
                        addToCart(CPECode);
                        Response.Write("<script>alert('Item added to Cart');</script>");
                    }
                }   
                else
                {
                    // cart is empty
                    addToCart(CPECode);
                    Response.Write("<script>alert('Item added to Cart');</script>");
                }
            }
        }

        private void addToCart(String CPECode)
        {
            ArrayList temp_cart = new ArrayList(); // temporary cart to be temporary clone of Session["Cart"]
            foreach (String course in (ArrayList)Session["Cart"])
            {
                temp_cart.Add(course); // copy all items inside session["cart"] into temp_cart, clone all things
            }
            temp_cart.Add(CPECode); // push in newest course
            Session["Cart"] = temp_cart; // update latest cart into session["cart"]
        }
    }
}