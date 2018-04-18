using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;

namespace ListenAndWrite.ModelIdentify
{
    public class DatabaseInitializerClass : DropCreateDatabaseIfModelChanges<ModelsContent>
    {
        protected override void Seed(ModelsContent context)
        {
            GenMembers().ForEach(m => context.Members.Add(m));
            context.SaveChanges();
            GenCategories().ForEach(c => context.Categories.Add(c));
            context.SaveChanges();
            //GenLession().ForEach(l => context.Lessons.Add(l));
            //context.SaveChanges();
            //GenLessionCategories().ForEach(lc => context.LessonCateogies.Add(lc));
            //context.SaveChanges();
            //GenScores().ForEach(s => context.Scores.Add(s));
            //context.SaveChanges();
        }

        public static List<ApplicationUser> GenMembers()
        {
            List<ApplicationUser> list = new List<ApplicationUser>{
                new ApplicationUser{
                    Id = "1",
                    DictationLanguage = "EN",
                    Point=0,
                    ListeningLevel = 0,
                    Email = "ListenAndWrite.com",
                    EmailConfirmed = false,
                    PasswordHash = "",
                    SecurityStamp = "680307ff-322d-4ef8-8902-182e32409fc6",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "ListenAndWrite.com",
                },
            };
            return list;
        }

        public static List<Category> GenCategories()
        {
            List<Category> list = new List<Category>();
            if (File.Exists(@"C:\Users\luuvanvu\OneDrive\ListenAndWrite\ListenAndWrite\DefineCategory.txt"))
            {
                String[] arr = File.ReadAllLines(@"C:\Users\luuvanvu\OneDrive\ListenAndWrite\ListenAndWrite\DefineCategory.txt");
                foreach(String str in arr){
                    list.Add(new Category{
                        CategoryName = str,
                    });
                }
            }
            return list;
        }

        public static List<Lesson> GenLession()
        {
            List<Lesson> list = new List<Lesson>();
            for (int i = 1; i <= 100; i++)
            {
                list.Add(new Lesson{
                    ProviderID = "1",
                    //ProviderName = "ListenAndWrite.com",
                    Title = "Spring " + i,
                    ViewCount = 0,
                    UploadedTime = DateTime.Now,
                    Length = (i % 4) * 60 + i % 60,
                    Level = i %5 + 1,
                    Language = "EN",
                    Description = "",
                    Tracks = GenLessionParts(),
                });
            }
            return list;
        }

        public static List<Track> GenLessionParts()
        {
            List<Track> list = new List<Track>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Track
                {
                    AudioPath = ModelsContent.PRE_AUDIO_PATH + "Demo.mp3",
                    ScriptText = "I am will not",
                });
            }
            return list;
        }

        public static List<LessonCategory> GenLessionCategories()
        {
            List<LessonCategory> list = new List<LessonCategory>();
            for (int i = 1; i <= 100; i++)
            {
                if (i < 11)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        list.Add(new LessonCategory
                        {
                            LessonID = i,
                            CategoryID = j,
                        });
                    }
                }
                else
                {
                    list.Add(new LessonCategory
                    {
                        LessonID = i,
                        CategoryID = i % 4 + 1,
                    });
                }
            }
            return list;
        }

        //public static List<Score> GenScores()
        //{
        //    List<Score> list = new List<Score>{
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 1,
        //            NearestDateWithBestScore = DateTime.Now,
        //            MaxScore = 80,
        //            TestType = TestType.FullMode,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 2,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(1),
        //            MaxScore = 100,
        //            TestType = TestType.FullMode,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 3,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(2),
        //            MaxScore = 120,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 4,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(2),
        //            MaxScore = 140,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 5,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(3),
        //            MaxScore = 80,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 6,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(4),
        //            MaxScore = 80,
        //        },
        //        new Score{
        //            MemberID = "1",
        //            LessionID = 7,
        //            NearestDateWithBestScore = DateTime.Now.AddDays(4),
        //            MaxScore = 80,
        //        },
        //    };
        //    return list;
        //}
    }
}