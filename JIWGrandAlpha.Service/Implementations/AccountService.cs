using System.Security.Claims;
using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Domain.Enum;
using JIWGrandAlpha.Domain.Helpers;
using JIWGrandAlpha.Domain.Response;
using JIWGrandAlpha.Domain.ViewModels.User;
using JIWGrandAlpha.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IBaseRepository<Student> _studentRepository;
    private readonly IBaseRepository<Teacher> _teacherRepository;
    private readonly IBaseRepository<Group> _groupRepository;

    public AccountService(IBaseRepository<Student> studentRepository, IBaseRepository<Group> groupRepository,IBaseRepository<Teacher> teacherRepository)
    {
        _studentRepository = studentRepository;
        _groupRepository = groupRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<BaseResponse<ClaimsIdentity>> LoginStudent(StudentViewModel model)
    {
        try
        {
            var user = await _studentRepository.GetAll()
                .FirstOrDefaultAsync(x => x.IdGroup == model.IdGroup && x.Lastname == model.Lastname);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Такого студента нет в данной группе",
                };
            }

            if (user.Birthday.ToShortDateString() != model.Birthday)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверная дата рождения",
                };
            }

            var result = Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<BaseResponse<ClaimsIdentity>> LoginTeacher(TeacherViewModel model)
    {
        try
        {
            var user = await _teacherRepository.GetAll().FirstOrDefaultAsync(x =>x.Id == model.Id);
            if (user.Password != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль"
                };
            }

            var result = Authenticate(user);
            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public BaseResponse<Dictionary<long, string>> GetGroups()
    {
        try
        {
            var groups = _groupRepository.GetAll().ToDictionary(x=>x.Id, y=>y.Title);
            return new BaseResponse<Dictionary<long, string>>()
            {
                Data = groups,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<long,string>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public BaseResponse<Dictionary<long, string>> GetTeachers()
    {
        try
        {
            var teachers = _teacherRepository.GetAll().ToDictionary(x => x.Id, y => $"{y.Lastname} {y.Firstname[0]}.{y.Middlename[0]}.");
            return new BaseResponse<Dictionary<long, string>>()
            {
                Data = teachers,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Dictionary<long, string>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    private ClaimsIdentity Authenticate(Student user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Lastname} {user.Firstname} {user.Middlename}"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.Студент.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
    private ClaimsIdentity Authenticate(Teacher user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Lastname} {user.Firstname} {user.Middlename}"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.Преподаватель.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}