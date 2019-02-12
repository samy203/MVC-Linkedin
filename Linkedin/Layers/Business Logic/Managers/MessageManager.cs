namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class MessageManager : Repository<Message, ApplicationDbContext>
    {
        public MessageManager(ApplicationDbContext context) : base(context)
        { }
    }
}