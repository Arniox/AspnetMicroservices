﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        //Constructor
        public CartModel(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            //Get username
            var userName = "nikkolas";
            //Get cart
            Cart = await _basketService.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            //Get username and basket
            var userName = "nikkolas";
            var basket = await _basketService.GetBasket(userName);

            //Get item
            var item = basket.Items.Single(x => x.ProductId == productId);
            //Remove
            basket.Items.Remove(item);

            //Update basket
            var basketUpdated = await _basketService.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}