using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teknikServis.Entities.Fatura;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Abstract
{
	public interface IIslemRepository:IRepository<Islem>
	{
		List<IslemRaporViewModel> GetAllReport(int id);
	}
}
