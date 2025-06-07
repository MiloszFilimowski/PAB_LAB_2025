using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CarDealer.WebApi.Controllers;
using CarDealer.Application.Interfaces;
using CarDealer.Domain;

namespace CarDealer.Tests
{
    public class CarsControllerTests
    {
        private readonly Mock<ICarRepository> _mockRepo;
        private readonly CarsController _controller;

        public CarsControllerTests()
        {
            _mockRepo = new Mock<ICarRepository>();
            _controller = new CarsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkObjectResult_WithListOfCars()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1 },
                new Car { Id = 2 }
            };
            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(cars);

            var result = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Car>>(ok.Value);
            Assert.Equal(2, ((List<Car>)returned).Count);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCarIsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync((Car)null!);

            var result = await _controller.GetById(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOkObjectResult_WhenCarExists()
        {
            var car = new Car { Id = 1 };
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(car);

            var result = await _controller.GetById(1);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Car>(ok.Value);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var car = new Car { Id = 42 };
            _mockRepo.Setup(r => r.AddAsync(car))
                     .Returns(Task.CompletedTask);

            var result = await _controller.Create(car);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetById), created.ActionName);
            Assert.Equal(car, created.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenIdsMatch()
        {
            var car = new Car { Id = 5 };
            _mockRepo.Setup(r => r.UpdateAsync(car))
                     .Returns(Task.CompletedTask);

            var result = await _controller.Update(5, car);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var car = new Car { Id = 10 };

            var result = await _controller.Update(1, car);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            _mockRepo.Setup(r => r.DeleteAsync(7))
                     .Returns(Task.CompletedTask);

            var result = await _controller.Delete(7);
            Assert.IsType<NoContentResult>(result);
        }
    }
} 