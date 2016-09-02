using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager
{
    public class DBHandler
    {
        public TaskManagerDBEntities db = new TaskManagerDBEntities();

        public User getUserByID(int ID)
        {
            return db.Users.FirstOrDefault(u => u.ID == ID);
        }

        public TTask getTaskByID(int ID)
        {
            return db.TTasks.FirstOrDefault(u => u.ID == ID);
        }

        public void addNewUser(string username, string password, string email)
        {
            User newUser = new User(getFreeIDForUser(), username, email, password);
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public void DeleteTask(int taskID)
        {
            TTask task = getTaskByID(taskID);
            User[] usersOfTask = getTasksUser(task);

            foreach (User us in usersOfTask)
                us.DeleteTask(task.ID);

            db.TTasks.Remove(task);
            db.SaveChanges();
        }

        public int getFreeIDForUser()
        {
            int ID = 0;

            while (true)
            {
                User user = getUserByID(ID);

                if (user == null)
                    return ID;

                ID++;
            }
        }

        public int getFreeIDForTask()
        {
            int ID = 0;

            while (true)
            {
                TTask task = getTaskByID(ID);

                if (task == null)
                    return ID;

                ID++;
            }
        }

        public User getUserByEmail(string email)
        {
            foreach (User us in db.Users)
                if (us.Email == email)
                    return us;

            return null;
        }

        public TTask[] getUserTasks(User user)
        {
            if (String.IsNullOrEmpty(user.Tasks))
                return new TTask[] { };

            if (!user.Tasks.Contains(","))
            {
                return new TTask[]
                {
                   getTaskByID(Int32.Parse(user.Tasks))
                };
            }

            string[] nums = user.Tasks.Split(',');

            List<TTask> result = new List<TTask>();

            for (int i = 0; i < nums.Length; i++)
            {
                int taskIndex = Int32.Parse(nums[i]);
                TTask task = db.TTasks.FirstOrDefault(t => t.ID == taskIndex);

                if (task != null)
                    result.Add(task); 
            }

            return result.ToArray();
        }


        public User[] getTasksUser(TTask task)
        {
            if (task.Users == null)
                return new User[] { };

            string[] nums = task.Users.Split(',');

            User[] result = new User[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                int taskIndex = Int32.Parse(nums[i]);
                result[i] = db.Users.FirstOrDefault(t => t.ID == taskIndex);
            }

            return result;
        }

        public User getUserByUsername(string username)
        {
            foreach (User us in db.Users)
                if (us.Username == username)
                        return us;

            return null;
        }

        int getTaskPosFromUser(User user, TTask taskToLook)
        {
            TTask[] tasks = getUserTasks(user);

            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].ID == taskToLook.ID)
                {
                    return i;
                }
            }

            return -1;
        }

        User getUserWhoSharedTask(int taskPos, User owner)
        {
            if (String.IsNullOrEmpty(owner.TasksSharedBy))
                return null;

            if (!owner.TasksSharedBy.Contains(','))
                if (taskPos == 0)
                {
                    return getUserByID(Int32.Parse(owner.TasksSharedBy));
                }

            string[] sharedIDs = owner.TasksSharedBy.Split(',');

            for (int i = 0; i < sharedIDs.Length; i++)
                if (i == taskPos)
                    return getUserByID(Int32.Parse(sharedIDs[i]));

            return null;
        }

        public string[] getTaskInfoForTable(TTask task, User user)
        {
            string[] taskInfo = new string[9];

            taskInfo[0] = task.ID.ToString();
            taskInfo[1] = getUserByID((int)task.CreatorID).Username;
            taskInfo[2] = task.NameOfTask.ToString();
            taskInfo[3] = task.TextOfTask.ToString();
            taskInfo[4] = task.StartDate.ToString();
            taskInfo[5] = task.EndDate.ToString();
            taskInfo[6] = getUserByID((int)task.LastEditorID).Username;
            taskInfo[7] = task.LastEditedDate.ToString();
            
            int taskPos = getTaskPosFromUser(user, task);

            User us = getUserWhoSharedTask(taskPos, user);

            taskInfo[8] = us.Username.ToString();

            return taskInfo;
        }

        private static DBHandler instance;
        public static DBHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBHandler();

                return instance;
            }
        }
    }
}