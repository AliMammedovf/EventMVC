﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogerMVC.Business.Exceptions;

public class FileNullException : Exception
{
	public FileNullException(string? message) : base(message)
	{
	}
}