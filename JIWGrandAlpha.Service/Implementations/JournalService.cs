using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Domain.Enum;
using JIWGrandAlpha.Domain.Response;
using JIWGrandAlpha.Domain.ViewModels.Cell;
using JIWGrandAlpha.Domain.ViewModels.Class;
using JIWGrandAlpha.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.Service.Implementations;

public class JournalService : IJournalService
{
    private readonly IBaseRepository<Cell> _cellRepository;
    private readonly IBaseRepository<Student> _studentRepository;
    private readonly IBaseRepository<Teacher> _teacherRepository;
    private readonly IBaseRepository<TeacherObject> _teacherObjectRepository;
    private readonly IBaseRepository<Group> _groupRepository;
    private readonly IBaseRepository<GroupObject> _groupObjectRepository;
    private readonly IBaseRepository<Object> _objectRepository;
    private readonly IBaseRepository<Class> _classRepository;

    public JournalService(IBaseRepository<Class> classRepository, IBaseRepository<Teacher> teacherRepository, IBaseRepository<GroupObject> groupObjectRepository, IBaseRepository<Object> objectRepository, IBaseRepository<Group> groupRepository, IBaseRepository<Cell> cellRepository, IBaseRepository<Student> studentRepository, IBaseRepository<TeacherObject> teacherObjectRepository)
    {
        _groupRepository = groupRepository;
        _cellRepository = cellRepository;
        _studentRepository = studentRepository;
        _teacherObjectRepository = teacherObjectRepository;
        _groupObjectRepository = groupObjectRepository;
        _objectRepository = objectRepository;
        _teacherRepository = teacherRepository;
        _classRepository = classRepository;
    }
    public async Task<IBaseResponse<IEnumerable<Group>>> getGroupsT(string Name)
    {
        try
        {
            var FIO = Name.Split(" ");

            var teacher = _teacherRepository
                .GetAll()
                .FirstOrDefault(x =>
                x.Lastname == FIO[0] &&
                x.Firstname == FIO[1] &&
                x.Middlename == FIO[2]);

            var teacherObj = _teacherObjectRepository
                .GetAll()
                .Include(x => x.Object)
                .ThenInclude(x => x.GroupsObjects)
                .Where(x => x.IdTeacher == teacher.Id);

            HashSet<long> idGroups = new();
            foreach (var teacherO in teacherObj)
                foreach (var groupO in teacherO.Object.GroupsObjects)
                    idGroups.Add(groupO.IdGroup);
            List<Group> groups = new();
            foreach (var item in idGroups)
                groups.AddRange(_groupRepository.GetAll().Where(x => x.Id == item));
            return new BaseResponse<IEnumerable<Group>>()
            {
                Data = groups,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Group>>()
            {
                Description = $"[JournalService.getGroupsT] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<Object>>> getObjectsT(string Name, long idGroup)
    {
        try
        {
            var FIO = Name.Split(" ");

            var teacher = _teacherRepository
                .GetAll()
                .FirstOrDefault(x =>
                    x.Lastname == FIO[0] &&
                    x.Firstname == FIO[1] &&
                    x.Middlename == FIO[2]);
            var teacherObj = _teacherObjectRepository
                .GetAll()
                .Include(x => x.Object)
                .Where(x => x.IdTeacher == teacher.Id);

            HashSet<long> idObj = new();
            foreach (var to in teacherObj)
            {
                idObj.Add(to.Object.Id);
            }

            var groupObj = _groupObjectRepository
                .GetAll()
                .Include(x => x.Object)
                .Where(x => x.IdGroup == idGroup);

            List<Object> objects = new();
            foreach (var obj in groupObj)
            {
                foreach (var item in idObj)
                    if (obj.Object.Id == item)
                        objects.Add(obj.Object);
            }

            return new BaseResponse<IEnumerable<Object>>()
            {
                Data = objects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Object>>()
            {
                Description = $"[JournalService.getObjectsT] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<Object>>> getObjectsS(string Name)
    {
        try
        {
            var FIO = Name.Split(" ");
            var student = _studentRepository
                .GetAll()
                .FirstOrDefault(x =>
                    x.Lastname == FIO[0] &&
                    x.Firstname == FIO[1] &&
                    x.Middlename == FIO[2]);

            HashSet<long> idObj = new();
            foreach (var go in _groupObjectRepository.GetAll().Where(x=>x.IdGroup==student.IdGroup))
            {
                idObj.Add(go.IdObject);
            }

            List<Object> objects = new();
            foreach (var id in idObj)
            {
                objects.Add(_objectRepository.GetAll().Include(x=>x.Classes.Where(x=>x.Cell.IdStudent==student.Id)).ThenInclude(x=>x.Cell).FirstOrDefault(x=>x.Id==id));
            }
            return new BaseResponse<IEnumerable<Object>>()
            {
                Data = objects,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Object>>()
            {
                Description = $"[JournalService.getObjectsS] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<DateOnly>>> getDatesS(string Name)
    {
        try
        {
            var FIO = Name.Split(" ");
            var student = _studentRepository
                .GetAll()
                .FirstOrDefault(x =>
                    x.Lastname == FIO[0] &&
                    x.Firstname == FIO[1] &&
                    x.Middlename == FIO[2]);
            var classes = _classRepository.GetAll().Where(x => x.IdGroup == student.IdGroup);
            HashSet<DateOnly> dates = new();
            foreach (var date in classes)
            {
                dates.Add(date.Date1);
            }

            var d = dates.OrderBy(x => x.DayOfYear);
            return new BaseResponse<IEnumerable<DateOnly>>()
            {
                Data = d,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<DateOnly>>()
            {
                Description = $"[JournalService.getDatesS] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<Class>>> getClassT(long idObject, long idGroup)
    {
        try
        {
            var classes = await _classRepository.GetAll().Where(x => x.IdObject == idObject && x.IdGroup == idGroup).OrderBy(x => x.Date1).ToListAsync();
            return new BaseResponse<IEnumerable<Class>>()
            {
                Data = classes,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Class>>()
            {
                Description = $"[JournalService.getClassT] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<IEnumerable<Student>>> getStudentsT(long idObject, long idGroup)
    {
        try
        {
            var students = _studentRepository.GetAll()
                .Include(x => x.Cells.Where(x => x.Class.IdObject == idObject))
                .Where(x => x.IdGroup == idGroup);
            return new BaseResponse<IEnumerable<Student>>()
            {
                Data = students,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Student>>()
            {
                Description = $"[JournalService.getStudentsT] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public BaseResponse<Dictionary<int, string>> getCellsTypes()
    {
        try
        {
            var types = ((TypeValue[])Enum.GetValues(typeof(TypeValue)))
                .ToDictionary(k => (int)k, t => t.ToString());

            return new BaseResponse<Dictionary<int, string>>()
            {
                Data = types,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<int, string>>()
            {
                Description = $"[JournalService.getCellTypes] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public BaseResponse<Dictionary<int, string>> getClassTypes()
    {
        try
        {
            var types = ((TypeClass[])Enum.GetValues(typeof(TypeClass)))
                .ToDictionary(k => (int)k, t => t.ToString());

            return new BaseResponse<Dictionary<int, string>>()
            {
                Data = types,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<int, string>>()
            {
                Description = $"[JournalService.getClassTypes] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<CellViewModel>> getCell(long id, long idStudent, long idClass)
    {
        try
        {
            var cell = await _cellRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (cell == null)
                return new BaseResponse<CellViewModel>()
                {
                    Data = new CellViewModel() { Value = 0, Type = TypeValue.Оценка, IdStudent = idStudent, IdClass = idClass},
                    StatusCode = StatusCode.OK
                };
            return new BaseResponse<CellViewModel>()
            {
                Data = new CellViewModel() { Value = cell.Value, Type = cell.Type, IdClass = cell.IdClass, IdStudent= cell.IdStudent, Id = cell.Id},
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<CellViewModel>()
            {
                Description = $"[JournalService.getCell] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<ClassViewModel>> getClass(long id, long idObject, long idGroup)
    {
        try
        {
            var clas = await _classRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (clas == null)
                return new BaseResponse<ClassViewModel>()
                {
                    Data = new ClassViewModel() { Type = 0, IdGroup = idGroup, IdObject = idObject},
                    StatusCode = StatusCode.OK
                };
            return new BaseResponse<ClassViewModel>()
            {
                Data = new ClassViewModel() { Id = clas.Id, Type = clas.Type, IdObject = clas.IdObject, IdGroup = clas.IdGroup, Date1 = clas.Date1.ToString("yyyy-MM-dd")},
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClassViewModel>()
            {
                Description = $"[JournalService.getClass] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<Cell>> CreateCell(CellViewModel model)
    {
        try
        {
            var cell = new Cell()
            {
                Value = model.Value,
                Type = model.Type,
                IdClass = model.IdClass,
                IdStudent = model.IdStudent,
            };
            await _cellRepository.Create(cell);
            return new BaseResponse<Cell>()
            {
                Data = cell,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Cell>()
            {
                Description = $"[JournalService.CreateCell] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<Cell>> EditCell(long id, CellViewModel model)
    {
        try
        {
            var cell = await _cellRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            cell.Value = model.Value;
            cell.Type = model.Type;

            await _cellRepository.Update(cell);


            return new BaseResponse<Cell>()
            {
                Data = cell,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Cell>()
            {
                Description = $"[JournalService.EditCell] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<bool>> RemoveCell(long id)
    {
        try
        {
            var cell = await _cellRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (cell == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Data = false
                };
            }

            await _cellRepository.Delete(cell);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[JournalService.RemoveCell] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<Class>> CreateClass(ClassViewModel model)
    {
        try
        {
            var clas = new Class()
            {
                Date1 = DateOnly.Parse(model.Date1),
                Type = model.Type,
                IdObject = model.IdObject,
                IdGroup = model.IdGroup,
            };
            await _classRepository.Create(clas);
            return new BaseResponse<Class>()
            {
                Data = clas,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Class>()
            {
                Description = $"[JournalService.CreateClass] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<Class>> EditClass(long id, ClassViewModel model)
        {
        try
        {
            var clas = await _classRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            clas.Type = model.Type;
            clas.Date1 = DateOnly.Parse(model.Date1);

            await _classRepository.Update(clas);


            return new BaseResponse<Class>()
            {
                Data = clas,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Class>()
            {
                Description = $"[JournalService.EditClass] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<bool>> RemoveClass(long id)
    {
        try
        {
            var clas = await _classRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (clas == null)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Data = false
                };
            }

            await _classRepository.Delete(clas);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[JournalService.RemoveClass] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}