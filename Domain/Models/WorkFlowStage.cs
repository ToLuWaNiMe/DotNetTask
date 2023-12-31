﻿namespace Domain.Models
{
    public class WorkFlowStage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Questions { get; set; }
        public string AdditionalInfo { get; set; }
        public string Duration { get; set; }
        public int DeadLine { get; set; }
    }
}
