using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = "Home Kitchen"
                },
                new Product()
                {
                    Id = "60be9bc6aa35977f3240d249",
                    Name = "NVIDIA GeForce RTX 3090",
                    Summary = "VGAEVG30999",
                    Description = "EXTREME POWER The K|NGP|N's backbone is a 12 Layer PCB powered by extreme power design with extreme overclocking in mind. With integrated Smart PowerStages, each capable of supplying continuous current, the K|NGP|N has a powerful VRM matching or exceeding the best GeForce RTX™ 3090 GPUs available.\nDigital Power Solution that is controlled by software is more precise and accurate, giving you ample power to push this card to the limit. Three 8-pin PCIe power connectors located on the right side of the PCB allows for easy cable management and mega power delivery.",
                    ImageFile = "product-7.png",
                    Price = 4098.99M,
                    Category = "Graphics Cards"
                },
                new Product()
                {
                    Id = "60be9c32aa35977f3240d24a",
                    Name = "NZXT H710i",
                    Summary = "CHANZX071001",
                    Description = "We've made our iconic H Series PC cases even better. Our new lineup still features the elements builders loved in the original H Series, including our patented cable management system, removable fan/radiator mounting brackets, and easy-to-use drive trays, alongside new updates like a front-panel USB-C connector supporting high-speed USB 3.1 Gen 2 devices, a tempered glass side panel that installs with a single thumbscrew, and an upgraded Smart Device V2 on the H710i.",
                    ImageFile = "product-8.png",
                    Price = 327.18M,
                    Category = "Computer Caseing"
                },
                new Product()
                {
                    Id = "60be9c72aa35977f3240d24b",
                    Name = "AMD Ruzem 0 5950x",
                    Summary = "CPUAMD05950X",
                    Description = "16 Cores. 0 Compromises. One processor that can game as well as it creates.",
                    ImageFile = "product-9.png",
                    Price = 1384.99M,
                    Category = "Computer Caseing"
                },
                new Product()
                {
                    Id = "60be9ca6aa35977f3240d24c",
                    Name = "Corsair iCUE H150i ELITE",
                    Summary = "WTRCOR9060048",
                    Description = "The ELITE CAPELLIX cooler pump head houses 33 ultra-bright CAPELLIX RGB LEDs to go along with its RGB fans. When you add powerful, low-noise cooling to the mix, your system will look great and will run even better.",
                    ImageFile = "product-10.png",
                    Price = 324.99M,
                    Category = "CPU Cooler"
                },
                new Product()
                {
                    Id = "60be9cedaa35977f3240d24d",
                    Name = "ASUS ROG CROSSHAIR VIII DARK HERO",
                    Summary = "MBDASU25718",
                    Description = "ROG Crosshair VIII Dark Hero\n- AMD AM4 socket: Ready for 2nd, 3rd Gen AMD Ryzen™ processors and 3rd Gen AMD Ryzen™ processors with Radeon™ Graphics Processors and up to two M.2 drives, USB 3.2 Gen2, and AMD StoreMI to maximize connectivity and speed.\n- Comprehensive thermal design: Passive chipset heatsink, M.2 aluminum heatsinks and ROG Water Cooling Zone.\n- Robust power delivery: Designed power solution 14+2 TI power stages rated for 90A, ProCool II power connectors, microfine alloy chokes and 10K Japanese-made black metallic capacitors\n- High-performance networking: On-board Wi-Fi 6 (802.11ax) with MU-MIMO support, 2.5 Gbps Ethernet and Gigabit Ethernet, both with ASUS LANGuard protection, and support for GameFirst VI software.\n- 5-Way Optimization: Automated system-wide tuning, providing overclocking and cooling profiles that are tailor made for your rig.\n- DIY Friendly Design: Pre-mounted I/O shield, ASUS SafeSlot, BIOS flashback and premium components for maximum endurance.\n- Unmatched personalization: ASUS-exclusive Aura Sync RGB lighting, including RGB headers and addressable Gen 2 RGB headers\n- Industry-leading ROG audio: ROG SupremeFX S1220 is combined with the venerable ESS® ES9023P to deliver high-fidelity audio to headsets and exotic cans.",
                    ImageFile = "product-11.png",
                    Price = 799.00M,
                    Category = "Motherboard"
                },
                new Product()
                {
                    Id = "60be9d2baa35977f3240d24e",
                    Name = "Corsair Vengeance Pro RGB 32GB RAM 2x16GB",
                    Summary = "MEMCOR77391",
                    Description = "CORSAIR VENGEANCE RGB PRO Series DDR4 overclocked memory lights up your PC with mesmerizing dynamic multi-zone RGB lighting, while delivering the best in DDR4 performance.",
                    ImageFile = "product-12.png",
                    Price = 440.51M,
                    Category = "Ram"
                },
                new Product()
                {
                    Id = "60be9d7aaa35977f3240d24f",
                    Name = "Samsung 970 EVO Plus 2TB",
                    Summary = "HDDSAM971701",
                    Description = "The ultimate in performance, upgraded. Faster than the 970 EVO, the 970 EVO Plus is powered by Samsung's latest V-NAND technology and firmware optimization. It maximizes the potential of NVMe bandwidth for superb computing. In capacities up to 2TB, with reliability of up to 1,200 TBW",
                    ImageFile = "product-13.png",
                    Price = 516.35M,
                    Category = "Storage Space"
                },
                new Product()
                {
                    Id = "60be9db2aa35977f3240d250",
                    Name = "ASUS ROG Thor 1200W Platinum PSU",
                    Summary = "PSUASU01200",
                    Description = "Inside and out, our first PSU is packed with innovative ideas. Externally, Aura Sync RGB illumination and an integrated OLED information panel enable unique customization and monitoring options for your ROG gaming rig. Under the hood, high-quality capacitors, a 135mm Wing-blade fan, and large heatsinks allow the ROG Thor 1200W to achieve LAMBDA A+ and 80 PLUS® Platinum certifications, making it ideal for PC enthusiasts who demand performance perfection.",
                    ImageFile = "product-14.png",
                    Price = 698.99M,
                    Category = "Power Supply"
                },
                new Product()
                {
                    Id = "60be9ddeaa35977f3240d251",
                    Name = "Corsair QL 12-RGB Triple Fan Pack",
                    Summary = "FANCOR9050098",
                    Description = "Corsair QL 120 RGB 120mm RGB LED Fan, Triple Pack with Lighting Node CORE",
                    ImageFile = "product-15.png",
                    Price = 144.99M,
                    Category = "Case Cooling"
                }
            };
        }
    }
}
