namespace ChildrenWebApp.Models
{
    public class ChildModel
    {
        public Child Child { get; set; }

        public List<string> AnimalOptions { get; set; } = new List<string> { "Dog", "Cat", "Bunny", "Pony", "Fish" };

        public List<string> GenderOptions { get; set; } = new List<string> { "Male", "Female", "Diverse" };

        public List<Child> ChildrenList { get; set; } = new List<Child>();

        public string Notification { get; set; }

        public ChildModel()
        {
            
        }

    }
}
