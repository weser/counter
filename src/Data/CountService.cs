using booka.counter.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace booka.counter.Data
{
    public class CountService
    {
        private readonly ApplicationDbContext context;
        private readonly AuthenticationStateProvider asProvider;
        private readonly UserManager<ApplicationUser> userManager;
        private static readonly List<Count> cachedCounts = new();
        private static readonly List<Tag> cachedTags = new();

        private static readonly Dictionary<Guid, TagPageModel> sharedPageModels = new();

        public CountService(ApplicationDbContext context, AuthenticationStateProvider asProvider, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.asProvider = asProvider;
            this.userManager = userManager;
        }

        public async Task<List<Tag>> GetMyTags()
        {
            await EnsureCache();
            var cp = (await asProvider.GetAuthenticationStateAsync()).User;
            var user = await userManager.GetUserAsync(cp);
            if (user == null) return new List<Tag>();

            var tagIds = cachedCounts.Where(c => c.UserId == user.Id).Select(c => c.TagId).Distinct().ToList();
            return cachedTags.Where(t => tagIds.Contains(t.Id)).OrderBy(t => t.Title).ToList();
        }

        public async Task<ApplicationUser> GetUser()
        {
            var cp = (await asProvider.GetAuthenticationStateAsync()).User;
            return await userManager.GetUserAsync(cp);
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await context.Users.OrderBy(u => u.UserName).ToListAsync();
        }

        public async Task<TagPageModel> GetPageModel(Guid tagId)
        {
            await EnsureCache();

            if (!sharedPageModels.ContainsKey(tagId))
                sharedPageModels.Add(tagId, new TagPageModel()
                {
                    Tag = cachedTags.FirstOrDefault(t => t.Id == tagId),
                    Counts = new ObservableCollection<Count>(cachedCounts.Where(c => c.TagId == tagId).ToList())
                });

            return sharedPageModels[tagId];
        }

        public async Task AddCount(Tag tag, int value)
        {
            await EnsureCache();
            var cp = (await asProvider.GetAuthenticationStateAsync()).User;
            var user = await userManager.GetUserAsync(cp); 

            var count = new Count()
            {
                TagId = tag.Id,
                UserId = user.Id,
                Value = value,
                TimeStamp = DateTime.Now
            };

            cachedCounts.Add(count);
            context.Counts.Add(count);
            await context.SaveChangesAsync();

            sharedPageModels[tag.Id].Counts.Add(count);
        }

        public async Task<Tag> CreateTag(string titel, string description)
        {
            var tag = new Tag()
            {
                Description = description,
                Title = titel
            };
            context.Tags.Add(tag);
            await context.SaveChangesAsync();

            cachedTags.Add(tag);

            return tag;
        }

        private async Task EnsureCache()
        {
            if (!cachedCounts.Any()) cachedCounts.AddRange(await context.Counts.ToListAsync());
            if (!cachedTags.Any()) cachedTags.AddRange(await context.Tags.ToListAsync());
        }
    }

    public class TagPageModel : INotifyPropertyChanged
    {
        private Tag tag;

        public Tag Tag
        {
            get => tag;
            set
            {
                if (Equals(Tag, value)) return;
                tag = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tag)));
            }
        }
        public ObservableCollection<Count> Counts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
