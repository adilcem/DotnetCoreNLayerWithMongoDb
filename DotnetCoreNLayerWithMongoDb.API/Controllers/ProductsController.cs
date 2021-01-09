﻿using AutoMapper;
using DotnetCoreNLayer.API.DTO.Product;
using DotnetCoreNLayer.API.Filters;
using DotnetCoreNLayer.Core.Models;
using DotnetCoreNLayer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {            
            var products = await _productService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(long id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        [ValidationFilter]
        [ServiceFilter(typeof(CategoryNotFoundFilter))] // For foreign key check
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));
        }

        [ValidationFilter]
        [ServiceFilter(typeof(ProductNotFoundFilter))]        
        [HttpPut]
        public IActionResult Update(UpdateProductDto updateProductDto)
        {
            _productService.Update(_mapper.Map<Product>(updateProductDto));

            return NoContent();
        }

        [ServiceFilter(typeof(ProductNotFoundFilter))]
        [HttpDelete]
        public IActionResult Remove(long id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);

            return NoContent();
        }
    }
}
