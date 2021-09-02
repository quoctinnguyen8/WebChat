using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.ViewModels.AppUser
{
	public class AddAppUser
	{
		[Required(ErrorMessage = "Dữ liệu này là bắt buộc")]
		[MinLength(3, ErrorMessage = "Dữ liệu quá ngắn")]
		[MaxLength(200, ErrorMessage = "Dữ liệu quá dài")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Dữ liệu này là bắt buộc")]
		[DataType(DataType.Password)]
		[MinLength(4, ErrorMessage = "Mật khẩu quá ngắn")]
		public string Password { get; set; }

		[DisplayName("Xác nhận mật khẩu")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Tên đầy đủ")]
		[Required(ErrorMessage = "Dữ liệu này là bắt buộc")]
		public string FullName { get; set; }
	}
}
