﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Domain.Entities;
using UTB.Eshop.Infrastructure.Database;

namespace UTB.Eshop.Application.Implementation
{
    public class ProductService : IProductService
    {
        EshopDbContext _eshopDbContext;
        public ProductService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }

        public IList<Product> Select()
        {
            return _eshopDbContext.Products.ToList();
        }

        public void Create(Product product)
        {
            //pridani produktu
            if (_eshopDbContext.Products != null)
            {
                _eshopDbContext.Products.Add(product);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Product? product =
                _eshopDbContext.Products.FirstOrDefault(
                prod => prod.Id == id);

            if (product != null)
            {
                _eshopDbContext.Products.Remove(product);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }

        public bool Edit(Product updatedProduct)
        {
            bool edited = false;
            Product existingProduct = _eshopDbContext.Products.FirstOrDefault(prod => prod.Id == updatedProduct.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.ImageSrc = updatedProduct.ImageSrc;
                _eshopDbContext.SaveChanges();
                edited = true;
            }

            return edited;

        }

        public Product? SelectById(int id)
        {
            Product selectedProduct = _eshopDbContext.Products.FirstOrDefault(prod => prod.Id == id);
            return selectedProduct;
        }
    }
}
