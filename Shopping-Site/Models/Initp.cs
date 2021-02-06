using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class Initp : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {   
     
        protected override void Seed(ApplicationDbContext ctx)
        {  
            
            Processor apple_processor = new Processor
            {
                
                Model = "Apple A13 Bionic",
                Cores_number = 6,
                Frequency = 2.65f
            };

            Processor samsung_processor1 = new Processor
            {
                Model = "Exynos 980",
                Cores_number = 8,
                Frequency = 2.2f
            };

            Processor samsung_processor2 = new Processor
            {
                Model = "Exynos 1080",
                Cores_number = 6,
                Frequency = 2.0f
            };

            Processor snapdragon = new Processor
            {
                Model = "Qualcomm SM7125 Snapdragon 720G ",
                Cores_number = 8,
                Frequency = 2.3f
            };

            Processor Exynos = new Processor
            {
                Model = "Exynos 990",
                Cores_number = 8,
                Frequency = 2.73f
            };

            Processor nokia = new Processor
            {
                Model = "Nokia 606",
                Cores_number = 2,
                Frequency = 1.0f
            };

            ctx.Processors.Add(nokia);
            ctx.Processors.Add(Exynos);
            ctx.Processors.Add(snapdragon);
            ctx.Processors.Add(samsung_processor1);
            ctx.Processors.Add(samsung_processor2);
            ctx.Processors.Add(apple_processor);


            Battery apple_battery = new Battery
            {
                
                Name = "Li-Ion",
                Capacity = 3110
            };

            Battery samsung_battery1 = new Battery
            {
                Name = "Li-Po",
                Capacity = 4500
            };

            Battery xiaomi_battery1 = new Battery
            {
                Name = "Li-Po",
                Capacity = 5020
            };

            Battery samsung_battery2 = new Battery
            {
                Name = "Li-Po",
                Capacity = 5000
            };

            Battery nokia_battery = new Battery
            {
                Name = "Li-Ion",
                Capacity = 1200
            };

            ctx.Batteries.Add(nokia_battery);
            ctx.Batteries.Add(samsung_battery2);
            ctx.Batteries.Add(xiaomi_battery1);
            ctx.Batteries.Add(samsung_battery1);
            ctx.Batteries.Add(apple_battery);

            Feature apple_feature_camera1 = new Feature
            {
                Name = "HDR"
            };
            Feature apple_feature_camera2 = new Feature
            {
                Name = "Panorama"
            };
            Feature apple_feature_camera3 = new Feature
            {
                Name = "Slow motion"
            };
            Feature apple_feature_camera4 = new Feature
            {
                Name = "Geotagging"
            };
            Feature nokia_feature_camera1 = new Feature
            {
                Name = "Fix focus"
            };

            ctx.Features.Add(nokia_feature_camera1);
            ctx.Features.Add(apple_feature_camera1);
            ctx.Features.Add(apple_feature_camera2);
            ctx.Features.Add(apple_feature_camera3);
            ctx.Features.Add(apple_feature_camera4);


            Camera apple_camera1 = new Camera
            {
                Features = new List<Feature>
                {
                    apple_feature_camera1,
                    apple_feature_camera2,
                    apple_feature_camera3,
                    apple_feature_camera4
                },
                Resolution = 12,
                Type = "Main Camera",
                Flash = true,
            };

            Camera apple_camera2 = new Camera
            {
                Features = new List<Feature>
                {
                    apple_feature_camera1
                },
                Resolution = 8,
                Type = "Front Camera",
                Flash = true,
            };


            Camera samsung_camera1 = new Camera
            {
                Features = new List<Feature>
                {
                    apple_feature_camera1,
                    apple_feature_camera2
                },
                Resolution = 64,
                Type = "Main Camera",
                Flash = true,
            };

            Camera samsung_camera2 = new Camera
            {
                Features = new List<Feature>
                {
                    apple_feature_camera1,
                    apple_feature_camera2
                },
                Resolution = 108,
                Type = "Main Camera",
                Flash = true,
            };

            Camera samsung_camera3 = new Camera
            {
                Features = new List<Feature>
                {
                    apple_feature_camera1,
                    apple_feature_camera2
                },
                Resolution = 12,
                Type = "Front Camera",
                Flash = true,
            };

            Camera nokia_camera1 = new Camera
            {
                Features = new List<Feature>
                {
                    nokia_feature_camera1
                },
                Resolution = 2,
                Type = "Front Camera",
                Flash = true,
            };


            Camera nokia_camera2 = new Camera
            {
                Features = new List<Feature>
                {
                    nokia_feature_camera1
                },
                Resolution = 2,
                Type = "Main Camera",
                Flash = true,
            };

            ctx.Cameras.Add(nokia_camera1);
            ctx.Cameras.Add(nokia_camera2);
            ctx.Cameras.Add(samsung_camera3);
            ctx.Cameras.Add(samsung_camera2);
            ctx.Cameras.Add(samsung_camera1);
            ctx.Cameras.Add(apple_camera1);
            ctx.Cameras.Add(apple_camera2);


            ctx.Phones.Add(
                new Phone
                {
                    Name = "Iphone X",
                    Brand = "Apple",
                    Price = 1200,
                    Capacity = 32,
                    Memory = 4,
                    Color = "Black",
                    Type = "Smartphone",
                    SIM_slots = "Dual Sim",
                    Operating_system = "IOS",
                    Description = "New two-chamber system. All day battery. The toughest glass ever used in a smartphone. The fastest Apple processor of all time.",
                    Image = "https://s13emagst.akamaized.net/products/25344/25343941/images/res_99d57ec9e3d9bb8d3242f384288ce0a3.jpg?width=450&height=450&hash=786B6F02C39C1B12EDCE7B0392025549",
                    Stock = true,
                    Processor = apple_processor,
                    Cameras = new List<Camera>
                    {
                        apple_camera1,
                        apple_camera2
                    },
                    Battery = apple_battery
                    
                });

            ctx.Phones.Add(
               new Phone
               {
                   Name = "Iphone 11",
                   Brand = "Apple",
                   Price = 1400,
                   Capacity = 64,
                   Memory = 4,
                   Color = "White",
                   Type = "Smartphone",
                   SIM_slots = "Dual Sim",
                   Operating_system = "IOS",
                   Description = "4K video at 60 fps on any camera. Wide angle lens for a scene four times larger. Rotate, cut and add the usage filter as in the pictures.",
                   Image = "https://s13emagst.akamaized.net/products/25344/25343956/images/res_952370cf50cb22a45eb4898b244e49a8.jpg?width=450&height=450&hash=AB02E0258DB85859FACF6F3085B37D14",
                   Stock = true,
                   Processor = apple_processor,
                   Cameras = new List<Camera>
                   {
                        apple_camera1,
                        apple_camera2
                   },
                   Battery = apple_battery

               });


            ctx.Phones.Add(
           new Phone
           {
               Name = "Galaxy A71",
               Brand = "Samsung",
               Price = 2000,
               Capacity = 128,
               Memory = 6,
               Color = "Black",
               Type = "Smartphone",
               SIM_slots = "Dual Sim",
               Operating_system = "Android",
               Description = "Galaxy A71 has a fast processing and a generous storage space, so that you can be focused now. An advanced Octa-core processor and 6 GB of RAM offer smooth and efficient performance. Download more and delete less, with internal storage of up to 128 GB. Add even more with a 512 GB microSD card.",
               Image = "https://s13emagst.akamaized.net/products/27783/27782590/images/res_1785a69cd0dd722d5fd777071ac65b4d.jpg",
               Stock = true,
               Processor = samsung_processor1,
               Cameras = new List<Camera>
               {
                       samsung_camera1,
                       apple_camera2
               },
               Battery = samsung_battery1

           });


            ctx.Phones.Add(
           new Phone
           {
               Name = "Redmi Note 9 Pro",
               Brand = "Xiaomi",
               Price = 1800,
               Capacity = 128,
               Memory = 6,
               Color = "Black",
               Type = "Smartphone",
               SIM_slots = "Dual Sim",
               Operating_system = "Android",
               Description = "The 5020 mAh battery offers up to 147 hours of music listening, 33 hours of talk time, 16 hours of browsing or 13 hours of operation when playing on the phone. 30W fast charging ensures fast and efficient charging.",
               Image = "https://s13emagst.akamaized.net/products/30480/30479534/images/res_16f730f66b4d460e639d97247670540f.jpg",
               Stock = true,
               Processor = snapdragon,
               Cameras = new List<Camera>
               {
                       samsung_camera1,
                       apple_camera2
               },
               Battery = xiaomi_battery1

           });
            ctx.Phones.Add(
           new Phone
           {
               Name = "Galaxy S20 Ultra",
               Brand = "Samsung",
               Price = 2500,
               Capacity = 128,
               Memory = 8,
               Color = "Black",
               Type = "Smartphone",
               SIM_slots = "Dual Sim",
               Operating_system = "Android",
               Description = "Discover the Galaxy S20, S20 + and S20 Ultra. With 8K video, it's changing the way you make both video and photos — and 5G is revolutionizing continuous distribution. If you add Samsung Knox security, smart battery, powerful processor and massive storage space - Galaxy S20 series device reveals a new era of mobile telephony.",
               Image = "https://s13emagst.akamaized.net/products/28451/28450682/images/res_eedc4a19f71ea1ba97eb58814b258437.jpg",
               Stock = true,
               Processor = Exynos,
               Cameras = new List<Camera>
                    {
                            samsung_camera2,
                            samsung_camera3
                    },
               Battery = samsung_battery2

           });

            ctx.Phones.Add(
           new Phone
           {
               Name = "230",
               Brand = "Nokia",
               Price = 500,
               Capacity = 1,
               Memory = 1,
               Color = "Black",
               Type = "Classic",
               SIM_slots = "Dual Sim",
               Operating_system = "ThreadX",
               Description = "With a durable polycarbonate chassis, an aluminum case and a slim and sleek profile, the Nokia 230 offers you both a modern and sophisticated design and a great build quality.",
               Image = "https://s13emagst.akamaized.net/products/2888/2887966/images/res_11d15e1505fc6ab312c8693fc64fcc33.jpg",
               Stock = true,
               Processor = nokia,
               Cameras = new List<Camera>
               {
                            nokia_camera1,
                            nokia_camera2
               },
               Battery = nokia_battery

           });

            ctx.SaveChanges();
            base.Seed(ctx);
        }

    }
}