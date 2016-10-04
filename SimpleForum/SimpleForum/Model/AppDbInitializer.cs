using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleForum.Model
{
    public class AppDbInitializer : IDisposable
    {
        private UserManager<AppUser> userManager;
        private AppDbContext dc;

        private AppUser user1, user2, user3;

        public AppDbInitializer(UserManager<AppUser> userManager, AppDbContext dc)
        {
            this.userManager = userManager;
            this.dc = dc;
        }

        public async Task InitializeDatabaseAsync()
        {
            dc.Database.EnsureCreated();

            await CreateUsers().ConfigureAwait(false);
            await CreateForumThreads().ConfigureAwait(false);
        }

        private async Task CreateUsers()
        {
            user1 = await userManager.FindByEmailAsync("user1@mail.com");
            if (user1 == null)
            {
                user1 = new AppUser()
                {
                    Email = "user1@mail.com",
                    UserName = "user1"
                };
                await userManager.CreateAsync(user1);
                await userManager.AddPasswordAsync(user1, "simpleforum");
            }

            user2 = await userManager.FindByEmailAsync("user2@mail.com");
            if (user2 == null)
            {
                user2 = new AppUser()
                {
                    Email = "user2@mail.com",
                    UserName = "user2"
                };
                await userManager.CreateAsync(user2);
                await userManager.AddPasswordAsync(user2, "simpleforum");
            }

            user3 = await userManager.FindByEmailAsync("user3@mail.com");
            if (user3 == null)
            {
                user3 = new AppUser()
                {
                    Email = "user3@mail.com",
                    UserName = "user3"
                };
                await userManager.CreateAsync(user3);
                await userManager.AddPasswordAsync(user3, "simpleforum");
            }
        }

        private async Task CreateForumThreads()
        {
            if (!dc.ForumThreads.Any())
            {
                dc.ForumThreads.Add(new ForumThread()
                {
                    CreatedDate = new DateTime(2016, 7, 3, 14, 5, 32),
                    Title = "First DotVVM Question",
                    ForumPosts =
                    {
                        new ForumPost()
                        {
                            Message = "Is DotVVM the best framework in the world?",
                            CreatedDate = new DateTime(2016, 7, 3, 14, 5, 32),
                            AppUserId = user1.Id
                        },
                        new ForumPost()
                        {
                            Message = "Yes.",
                            CreatedDate = new DateTime(2016, 7, 4, 8, 41, 12),
                            AppUserId = user2.Id
                        },
                        new ForumPost()
                        {
                            Message = "Of course. You don't have to write tons of JS code at all!",
                            CreatedDate = new DateTime(2016, 7, 4, 11, 32, 12),
                            AppUserId = user3.Id
                        }
                    }
                });
                await dc.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            userManager.Dispose();
            dc.Dispose();
        }
    }
}
