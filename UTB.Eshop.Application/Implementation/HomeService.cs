using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Application.ViewModels;
using UTB.Eshop.Infrastructure.Database;

namespace UTB.Eshop.Application.Implementation
{
    public class HomeService : IHomeService
    {
        EshopDbContext _eshopDbContext;
        public HomeService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }

        public CarouselProductViewModel GetHomeIndexViewModel()
        {
            CarouselProductViewModel viewModel = new CarouselProductViewModel();
            viewModel.Products = _eshopDbContext.Products.ToList();
            viewModel.Carousels = _eshopDbContext.Carousels.ToList();
            return viewModel;
        }
    }
}