﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;
using DesiClothing4u.Common.Models;


using EnableCorsAttribute = System.Web.Http.Cors.EnableCorsAttribute;

namespace DesiClothing4u.API.Controllers
{

    //[Microsoft.AspNetCore.Cors.EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ControllerBase
    {
        private readonly desiclothingContext _context;

        public ProductsController(desiclothingContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //GET: api/PostProduct
        [HttpPost("PostProduct")]
        public async Task<ActionResult<Product>> PostProduct(string ProductName, string ShortDescription, string FullDescription, int Price, int ProductTypeId, int VendorId, string CategoryId)
        {
            Product product = new Product
            {
                Name = ProductName,
                ShortDescription = ShortDescription,
                FullDescription = FullDescription,
                CreatedOnUtc = DateTime.UtcNow,
                ProductTypeId = ProductTypeId,
                Price = Price,
                ParentGroupedProductId = 1,
                VisibleIndividually = true,
                ProductTemplateId = 1,
                VendorId = VendorId,
                ShowOnHomepage = true,
                AllowCustomerReviews = true,
                ApprovedRatingSum = 1,
                NotApprovedRatingSum = 1,
                ApprovedTotalReviews = 1,
                NotApprovedTotalReviews = 1,
                SubjectToAcl = true,
                LimitedToStores = true,
                IsGiftCard = true,
                GiftCardTypeId = 1,
                RequireOtherProducts = true,
                AutomaticallyAddRequiredProducts = true,
                IsDownload = true,
                DownloadId = 0,
                UnlimitedDownloads = true,
                MaxNumberOfDownloads = 50000,
                DownloadActivationTypeId = 0,
                HasSampleDownload = true,
                SampleDownloadId = 0,
                HasUserAgreement = true,
                IsRecurring = true,
                RecurringCycleLength = 1,
                RecurringCyclePeriodId = 1,
                RecurringTotalCycles = 1,
                IsRental = true,
                RentalPriceLength = 1,
                RentalPricePeriodId = 1,
                IsShipEnabled = true,
                IsFreeShipping = true,
                ShipSeparately = true,
                AdditionalShippingCharge = 1,
                DeliveryDateId = 1,
                IsTaxExempt = true,
                TaxCategoryId = 1,
                IsTelecommunicationsOrBroadcastingOrElectronicServices = true,
                ManageInventoryMethodId = 1,
                ProductAvailabilityRangeId = 1,
                UseMultipleWarehouses = true,
                WarehouseId = 1,
                StockQuantity = 1,
                DisplayStockAvailability = true,
                DisplayStockQuantity = true,
                MinStockQuantity = 1,
                LowStockActivityId = 1,
                NotifyAdminForQuantityBelow = 1,
                BackorderModeId = 1,
                AllowBackInStockSubscriptions = true,
                OrderMinimumQuantity = 1,
                OrderMaximumQuantity = 1,
                AllowAddingOnlyExistingAttributeCombinations = true,
                NotReturnable = true,
                DisableBuyButton = true,
                DisableWishlistButton = true,
                AvailableForPreOrder = true,
                CallForPrice = true,
                OldPrice = 1,
                ProductCost = 1,
                CustomerEntersPrice = true,
                MinimumCustomerEnteredPrice = 1,
                MaximumCustomerEnteredPrice = 1,
                BasepriceEnabled = true,
                BasepriceAmount = 1,
                BasepriceUnitId = 1,
                BasepriceBaseAmount = 1,
                BasepriceBaseUnitId = 1,
                MarkAsNew = true,
                HasTierPrices = true,
                HasDiscountsApplied = true,
                Weight = 1,
                Length = 1,
                Width = 1,
                Height = 1,
                DisplayOrder = 1,
                Published = true,
                Deleted = true,
                UpdatedOnUtc = DateTime.UtcNow,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            //return NoContent();
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}