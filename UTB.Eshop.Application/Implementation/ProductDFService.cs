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
    public class ProductDFService : IProductService
    {
        public IList<Product> Select()
        {
            return DatabaseFake.Products;
        }

        public void Create(Product product)
        {
            //fake id
            if (DatabaseFake.Products != null
                && DatabaseFake.Products.Count > 0)
            {
                product.Id = DatabaseFake.Products.Last().Id + 1;
            }
            else
                product.Id = 1;

            //pridani produktu
            if (DatabaseFake.Products != null)
                DatabaseFake.Products.Add(product);
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Product? product = 
                DatabaseFake.Products.FirstOrDefault(
                prod => prod.Id == id);

            if (product != null)
            {
                deleted = DatabaseFake.Products.Remove(product);
            }

            return deleted;
        }

        public bool Edit(Product updatedProduct)
        {
            bool edited = false;
            Product existingProduct = DatabaseFake.Products.FirstOrDefault(prod => prod.Id == updatedProduct.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.ImageSrc = updatedProduct.ImageSrc;
                existingProduct.Kategory = updatedProduct.Kategory;

                edited = true;
            }

            return edited;

        }

        public Product? SelectById(int id)
        {
            Product selectedProduct = DatabaseFake.Products.FirstOrDefault(prod => prod.Id == id);
            return selectedProduct;
        }
    }
}
