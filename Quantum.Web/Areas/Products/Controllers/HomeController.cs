using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quantum.Core.Entities;
using Quantum.Core.Interfaces;
using Quantum.Web.Areas.Products.Models;

namespace Quantum.Web.Areas.Products.Controllers
{
    //[Authorize]
    [ApiController]
    [Area("Products")]
    [Route("Products/[Action]")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.ListAllProductsAsync();

            var productsVM = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                HSNCode = p.HSNCode,
                Price = p.Price.ToString(),
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                CreatedBy = p.CreatedBy.FullName,
                ModifiedDate = p.ModifiedDate,
                ModifiedBy = p.ModifiedBy != null ? p.ModifiedBy.FullName : null
            });

            return Ok(productsVM);
        }

        [HttpPost]
        //[Route("api/Products/Create")]
        public async Task<IActionResult> Create([FromBody]ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                //product.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //product = await _productRepository.CreateProductAsync(product);
                var newProduct = new Product()
                {
                    Name = product.Name,
                    HSNCode = product.HSNCode,
                    Description = product.Description,
                    IsActive = true,
                    Price = Convert.ToDouble(product.Price),
                    CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };

                newProduct = await _productRepository.CreateProductAsync(newProduct);
                return Ok(newProduct);
            }

            return BadRequest();
        }
    }
}