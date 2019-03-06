using Linkedin.Layers.BL.Managers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Linkedin.Models;

namespace Linkedin
{
    public class LinkedInHubExchangeModel
    {
        //Announce comment.
        public int PostId { get; set; }
        public string PostUserId { get; set; }
        public string CommentWriterId { get; set; }
        public string Date { get; set; }
        public string CommentContent { get; set; }
        //Announce comment.    
    }

    [HubName("linkedInHub")]
    public class LinkedInHub : Hub
    {
        public override Task OnConnected()
        {
            string name = Context.User.Identity.GetUserId();

            Groups.Add(Context.ConnectionId, name).Wait();

            return base.OnConnected();
        }


        public void AnnounceComment(LinkedInHubExchangeModel model)
        {
            string userID = Context.User.Identity.GetUserId();
            ApplicationUser owner;
            using (var context = new ApplicationDbContext())
            {
                owner = context.Users.Include(u => u.Friends).Where(u => u.Id == model.PostUserId).FirstOrDefault();
            }

            var idList = owner.Friends.Select(f => f.FriendUserID).ToList();

            idList.Add(model.PostUserId);

            Clients.OthersInGroups(idList).AnnounceComment(model.PostId, model.CommentWriterId, model.CommentContent);
        }

        public void AnnounceLike(LinkedInHubExchangeModel model)
        {

        }





    }



}
