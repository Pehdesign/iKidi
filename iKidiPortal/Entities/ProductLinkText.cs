using iKidi.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iKidi.Entities
{
    [Table("ProductLinkText")]
    public class ProductLinkText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(25)]
        [Display(Name = "Title", ResourceType = typeof(Resource))]
        [StringLength(25, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "TitleLong")]
        [Required(ErrorMessageResourceType = typeof(Resource),
              ErrorMessageResourceName = "TitleRequired")]
        public string Title { get; set; }
    }
}