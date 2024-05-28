using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Enums;
using StudentsManagement.DataAccess.Repositories;

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
            var specialities = new List<Speciality>
            {
                new Speciality { ShortName = "ЭУП", FullName = "Экономика и управление проектами" },
                new Speciality { ShortName = "МОЭ", FullName = "Маркетинг и отраслевая экономика" },
                new Speciality { ShortName = "ПТЭ", FullName = "Промышленная теплоэнергетика" },
                new Speciality { ShortName = "ЭТ", FullName = "Электротехника" },
                new Speciality { ShortName = "ИП", FullName = "Информатика и технологии программирования" },
                new Speciality { ShortName = "АСОИ", FullName = "Автоматизированные сисемы обработки информации" },
                new Speciality { ShortName = "ПЭ", FullName = "Промышленная электроника" },
                new Speciality { ShortName = "СХ", FullName = "Сельскохозяйственные машины" },
                new Speciality { ShortName = "РК", FullName = "Робототехника и автоматизированные комплексы" },
            };
            DeleteEntities(_specialityRepository);
            CreateEntities(specialities, _specialityRepository);
            Console.WriteLine($"Created specs");

            var subjects = new List<Subject>()
            {
                new Subject { ShortName = "Матем", FullName = "Математика" },
                new Subject { ShortName = "ЛАиАГ", FullName = "Линейная алгебра и аналитическая геометрия" },
                new Subject { ShortName = "СПЭ", FullName = "Современная политэкономия" },
                new Subject { ShortName = "ИстБел", FullName = "История Беларуси в контексте мировой цивилизации" },
                new Subject { ShortName = "Сопромат", FullName = "Сопротивление материалов" },
                new Subject { ShortName = "ИГ", FullName = "Инженерная графика" },
                new Subject { ShortName = "Инф", FullName = "Информатика" },
                new Subject { ShortName = "Прогр", FullName = "Программирование" },
                new Subject { ShortName = "КС", FullName = "Компьютерные сети" },
                new Subject { ShortName = "Физ", FullName = "Физика" },
            };
            DeleteEntities(_subjectRepository);
            CreateEntities(subjects, _subjectRepository);
            Console.WriteLine($"Created discs");

            var groups = specialities.Select(x => new List<Group>()
            {
                new Group { Name = "1гр", SpecialityId = x.Id, Cource = 1, EnrollYear = 2023, Graduated = false },
                new Group { Name = "2гр", SpecialityId = x.Id, Cource = 1, EnrollYear = 2023, Graduated = false },
                new Group { Name = "3гр", SpecialityId = x.Id, Cource = 2, EnrollYear = 2022, Graduated = false },
                new Group { Name = "4гр", SpecialityId = x.Id, Cource = 2, EnrollYear = 2022, Graduated = false },
                new Group { Name = "5гр", SpecialityId = x.Id, Cource = 3, EnrollYear = 2021, Graduated = false },
                new Group { Name = "6гр", SpecialityId = x.Id, Cource = 3, EnrollYear = 2021, Graduated = false },
            });

            DeleteEntities(_groupRepository);
            foreach (var specialityGroup in groups)
            {
                CreateEntities(specialityGroup, _groupRepository);
            };
            Console.WriteLine($"Created groups");

            var units = new (int cource, int type)?[9][]
            {
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
                [ null, null, null, null, null ],
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
    }
}
