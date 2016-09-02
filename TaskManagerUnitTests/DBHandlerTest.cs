using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;
using System.Data.Entity;
using TaskManager.Models;

namespace TaskManagerUnitTests
{
    [TestClass]
    public class DBHandlerTest
    {
        [TestMethod]
        public void getUserByEmail()
        {
            Assert.IsNull(DBHandler.Instance.getUserByEmail("ssssssss"));
        }
    }
}
