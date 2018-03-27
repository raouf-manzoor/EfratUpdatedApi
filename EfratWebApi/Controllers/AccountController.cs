using EfratWebApi.EfratDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EfratWebApi.Controllers
{
    [EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    public class AccountController : ApiController
    {
        // GET: Account
        [HttpPost]
        public dynamic SignUp(User input)
        {
            try
            {
                Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
                dbContext.Users.Add(input);
                dbContext.SaveChanges();
                return new
                {
                    IsSignUp = true
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    IsSignUp = false
                };
            }
        }
        [HttpPost]
        public dynamic SignIn(User input)
        {
            try
            {
                Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
                var user = dbContext.Users.Single(e => e.Email == input.Email && e.Password == input.Password);
                return new
                {
                    IsSignIn = true,
                    signInUser = user
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    IsSignIn = false,
                };
            }
        }
        [HttpPost]
        public dynamic GetPreviousOrders(User input)
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.RentsInfoes.Where(e => e.UserID == input.UserId).ToList();
        }
        [HttpPost]
        public dynamic GetAllBranches()
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.Branches.ToList();
        }
        [HttpPost]
        public dynamic GetListOfAvailableCars(int branchId)
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.CarsForRents.Where(e => e.Available && e.BranchID == branchId).ToList();
        }
        [HttpPost]
        public dynamic PlaceAnOrder(RentsInfo input)
        {
            
            try
            {
                Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
                dbContext.RentsInfoes.Add(input);
                var carToUpdate = dbContext.CarsForRents.Single(e => e.CarID == input.CarID);
                carToUpdate.Available = false;
                dbContext.SaveChanges();
                return new
                {
                    IsOrderPlaced = true
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    IsOrderPlaced = false
                };
            }
        }

        // For Empolyee And Admin ALso
        [HttpPost]
        public dynamic GetListOfOrders()
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.RentsInfoes.Where(e => !e.ReturnDate.HasValue).ToList();
        }


        [HttpPost]
        public dynamic SetCarToAvailable(RentsInfo input)
        {

            try
            {
                Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
                var orderToUpdate = dbContext.RentsInfoes.Single(e => e.Id == input.Id);
                var carToUpdate = dbContext.CarsForRents.Single(e => e.CarID == input.CarID);
                orderToUpdate.ReturnDate = input.ReturnDate;
                carToUpdate.Available = true;
                //carToUpdate.KM = input.K;

                // Make Dto For RentInfos With Additional Property of CurrentKms

                dbContext.SaveChanges();
                return new
                {
                    IsCarAvailable = true
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    IsCarAvailable = false
                };
            }
        }

        [HttpPost]
        public dynamic GetAllUsers()
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.Users.ToList();
        }

        // Update User Method Uodate Whole User Project 

        [HttpPost]
        public dynamic DeleteUser(int userId)
        {
            
            try
            {
                Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
                var userToDelete = dbContext.Users.Single(e => e.UserId == userId);
                dbContext.Users.Remove(userToDelete);
                dbContext.SaveChanges();
                return new
                {
                    IsUserDeleted = true
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    IsUserDeleted = false
                };
            }
        }

        [HttpPost]
        public dynamic GetAllVehicles()
        {
            Project_CarRentalEntities1 dbContext = new Project_CarRentalEntities1();
            return dbContext.CarsForRents.ToList();
        }


        // Add Edit Delete For Vehicle Functions 
    }
}