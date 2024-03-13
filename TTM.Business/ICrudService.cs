using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTM.Business
{
    public interface ICrudService<TModel>
    {
        TModel GetById(int id);
        IEnumerable<TModel> GetAll();
        CommandResult Create(int id);
        CommandResult Create(TModel model);
        CommandResult Update(TModel model);
        CommandResult Delete(int id);
        CommandResult Delete(TModel model);
    }
}
