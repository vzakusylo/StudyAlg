
namespace Course.Models
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
