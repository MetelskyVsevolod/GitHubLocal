using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggerUser"] != null)
                Response.Redirect("Default.aspx");

            if (Session["IAmAlreadyRegistered"] != null)
            {
                bool IAmAlreadyRegistered = (bool)Session["IAmAlreadyRegistered"];

                IAmRegisteredButton.Visible = !IAmAlreadyRegistered;
                CreateNewUserButton.Visible = IAmAlreadyRegistered;

                PasswordLabel.Visible = true;
                PasswordLoginTextbox.Visible = true;

                UsernameLabel.Visible = true;
                UsernameLoginTextbox.Visible = true;

                RegisterButton.Visible = !IAmAlreadyRegistered;
                LoginButton.Visible = IAmAlreadyRegistered;

                ErrorInEmail.Visible = !IAmAlreadyRegistered;
                EmailLabel.Visible = !IAmAlreadyRegistered;
                EmailLoginTextbox.Visible = !IAmAlreadyRegistered;
            }

            if (Session["RegisterSuccess"] != null)
                RegisterSuccess.Text = (string)Session["RegisterSuccess"];
            else
                RegisterSuccess.Text = String.Empty;

            Session["RegisterSuccess"] = null;

                if (Session["LoginError"] != null)
                LoginError.Text = (string)Session["LoginError"];
            else
                LoginError.Text = String.Empty;

            Session["LoginError"] = null;

            if (Session["ErrorInPassword"] != null)
                PasswordErrorLabel.Text = (string)Session["ErrorInPassword"];
            else
                PasswordErrorLabel.Text = String.Empty;

            Session["ErrorInPassword"] = null;

            if (Session["ErrorInUsername"] != null)
                UsernameErrorLabel.Text = (string)Session["ErrorInUsername"];
            else
                UsernameErrorLabel.Text = String.Empty;

            Session["ErrorInUsername"] = null;

            if (Session["ErrorInEmail"] != null)
                ErrorInEmail.Text = (string)Session["ErrorInEmail"];
            else
                ErrorInEmail.Text = String.Empty;

            Session["ErrorInEmail"] = null;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Session["IAmAlreadyRegistered"] = true;
            Response.Redirect(Request.RawUrl);
        }

        protected void CreateNewUserButton_Click(object sender, EventArgs e)
        {
            Session["IAmAlreadyRegistered"] = false;
            Response.Redirect(Request.RawUrl);
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = UsernameLoginTextbox.Text;
            string password = PasswordLoginTextbox.Text;
            string email = EmailLoginTextbox.Text;

            if (String.IsNullOrEmpty(password))
                Session["ErrorInPassword"] = "Field cannot be empty!";

            if (String.IsNullOrEmpty(username))
                Session["ErrorInUsername"] = "Field cannot be empty!";

            if (String.IsNullOrEmpty(email))
                Session["ErrorInEmail"] = "Field cannot be empty!";

            if (Session["ErrorInUsername"] != null || 
                Session["ErrorInPassword"] != null ||
                Session["ErrorInEmail"] != null)
                Response.Redirect(Request.RawUrl);

            if (!email.IsValidEmail())
            {
                Session["ErrorInEmail"] = "Email is invalid";
                Response.Redirect(Request.RawUrl);
            }

            User user = DBHandler.Instance.getUserByUsername(UsernameLoginTextbox.Text);

            if (user == null)
            {
                if (DBHandler.Instance.getUserByEmail(EmailLoginTextbox.Text) != null)
                {
                    Session["LoginError"] = "User with the same email already exists!";
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Session["RegisterSuccess"] = "User was successfully registered! You can now log in.";
                    DBHandler.Instance.addNewUser(username, password, email);
                    Session["IAmAlreadyRegistered"] = true;
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                if (user.Email == EmailLoginTextbox.Text)
                    Session["LoginError"] = "User with the same name and email already exists!";
                else
                    Session["LoginError"] = "User with the same username already exists!";

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void LoginButton_Click1(object sender, EventArgs e)
        {
            bool toRedirect = false;

            if (String.IsNullOrEmpty(UsernameLoginTextbox.Text))
            {
                Session["ErrorInUsername"] = "Field cannot be empty!";
                toRedirect = true;
            }
            if (String.IsNullOrEmpty(PasswordLoginTextbox.Text))
            {
                Session["ErrorInPassword"] = "Field cannot be empty!";
                toRedirect = true;
            }

            if(toRedirect)
                Response.Redirect(Request.RawUrl);

            User user = DBHandler.Instance.getUserByUsername(UsernameLoginTextbox.Text);

            if (user == null)
            {
                Session["LoginError"] = "Username is wrong!";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                if (user.Password == PasswordLoginTextbox.Text)
                {
                    Session["LoggerUser"] = user;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Session["LoginError"] = "Password is wrong!";
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}