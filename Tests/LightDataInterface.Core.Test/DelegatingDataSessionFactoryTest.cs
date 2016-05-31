using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LightDataInterface.Core.Test
{
    [TestClass]
    public class DelegatingDataSessionFactoryTest
    {
        private DelegatingDataSessionFactory _delegatingDataSessionFactory;
        private Mock<IDataSessionFactory> _dataSessionFactoryMock;
        private Mock<IDataSession> _dataSessionMock;

        [TestInitialize]
        public void Setup()
        {
            _delegatingDataSessionFactory = new DelegatingDataSessionFactory();
            _dataSessionFactoryMock = new Mock<IDataSessionFactory>();
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
                _delegatingDataSessionFactory.CreateDataSession("uknown name");
            }
            catch (DataAccessException)
            {
                thrown = true;
            }

            // assert
            Assert.IsTrue(thrown, "DataAccessException was not thrown.");
        }

        [TestMethod]
        public void It_delegates_creation_to_factory_method()
        {
            // arrange
            const string name = "name";
            _dataSessionFactoryMock.Setup(x => x.CreateDataSession(name)).Returns(_dataSessionMock.Object);
            _delegatingDataSessionFactory.AddFactoryForName(name, _dataSessionFactoryMock.Object);

            // act
            _delegatingDataSessionFactory.CreateDataSession(name);

            // assert
            _dataSessionFactoryMock.Verify(x => x.CreateDataSession(name), Times.Once);
        }
    }
}
