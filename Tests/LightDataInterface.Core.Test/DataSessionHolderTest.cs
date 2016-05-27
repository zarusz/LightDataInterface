using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LightDataInterface.Core.Test
{
    [TestClass]
    public class DataSessionHolderTest
    {
        private DataSessionHolder _dataSessionHolder;
        private Mock<IDataSession> _dataSessionMock;
        private Mock<IDataSessionFactory> _dataSessionFactoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _dataSessionFactoryMock = new Mock<IDataSessionFactory>();
            _dataSessionMock = new Mock<IDataSession>();
            _dataSessionHolder = new DataSessionHolder(_dataSessionFactoryMock.Object);
        }

        [TestMethod]
        public void It_has_default_name_set()
        {
            // arrange

            // act

            // assert
            Assert.AreSame(_dataSessionHolder.DefaultName, "default");
        }


        [TestMethod]
        public void It_creates_new_datasession_if_not_exists_in_holder()
        {
            // arrange
            const string name = "some name";
            _dataSessionFactoryMock.Setup(x => x.CreateDataSession(name)).Returns(_dataSessionMock.Object);

            // act
            var dataSession = _dataSessionHolder.GetByName(name);

            // assert
            _dataSessionFactoryMock.Verify(x => x.CreateDataSession(name), Times.Once);
            Assert.AreSame(dataSession, _dataSessionMock.Object);
        }

        [TestMethod]
        public void It_reuses_existing_datasession_from_holder()
        {
            // arrange
            const string name = "some name";
            _dataSessionFactoryMock.Setup(x => x.CreateDataSession(name)).Returns(_dataSessionMock.Object);
            // ensure data session created
            _dataSessionHolder.GetByName(name);

            // act
            var dataSession = _dataSessionHolder.GetByName(name);

            // assert
            _dataSessionFactoryMock.Verify(x => x.CreateDataSession(name), Times.Once);
            Assert.AreSame(dataSession, _dataSessionMock.Object);
        }

        [TestMethod]
        public void It_disposes_all_datasessions_in_holder_uppon_dispose()
        {
            // arrange
            const string name1 = "some name";
            const string name2 = "some name 2";
            var dataSessionMock1 = new Mock<IDataSession>();
            var dataSessionMock2 = new Mock<IDataSession>();
            _dataSessionFactoryMock.Setup(x => x.CreateDataSession(name1)).Returns(dataSessionMock1.Object);
            _dataSessionFactoryMock.Setup(x => x.CreateDataSession(name2)).Returns(dataSessionMock2.Object);

            // ensure data sessions created
            _dataSessionHolder.GetByName(name1);
            _dataSessionHolder.GetByName(name2);

            // act
            _dataSessionHolder.Dispose();

            // assert
            dataSessionMock1.Verify(x => x.Dispose(), Times.Once);
            dataSessionMock2.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
