using System;

namespace def_fun_domains_as_primary_line_of_defense.Models
{
    public class PersonalName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }

        public PersonalName(string firstName, string middleNames, string lastName)
        {
            if (IsBadMandatoryPart(firstName) ||
                IsBadOptionalPart(MiddleNames) ||
                IsBadMandatoryPart(lastName))
                throw new ArgumentException();

            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
        }

        private bool IsBadOptionalPart(string part) => part == null ||
            part.Length > 0 && char.IsHighSurrogate(part[part.Length - 1]);

        private bool IsBadMandatoryPart(string part) =>
            IsBadOptionalPart(part) || part == string.Empty;

        public override bool Equals(object obj)
        {
            return Equals(obj as PersonalName);
        }

        private bool Equals(PersonalName other) =>
            other != null &&
            ArePartsEqual(other.FirstName, FirstName) &&
            ArePartsEqual(other.LastName, LastName) &&
            ArePartsEqual(other.MiddleNames, MiddleNames);


        public override int GetHashCode() =>
            FirstName.GetHashCode() ^
            MiddleNames.GetHashCode() ^
            LastName.GetHashCode();

        private bool ArePartsEqual(string part1, string part2) =>
            string.Compare(part1, part2, StringComparison.OrdinalIgnoreCase) == 0;
        // string.Equals(p.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase)

    }

}
