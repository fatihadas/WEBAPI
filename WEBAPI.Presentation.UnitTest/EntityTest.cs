using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEBAPI.Data.UnitOfWork;
using WEBAPI.Data.Repositories;
using WEBAPI.Data.Context;
using WEBAPI.Data.Model;

namespace WEBAPI.Presentation.UnitTest
{
    [TestClass]
    public class EntityTest
    {
        private ApiContext _dbContext;

        private IUnitOfWork _uow;
        private IRepository<User> _userRepository;
        private IRepository<Article> _articleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new ApiContext();
            
            _uow = new EFUnitOfWork(_dbContext);
            _userRepository = new EFRepository<User>(_dbContext);
            _articleRepository = new EFRepository<Article>(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext = null;
            _uow.Dispose();
        }


        [TestMethod]
        public void AddUser()
        {
            User user = new User
            {
                Name = "Fatih",
                Username = "Adaş",
                CreatedDate = DateTime.Now,
                Email = "fatih@fatihadas.com",
                Password = "123456"
            };

            _userRepository.Add(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void GetUser()
        {
            User user = _userRepository.GetById(2);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = _userRepository.GetById(1);

            user.Name = "Fatih";

            _userRepository.Update(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void DeleteUser()
        {
            User user = _userRepository.GetById(1);

            _userRepository.Delete(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }
    }
}
