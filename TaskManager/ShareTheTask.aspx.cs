using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    public partial class ShareTheTask : System.Web.UI.Page
    {
        DBHandler DB;
        TTask selectedTask;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DB == null)
                DB = DBHandler.Instance;

            if (Session["selectedTask"] != null)
            {
                int taskindex = (int)Session["selectedTask"];
                selectedTask = DB.db.TTasks.FirstOrDefault(t => t.ID == taskindex);

                TaskCreatedByLabel.Text = "Created by " + DB.getUserByID((int)selectedTask.CreatorID).Username;
                TaskLastEditedByLabel.Text = "Last edited by " + DB.getUserByID((int)selectedTask.LastEditorID).Username + " on " + selectedTask.LastEditedDate;
                StartDateEndDateLabel.Text = "Should be done in: "+ selectedTask.StartDate + " - " + selectedTask.EndDate;
                TextOfTaskLabel.Text = selectedTask.TextOfTask;

                if (Session["SharingTaskError"] != null)
                    SharingTasksError.Text = (string)Session["SharingTaskError"];
                else
                    SharingTasksError.Text = String.Empty;
            }
        }

        protected void ShareButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(UsersEmailsToShare.Text))
            {
                string[] inputEmails = UsersEmailsToShare.Text.Split(',');

                if (inputEmails.Length > 0)
                {
                    List<string> invalidEmails = new List<string>();
                    List<string> noUsersWithThisEmail = new List<string>();

                    for (int i = 0; i < inputEmails.Length; i++)
                    {
                        inputEmails[i] = inputEmails[i].Replace(" ", "");

                        bool isValid = inputEmails[i].IsValidEmail();

                        if (!isValid)
                            invalidEmails.Add(inputEmails[i]);
                        else
                        {
                            User userWithCurrentEmail = new User();

                            userWithCurrentEmail = DB.getUserByEmail(inputEmails[i]);

                            if (userWithCurrentEmail == null)
                                noUsersWithThisEmail.Add(inputEmails[i]);
                        }
                    }

                    if (invalidEmails.Count > 0)
                    {
                        string errorText = "";

                        if (invalidEmails.Count > 1)
                            errorText = "You have entered invalid emails:<br/>";
                        else
                            errorText = "You have entered invalid email: ";

                        foreach (string str in invalidEmails)
                            errorText += str + ", ";

                        errorText = errorText.TrimEnd(", ".ToCharArray());

                        Session["SharingTaskError"] = errorText; Response.Redirect(Request.RawUrl);
                    }
                    else if (noUsersWithThisEmail.Count > 0)
                    {
                        string errorText = "";

                        if (noUsersWithThisEmail.Count > 1)
                            errorText = "There are no users with this emails in the system:</br>";
                        else
                            errorText = "There is no user with this email in the system: ";

                        foreach (string str in noUsersWithThisEmail)
                            errorText += str + "</br";

                        errorText = errorText.TrimEnd(", ".ToCharArray());

                        Session["SharingTaskError"] = errorText; Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        string resultText = "";

                        for (int i = 0; i < inputEmails.Length; i++)
                        {
                            User currentUser = DB.getUserByEmail(inputEmails[i]);
                            TTask[] userTasks = DB.getUserTasks(currentUser);

                            if (userTasks.Contains(selectedTask))
                            {
                                resultText += "User " + currentUser.Username + " already has task: " + selectedTask.NameOfTask + "<br/>";
                            }
                            else
                            {
                                currentUser.AddNewTask(selectedTask.ID, ((User)Session["LoggerUser"]).ID);
                                resultText += "User " + currentUser.Username + " successfuly received the task!" + "<br/>";
                            }
                        }

                        DB.db.SaveChanges();

                        Session["sharingTasks"] = resultText; Response.Redirect("Default.aspx");
                    }
                }
            }
            else
            {
                Session["SharingTaskError"] = "Emails field cannot be left empty!"; Response.Redirect(Request.RawUrl);
            }
        }
    }
}