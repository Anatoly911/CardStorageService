using CardStorageService.Data;
using CardStorageService.Services.Impl;
using FluentValidation;

namespace CardStorageService
{
    public class ClientManager
    {
        ClientValidator _validator;
        ClientRepository _repository;
        public ClientManager()
        {
            _repository = new ClientRepository();
            _validator = new ClientValidator();
        }
        public void Add(Client client)
        {
            if (!ValidateClient(client))
            {
                return;
            }
            _repository.Add(client);
            Console.WriteLine($"Покупатель {client.FirstName} {client.Surname} успешно добавлен.");
        }
        private bool ValidateClient(Client client)
        {
            var result = _validator.Validate(client);
            if (result.IsValid)
            {
                return true;
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return false;
        }
        class ClientValidator : AbstractValidator<Client>
        {
            public ClientValidator()
            {
                var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";
                RuleFor(c => c.FirstName)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msg);
                RuleFor(c => c.Surname)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msg);
                RuleFor(c => c.ClientId)
                .GreaterThan(14).WithMessage(msg)
                .LessThan(180).WithMessage(msg);
                RuleFor(c => c.Patronymic)
                    .Must(c => c.All(Char.IsLetter)).WithMessage(msg);
            }
        }
        class ClientRepository
        {
            Random _random;
            public ClientRepository()
            {
                _random = new Random();
            }
            public void Add(Client customer)
            {
                var sleepInSeconds = _random.Next(2, 7);
                Thread.Sleep(1000 * sleepInSeconds);
            }
        }
    }
}
