using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results;
using MediatR;

public class GetContractByIdQuery : IRequest<ApiResponse<ContractDetailsResponse>>
{
    public int ContractId { get; set; }
    public GetContractByIdQuery(int id)
    {
        ContractId = id;
    }
}
