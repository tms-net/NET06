using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using TMS.Homework.MVConsole.Service;
using TMS.HomeWork.MVConsole.App;

namespace TMS.Homework.MVConsole.App.Tests
{
    public class ControllerTests
    {
        private DirectoryInfo _rootPath;

        [SetUp]
        public void Setup()
        {
            var rootPath = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (rootPath != null && !rootPath.GetFiles("*.sln").Any())
            {
                rootPath = rootPath.Parent;
            }
            _rootPath = rootPath;
        }

        [Test]
        public void RunShouldSaveStudentObjectsToCsvFile()
        {
            // arrange
            var serviceMock = new Mock<ICsvService>();
            var controller = new Controller(
                new UI.UI(),
                serviceMock.Object);
            var now = DateTime.Now;

            // act
            controller.Run();

            Action<ICsvService> action =
                 service =>
                     service.SaveToCSV(Enumerable.Empty<Student>());

            //serviceMock.Verify(action);

            // assert
            serviceMock.Verify(service =>
                //service.SaveToCSV(Enumerable.Empty<Student>()));
                service.SaveToCSV(It.IsAny<IEnumerable<Student>>()));
            //It.Is<IEnumerable<Student>>(
            //    actualStudents => actualStudents != null)));
        }
    }
}