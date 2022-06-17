using JIWGrandAlpha.DAL;
using JIWGrandAlpha.DAL.Repositories;
using JIWGrandAlpha.Domain.Entity;
using JIWGrandAlpha.Domain.Enum;
using Object = JIWGrandAlpha.Domain.Entity.Object;


//DateRepository _dateRepository = new DateRepository(new ApplicationDbContext());
//DateTime dt = DateTime.Now;
//for (int i = 0; i < 116; i++)
//{
//    await _dateRepository.Create(new Date() {Date1 = DateOnly.FromDateTime(dt)});
//    dt = dt.AddDays(-1);
//    Console.WriteLine(dt.ToShortDateString());
//}

//StudentRepository _StudentRepository = new StudentRepository(new ApplicationDbContext());
//List<Student> _Students = new List<Student>();
//_Students.Add(new Student() { Lastname = "Абдураимов", Firstname = "Рустам", Middlename = "О", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Воспяков", Firstname = "Стас", Middlename = "С", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Гаврильчик", Firstname = "Артём", Middlename = "Д", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Герасименя", Firstname = "Егор", Middlename = "О", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Гурин", Firstname = "Миша", Middlename = "О", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Ермалинский", Firstname = "Влад", Middlename = "О", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Зеленецкий", Firstname = "Паша", Middlename = "Д", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Зубрей", Firstname = "Георгий", Middlename = "Н", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Камеш", Firstname = "Илья", Middlename = "А", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Клевитский", Firstname = "Еремей", Middlename = "В", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Лобан", Firstname = "Никита", Middlename = "А", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Никалаёнок", Firstname = "Вася", Middlename = "В", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Носков", Firstname = "Илья", Middlename = "А", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Соколюк", Firstname = "Влад", Middlename = "С", Birthday = new DateOnly(), IdGroup = 1 });
//_Students.Add(new Student() { Lastname = "Хаецкий", Firstname = "Влад", Middlename = "Д", Birthday = new DateOnly(), IdGroup = 1 });
//foreach (var Student in _Students)
//{
//    await _StudentRepository.Create(Student);
//}

//GroupRepository _groupRepository = new GroupRepository(new ApplicationDbContext());
//List<Group> _groups = new List<Group>();
//_groups.Add(new Group(){Title = ""});

//ObjectRepository _objectRepository = new ObjectRepository(new ApplicationDbContext());
//List<Object> _objects = new List<Object>();
//_objects.Add(new Object() { Title = "ФизКультура" });
//_objects.Add(new Object() { Title = "ПрактПрограм" });
//_objects.Add(new Object() { Title = "БДиСУБД" });
//_objects.Add(new Object() { Title = "ИнЯзПро" });
//_objects.Add(new Object() { Title = "ПрактикаWeb" });
//_objects.Add(new Object() { Title = "КПиЯП" });
//_objects.Add(new Object() { Title = "ОсновыПДиУП" });
//_objects.Add(new Object() { Title = "ПрСрСоздInter" });
//_objects.Add(new Object() { Title = "ТестированиеПО" });
//_objects.Add(new Object() { Title = "ПрактРабПроф" });
//_objects.Add(new Object() { Title = "ОснКроссПлПрог" });
//_objects.Add(new Object() { Title = "ЗКИ" });
//_objects.Add(new Object() { Title = "ПрОбОбрГрафИнф" });
//foreach (var obj in _objects)
//{
//    await _objectRepository.Create(obj);
//}
