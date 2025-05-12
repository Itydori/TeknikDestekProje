using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Entities;
using TeknikServis.Entities.Auth;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Abstract
{
	public interface IUserService

	{

		ValueTask CreateUserAsync(AppUser user);
		ValueTask<List<AppUser>> GetAllListAsync();
		ValueTask<List<AppUser>> GetAllListByParametrerAsync(string roleName);

		ValueTask SignInAsync();
	}
}
