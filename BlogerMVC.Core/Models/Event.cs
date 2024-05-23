using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Core.Models;

public class Event:BaseEntity
{
	[Required]
	[MaxLength(50)]
	public string Title { get; set; }

	
	[MaxLength(50)]
	public string? Content { get; set; }

	[Required]
	[MaxLength(50)]
	public string Description	{ get; set; }

	public string? ImageUrl { get; set; }

	[NotMapped]
	public IFormFile? ImageFile { get; set; }
}
