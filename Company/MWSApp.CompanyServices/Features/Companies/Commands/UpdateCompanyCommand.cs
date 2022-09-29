using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWSApp.CompanyServices.Features.Companies.Commands
{
    public class UpdateCompanyCommand :IRequest<ActionResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, ActionResponse>
    {
        IRepository<Company> _repository;
        public UpdateCompanyHandler(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<ActionResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new ActionResponse();
            Company company = await _repository.GetByIdAsync(request.Id);
            company.Name = request.Name;
            company.Email = request.Email;
            _repository.Update(company);
            await _repository.SaveChangesAsync();
            response.Id = company.Id.ToString();
            return response;
        }
    }
}
