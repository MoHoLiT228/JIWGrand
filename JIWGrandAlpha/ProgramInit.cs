using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.DAL.Repositories;
using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Service.Implementations;
using JIWGrandAlpha.Service.Interfaces;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha;

public static class ProgramInit
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Cell>, CellRepository>();
        services.AddScoped<IBaseRepository<Class>, ClassRepository>();
        services.AddScoped<IBaseRepository<GroupObject>, GroupobjectRepository>();
        services.AddScoped<IBaseRepository<Group>, GroupRepository>();
        services.AddScoped<IBaseRepository<Teacher>, TeacherRepository>();
        services.AddScoped<IBaseRepository<Student>, StudentRepository>();
        services.AddScoped<IBaseRepository<TeacherObject>, TeacherObjectRepository>();
        services.AddScoped<IBaseRepository<Object>, ObjectRepository>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IJournalService,JournalService>();
        //builder.Services.AddScoped<IDateService,DateService>();
        //builder.Services.AddScoped<IGroupObjectService,GroupobjectService>();
        //builder.Services.AddScoped<IGroupService,GroupService>();
        //builder.Services.AddScoped<ITeacherObjectService,TeacherObjectService>();
        //builder.Services.AddScoped<IObjectService,ObjectService>();
    }
}