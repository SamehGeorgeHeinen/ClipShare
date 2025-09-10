using ClipShare.Core.Entities;
using ClipShare.DataAccess.Data;
using ClipShare.Services.IServices;
using ClipShare.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Seed
{
    public static  class ContextInitializer
    {

        
        public static async Task InitializeAsync(Context context
            , UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , IPhotoService photoService,IWebHostEnvironment webHostEnvironment)
        {


            if (context.Database.GetPendingMigrations().Count() > 0)
            {
                context.Database.Migrate();

            }
            if (!roleManager.Roles.Any())
            {
                foreach (var role in SD.Roles)
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }
            }
            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    Name = "admin",
                    Email = "admin@example.com",
                    UserName = "admin",

                };
                await userManager.CreateAsync(admin, "Password123");
                await userManager.AddToRolesAsync(admin, [SD.AdminRole, SD.UserRole, SD.ModeratorRole]);

                var john = new AppUser
                {
                    Name = "john",
                    Email = "john@example.com",
                    UserName = "john"

                };
                await userManager.CreateAsync(john, "Password123");
                await userManager.AddToRoleAsync(john, SD.UserRole);
                var JohnChannel = new Channel
                {
                    Name = "JohnChannel",
                    About = "This is a lovely and nice and awsome amazing magnificient Channel",
                    AppUserId = john.Id


                };context.Channel.Add(JohnChannel);
                var peter = new AppUser
                {
                    Name = "peter",
                    Email = "peter@example.com",
                    UserName = "peter",

                };
                await userManager.CreateAsync(peter, "Password123");
                await userManager.AddToRoleAsync(peter, SD.UserRole);
                var PeterChannel = new Channel
                {
                    Name = "PeterChannel",
                    About = "This is a lovely and nice and awsome amazing magnificient Channel",
                    AppUserId = peter.Id


                }; context.Channel.Add(PeterChannel);
                var mary = new AppUser
                {
                    Name = "mary",
                    Email = "mary@example.com",
                    UserName = "mary",

                };
                await userManager.CreateAsync(mary, "Password123");
                await userManager.AddToRoleAsync(mary, SD.ModeratorRole);
                //add categories to database
                var animal = new Category { Name = "Animal" };
                var food = new Category { Name = "Food" };
                var game = new Category { Name = "Game" };
                var news = new Category { Name = "News" };
                var sport = new Category { Name = "Sport" };
                var nature = new Category { Name = "Nature" };

                context.Category.Add(animal);
                context.Category.Add(food);
                context.Category.Add(game);
                context.Category.Add(sport);
                context.Category.Add(news);
                context.Category.Add(nature);

                await context.SaveChangesAsync();
                var folderpath = Path.Combine(webHostEnvironment.WebRootPath, "images");
                //check if folder exists
                if(Directory.Exists(folderpath))
                {
                    Directory.Delete(folderpath, true);
                }
                //add videos and images to database
                var imageDirectory = new DirectoryInfo("Seed/Files/Thumbnails");
                var videoDirectory = new DirectoryInfo("Seed/Files/Videos");
                FileInfo[] imageFiles = imageDirectory.GetFiles();
                FileInfo[] videoFiles = videoDirectory.GetFiles();

                var description = "In a saga that has dragged out across the summer, Betis have been unable to strike an agreement with United who are determined to offload him permanently after three miserable seasons at the club.";

                for (int i = 0; i <30;i++)
                {
                    var allNames = videoFiles[i].Name.Split('-');
                    var categoryName =allNames[0];
                    var title= allNames[2].Split(".")[0];
                    var categoryId=await context.Category.Where(x=>x.Name.ToLower() == categoryName).Select(x=>x.Id).FirstOrDefaultAsync();
                    IFormFile imageFile = ConvertToFile(imageFiles[i]);
                    IFormFile videoFile = ConvertToFile(videoFiles[i]);
                    var videoToAdd = new Video
                    {
                        Title = title,
                        Description= description,
                        CategoryId= categoryId,
                        VideoFile= new VideoFile
                        {
                            ContentType = SD.GetContentType(videoFiles[i].Extension),

                            Contents = GetContentsAsync(videoFile).GetAwaiter().GetResult(),
                            Extension = videoFiles[i].Extension
                        },
                        ThumbnailUrl=photoService.UploadPhotoLocally(imageFile),
                        ChannelId=(i%2==0)?JohnChannel.Id:PeterChannel.Id,
                        CreatedAt= SD.GetRandomDate(new DateTime(2015,1,1),DateTime.UtcNow,i)

                    };
                    context.Video.Add(videoToAdd);
                    await context.SaveChangesAsync();

                }

            }
        }
        #region Private Methods
        private static IFormFile ConvertToFile(FileInfo fileInfo)
        {
            //open the file stream
            var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            //Create the IFormFile instance using the file stream
            IFormFile formFile = new FormFile(
                stream,  //FileStream
                0,//Start position of steam
                fileInfo.Length,//File Length
                fileInfo.Name,//Name of the form field
                fileInfo.Name//File name

                );
            return formFile;
        }
        private static async Task<byte[]> GetContentsAsync(IFormFile file)
        {
            byte[] contents;
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            contents = memoryStream.ToArray();
            return contents;
        }
        #endregion
    }
}
