namespace portfolioApi.Models
{
    public class Utility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ProjectUtility> ProjectUtilities { get; set; } = new();
    }
}
