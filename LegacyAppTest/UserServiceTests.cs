using LegacyApp;

namespace LegacyAppTest
{
    public class UserServiceTests
    {
        [Fact]
        public void Test1()
        {
            string firstName = "Test";
            string lastName = "Test";
            string email = "Test";
            DateTime dateOfBirth = DateTime.Now;
            int clientId = 1;


            bool result = new UserService().AddUser(firstName, lastName, email, dateOfBirth, clientId);

            Assert.False(result);

        }
    }
}