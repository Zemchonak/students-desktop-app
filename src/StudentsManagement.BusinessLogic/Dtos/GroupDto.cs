namespace StudentsManagement.BusinessLogic.Dtos
{
    public class GroupDto : BaseDto
    {
        public int Cource { get; set; }
        public string Name { get; set; }

        public bool Graduated { get; set; }
        public int EnrollYear { get; set; }
        public Guid SpecialityId { get; set; }

        public string GraduatedText { get => Graduated ? "Да" : "Нет"; }
    }
}
