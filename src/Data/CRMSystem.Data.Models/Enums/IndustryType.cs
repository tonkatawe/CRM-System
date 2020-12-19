namespace CRMSystem.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum IndustryType
    {
        [Display(Name = "Agriculture")]
        Agriculture = 1,

        [Display(Name = "Finance")]
        Finance = 2,

        [Display(Name = "Business Services Consulting")]
        BusinessServicesConsulting = 3,

        [Display(Name = "Communications")]
        Communications = 4,

        [Display(Name = "Computer Related Products Services")]
        ComputerRelatedProductsServices = 5,

        [Display(Name = "Construction")]
        Construction = 6,

        [Display(Name = "Education")]
        Education = 7,

        [Display(Name = "Engineering Architecture")]
        EngineeringArchitecture = 8,

        [Display(Name = "Government")]
        Government = 9,

        [Display(Name = "Healthcare")]
        Healthcare = 10,

        [Display(Name = "Hospitality")]
        Hospitality = 11,

        [Display(Name = "Legal")]
        Legal = 12,

        [Display(Name = "Manufacturing")]
        Manufacturing = 13,

        [Display(Name = "Media Marketing Advertising")]
        MediaMarketingAdvertising = 12,

        [Display(Name = "Mining")]
        Mining = 15,

        [Display(Name = "Non Profit")]
        NonProfit = 16,

        [Display(Name = "Personal Services")]
        PersonalServices = 17,

        [Display(Name = "Printing Publishing")]
        PrintingPublishing = 18,

        [Display(Name = "Good Boy")]
        RealEstate = 19,
        [Display(Name = "Retail")]
        Retail = 20,

        [Display(Name = "Transportation")]
        Transportation = 21,

        [Display(Name = "Utilities")]
        Utilities = 22,

        [Display(Name = "Wholesale")]
        Wholesale = 23,

        [Display(Name = "Other")]
        Other = 23,
    }
}
