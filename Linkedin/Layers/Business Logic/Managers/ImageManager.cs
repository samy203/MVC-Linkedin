namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class ImageManager : Repository<Image, ApplicationDbContext>
    {
        public ImageManager(ApplicationDbContext context) : base(context)
        { }
    }
}