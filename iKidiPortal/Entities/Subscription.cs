using iKidi.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iKidi.Entities
{
    [Table("Subscription")]
    public class Subscription
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

        [MaxLength(20)]
        [Display(Name = "RegistrationCode", ResourceType = typeof(Resource))]
        [StringLength(20, ErrorMessageResourceType = typeof(Resource),
                      ErrorMessageResourceName = "RegistrationCodeLong")]
        public string RegistrationCode { get; set; }


    }
}