using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Domain.Enum;
using JIWGrandAlpha.Domain.Response;
using JIWGrandAlpha.Domain.ViewModels.Cell;
using JIWGrandAlpha.Domain.ViewModels.Class;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.Service.Interfaces;

public interface IJournalService
{
    Task<IBaseResponse<IEnumerable<Group>>> getGroupsT(string Name);
    Task<IBaseResponse<IEnumerable<Object>>> getObjectsT(string Name, long idGroup);
    Task<IBaseResponse<IEnumerable<Class>>> getClassT(long idObject, long idGroup);
    Task<IBaseResponse<IEnumerable<DateOnly>>> getDatesS(string Name);
    Task<IBaseResponse<IEnumerable<Object>>> getObjectsS(string Name);
    Task<IBaseResponse<IEnumerable<Student>>> getStudentsT(long idObject,long idGroup);
    BaseResponse<Dictionary<int, string>> getCellsTypes();
    BaseResponse<Dictionary<int, string>> getClassTypes();
    Task<IBaseResponse<CellViewModel>> getCell(long id, long idStudent, long idClass);
    Task<IBaseResponse<ClassViewModel>> getClass(long id, long idObject, long idGroup);
    Task<IBaseResponse<Cell>> CreateCell(CellViewModel model);
    Task<IBaseResponse<Cell>> EditCell(long id, CellViewModel model);
    Task<IBaseResponse<bool>> RemoveCell(long id);
    Task<IBaseResponse<Class>> CreateClass(ClassViewModel model);
    Task<IBaseResponse<Class>> EditClass(long id, ClassViewModel model);
    Task<IBaseResponse<bool>> RemoveClass(long id);
}