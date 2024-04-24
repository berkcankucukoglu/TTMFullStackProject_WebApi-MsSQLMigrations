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
        IEnumerable<TModel> GetAllByUserToken(string? token);
        CommandResult Create(TModel model);
        CommandResult CreateByUserToken(TModel model, string? token);
        CommandResult Update(TModel model);
        CommandResult Delete(TModel model);
        CommandResult Delete(int id);
        CommandResult RecordExists(TModel model);
        CommandResult WipeProjectDuties(int id);
    }
}
