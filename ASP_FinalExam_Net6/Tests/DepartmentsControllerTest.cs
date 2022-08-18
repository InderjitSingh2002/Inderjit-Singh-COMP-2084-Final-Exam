using ASP_FinalExam_Net6.Controllers;
using ASP_FinalExam_Net6.Data;
using ASP_FinalExam_Net6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ASP_FinalExam_Net6.Tests
{
    public class DepartmentsControllerTest
    {
        [Fact]
        public async Task Index_Basic_Test()
        {
            using (var testDb = new ApplicationDbContext(this.GetTestDbOpts()))
            {
                var testCtrl = new DepartmentsController(testDb);
                var res = await testCtrl.Index();
                var resVr = Assert.IsType<ViewResult>(res);
                Assert.IsAssignableFrom<IEnumerable<Department>>(resVr.ViewData.Model);
            }
        }

        [Fact]
        public async Task Add_And_Remove_Test()
        {
            using (var testDb = new ApplicationDbContext(this.GetTestDbOpts()))
            {
                var testCtrl = new DepartmentsController(testDb);
                var fakeDepartments = MakeFakeDepartments(3);

                // Adding Departments
                foreach (var department in fakeDepartments)
                {
                    var res = await testCtrl.Create(department);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }

                // Testing Saved Values
                var idxRes = await testCtrl.Index();
                var idxResVr = Assert.IsType<ViewResult>(idxRes);
                var returnedDepartments = Assert.IsAssignableFrom<IEnumerable<Department>>(idxResVr.ViewData.Model);
                foreach (var department in fakeDepartments)
                {
                    Assert.Contains(department, returnedDepartments);
                }

                // Removing All Existing Departments
                foreach (var department in returnedDepartments)
                {
                    var res = await testCtrl.DeleteConfirmed(department.Id);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }
            }
        }

        // Create the DB Context to use (note this should be a test database)
        private DbContextOptions<ApplicationDbContext> GetTestDbOpts()
        {
            var opts = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "FinalExamDB").Options;
            return opts;
        }

        private List<Department> MakeFakeDepartments(int i)
        {
            var departments = new List<Department>();
            for (int j = 0; j < i; j++)
            {
                departments.Add(new Department
                {
                    Name = $"test{j}",
                    EmployeeCount = j
                });
            }
            return departments;
        }
    }
}
