﻿namespace portfolioApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectUrl { get; set; }
        public string Description { get; set; }
        public string Improvements { get; set; }
        public string LessonsLearned { get; set; }
        public string Notes { get; set; }
        public string GithubUrl { get; set; }
        public string Status { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }

        public List<ProjectUtility> ProjectUtilities { get; set; } = new();
    }
}
