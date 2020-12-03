

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CRMSystem.Web.ViewModels.Products
{
    public class EditProductInputModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
        public ICollection<Image> Images { get; set; }
        
    }
}
