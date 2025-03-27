using AutoMapper;
using BarberSuite.Application.Contracts;
using BarberSuite.Domain.Models.Shops;
using BarberSuite.Domain.ValueObjects;
using MediatR;

namespace BarberSuite.Application.Features.Shops.Queries.GetShopsList
{
    public class GetShopListVM
    {
        public Guid Id { get; set; }
        public string Cvr { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int ServiceCount { get; set; }
    }

    public class GetShopListQuery : IRequest<List<GetShopListVM>>
    {
        public string? Name { get; set; }
        public string? City { get; set; }

    }


    public class GetShopListQueryHandler : IRequestHandler<GetShopListQuery, List<GetShopListVM>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShopListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetShopListVM>> Handle(GetShopListQuery request, CancellationToken cancellationToken)
        {
            var criteria = new ShopSearchCriteria
            {
                Name = request.Name,
                City = request.City
            };
            var shops = await _unitOfWork.ShopRepository.SearchAsync(criteria);
            return _mapper.Map<List<GetShopListVM>>(shops);
        }
    }

}
