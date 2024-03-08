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
                allCourses.SelectCommand = "SELECT * FROM CPE_Course";
                allCourses.DataBind();
                DataList1.DataBind();
            }

            // Filter CPE Course Types
            if (courseTypes.SelectedValue == "-1")
            {
                allCourses.SelectCommand = "SELECT * FROM CPE_Course";
            }
            else
            {
                //filter other categories (tbc)
                //allCourses.SelectCommand = "SELECT * FROM [CPE_Course] WHERE CPE_Course.CPEType=@CPEType";
            }



        }

        protected void open_modal(object sender, EventArgs e)
        {
            string script = "$('#courseDetailsModal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        protected void view_course_info(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            txtCPECode.Text = id;
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
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
            Session["IsUpdateFlag"] = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#courseDetailsModal').modal('show');", true);
        }

        protected void CartBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string CPECode = btn.CommandArgument.ToString();

            int seatsLeft = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand commandSelect = new SqlCommand("Select CPESeatAmount FROM CPE_Course Where CPECode= '" + CPECode + "'", con);
            SqlDataReader readQuantity = commandSelect.ExecuteReader();
            if (readQuantity.Read())
            {
                seatsLeft = readQuantity.GetInt32(0);
            }

            con.Close();

            if (seatsLeft == 0)
            {
                Response.Write("<script>alert('No more seats left');</script>");
            }
            else
            {

                if (Session["Cart"] != null)
                {
                    if (Session["Cart"].ToString().Contains(CPECode))
                    {
                        Response.Write("<script>alert('Course already added in cart');</script>");
                    }
                    else
                    {
                        Session["Cart"] = Session["Cart"] + "," + CPECode;
                    }
                }
                else
                {
                    Session["Cart"] = CPECode;
                }
            }

        }
        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}