namespace TaskManagementAPI.Models
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int MemberCount { get; set; }
        public int TaskCount { get; set; }
        public TeamDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;

         }
    }

}
