using System.Security.Claims;
using JIWGrandAlpha.Domain.Response;
using JIWGrandAlpha.Domain.ViewModels.User;

namespace JIWGrandAlpha.Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<ClaimsIdentity>> LoginStudent(StudentViewModel model);
    Task<BaseResponse<ClaimsIdentity>> LoginTeacher(TeacherViewModel model);
    BaseResponse<Dictionary<long, string>> GetGroups();
    BaseResponse<Dictionary<long, string>> GetTeachers();
}