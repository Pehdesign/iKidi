using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using iKidi.App_GlobalResources;


namespace iKidi.Entities
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        [StringLength(255, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "TitleLong")]
        [Required(ErrorMessageResourceType = typeof(Resource),
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
    }
}