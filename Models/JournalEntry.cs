using SQLite;
using System;
using System.Collections.Generic;

namespace Journal_Application.Models
{
    public class JournalEntry
    {
        [PrimaryKey]
        public string DateKey { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string PrimaryMood { get; set; } = "";
        public string SecondaryMoods { get; set; } = ""; // Will store as comma-separated
        public string Tags { get; set; } = ""; // Will store as comma-separated

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}