using CocktailWizard.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CocktailWizard.Data.Seeder
{
    public static class ModelBuilderExtension
    {
        public static void Seeder(this ModelBuilder builder)
        {
            //SEEDING ROLES
            builder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"), Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Id = Guid.Parse("6C8FCD7E-62F6-4F3E-A73D-ACBFD60B97AB"), Name = "Member", NormalizedName = "MEMBER" }
            );

            //SEEDING MANAGER ACCOUNT
            var hasher = new PasswordHasher<User>();

            User managerUser = new User
            {
                Id = Guid.Parse("7BD06FE6-79CA-43A1-862B-446A1466BB93"),
                UserName = "manager@cw.com",
                NormalizedUserName = "MANAGER@CW.COM",
                Email = "manager@cw.com",
                NormalizedEmail = "MANAGER@CW.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN"
            };

            managerUser.PasswordHash = hasher.HashPassword(managerUser, "manager");

            builder.Entity<User>().HasData(managerUser);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"),
                    UserId = managerUser.Id
                });

            //SEEDING INGREDIENTS
            var whisky = new Ingredient
            {
                Id = Guid.Parse("91767830-FB0E-4E77-A93A-D01EB2520553"),
                Name = "Whisky"
            };
            var gin = new Ingredient
            {
                Id = Guid.Parse("AF31C27C-D4E5-4D19-8304-2C649ADB2F49"),
                Name = "Gin"
            };
            var vodka = new Ingredient
            {
                Id = Guid.Parse("8B4157A7-49F0-4487-B800-C569C9EC7DD6"),
                Name = "Vodka"
            };
            var rum = new Ingredient
            {
                Id = Guid.Parse("F97A5F83-F9DA-43A3-BEF9-67091533CCC9"),
                Name = "Rum"
            };
            var tequila = new Ingredient
            {
                Id = Guid.Parse("4F036905-92AF-4B1B-8879-41B0FA8F1020"),
                Name = "Tequila"
            };
            var cointreau = new Ingredient
            {
                Id = Guid.Parse("0303B014-79B5-4044-9994-85AC83F293FC"),
                Name = "Cointreau"
            };
            var cola = new Ingredient
            {
                Id = Guid.Parse("730BCB1E-ED31-4600-9E42-7019898154B5"),
                Name = "Cola"
            };
            var gingerAle = new Ingredient
            {
                Id = Guid.Parse("7F5402B0-2136-4ABB-B809-86C1CB502F62"),
                Name = "Ginger ale"
            };
            var clubSoda = new Ingredient
            {
                Id = Guid.Parse("F9D9AC89-7C03-4A41-8A1A-B69262F89E16"),
                Name = "Club soda"
            };
            var lemonSour = new Ingredient
            {
                Id = Guid.Parse("DD3B4DCD-1E23-4B02-BDF1-859D892A7D89"),
                Name = "Lemon Sour"
            };
            var cinnamonSyrup = new Ingredient
            {
                Id = Guid.Parse("59BD8032-57F2-4193-8D40-C805EC7D6122"),
                Name = "Cinnamon Syrup"
            };
            var tripleSec = new Ingredient
            {
                Id = Guid.Parse("2D403BD0-6656-48A3-80A5-1910F2526D94"),
                Name = "Triple sec"
            };
            var cranberryJuice = new Ingredient
            {
                Id = Guid.Parse("99667755-0B3C-4746-89DA-9D8DD7A6169A"),
                Name = "Cranberry juice"
            };
            var limeWedge = new Ingredient
            {
                Id = Guid.Parse("5998858C-01E8-41D2-8116-798649A2763F"),
                Name = "Lime wedge"
            };
            var passoa = new Ingredient
            {
                Id = Guid.Parse("B988748E-CD6A-48DA-AA36-20773102E426"),
                Name = "Passoa"
            };
            var sugarSyrup = new Ingredient
            {
                Id = Guid.Parse("5495D66C-52FF-43F3-B26D-1B5279E07767"),
                Name = "Sugar syrup"
            };
            var prosecco = new Ingredient
            {
                Id = Guid.Parse("E05C419E-2F82-425C-B769-2842179D7E85"),
                Name = "Prosecco"
            };
            var raspberries = new Ingredient
            {
                Id = Guid.Parse("12B1F952-2E1F-4758-9E80-1C1EFBA96CC5"),
                Name = "Raspberries"
            };
            var strawberries = new Ingredient
            {
                Id = Guid.Parse("7982B0CE-B116-44DD-85A8-06E66DF516CC"),
                Name = "Strawberries"
            };
            var peachSchnapps = new Ingredient
            {
                Id = Guid.Parse("145FD313-EF00-4003-B95A-A5E6558F255D"),
                Name = "Peach schnapps"
            };
            var oranges = new Ingredient
            {
                Id = Guid.Parse("47CAC5AB-7071-46C9-BFBB-0F6FFAFF0F0B"),
                Name = "Oranges"
            };
            var cherries = new Ingredient
            {
                Id = Guid.Parse("30743785-290F-460C-B16E-6FC992B44366"),
                Name = "Cherries"
            };
            var olives = new Ingredient
            {
                Id = Guid.Parse("CCE2C9A4-E14A-4CF9-83AD-111FA4A19EAF"),
                Name = "Olives"
            };
            var apples = new Ingredient
            {
                Id = Guid.Parse("2D5DD44C-41D7-43D0-9139-81AA791089A3"),
                Name = "Apples"
            };
            var kiwis = new Ingredient
            {
                Id = Guid.Parse("AD6430E6-0B4B-49AA-B412-7D6F6DDBE5B0"),
                Name = "Kiwis"
            };

            builder.Entity<Ingredient>().HasData(whisky, gin, vodka, rum, tequila, cointreau, cola, gingerAle, clubSoda, lemonSour, cinnamonSyrup, tripleSec, cranberryJuice, limeWedge, passoa, sugarSyrup, prosecco, raspberries, strawberries, peachSchnapps, oranges, cherries, olives, apples, kiwis);

            //SEEDING COCKTAILS
            var cosmopolitan = new Cocktail
            {
                Id = Guid.Parse("24E8A669-799A-48B7-BC6F-B6884A33D0C7"),
                Name = "Cosmopolitan cocktail",
                Info = "Lipsmackingly sweet-and-sour, the Cosmopolitan cocktail of vodka, cranberry, orange liqueur and citrus is a good time in a glass. Perfect for a party.",
                ImagePath = "/assets/img/cocktails/cosmopolitan-cocktail.jpg",
            };
            var passionFruit = new Cocktail
            {
                Id = Guid.Parse("E9B10C8B-46ED-45A3-9C66-A2C92D74E112"),
                Name = "Passion fruit martini",
                Info = "This easy passion fruit cocktail is bursting with zingy flavours and is perfect for celebrating with friends. Top with prosecco for a special tipple",
                ImagePath = "/assets/img/cocktails/passionfruit-martini.jpg",
            };
            var raspberryGin = new Cocktail
            {
                Id = Guid.Parse("3EBCD248-C91B-45E4-8900-90748828CC67"),
                Name = "Raspberry gin",
                Info = "Preserve the taste of summer in a bottle with this raspberry gin, perfect topped up with tonic. The gin will keep its lovely pink hue for a few months",
                ImagePath = "/assets/img/cocktails/raspberry-gin.jpg",
            };
            var sexOnTheBeach = new Cocktail
            {
                Id = Guid.Parse("C2ACDFFB-A172-4B52-9316-8A5BBE02C16D"),
                Name = "Sex on the beach cocktail",
                Info = "Combine vodka with peach schnapps and cranberry juice to make a classic sex on the beach cocktail. Garnish with cocktail cherries and orange slices.",
                ImagePath = "/assets/img/cocktails/sex-on-the-beach.jpg",
            };
            var pinkGinIcedTea = new Cocktail
            {
                Id = Guid.Parse("1686F69D-BAAF-4423-A857-4B4D1684496F"),
                Name = "Pink gin iced tea",
                Info = "Blend pink gin with iced tea and you have this unique cocktail, made with spiced rum, elderflower and pink grapefruit. Serve in a jug for a sharing cocktail.",
                ImagePath = "/assets/img/cocktails/pink-gin-iced-tea.jpg",
            };
            var longIsland = new Cocktail
            {
                Id = Guid.Parse("1DC069F0-0CAA-4B64-82C0-66A744607AB1"),
                Name = "Long Island iced tea",
                Info = "Mix a jug of this classic cocktail for a summer party. It's made with equal parts of vodka, gin, tequila, rum and triple sec, plus lime, cola and plenty of ice.",
                ImagePath = "/assets/img/cocktails/long-island-ice-tea.jpg",
            };
            var michelada = new Cocktail
            {
                Id = Guid.Parse("2B113D19-305B-43F0-B149-44E86E7F8308"),
                Name = "Michelada",
                Info = "Cold lager, chilli powder, pepper and lime: spice up your lager with this Mexican cocktail, popular throughout Latin America and great for a summer party.",
                ImagePath = "/assets/img/cocktails/michelada.jpg",
            };
            var whiteSangria = new Cocktail
            {
                Id = Guid.Parse("323BC1DD-C842-4689-BC7B-2953787A3129"),
                Name = "White wine sangria",
                Info = "Try this refreshing twist on a traditional sangria and use white wine instead of red with elderflower to complement the fruit. Perfect for summer parties.",
                ImagePath = "/assets/img/cocktails/wine-sangria.jpg",
            };
            var bucksFizz = new Cocktail
            {
                Id = Guid.Parse("B725576E-25CB-4D3E-B661-68E6DF37E2F7"),
                Name = "Bucks fizz",
                Info = "The simple and classic combination of orange juice and champagne makes a perfect cocktail for a celebratory brunch or party",
                ImagePath = "/assets/img/cocktails/bucks-fizz.jpg",
            };
            var cranberryVodka = new Cocktail
            {
                Id = Guid.Parse("86A1C72C-4DAF-4E26-A483-67B569B5271F"),
                Name = "Cranberry vodka",
                Info = "This bittersweet fruity vodka is best served well chilled in shot glasses. It can also be made with other berries like blackcurrants or strawberries.",
                ImagePath = "/assets/img/cocktails/cranberry-vodka.jpg",
            };

            builder.Entity<Cocktail>().HasData(cosmopolitan, passionFruit, raspberryGin, sexOnTheBeach, pinkGinIcedTea, longIsland, michelada, whiteSangria, bucksFizz, cranberryVodka);

            //SEEDING BARS
            var bar1 = new Bar
            {
                Id = Guid.Parse("19FB565F-DF92-4F81-AC42-BC256D10469A"),
                Name = "The Allegory",
                Info = "In the heart of Shoreditch's happening hub, Principal Place, The Allegory is an everyday escape in London’s buzzing unsquare mile. Linger over a long brunch before finishing with an espresso martini.Enjoy quick catch-ups over pastries and freshly ground coffee, wholesome sharing platters and creative cocktails with colleagues; these one-of-a-kind experiences will be found at The Allegory. With a beautiful alfresco terrace, large open plan bar and cosy candlelit corners, this is a destination you'll want to return to again and again.",
                Address = "1a Principal Place, Worship Street, London, EC2A 2BA",
                ImagePath = "/assets/img/bars/the-allegory.jpg",
                Phone = "020 3948 9810",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d9930.13386419354!2d-0.0794724!3d51.5217746!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0xfb717a4393ae1f7!2sThe%20Allegory!5e0!3m2!1sen!2sbg!4v1573065214726!5m2!1sen!2sbg",
            };
            var bar2 = new Bar
            {
                Id = Guid.Parse("49B52950-A806-4075-8260-9BA0BDC1AB06"),
                Name = "The Refinery",
                Info = "Statement wallpapers and furniture are complemented by soft lighting and cosy faux fur to create your uber chic, contemporary bar - The Refinery CityPoint. The all-day dining bar & restaurant features a private dining room, sunken lounge and alfresco terrace with a pizza oven in the summer. It suits all occasions from early morning breakfasts right through to late night drinks. Make the most of our set menus for larger groups, or pre order packages when you want a selection of nibbles to eat!",
                Address = "1 Ropemaker Street, London, EC2Y 9HT",
                ImagePath = "/assets/img/bars/the-refinery.jpg",
                Phone = "020 7382 0606",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d9931.461092179858!2d-0.12384975375194002!3d51.515687677335066!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487604af32e9d343%3A0x422a8e0b815341b!2sThe%20Refinery!5e0!3m2!1sen!2sbg!4v1573065262411!5m2!1sen!2sbg",
            };
            var bar3 = new Bar
            {
                Id = Guid.Parse("6C296D75-BCD2-4711-9A28-6D0B6BCAA34B"),
                Name = "The Fable",
                Info = "Inspired by the fantasy world of fairy tales and Aesop's fables, The Fable near Holborn Viaduct in central London, is anything but ordinary. From the vintage typewriter, to the leather bound books, every detail tells a story. Whether you visit for crafted cocktails, a morning latte & eggs Benedict or dinner at dusk, expect to be entranced, enthralled and enchanted.",
                Address = "52 Holborn Viaduct, London, EC1A 2FD",
                ImagePath = "/assets/img/bars/the-fable.jpg",
                Phone = "0207 651 4940",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2482.7646347568057!2d-0.1070710840279029!3d51.517533917782934!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48761b52ee3d7ad9%3A0xa677a8c2b71574ca!2sThe%20Fable!5e0!3m2!1sen!2sbg!4v1573065312782!5m2!1sen!2sbg",
            };
            var bar4 = new Bar
            {
                Id = Guid.Parse("4B6B8336-28DF-48B2-A1FD-BADE70B94EB6"),
                Name = "Devonshire Terrace",
                Info = "In the heart of the peaceful Devonshire Square, moments from Liverpool Street Station, Devonshire Terrace is your everyday escape from the hustle and bustle of City life. From quick catch-ups over freshly ground coffee to relaxing after work cocktails in one of our many gorgeous spaces, sit back and relax and we'll take care of the rest. No need to wait for the warmer months to drink and dine alfresco, enjoy our all year round terrace with its beautiful glass domed roof to protect you from the elements.",
                Address = "Devonshire Terrace, Devonshire Square, London, EC2M 4WY",
                ImagePath = "/assets/img/bars/devonshire-terrace.jpg",
                Phone = "020 7256 3233",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2482.788117828657!2d-0.0803738840278907!3d51.517103117814386!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48761cb30d422641%3A0x2c7c1dfd5e33c70!2sDevonshire%20Terrace!5e0!3m2!1sen!2sbg!4v1573065339941!5m2!1sen!2sbg",
            };
            var bar5 = new Bar
            {
                Id = Guid.Parse("93532AFB-FE74-43BE-88A4-1C6948C634B8"),
                Name = "The Anthologist",
                Info = "Located right in the heart of the City, The Anthologist is the ideal backdrop for all your drink and food needs, from breakfast meetings to client updates over lunch, after work drinks or dinner with friends. Sample new wines or vintages from across the globe, a unique range of innovative cocktails and relaxed all - day dining fare.",
                Address = "58 Gresham Street, London, EC2V 7BB",
                ImagePath = "/assets/img/bars/the-anthologist.jpg",
                Phone = "0207 726 8711",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2482.788117828657!2d-0.0803738840278907!3d51.517103117814386!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487603552b6c7317%3A0x8e08163a80221ab9!2sThe%20Anthologist!5e0!3m2!1sen!2sbg!4v1573065358702!5m2!1sen!2sbg",
            };
            var bar6 = new Bar
            {
                Id = Guid.Parse("64C6592D-2C7B-42C4-B18D-5AC34BDC39D1"),
                Name = "Drake & Morgan",
                Info = "Our flagship bar & restaurant located in the heart of London's most exciting dining destination with two floors, two show stopping bars, an open kitchen and an extensive year-round outside space. Perfect for alfresco dining and drinks in the sun. Drake & Morgan at King's Cross offers a relaxed drinking and dining space in a beautiful setting. Open from an early morning until late evening, it's perfect for every occasion - from a business meeting and working lunch to a romantic dinner or after work drinks.",
                Address = "6 Pancras Square, London, N1C 4AG",
                ImagePath = "/assets/img/bars/drakeandmorgan.jpg",
                Phone = "0203 826 4870",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2481.892053039409!2d-0.1282180840274676!3d51.53353971661222!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x48761b3c54efa6e1%3A0x9f4c59afccfb8b53!2sDrake%20%26%20Morgan%20at%20King&#39;s%20Cross!5e0!3m2!1sen!2sbg!4v1573065385773!5m2!1sen!2sbg",
            };
            var bar7 = new Bar
            {
                Id = Guid.Parse("84552D70-E670-4391-BD7B-1B54B0282B61"),
                Name = "The Pagination",
                Info = "Perfectly positioned on the riverside in Canary Wharf, next to the bridge leading over to West India Quay, The Pagination is the perfect antidote to busy London life. With industrial inspired details, exposed metals, and soft handwoven textures, it offers a sanctuary, day or night and the expansive terrace offers alfresco drinking and dining in both the warmer months and the colder due to the abundance of blankets and hot water bottles to keep you snug.",
                Address = "9 Cabot Square, London, E14 4EB",
                ImagePath = "/assets/img/bars/the-pagination.jpg",
                Phone = "020 7512 0397",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2483.4112946113805!2d-0.025301484028207793!3d51.50566991865038!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487602b7be6a030f%3A0x93dcb32d47e3e562!2sThe%20Pagination!5e0!3m2!1sen!2sbg!4v1573065408325!5m2!1sen!2sbg",
            };
            var bar8 = new Bar
            {
                Id = Guid.Parse("69CE843B-97C4-4164-8BA0-C8CA4EF02CF4"),
                Name = "The Parlour",
                Info = "Located in the Park Pavilion on Canada Square, Canary Wharf, The Parlour is a striking & innovative all-day bar with style, substance & seasonally tempting drinks and food. A secret garden-inspired lounge with timber panelling is a must for cocktail lovers & perfect for pre- or post-dinner drinks, whilst the mixology table is ideal for those who want to mix & muddle for themselves. A stunning alfresco terrace, complete with its own bar provides the perfect playpen for those wanting to soak up the sun.",
                Address = "The Park Pavilion, London, E14 5FW",
                ImagePath = "/assets/img/bars/the-parlour.jpg",
                Phone = "0207 715 9551",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d39734.535778488505!2d-0.05813209602882269!3d51.50572144844429!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487602b7642f2f9d%3A0x19f521ba29dd3f1a!2sThe%20Parlour!5e0!3m2!1sen!2sbg!4v1573065440869!5m2!1sen!2sbg",
            };
            var bar9 = new Bar
            {
                Id = Guid.Parse("363EA8FB-60F3-4AA2-8041-394520E3FB41"),
                Name = "Opium",
                Info = "Opium Cocktail Bar & Dim Sum Parlour is a chic, hidden venue in the heart of Chinatown. Run by experienced London bar moguls Dre Masso and Eric Yu, Opium certainly has some pedigree behind it. The decor is oriental themed but again keeps an element of freshness with a twist that makes it modern and current; metal finishes on miss-matched Chinese furniture gives Opium, a contemporary London feel that is very welcome. Expect 3 bars of amazing Asian cocktails and a selection of dim sum - just a little teaser to get your appetite going.",
                Address = "5-16 Gerrard Street, London, W1D 6JE",
                ImagePath = "/assets/img/bars/opium.jpg",
                Phone = "020 7734 7276",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2483.0790771431516!2d-0.13363628402805472!3d51.511765218204765!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487604d25acdb76d%3A0xc5c854eeaaa62990!2sOpium%20Cocktail%20bar%20and%20Dim%20Sum%20Parlour!5e0!3m2!1sen!2sbg!4v1573065470157!5m2!1sen!2sbg",
            };
            var bar10 = new Bar
            {
                Id = Guid.Parse("11F5779B-49B1-43AC-9153-5FA72D810B34"),
                Name = "The Sipping Room",
                Info = "An escape from the everyday, The Sipping Room specialises in thoughtful, inspired menus, locally sourced ingredients, and innovative, handcrafted cocktails. Retreat from the world while you enjoy our unrivalled service in the most welcoming environment. Our stylish outdoor terrace provides the perfect alfresco respite throughout the seasons.",
                Address = "16 Hertsmere Road, London, E14 4AX",
                ImagePath = "/assets/img/bars/the-sipping-room.jpg",
                Phone = "020 3907 0320",
                GoogleMapsURL = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2483.3029276456064!2d-0.0260607840281346!3d51.5076582185051!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x487602b6306f0be9%3A0x793dfd9d17079629!2sThe%20Sipping%20Room!5e0!3m2!1sen!2sbg!4v1573065491821!5m2!1sen!2sbg",
            };

            builder.Entity<Bar>().HasData(bar1, bar2, bar3, bar4, bar5, bar6, bar7, bar8, bar9, bar10);




















            //var barCocktail1 = new BarCocktail
            //{
            //    BarId = bar1.Id,
            //    CocktailId = cosmopolitan.Id,
            //};
            //var barCocktail2 = new BarCocktail
            //{
            //    BarId = bar1.Id,
            //    CocktailId = passionFruit.Id,
            //};
            //var barCocktail3 = new BarCocktail
            //{
            //    BarId = bar1.Id,
            //    CocktailId = raspberryGin.Id,
            //};
            //var barCocktail4 = new BarCocktail
            //{
            //    BarId = bar1.Id,
            //    CocktailId = sexOnTheBeach.Id,
            //};
            //var barCocktail5 = new BarCocktail
            //{
            //    BarId = bar2.Id,
            //    CocktailId = passionFruit.Id,
            //};
            //var barCocktail6 = new BarCocktail
            //{
            //    BarId = bar2.Id,
            //    CocktailId = passionFruit.Id,
            //};
        }
    }
}


//cosmopolitan, passionFruit, raspberryGin