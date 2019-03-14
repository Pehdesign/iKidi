using iKidi.App_GlobalResources;
using iKidi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKidi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        [StringLength(255, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "TitleLong")]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "TitleRequired")]
        public string Title { get; set; }

        [MaxLength(2048)]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [StringLength(2048, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "DescriptionLong")]
        public string Description { get; set; }

        [MaxLength(1024)]
        [Display(Name = "ImageUrl", ResourceType = typeof(Resource))]
        [StringLength(1024, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "ImageUrlLong")]
        public string ImageUrl { get; set; }

        public int ProductLinkTextId { get; set; }

        public int ProductTypeId { get; set; }

        [Display(Name = "ProductLinkText", ResourceType = typeof(Resource))]
        public ICollection<ProductLinkText> ProductLinkTexts { get; set; }

        [Display(Name = "ProductType", ResourceType = typeof(Resource))]
        public ICollection<ProductType> ProductTypes { get; set; }

        public string ProductType
        {
            get
            {
                return ProductTypes == null || ProductTypes.Count.Equals(0) ?
                    String.Empty : ProductTypes.First(
                        pt => pt.Id.Equals(ProductTypeId)).Title;
            }
        }

        public string ProductLinkText
        {
            get
            {
                return ProductLinkTexts == null || 
                    ProductLinkTexts.Count.Equals(0) ?
                    String.Empty : ProductLinkTexts.First(
                        pt => pt.Id.Equals(ProductLinkTextId)).Title;
            }
        }

    }
}