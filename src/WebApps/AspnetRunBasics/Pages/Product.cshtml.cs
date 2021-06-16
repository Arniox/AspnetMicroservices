using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        //Objects
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        //Constructor
        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryId)
        {
            //Get product list
            var productList = await _catalogService.GetCatalog();
            //Filter by distinct
            CategoryList = productList.Select(p => p.Category).Distinct();

            //Check if selected category is empty or not
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                //If Category is selected then filter products by category selected
                ProductList = productList.Where(p => p.Category == categoryId);
                SelectedCategory = categoryId;
            }
            else
            {
                //If not, then select all products
                ProductList = productList;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" }); 

            //Get product
            var product = await _catalogService.GetCatalog(productId);

            //Get username and basket
            var userName = "nikkolas";
            var basket = await _basketService.GetBasket(userName);

            //Set up new BasketItemMode
            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black",
                Description = product.Summary + "</br>" + product.Description
            });

            //Update basket
            var basketUpdated = await _basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}