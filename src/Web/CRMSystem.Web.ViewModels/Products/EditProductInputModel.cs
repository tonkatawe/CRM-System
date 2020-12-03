

using System.Linq;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;
using Microsoft.AspNetCore.Http;

namespace CRMSystem.Web.ViewModels.Products
{
    public class EditProductInputModel:ProductCreateInputModel, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, EditProductInputModel>()
                .ForMember(x => x.ImageUrl, options =>
                    options.MapFrom(x => x.Images.FirstOrDefault().Id == null ?
                        "images/default-img.gif" :
                        "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
