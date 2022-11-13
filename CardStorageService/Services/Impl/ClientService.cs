using CardStorageService.Data;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static ClientServiceProtos.ClientService;
using System.Threading.Tasks;
using System;
using ClientServiceProtos;

namespace CardStorageService.Services.Impl
{
    public class ClientService: ClientServiceBase
    {
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<ClientService> _logger;
        public ClientService(IClientRepositoryService clientRepositoryService, ILogger<ClientService> logger)
        {
            _clientRepositoryService = clientRepositoryService;
            _logger = logger;
        }
        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic
                });
                var response = new CreateClientResponse
                {
                    ClientId = clientId,
                    ErrorCode = 0,
                    ErrorMessage = String.Empty
                };
                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                var response = new CreateClientResponse
                {
                    ClientId = -1,
                    ErrorCode = 912,
                    ErrorMessage = "Create client error."
                };
                return Task.FromResult(response);
            }
        }
    }
}