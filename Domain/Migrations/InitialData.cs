using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Migrations
{
    public  class InitialData
    {
        public static async Task Seed(IConfiguration Configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                          RoleManager<ApplicationRole> roleManager)
        { 
            context.Database.EnsureCreated();

            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            // creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var user = await userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // here we assign the new user the "Admin" role 
                    await userManager.AddToRoleAsync(poweruser, "Admin");
                }
            }

            //Create a member
            var userUser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["MemberEmail"],
                Email = Configuration.GetSection("UserSettings")["MemberEmail"]
            };


            string userUserPassword = Configuration.GetSection("UserSettings")["MemberPassword"];
            var tempUser = await userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["MemberEmail"]);

            if (tempUser == null)
            {
                var createUserUser = await userManager.CreateAsync(userUser, userUserPassword);
                if (createUserUser.Succeeded)
                {
                    // here we assign the new user the "Member" role 
                    await userManager.AddToRoleAsync(userUser, "Member");
                }

            }
            else { 
                if(!context.Posts.Any())
                { 
            Post post = new Post
            {
                User = userUser,
                Title = "Hamlet",
                Created = DateTime.Now,
                Introduction = "Hazesan allihopa",
                TasteNote = new TasteNote
                    {
                    Apperance = "Black",
                    Aroma = "Funky",
                    Conclusion = "Awesome",
                    Improvments = "None. It's perfect",
                    Mouthfeel = "THICCCC",
                    Taste = "Massive attack"
                },
                Recipe = new Recipe
                {
                    MashSteps = new List<MashStep>()
                         {
                             new MashStep
                             {
                                 Temp=65,
                                 Time=60,
                             },
                             new MashStep
                             {
                                 Temp=74,
                                 Time=10,
                             }
                         },
                    BoilTime = 120,
                    FermentationDays = 7,
                    FermentationTemp = 20,
                    Style=new Style
                    {
                        Description = "Maffig",
                        Name = "Imperial Stout"
                    },
                    Ingredients = new List<Ingredient>()
                    {
                             new Ingredient
                             {
                                 Manufacturer= "Briess",
                                 Amount=2.3,
                                 Name="Pale ale malt",
                                 EBC = 20,
                                 Type = IngredientType.BASE_MALT
                             },
                             new Ingredient
                             {
                                 Manufacturer="Crisp",
                                 Amount=0.1,
                                 Name="Crystal 240",
                                 EBC=240,
                                 Type = IngredientType.CRYSTAL_MALT
                             },
                       new Ingredient
                             {
                                 Manufacturer ="",
                                 Amount=120,
                                 ContactTimeMinutes=0,
                                 Name = "Citra",
                                 AlphaAcid = 19.2,
                                 Type=IngredientType.HOP
                             },
                              new Ingredient
                             {
                                 Manufacturer ="",
                                 Amount=120,
                                 ContactTimeDays=5,
                                 Name = "Mosaic",
                                 DryHop = true,
                                 AlphaAcid = 10.3,
                                 Type=IngredientType.HOP

                             },
                        new Ingredient

                        {
                                 Manufacturer="WYeast",
                                 Amount=1,
                                 Name="London Ale III",
                                 Type = IngredientType.YEAST

                        }
                    }},
                TechnicalData = new TechnicalData
                {
                    BrewVol = 19.2,
                    FinalVol = 17,
                    BrewDate = DateTime.Now.AddDays(-7),
                    BottleDate = DateTime.Now,
                    ABV = 9.2,
                    OG = 1.102,
                    FG = 1.032,
                }
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            }
            }
        }

    }
}

