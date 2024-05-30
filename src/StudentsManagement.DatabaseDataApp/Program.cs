using Microsoft.EntityFrameworkCore;
using StudentsManagement.Common.Enums;
using StudentsManagement.DataAccess;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;
using System.Collections.Generic;

namespace StudentsManagement.DatabaseDataApp
{
    public class Program
    {
        private static IRepository<Speciality> _specialityRepository;
        private static IRepository<Subject> _subjectRepository;
        private static IRepository<CurriculumUnit> _curUnitRepository;
        private static IRepository<Group> _groupRepository;
        private static IRepository<Attestation> _attestationRepository;
        private static IRepository<User> _userRepository;
        private static IRepository<Mark> _markRepository;
        private static IRepository<RetakeResult> _retakeResultRepository;
        private static IRepository<WorkType> _workTypeRepository;

        static void CreateEntities()
        {
            var workTypes = new List<WorkType>()
            {
                new WorkType() { ShortName = "ЛР", FullName = "Лабораторная работа" },
                new WorkType() { ShortName = "СР", FullName = "Самостоятельная работа" },
                new WorkType() { ShortName = "КР", FullName = "Контрольная работа" },
                new WorkType() { ShortName = "ОКР", FullName = "Обязательная контрольная работа" },
                new WorkType() { ShortName = "З", FullName = "Зачёт" },
                new WorkType() { ShortName = "Э", FullName = "Экзамен" },
                new WorkType() { ShortName = "КП", FullName = "Курсовое проектирование" },
            };
            CreateEntities(workTypes, _workTypeRepository);
            Console.WriteLine($"Created work types");

            var specialities = new List<Speciality>
            {
                new Speciality { ShortName = "ИП", FullName = "Информатика и программирование" },
                new Speciality { ShortName = "ЭТ", FullName = "Электротехника" },
                new Speciality { ShortName = "ПТЭ", FullName = "Промышленная теплоэнергетика" },
            };
            CreateEntities(specialities, _specialityRepository);
            Console.WriteLine($"Created specs");

            var subjects = new List<Subject>()
            {
                new Subject { ShortName = "Матем", FullName = "Математика" },
                new Subject { ShortName = "Физ", FullName = "Физика" },
                new Subject { ShortName = "СПЭ", FullName = "Современная политэкономия" },
                new Subject { ShortName = "ИстБел", FullName = "История Беларуси в контексте мировой цивилизации" },
                new Subject { ShortName = "Сопромат", FullName = "Сопротивление материалов" },
                new Subject { ShortName = "ИГ", FullName = "Инженерная графика" },
                new Subject { ShortName = "Инф", FullName = "Информатика" },
                new Subject { ShortName = "Прогр", FullName = "Программирование" },
            };
            CreateEntities(subjects, _subjectRepository);
            Console.WriteLine($"Created subjects");

            var groupsList = new List<Group>();
            var groups = specialities.Select(x => new List<Group>()
            {
                new Group { Name = $"1{x.ShortName}21", SpecialityId = x.Id, Cource = 1, EnrollYear = 2023, Graduated = false },
                new Group { Name = $"1{x.ShortName}22", SpecialityId = x.Id, Cource = 1, EnrollYear = 2023, Graduated = false },
            });

            foreach (var list in groups)
            {
                groupsList.AddRange(list);
            };
            CreateEntities(groupsList, _groupRepository);
            Console.WriteLine($"Created groups");

            var users = GenerateUsers(50, UserRole.Student, groupsList.Select(x => x.Id).ToList());
            for (int i = 0; i < 10; i++)
            {
                users[i].Role = UserRole.Teacher;
                users[i].GroupId = null;
            }

            CreateEntities(users, _userRepository);
            Console.WriteLine($"Created users");

            // Учебный план

            var random = new Random();

            var units = new List<CurriculumUnit>();

            var unitsGroup = specialities.Select(sp =>
                GetCurriculumUnits(sp.Id, subjects[random.Next(0, subjects.Count - 1)].Id, 1,
                workTypes[0].Id, workTypes[3].Id, workTypes[4].Id, workTypes[5].Id, workTypes[6].Id).ToList());

            foreach(var one in unitsGroup)
            {
                units.AddRange(one);
            }

            CreateEntities(units, _curUnitRepository);
            Console.WriteLine($"Created cur units");

            // аттестации
            var teachers = users.Where(u => u.Role == UserRole.Teacher).ToList();
            var futureDateTime = DateTime.Now.AddDays(3);

            var attestations = new List<Attestation>();

            for (int i = 0; i < 15; i++)
            {
                attestations.Add(
                    new Attestation
                    {
                        Date = futureDateTime.AddDays(i*2),
                        CurriculumUnitId = units[random.Next(0, units.Count-1)].Id,
                        GroupId = groupsList[random.Next(0, groupsList.Count - 1)].Id,
                        TeacherId = teachers[random.Next(0, teachers.Count - 1)].Id
                    });
            }

            CreateEntities(attestations, _attestationRepository);
            Console.WriteLine($"Created atts");
        }

        private static List<CurriculumUnit> GetCurriculumUnits(Guid specialityId, Guid subjectId, int sem,
            Guid labId, Guid orkId, Guid zachId, Guid examId, Guid kursId)
        {
            var nextSem = sem + 1;
            return new List<CurriculumUnit>
            {
                new() { Name = "ЛР 1", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 2", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 3", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 4", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 5", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ОКР 1", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = orkId  },
                new() { Name = "Зачёт", Semester = sem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = zachId  },
                new() { Name = "ЛР 5", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 6", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 7", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 8", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ЛР 9", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = labId  },
                new() { Name = "ОКР 2", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = orkId  },
                new() { Name = "Экзамен", Semester = nextSem, SpecialityId = specialityId, SubjectId = subjectId, WorkTypeId = examId  },
            };
        }

        static void Main(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=StudentsDb;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
                "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using var context = new StudentsAppContext(
                new DbContextOptionsBuilder()
                    .UseSqlServer(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            _specialityRepository = new GenericRepository<Speciality>(context);
            _subjectRepository = new GenericRepository<Subject>(context);
            _workTypeRepository = new GenericRepository<WorkType>(context);
            _curUnitRepository = new GenericRepository<CurriculumUnit>(context);
            _groupRepository = new GenericRepository<Group>(context);
            _attestationRepository = new GenericRepository<Attestation>(context);
            _userRepository = new GenericRepository<User>(context);
            _markRepository = new GenericRepository<Mark>(context);
            _retakeResultRepository = new GenericRepository<RetakeResult>(context);

            CreateEntities();

            Console.WriteLine("Done!");
        }

        static void CreateEntities<T>(List<T> items, IRepository<T> repository)
            where T : class, IEntity
        {
            foreach (var one in items)
            {
                var createdId = repository.Create(one);
                one.Id = createdId;
            }
        }

        static void DeleteEntities<T>(IRepository<T> repository)
            where T : class, IEntity
        {
            var itemsToDelete = repository.GetAll();
            foreach (var deleteItem in itemsToDelete)
            {
                repository.Delete(deleteItem.Id);
            }
        }

        public static List<User> GenerateUsers(int number, UserRole role, List<Guid> groupIds)
        {
            var surnames = new List<string>
            {
                "Иванов", "Петров", "Глаголев", "Сухов", "Мишин", "Туполев", "Пименов", "Болдырев", "Сапожников"
            };

            var neutralSurnames = new List<string>
            {
                "Сидоренко", "Дмитренко", "Зубко", "Миханович", "Попкович", "Тумар",
            };

            var maleNames = new List<string>
            { "Алексей", "Иван", "Дмитрий", "Сергей", "Михаил", "Григорий", "Пётр", "Георгий", "Владимир", "Кирилл" };

            var lastNames = new List<string>
            { "Алексее", "Ивано", "Дмитрие", "Сергее", "Михаило", "Эдуардо", "Александро" };

            var femaleNames = new List<string>
            {
                "Екатерина", "Анна", "Ольга", "Наталья", "Ирина", "Валерия", "Анастасия"
            };

            var random = new Random();

            var range = Enumerable.Range(1, number+1);

            var users = range.Select(x =>
                new User { Email = $"user{x}@mail.box",
                    FirstName = femaleNames[random.Next(0,femaleNames.Count - 1)],
                    MiddleName = lastNames[random.Next(0, lastNames.Count - 1)]+"вна",
                    LastName = surnames[random.Next(0, surnames.Count - 1)]+"а",
                    Role = role,
                    PasswordHash = "123321",
                    GroupId = groupIds[random.Next(0, groupIds.Count - 1)]
                })
                .ToList();

            users.AddRange(
                range.Select(x =>
                new User
                {
                    Email = $"user{x}@mail.box",
                    FirstName = maleNames[random.Next(0, maleNames.Count - 1)],
                    MiddleName = lastNames[random.Next(0, lastNames.Count - 1)] + "вич",
                    LastName = surnames[random.Next(0, surnames.Count - 1)],
                    Role = role,
                    PasswordHash = "123321",
                    GroupId = groupIds[random.Next(0, groupIds.Count - 1)]
                }).ToList()
                );

            return users;
        }
    }
}
