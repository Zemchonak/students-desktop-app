using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Enums;
using StudentsManagement.DataAccess.Repositories;
using MT = StudentsManagement.DataAccess.Enums.MonitoringType;

namespace StudentsManagement.DatabaseDataApp
{
    public class Program
    {
        private static IRepository<Faculty> _facultyRepository;
        private static IRepository<Speciality> _specialityRepository;
        private static IRepository<Discipline> _disciplineRepository;
        private static IRepository<CurriculumUnit> _curUnitRepository;
        private static IRepository<Group> _groupRepository;
        private static IRepository<Attestation> _attestationRepository;
        private static IRepository<User> _userRepository;
        private static IRepository<Mark> _markRepository;
        private static IRepository<RetakeResult> _retakeResultRepository;

        static void CreateEntities()
        {
            var faculties = new List<Faculty>
            {
                new Faculty { ShortName = "ГФ", FullName = "Гуманитарный факультет" },
                new Faculty { ShortName = "ЭФ", FullName = "Энергетический факультет" },
                new Faculty { ShortName = "КСиС", FullName = "Факультет компьютерных сетей и систем" },
                new Faculty { ShortName = "МСФ", FullName = "Машиностроительный факультет" },
            };
            DeleteEntities(_facultyRepository);
            CreateEntities(faculties, _facultyRepository);
            Console.WriteLine($"Created facs");

            var specialities = new List<Speciality>
            {
                new Speciality { FacultyId = faculties[0].Id, ShortName = "ЭУП", FullName = "Экономика и управление проектами" },
                new Speciality { FacultyId = faculties[0].Id, ShortName = "МОЭ", FullName = "Маркетинг и отраслевая экономика" },
                new Speciality { FacultyId = faculties[1].Id, ShortName = "ПТЭ", FullName = "Промышленная теплоэнергетика" },
                new Speciality { FacultyId = faculties[1].Id, ShortName = "ЭТ", FullName = "Электротехника" },
                new Speciality { FacultyId = faculties[2].Id, ShortName = "ИП", FullName = "Информатика и технологии программирования" },
                new Speciality { FacultyId = faculties[2].Id, ShortName = "АСОИ", FullName = "Автоматизированные сисемы обработки информации" },
                new Speciality { FacultyId = faculties[2].Id, ShortName = "ПЭ", FullName = "Промышленная электроника" },
                new Speciality { FacultyId = faculties[3].Id, ShortName = "СХ", FullName = "Сельскохозяйственные машины" },
                new Speciality { FacultyId = faculties[3].Id, ShortName = "РК", FullName = "Робототехника и автоматизированные комплексы" },
            };
            DeleteEntities(_specialityRepository);
            CreateEntities(specialities, _specialityRepository);
            Console.WriteLine($"Created specs");

            var disciplines = new List<Discipline>()
            {
                new Discipline { ShortName = "ВМ", FullName = "Высшая математика" },
                new Discipline { ShortName = "ЛАиАГ", FullName = "Линейная алгебра и аналитическая геометрия" },
                new Discipline { ShortName = "СПЭ", FullName = "Современная политэкономия" },
                new Discipline { ShortName = "ИстБел", FullName = "Истори Беларуси в контексте мировой цивилизации" },
                new Discipline { ShortName = "Сопромат", FullName = "Сопротивление материалов" },
                new Discipline { ShortName = "ИГ", FullName = "Инженерная графика" },
                new Discipline { ShortName = "Инф", FullName = "Информатика" },
                new Discipline { ShortName = "Прогр", FullName = "Программирование" },
                new Discipline { ShortName = "КС", FullName = "Компьютерные сети" },
                new Discipline { ShortName = "Физ", FullName = "Физика" },
            };
            DeleteEntities(_disciplineRepository);
            CreateEntities(disciplines, _disciplineRepository);
            Console.WriteLine($"Created discs");

            var groups = specialities.Select(x => new List<Group>()
            {
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 1, Number = 1, EnrollYear = 2023, Graduated = false },
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 1, Number = 2, EnrollYear = 2023, Graduated = false },
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 2, Number = 1, EnrollYear = 2022, Graduated = false },
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 2, Number = 2, EnrollYear = 2022, Graduated = false },
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 3, Number = 1, EnrollYear = 2021, Graduated = false },
                new Group { SpecialityShortName = x.ShortName, SpecialityId = x.Id, Cource = 3, Number = 2, EnrollYear = 2021, Graduated = false },
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

            var curUnits = new List<CurriculumUnit>();

            curUnits.AddRange(CreateSpecialityCurriculum(specialities, disciplines, 0, 0, [
                ,
                (2, MonitoringType.Exam),
            ]));
            curUnits.AddRange(CreateSpecialityCurriculum(specialities, disciplines, 0, 0, [
                (1, MonitoringType.Zach),
                (2, MonitoringType.Exam),
            ]));
            /*
                0 "Экономика и управление проектами" },
                0 - 1z,2e,3z
                2 - 1z,2c,2e,3z
                3 - 1z,2z,3e


                1 "Маркетинг и отраслевая экономика" },
                0 - 1z,2e,3z
                2 - 1z,2c,2e,3z
                3 - 1z,2z,3e

               2 "Историческое дело" },
                0 - 1z,2e,3z
                2 - 1z,2e,3z
                3 - 1e,2c,2e,3e

               3 "Промышленная теплоэнергетика" },

                0 1e 2e 3z
                1 1e
                2 2z


                new Discipline { ShortName = "ВМ", FullName = "Высшая математика" },

                new Discipline { ShortName = "ЛАиАГ", FullName = "Линейная алгебра и аналитическая геометрия" },
                new Discipline { ShortName = "СПЭ", FullName = "Современная политэкономия" },
                new Discipline { ShortName = "ИстБел", FullName = "Истори Беларуси в контексте мировой цивилизации" },

                new Discipline { ShortName = "Сопромат", FullName = "Сопротивление материалов" },
                new Discipline { ShortName = "ИГ", FullName = "Инженерная графика" },
                new Discipline { ShortName = "Инф", FullName = "Информатика" },

                new Discipline { ShortName = "Прогр", FullName = "Программирование" },
                new Discipline { ShortName = "КС", FullName = "Компьютерные сети" },
                new Discipline { ShortName = "Физ", FullName = "Физика" },

               4 "Электротехника" },

               5 "Атомная энергетика" },

               6 "Информатика и технологии программирования" },

               7 "Автоматизированные сисемы обработки информации" },

               8 "Промышленная электроника" },

               9 "Сельскохозяйственные машины" },

               10 "Робототехника и автоматизированные комплексы" },



             */

            DeleteEntities(_curUnitRepository);
            CreateEntities(curUnits, _curUnitRepository);
        }

        static List<CurriculumUnit> CreateSpecialityCurriculum(List<Speciality> specialities, List<Discipline> disciplines,
            int specialityIndex, int disciplineIndex, (int cource, MonitoringType type)[] units)
        {
            var specialityId = specialities[specialityIndex].Id;
            var disciplineId = disciplines[disciplineIndex].Id;

            return units.Select(x =>
                new CurriculumUnit {
                    SpecialityId = specialityId,
                    Cource = x.cource,
                    DisciplineId = disciplineId,
                    Type = x.type
                }).ToList();
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

            _facultyRepository = new GenericRepository<Faculty>(context);
            _specialityRepository = new GenericRepository<Speciality>(context);
            _disciplineRepository = new GenericRepository<Discipline>(context);
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
