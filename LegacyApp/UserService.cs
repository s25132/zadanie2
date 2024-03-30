using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;
        private IUserDataAccess _userDataAccess;

        [Obsolete("Constructor UserService() is deprecated, please use UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataAccess userDataAccess).")]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
            _userDataAccess = new UserDataAccessAdapter();

        }

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataAccess userDataAccess)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _userDataAccess = userDataAccess;

        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidateName(firstName, lastName) || !ValidateEmail(email) || !ValidateAge(dateOfBirth))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                return false;
            }

            var user = GetUser(firstName, lastName, email, dateOfBirth, client);

            SetUserCreditLimit(client.Type, user);

            if (!ValidateCreditLimit(user))
            {
                return false;
            }

            _userDataAccess.AddUser(user);
            return true;
        }

        private bool ValidateName(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        private bool ValidateEmail(string email)
        {
            if(email == null)
            {
                return false;
            }
            return email.Contains("@") || email.Contains(".");

        }

        private bool ValidateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age >= 21;
        }

        private User GetUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        private void SetUserCreditLimit(string clientType, User user)
        {
            if (clientType == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (clientType == "ImportantClient")
                {
                    creditLimit *= 2;
                }
                user.CreditLimit = creditLimit;
            }
        }

        private bool ValidateCreditLimit(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}