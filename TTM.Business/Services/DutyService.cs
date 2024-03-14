using System.Diagnostics;
using TTM.Business.Validators;
using TTM.DataAccess;
using TTM.Domain;

namespace TTM.Business.Services
{
    public class DutyService : BaseService<DutyDto, Duty>
    {
        public DutyService(TTMContext ttmContext) : base(ttmContext)
        {
        }

        private readonly DutyValidator _validotar = new DutyValidator();
        public override CommandResult Create(DutyDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = new Duty();
                _mapper.Map(model, entity, typeof(DutyDto), typeof(Duty));

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity.Id != null)
                {
                    return CommandResult.Error("Record was found! This creation terminated!", new Exception());
                }
                _context.Duties.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success("Duty created successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Duty Creation Error!", ex);
            }
        }
        public override CommandResult Update(DutyDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            try
            {
                var entity = _context.Duties.Find(model.Id);
                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
                }
                if (entity == null)
                {
                    return CommandResult.Error("Record was not found!", new Exception());
                }
                _mapper.Map(model, entity, typeof(DutyDto), typeof(Duty));
                _context.Duties.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success("Duty updated successfully!");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"{DateTime.Now} - {ex}");
                return CommandResult.Error("Duty Update Error!", ex);
            }
        }
    }
}
