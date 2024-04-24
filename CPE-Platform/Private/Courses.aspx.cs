using Newtonsoft.Json.Linq;
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
using System.Runtime.Remoting.Messaging;
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
                //allCourses.SelectCommand = "SELECT * FROM CPE_Course";
                //allCourses.DataBind(); // when need to retrieve data and display in frontend inside ASP control template, need bind (if backend oerations only, no ned bind, just read)

                // Dropdown course type selection filter
                //courseTypes.SelectCommand = "SELECT DISTINCT CPEType FROM [CPE_Course]";
                //courseTypes.DataBind();

            }

            // Filter CPE Course Types
            //if (dropdownCourseTypes.SelectedValue == "-1")
            //{
            //}
            //else
            //{
            //    String courseType = dropdownCourseTypes.SelectedValue.Trim();
            //    allCourses.SelectCommand = "SELECT * FROM CPE_Course WHERE CPEType='" + courseType + "'";
            //    allCourses.DataBind();

            //}

            // get today date
            //DateTime today = DateTime.Today;
            //String todayDate = today.ToString("d"); // get date only (given format is mm/dd/yyyy)

            allCourses.SelectCommand = "SELECT * FROM CPE_Course"; // add: WHERE end date does not exceed today's date
            allCourses.DataBind();



            // hide all toasts
            toast1.CssClass = toast1.CssClass.Replace("show", "hide");
            toast2.CssClass = toast2.CssClass.Replace("show", "hide");
            toast3.CssClass = toast3.CssClass.Replace("show", "hide");
            toast4.CssClass = toast4.CssClass.Replace("show", "hide");



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
                    txtRewards.Text = dataReader["Rewards"].ToString();
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
            int numOfItems = Convert.ToInt32(Session["numOfItems"]); // for cart number badge
            String CPECode = Session["currentCPECode"].ToString();
            bool itemInsideCart = false;

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CPESeatAmount FROM CPE_Course where CPECode='" + CPECode + "'", con);
            SqlDataReader getSeatsLeft = cmd.ExecuteReader();

            if (getSeatsLeft.Read())
            {
                seatsLeft = getSeatsLeft.GetInt32(0); // retrieve seats left for currently selected CPE Course
            }

            if (seatsLeft == 0)
            {
                toast3.CssClass = toast3.CssClass.Replace("hide", "show");
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
                    if (itemInsideCart)
                    {
                        toast2.CssClass = toast2.CssClass.Replace("hide", "show");
                    }
                    else   // item not in cart, proceed to add new item to cart
                    {
                        bool alreadyActiveCourse = checkActiveCourse(CPECode); // check if student already registered for this course
                        if (alreadyActiveCourse)
                        {
                            toast4.CssClass = toast4.CssClass.Replace("hide", "show");
                        }
                        else
                        {
                            addToCart(CPECode);
                            numOfItems++;
                            updateCartNumber(numOfItems);
                            toast1.CssClass = toast1.CssClass.Replace("hide", "show");
                        }
                    }
                }
                else
                {
                    // cart is empty
                    bool alreadyActiveCourse = checkActiveCourse(CPECode); // check if student already registered for this course
                    if (alreadyActiveCourse)
                    {
                        toast4.CssClass = toast4.CssClass.Replace("hide", "show");
                    }
                    else
                    {
                        addToCart(CPECode);
                        numOfItems++;
                        updateCartNumber(numOfItems);
                        toast1.CssClass = toast1.CssClass.Replace("hide", "show");
                    }
                }
            }
            Session["numOfItems"] = numOfItems;
        }

        private void addToCart(String CPECode)
        {
            ArrayList temp_cart = new ArrayList(); // temporary cart to be temporary clone of Session["Cart"]
            if ((ArrayList)Session["Cart"] != null)
            {
                foreach (String course in (ArrayList)Session["Cart"])
                {
                    temp_cart.Add(course); // copy all items inside session["cart"] into temp_cart, clone all things
                }
                temp_cart.Add(CPECode); // push in newest course
            }
            else // when cart is empty
            {
                temp_cart.Add(CPECode); // push in newest course
            }
            Session["Cart"] = temp_cart; // update latest cart into session["cart"]

        }

        private void updateCartNumber(int numOfItems)
        {
            if (numOfItems > 0)
            {
                // access element form Master page
                System.Web.UI.WebControls.Label cartBadge = (System.Web.UI.WebControls.Label)Master.FindControl("cartBadge");
                cartBadge.Text = "" + numOfItems;
            }
        }

        private bool checkActiveCourse(String CPECode)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            bool alreadyActiveCourse = false;

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                if (Session["StudentID"] != null)
                {
                    String studentID = Session["StudentID"].ToString();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CPE_Registration WHERE StudentID = @StudentID AND CPECode = @CPECode ", con);

                    con.Open();

                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@CPECode", CPECode);

                    int activeCourses = (int)cmd.ExecuteScalar();

                    if (activeCourses == 0) // student have not registered for this course before
                    {
                        alreadyActiveCourse = false;
                    }
                    else
                    {
                        alreadyActiveCourse = true; // student already registered for this course, its ongoing active course
                    }

                    con.Close();
                }
            }
            return alreadyActiveCourse;
        }
    }
    }