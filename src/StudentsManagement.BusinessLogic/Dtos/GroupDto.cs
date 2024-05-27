namespace StudentsManagement.BusinessLogic.Dtos
{
    public class GroupDto : BaseDto
    {
        public string SpecialityShortName { get; set; }
        public int Cource { get; set; }
        public int Number { get; set; }
        public string Name { get => $"{SpecialityShortName}-{Cource}{Number}"; }

        public bool Graduated { get; set; }
        public int EnrollYear { get; set; }
        public Guid SpecialityId { get; set; }

        public string GraduatedText { get => Graduated ? "Да" : "Нет"; }
    }
}
