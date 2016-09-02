using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class EditAdd : System.Web.UI.Page
    {
        DBHandler DB;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DB == null)
                DB = DBHandler.Instance;

            EditOrAddLabel.Text = (Session["TaskToEdit"] == null) ? "Add new task" : "Edit task";
            SubmitChanges.Text = (Session["TaskToEdit"] == null) ? "Create task" : "Save changes";

            for (int i = 1; i <= 31; i++)
            {
                StartTaskDay.Items.Add(i.ToString());
                EndTaskDay.Items.Add(i.ToString());
            }

            for (int i = 1; i <= 12; i++)
            {
                StartTaskMonth.Items.Add(i.ToString());
                EndTaskMonth.Items.Add(i.ToString());
            }

            for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 20; i++)
            {
                StartTaskYear.Items.Add(i.ToString());
                EndTaskYear.Items.Add(i.ToString());
            }

            if(!IsPostBack)
                if (Session["TaskToEdit"] != null)
                {
                    TTask taskToEdit = DB.getTaskByID((int)Session["TaskToEdit"]);
                    NameOfTheTaskTB.Text = taskToEdit.NameOfTask;
                    TextOFTheTaskTB.Text = taskToEdit.TextOfTask;

                    StartTaskDay.SelectedIndex = EndTaskDay.SelectedIndex = taskToEdit.StartDate.Value.Day;
                    StartTaskMonth.SelectedIndex = EndTaskMonth.SelectedIndex = taskToEdit.StartDate.Value.Month;
                    EndTaskYear.SelectedIndex = StartTaskYear.SelectedIndex = 0;
                }
        }

        protected void SubmitChanges_Click(object sender, EventArgs e)
        {
            TTask taskToEdit = new TTask();

            if (Session["TaskToEdit"] != null)
            {
                taskToEdit = DB.getTaskByID((int)Session["TaskToEdit"]);
            }
            else
                taskToEdit.ID = DB.getFreeIDForTask();

            taskToEdit.NameOfTask = NameOfTheTaskTB.Text;
            taskToEdit.TextOfTask = TextOFTheTaskTB.Text;

            DateTime newStartDate = new DateTime(
                Int32.Parse(StartTaskYear.Items[StartTaskYear.SelectedIndex].Text),
                Int32.Parse(StartTaskMonth.Items[StartTaskMonth.SelectedIndex].Text),
                Int32.Parse(StartTaskDay.Items[StartTaskDay.SelectedIndex].Text));

            taskToEdit.StartDate = newStartDate;

            DateTime newEndDate = new DateTime(
                Int32.Parse(EndTaskYear.Items[EndTaskYear.SelectedIndex].Text),
                Int32.Parse(EndTaskMonth.Items[EndTaskMonth.SelectedIndex].Text),
                Int32.Parse(EndTaskDay.Items[EndTaskDay.SelectedIndex].Text));

            taskToEdit.EndDate = newEndDate;

            taskToEdit.LastEditorID = ((User)Session["LoggerUser"]).ID;
            taskToEdit.LastEditedDate = DateTime.Now;

            taskToEdit.AddNewUser((int)taskToEdit.LastEditorID);

            if (Session["TaskToEdit"] == null)
            {
                taskToEdit.CreatorID = taskToEdit.LastEditorID;
                DB.db.TTasks.Add(taskToEdit);
            }

            DB.db.SaveChanges();

            if (Session["TaskToEdit"] == null)
            {
                DB.getUserByID((int)taskToEdit.LastEditorID).AddNewTask(taskToEdit.ID, (int)taskToEdit.CreatorID);
            }

            Session["TaskToEdit"] = null;

            Response.Redirect("Default.aspx");
        }
    }
}