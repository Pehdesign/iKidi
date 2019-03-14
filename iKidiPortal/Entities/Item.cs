using iKidi.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iKidi.Entities
{
    [Table("Item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Display(Name = "Url", ResourceType = typeof(Resource))]
        [StringLength(1024, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "UrlLong")]
        public string Url { get; set; }

        [MaxLength(1024)]
        [Display(Name = "ImageUrl", ResourceType = typeof(Resource))]
        [StringLength(1024, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "ImageUrlLong")]
        public string ImageUrl { get; set; }

        [AllowHtml]
        public string HTML { get; set; }

        [DefaultValue(0)]
        [Display(Name = "WaitDays", ResourceType = typeof(Resource))]
        public int WaitDays { get; set; }

        [Display(Name = "HTMLShort", ResourceType = typeof(Resource))]
        public string HTMLShort {
            get { return HTML == null || HTML.Length < 50 ? 
                    HTML : HTML.Substring(0, 50); }
        }

        public int ProductId { get; set; }
        public int ItemTypeId { get; set; }
        public int SectionId { get; set; }
        public int PartId { get; set; }

        [Display(Name = "IsFree", ResourceType = typeof(Resource))]
        public bool IsFree { get; set; }

        [Display(Name = "ItemTypes", ResourceType = typeof(Resource))]
        public ICollection<ItemType> ItemTypes { get; set; }

        [Display(Name = "Sections", ResourceType = typeof(Resource))]
        public ICollection<Section> Sections { get; set; }

        [Display(Name = "Parts", ResourceType = typeof(Resource))]
        public ICollection<Part> Parts { get; set; }
    }
}