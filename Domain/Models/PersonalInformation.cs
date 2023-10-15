﻿namespace Domain.Models
{
    public class PersonalInformation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
