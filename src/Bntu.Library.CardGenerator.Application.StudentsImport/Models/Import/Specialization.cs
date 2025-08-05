using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models.Import
{
    public class Specialization
    {
        public string StudyDirection { get; }

        public string Profile { get; }

        public Specialization(string studyDirection, string profile)
        {
            StudyDirection = studyDirection;
            Profile = profile;
        }

        public override bool Equals(object? obj)
        {
            return obj is Specialization specialization &&
                   StudyDirection == specialization.StudyDirection &&
                   Profile == specialization.Profile;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StudyDirection, Profile);
        }
    }
}
