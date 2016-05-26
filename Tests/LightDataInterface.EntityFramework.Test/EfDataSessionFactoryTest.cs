using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LightDataInterface.EntityFramework.Test
{
    [TestClass]
    public class EfDataSessionFactoryTest
    {
        private const string DefaultContextName = "default";
        private EfDataSessionFactory _efDataSessionFactory;
        private Mock<Func<string, IDataSession>> _factoryMethodMock;
        private Mock<IDataSession> _dataSessionMock;

        [TestInitialize]
        public void Setup()
        {
            _factoryMethodMock = new Mock<Func<string, IDataSession>>();
            _efDataSessionFactory = new EfDataSessionFactory(DefaultContextName, _factoryMethodMock.Object);
            _dataSessionMock = new Mock<IDataSession>();
        }


        [TestMethod]
        public void It_throws_exception_if_name_not_recognized()
        {
            // arrange
            var thrown = false;

            // act
            try
            {
                _efDataSessionFactory.CreateDataSession("uknown name");
            }
            catch (DataAccessException)
            {
                thrown = true;
            }

            // assert
            Assert.IsTrue(thrown, "DataAccessException was not thrown.");
        }

        [TestMethod]
        public void It_uses_the_default_name()
        {
            // arrange
            _factoryMethodMock.Setup(x => x(DefaultContextName)).Returns(_dataSessionMock.Object);

            // act
            var dataSession = _efDataSessionFactory.CreateDataSession();

            // assert
            Assert.AreSame(_dataSessionMock.Object, dataSession);
        }

        [TestMethod]
        public void It_delegates_creation_to_factory_method()
        {
            // arrange
            _factoryMethodMock.Setup(x => x("name")).Returns(_dataSessionMock.Object);

            // act
            _efDataSessionFactory.CreateDataSession("name");

            // assert
            _factoryMethodMock.Verify(x => x("name"), Times.Once);
        }
    }
}
