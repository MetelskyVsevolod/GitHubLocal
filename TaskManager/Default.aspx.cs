using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManager
{
    public partial class _Default : Page
    {
        DBHandler DB;
        const int CellsInRow = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DB == null)
                DB = DBHandler.Instance;

            if (Session["LoggerUser"] == null)
                Response.Redirect("Login.aspx");

            if (Session["sharingTasks"] != null)
                sharingTasks.Text = (string)Session["sharingTasks"];
            else
                sharingTasks.Text = String.Empty;

            Session["sharingTasks"] = null;

            User loggedUser = (User)Session["LoggerUser"];

            bool haveAnyTasks = false;

            var tasks = DB.getUserTasks(loggedUser);

            if (tasks != null)
                haveAnyTasks = tasks.Length != 0;

            if (haveAnyTasks)
                makeTable();

            TaskCount.Visible = !haveAnyTasks;
        }

        void makeTable()
        {
            User loggedUser = (User)Session["LoggerUser"];

            TableRow row = new TableRow();
            row.createNewRow(CellsInRow, "ID", "Created by", "Task name", "Task text", "Start date", "End date", "Last edited by", "Last edited date", "Shared by");

            Table1.Rows.Add(row);

            TTask[] userTasks = DB.getUserTasks(loggedUser);

            foreach (TTask task in userTasks)
            {
                string[] taskInfo = DB.getTaskInfoForTable(task, loggedUser);

                row = new TableRow();

                row.createNewRow(CellsInRow, taskInfo);

                Table1.Rows.Add(row);

                row.addButtonToCell(9, new Button() {CssClass= "btn btn - primary" }, "Edit", new EventHandler(delegate { sentToEditNAddPage(task); }));
                row.addButtonToCell(10, new Button() { CssClass = "btn btn-danger" }, "Delete", new EventHandler(delegate { deleteTask(task.ID); }));
                row.addButtonToCell(11, new Button() { CssClass = "btn btn-info"}, "Share", new EventHandler(delegate { Session["selectedTask"] = task.ID; Response.Redirect("ShareTheTask.aspx");}));
            }
        }
        void deleteTask(int taskID)
        {
            TTask task = DB.db.TTasks.FirstOrDefault(b => b.ID == taskID);

            if (task != null)
            {
                DB.DeleteTask(task.ID);
                Response.Redirect(Request.RawUrl);
            }
        }

        void sentToEditNAddPage()
        {
            Response.Redirect("EditAdd.aspx");
        }

        void sentToEditNAddPage(TTask taskToEdit)
        {
            Session["TaskToEdit"] = taskToEdit.ID;
            sentToEditNAddPage();
        }
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            sentToEditNAddPage();
        }
    }
}