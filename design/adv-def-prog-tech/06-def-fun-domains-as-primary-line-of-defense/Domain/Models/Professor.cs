
namespace def_fun_domains_as_primary_line_of_defense.Models
{
    public class Professor
    {    
        public PersonalName Name { get; }

        public Professor(PersonalName name)
        {
            if (name is null)
            {
                throw new System.ArgumentNullException(nameof(name));
            }
            Name = name;
        }
    }

}
