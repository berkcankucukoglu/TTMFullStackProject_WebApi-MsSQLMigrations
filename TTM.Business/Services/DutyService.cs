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
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Id != null)
            {
                return CommandResult.Error("Some other record was found about this duty! This creation terminated!", new Exception());
            }
            try
            {
                var entity = new Duty();
                _mapper.Map(model, entity, typeof(DutyDto), typeof(Duty));
                entity.CreatedDate = DateTime.Now;

                var validationResult = _validotar.Validate(entity);
                if (validationResult.HasErrors)
                {
                    return CommandResult.Failure(validationResult.ErrorString);
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
        public override List<DutyDto> GetAllByUserToken(string? token)
        {
            try
            {
                var dtoList = new List<DutyDto>();
                var allEntities = _context.Duties.Where(p => p.UserId == 2010);
                foreach (var entity in allEntities)
                {
                    var dto = _mapper.Map<DutyDto>(entity);
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return new List<DutyDto>();
            }
        }
        public override CommandResult WipeProjectDuties(int id)
        {
            try
            {
                var allEntities = _context.Duties.Where(p => p.ProjectId == id).ToList();
                if (allEntities.Count > 0)
                {
                    foreach (var entity in allEntities)
                    {
                        _context.Remove(entity);
                    }
                    _context.SaveChanges();
                    return CommandResult.Success();
                }
                else
                {
                    return CommandResult.Failure();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }

    }
}
