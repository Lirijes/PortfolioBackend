namespace portfolioApi.Models
{
    public class ProjectUtility
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int UtilityId { get; set; }
        public Utility Utility { get; set; }

        // This creates a PK of two columns (ProjectId + UtilityId)
        public override int GetHashCode() => HashCode.Combine(ProjectId, UtilityId);
    }
}
