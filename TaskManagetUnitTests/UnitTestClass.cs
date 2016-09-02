using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;
using System.Linq;
using System.Web.UI.WebControls;

namespace TaskManagetUnitTests
{
    [TestClass]
    public class UnitTestClass
    {
        [TestMethod]
        public void getUserByID()
        {
            User user = new User();
            user.ID = 999;

            DBHandler.Instance.db.Users.Add(user);

            DBHandler.Instance.db.SaveChanges();

            Assert.AreEqual(user, DBHandler.Instance.getUserByID(999));

            DBHandler.Instance.db.Users.Remove(user);

            DBHandler.Instance.db.SaveChanges();
        }
        [TestMethod]
        public void getTaskByID()
        {
            TTask task = new TTask();
            task.ID = 999;

            DBHandler.Instance.db.TTasks.Add(task);

            DBHandler.Instance.db.SaveChanges();

            Assert.AreEqual(task, DBHandler.Instance.getTaskByID(999));

            DBHandler.Instance.db.TTasks.Remove(task);

            DBHandler.Instance.db.SaveChanges();
        }

        [TestMethod]
        public void addNewUser()
        {
            string name = "MyPersonalTestName";
            string password = "UltraPower";
            string email = "scachMosk";

            DBHandler.Instance.addNewUser(name, password, email);

            User user = DBHandler.Instance.getUserByUsername(name);

            Assert.AreEqual(user.Username, name);
            Assert.AreEqual(user.Password, password);
            Assert.AreEqual(user.Email, email);

            DBHandler.Instance.db.Users.Remove(user);

            DBHandler.Instance.db.SaveChanges();
        }

       [TestMethod]
        public void DeleteTask()
        {
            TTask task = new TTask() { ID = 999 };

            DBHandler.Instance.db.TTasks.Add(task);

            DBHandler.Instance.db.SaveChanges();

            DBHandler.Instance.DeleteTask(task.ID);

            DBHandler.Instance.db.SaveChanges();

            Assert.IsNull(DBHandler.Instance.getTaskByID(999));
        }

        [TestMethod]
        public void getFreeIDForUser()
        {
            User user1 = new User() {ID = 0};
            User user3 = new User() { ID = 1 };

            DBHandler.Instance.db.Users.Add(user1);
            DBHandler.Instance.db.Users.Add(user3);

            DBHandler.Instance.db.SaveChanges();

            Assert.AreEqual(2, DBHandler.Instance.getFreeIDForUser());

            DBHandler.Instance.db.Users.Remove(user1);
            DBHandler.Instance.db.Users.Remove(user3);
            DBHandler.Instance.db.SaveChanges();
        }

        [TestMethod]
        public void getUserByEmail()
        {
            User user = new User();
            user.Email = "MyEmail";

            DBHandler.Instance.db.Users.Add(user);

            DBHandler.Instance.db.SaveChanges();

            Assert.AreEqual(user, DBHandler.Instance.getUserByEmail(user.Email));

            DBHandler.Instance.db.Users.Remove(user);

            DBHandler.Instance.db.SaveChanges();
        }
        [TestMethod]
        public void getUserTasks()
        {
            User user = new User();

            TTask task1 = new TTask() { ID = 1 };
            TTask task2 = new TTask() { ID = 2 };
            TTask task3 = new TTask() { ID = 3 };
            TTask task4 = new TTask() { ID = 4 };

            user.Tasks = "1,2,3,4";

            DBHandler.Instance.db.TTasks.Add(task1);
            DBHandler.Instance.db.TTasks.Add(task2);
            DBHandler.Instance.db.TTasks.Add(task3);
            DBHandler.Instance.db.TTasks.Add(task4);
            DBHandler.Instance.db.Users.Add(user);

            DBHandler.Instance.db.SaveChanges();
            
            Assert.AreEqual(true, Enumerable.SequenceEqual(new TTask[] { task1, task2, task3, task4 }, DBHandler.Instance.getUserTasks(user)));

            DBHandler.Instance.db.Users.Remove(user);
            DBHandler.Instance.db.TTasks.Remove(task1);
            DBHandler.Instance.db.TTasks.Remove(task2);
            DBHandler.Instance.db.TTasks.Remove(task3);
            DBHandler.Instance.db.TTasks.Remove(task4);
            DBHandler.Instance.db.SaveChanges();
        }

        [TestMethod]
        public void IsValidEmail()
        {
            string emai1 = "fff@gmail.com";
            string emai2 = String.Empty;
            string emai3 = "                   ";
            string emai4 = "ddfsff22432";
            string emai5 = "akra@gmail.com";

            Assert.AreEqual(true, emai1.IsValidEmail());
            Assert.AreEqual(false, emai2.IsValidEmail());
            Assert.AreEqual(false, emai3.IsValidEmail());
            Assert.AreEqual(false, emai4.IsValidEmail());
            Assert.AreEqual(false, (emai4 + emai1 + emai3 + emai4).IsValidEmail());
            Assert.AreEqual(true, emai5.IsValidEmail());
        }
        [TestMethod]
        public void addButtonToCell()
        {
            TableRow r = new TableRow();

            for (int i = 0; i < 10; i++)
                r.Cells.Add(new TableCell());

            Button button = new Button();

            const int celltoAdd = 5;

            EventHandler eh = new EventHandler(test);

            string ButtonBame = "TestButtonName";

            r.addButtonToCell(celltoAdd, button, ButtonBame, eh);

            Button buttonFromRow = (Button)r.Cells[celltoAdd].Controls[0];

            Assert.AreEqual(buttonFromRow.Text, ButtonBame);
            Assert.AreEqual(buttonFromRow, button);
        }

        [TestMethod]
        public void createNewRow()
        {
            TableRow r = new TableRow();

            int cellsCount = 6;

            string[] cellsTexts = new string[] { "My", "friend", "loves", "sushi", ".wut?", "ha", "That was funny" };

            r.createNewRow(cellsCount, cellsTexts);

            for (int i = 0; i < cellsCount; i++)
                Assert.AreEqual(r.Cells[i].Text, cellsTexts[i]);
        }

        void test(Object sender, EventArgs e)
        { }
    }
}
