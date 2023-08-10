using EmployeeHolidayTrackingSystem.Core.Contracts.Patterns;
using EmployeeHolidayTrackingSystem.Core.Contracts.Repositories;
using EmployeeHolidayTrackingSystem.Core.Contracts.Services;
using EmployeeHolidayTrackingSystem.Core.Exceptions;
using EmployeeHolidayTrackingSystem.Core.Models.User;
using EmployeeHolidayTrackingSystem.Data.Entities;
using EmployeeHolidayTrackingSystem.Service.Services;
using Moq;

namespace EmployeeHolidayTrackingSystem.Test.Services;

[TestClass]
public class UserServiceTest
{
   private IUserService userService;
   private readonly Mock<IUserRepository> userRepositoryMock = new();
   private readonly Mock<IUnitOfWork> unitOfWorkMock = new();
   private UserEntity userEntity;

   [TestInitialize]
   public void Setup()
   {
      userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);
      userEntity = new UserEntity();
   }

   [TestMethod]
   public async Task GetByIdMethodShoutReturnUser()
   {
      // Arrange
      userEntity = CreateUserEntity();

      userRepositoryMock
         .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
         .ReturnsAsync(userEntity);

      // Act
      var user = await userService.GetByIdAsync(userEntity.Id);

      // Assert
      Assert.AreEqual(userEntity.Id, user.Id);
   }

   [TestMethod]
   public async Task GetByIdMethodShoutThrowException()
      => await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await userService.GetByIdAsync(GetUserId()));

   [TestMethod]
   public async Task GetAllMethodShoutReturnUsers()
   {
      const int CountUsers = 5;

      var users = new HashSet<UserEntity>();

      for (int i = 0; i < CountUsers; i++)
      {
         userEntity = CreateUserEntity();

         users.Add(userEntity);
      }

      userRepositoryMock
         .Setup(x => x.GetAllAsync())
         .ReturnsAsync(users);

      var allUsers = await userService.GetAllAsync();

      Assert.AreEqual(users.Count, allUsers.Count());
   }

   [TestMethod]
   public async Task GetAllMethodShoutReturnException()
      => await Assert.ThrowsExceptionAsync<EmptyCollectionException>(async () => await userService.GetAllAsync());

   [TestMethod]
   public async Task CreateMethodShoutCorrectCreateUser()
   {
      userEntity = CreateUserEntity();

      userRepositoryMock
         .Setup(x => x.CreateAsync(userEntity));

      var userModel = CreateUserModel();

      var createdUser = await userService.CreateAsync(userModel);

      Assert.AreEqual(userEntity.UserName, createdUser.UserName);
   }

   [TestMethod]
   public async Task CreateMethodShoutThrowExceptionWhenUserAlreadyExist()
   {
      var userEntity = CreateUserEntity();

      userRepositoryMock
         .Setup(x => x.CreateAsync(It.IsAny<UserEntity>()));

      userRepositoryMock
         .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
         .ReturnsAsync(userEntity);

      await Assert.ThrowsExceptionAsync<ItemAlreadyExistException>(async () => await userService.CreateAsync(CreateUserModel()));
   }

   [TestMethod]
   public async Task EditMethodShoutEditUser()
   {
      const string EditUserName = "IChangedYourUserName";

      userEntity = CreateUserEntity();

      userRepositoryMock
         .Setup(x => x.Edit(userEntity));

      userRepositoryMock
         .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
         .ReturnsAsync(userEntity);

      userEntity.UserName = EditUserName;
      var userModel = CreateUserModel();

      var editedUser = await userService.EditAsync(userModel);

      Assert.AreEqual(userEntity.UserName, editedUser.UserName);
   }

   [TestMethod]
   public async Task EditMethodShoutThrowExceptionWhenUserIsNull()
      => await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await userService.EditAsync(CreateUserModel()));

   [TestMethod]
   public async Task DeleteByIdMethodShoutCorrectDeleteUser()
   {
      userEntity = CreateUserEntity();

      userRepositoryMock
         .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
         .ReturnsAsync(userEntity);

      await userService.DeleteByIdAsync(userEntity.Id);

      unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
   }

   [TestMethod]
   public async Task DeleteMethodShoutThrowExceptionWhenUserIsNull()
      => await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await userService.DeleteByIdAsync(GetUserId()));

   [TestMethod]
   public async Task DeleteMethodShoutThrowExceptionWhenUserAlreadyIsDeleted()
   {
      var userModel = CreateUserModel();
      userModel.IsActive = false;

      await Assert.ThrowsExceptionAsync<ItemNotFoundException>(async () => await userService.DeleteByIdAsync(userModel.Id));
   }

   private UserModel CreateUserModel()
   {
      return new UserModel
      {
         Id = userEntity.Id,
         UserName = userEntity.UserName
      };
   }

   private UserEntity CreateUserEntity()
   {
      userEntity.Id = GetUserId();
      userEntity.UserName = GetUserName();

      return userEntity;
   }

   private static Guid GetUserId()
      => Guid.NewGuid();

   private static string GetUserName()
      => $"UserName: {new Random().Next(10)}";
}
