using System.ComponentModel.DataAnnotations;

namespace BlogerMVC.ViewModels
{
	public class UserRegisterVm
	{
	
		[Required]
		[MaxLength(50)]
		public string UserName { get; set; }

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string RepeatPassword { get; set; }

		[Required]
		[MaxLength(50)]
		[EmailAddress]
		public string Email { get; set; }
	}
}
