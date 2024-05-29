using N_Tier.Core.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace N_Tier.Application.Models.Loan
{
    public class CreateLoanModel
    {
        [Required]
        [Display(Name = "Loan Date")]
        [DataType(DataType.DateTime)]
        public DateTime LoanDate { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Fine")]
        [DataType(DataType.Currency)]
        public decimal Fine { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public ApplicationUser Customer { get; set; }

        [Required]
        [Display(Name = "Librarian")]
        public ApplicationUser Librarian { get; set; }

        [Required]
        [Display(Name = "Book")]
        public Guid BookId { get; set; }
    }

    public class CreateLoanResponseModel : BaseResponseModel { }
}
