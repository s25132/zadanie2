using LegacyApp;

namespace LegacyAppTest
{
    public class LegacyAppTest
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
        }

        [Test]
        public void AddUser_ValidInput_ReturnsTrue()
        {
            string firstName = "Black";
            string lastName = "Smith";
            string email = "test@test.com";
            DateTime dateOfBirth = DateTime.Parse("1900-01-01");
            int clientId = 1;

            bool result = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddUser_InvalidName_ReturnsFalse()
        {
            string firstName = "";
            string lastName = "Smith";
            string email = "test@test.com";
            DateTime dateOfBirth = DateTime.Parse("1900-01-01");
            int clientId = 1;

            bool result = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddUser_InvalidEmail_ReturnsFalse()
        {
            string firstName = "Black";
            string lastName = "Smith";
            string email = "test";
            DateTime dateOfBirth = DateTime.Parse("1900-01-01");
            int clientId = 1;

            bool result = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddUser_Underage_ReturnsFalse()
        {
            string firstName = "Black";
            string lastName = "Smith";
            string email = "test@test.com";
            DateTime dateOfBirth = DateTime.Now.AddYears(-1);
            int clientId = 1;

            bool result = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddUser_ValidClientNoCreditLimit_ReturnsTrue()
        {
            string firstName = "Black";
            string lastName = "Smith";
            string email = "test@test.com";
            DateTime dateOfBirth = DateTime.Parse("1900-01-01");
            int clientId = 2;

            bool result = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.IsTrue(result);
        }
    }
}