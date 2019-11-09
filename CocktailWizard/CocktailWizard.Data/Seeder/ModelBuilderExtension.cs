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
            //SEEDING ROLES
            //SEEDING ROLES
            builder.Entity<Role>().HasData(
                new Role { Id = Guid.Parse("297D06E6-C058-486F-A18A-06A971EBFCD7"), Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Id = Guid.Parse("6C8FCD7E-62F6-4F3E-A73D-ACBFD60B97AB"), Name = "Member", NormalizedName = "MEMBER" }
            );

            //SEEDING MANAGER ACCOUNT
            //SEEDING MANAGER ACCOUNT
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
            //SEEDING INGREDIENTS
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
            //SEEDING COCKTAILS
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
            //SEEDING BARS
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

            //SEEDING BARCOCKTAILS
            //SEEDING BARCOCKTAILS
            //SEEDING BARCOCKTAILS
            var barCocktail1 = new BarCocktail
            {
                BarId = bar1.Id,
                CocktailId = cosmopolitan.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail2 = new BarCocktail
            {
                BarId = bar1.Id,
                CocktailId = passionFruit.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail3 = new BarCocktail
            {
                BarId = bar1.Id,
                CocktailId = raspberryGin.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail4 = new BarCocktail
            {
                BarId = bar1.Id,
                CocktailId = sexOnTheBeach.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail5 = new BarCocktail
            {
                BarId = bar2.Id,
                CocktailId = passionFruit.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail6 = new BarCocktail
            {
                BarId = bar2.Id,
                CocktailId = bucksFizz.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail7 = new BarCocktail
            {
                BarId = bar3.Id,
                CocktailId = longIsland.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail8 = new BarCocktail
            {
                BarId = bar3.Id,
                CocktailId = michelada.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail9 = new BarCocktail
            {
                BarId = bar3.Id,
                CocktailId = cranberryVodka.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail10 = new BarCocktail
            {
                BarId = bar4.Id,
                CocktailId = sexOnTheBeach.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail11 = new BarCocktail
            {
                BarId = bar5.Id,
                CocktailId = whiteSangria.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail12 = new BarCocktail
            {
                BarId = bar5.Id,
                CocktailId = pinkGinIcedTea.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail13 = new BarCocktail
            {
                BarId = bar5.Id,
                CocktailId = michelada.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail14 = new BarCocktail
            {
                BarId = bar6.Id,
                CocktailId = passionFruit.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail15 = new BarCocktail
            {
                BarId = bar6.Id,
                CocktailId = bucksFizz.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail16 = new BarCocktail
            {
                BarId = bar6.Id,
                CocktailId = raspberryGin.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail17 = new BarCocktail
            {
                BarId = bar7.Id,
                CocktailId = longIsland.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail18 = new BarCocktail
            {
                BarId = bar7.Id,
                CocktailId = cosmopolitan.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail19 = new BarCocktail
            {
                BarId = bar7.Id,
                CocktailId = cranberryVodka.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail20 = new BarCocktail
            {
                BarId = bar8.Id,
                CocktailId = sexOnTheBeach.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail21 = new BarCocktail
            {
                BarId = bar9.Id,
                CocktailId = passionFruit.Id,
                CreatedOn = DateTime.Now
            };
            var barCocktail22 = new BarCocktail
            {
                BarId = bar10.Id,
                CocktailId = bucksFizz.Id,
                CreatedOn = DateTime.Now
            };

            builder.Entity<BarCocktail>().HasData(barCocktail1, barCocktail2, barCocktail3, barCocktail4, barCocktail5, barCocktail6, barCocktail7, barCocktail8, barCocktail9, barCocktail10, barCocktail11, barCocktail12, barCocktail13, barCocktail14, barCocktail15, barCocktail16, barCocktail17, barCocktail18, barCocktail19, barCocktail20, barCocktail21, barCocktail22);

            //SEEDING MOCKUSERS
            //SEEDING MOCKUSERS
            //SEEDING MOCKUSERS
            var user1 = new User
            {
                Id = Guid.Parse("8C09D76B-AE1E-48BA-8AF3-DFB85889053E"),
                UserName = "JohnSmith@Gmail.Com",
                Email = "JohnSmith@Gmail.Com"
            };
            var user2 = new User
            {
                Id = Guid.Parse("F069807C-7BCE-4879-928F-4B5771524260"),
                UserName = "BobRoss@Gmail.Com",
                Email = "BobRoss@Gmail.Com"
            };
            var user3 = new User
            {
                Id = Guid.Parse("71183A0B-759E-4903-966F-E7325E8D2EA2"),
                UserName = "DeanThomas@Gmail.Com",
                Email = "DeanThomas@Gmail.Com"
            };
            var user4 = new User
            {
                Id = Guid.Parse("B8BA7616-6FF1-4A98-B848-54DD38B11ADA"),
                UserName = "JackWinter@Gmail.Com",
                Email = "JackWinter@Gmail.Com"
            };
            var user5 = new User
            {
                Id = Guid.Parse("B3179225-FC8F-420A-A825-710FD46DB592"),
                UserName = "KateP@Gmail.Com",
                Email = "KateP@Gmail.Com"
            };
            var user6 = new User
            {
                Id = Guid.Parse("15E5F8BC-801C-4B10-87C6-0B4E3B7EBA8B"),
                UserName = "SandeepPatel@Gmail.Com",
                Email = "SandeepPatel@Gmail.Com"
            };
            var user7 = new User
            {
                Id = Guid.Parse("5DD842C4-7706-4E9E-B481-189E2ADCD744"),
                UserName = "KumarJoshi@Gmail.Com",
                Email = "KumarJoshi@Gmail.Com"
            };

            builder.Entity<User>().HasData(user1, user2, user3, user4, user5, user6, user7);

            //SEEDING BARCOMMENTS
            //SEEDING BARCOMMENTS
            //SEEDING BARCOMMENTS
            var barComment1 = new BarComment
            {
                BarId = bar1.Id,
                UserId = user1.Id,
                Body = "I must be honest I picked the place because I have never been there. I was by myself and the plan for me was to drink as I needed a night by myself to think and forget everything around me. The barman (by the accent was French) was freaking cool.asked a lot of questions, suggested drinks and was quick and nice to me all night long.",
                CreatedOn = DateTime.Now
            };
            var barComment2 = new BarComment
            {
                BarId = bar1.Id,
                UserId = user2.Id,
                Body = "Great bar to visit after work. We have been visiting after work most Fridays and some weekdays for the best part of 2 years now. Good atmosphere, great decor and all round friendly staff. Highly recommended.",
                CreatedOn = DateTime.Now
            };
            var barComment3 = new BarComment
            {
                BarId = bar1.Id,
                UserId = user3.Id,
                Body = "I stumbled across while visiting Canary Wharf and I have to say I was really impressed by how nice and accomodating the staff were. I received a very warm welcome straight from walking through the doors, they looked like they had some sort of event in the evening as all the tables were reserved but they let me seat before the party arrived. I ordered for a beer and some nibbles which came quick and was really tasty. Excactly what I needed after a long day visiting and shopping.",
                CreatedOn = DateTime.Now
            };
            var barComment4 = new BarComment
            {
                BarId = bar2.Id,
                UserId = user4.Id,
                Body = "Love this place. I've been coming here for 3 years and the staff are wonderful. Amazing service, great location and menu.  They are very dog friendly and treat mine like their own... Highly recommend this place.",
                CreatedOn = DateTime.Now
            };
            var barComment5 = new BarComment
            {
                BarId = bar2.Id,
                UserId = user5.Id,
                Body = "Sure, I am happy to provide more information. I visited for a work event yesterday evening. The place is good with some nice outside space. Drink options are ok/alright. Music was a big negative though ; as this was a work event we were there to network and talk. This was made impossible as the music volume suddenly (around 7.30pm) was increased dramatically making it impossible to continue any conversation.",
                CreatedOn = DateTime.Now
            };
            var barComment6 = new BarComment
            {
                BarId = bar2.Id,
                UserId = user6.Id,
                Body = "It’s quite a nice place to go for some drinks and food after work, which is what I did. Although a little cold, there are blankets and warm things to keep you warm under the heaters. Food was good though we was told that the menu we was not the correct one, then was waiting around for ages.",
                CreatedOn = DateTime.Now
            };
            var barComment7 = new BarComment
            {
                BarId = bar3.Id,
                UserId = user7.Id,
                Body = "I went on Tuesday of the last week and the weather was horrible so I really like the detail of having some blankets outside and also the waitress was super nice and attentive. Best place in Canary wharf!",
                CreatedOn = DateTime.Now
            };
            var barComment8 = new BarComment
            {
                BarId = bar3.Id,
                UserId = user1.Id,
                Body = "Stuff are friendly, location is charming.",
                CreatedOn = DateTime.Now
            };
            var barComment9 = new BarComment
            {
                BarId = bar3.Id,
                UserId = user2.Id,
                Body = "Awsome music and drinks.",
                CreatedOn = DateTime.Now
            };
            var barComment10 = new BarComment
            {
                BarId = bar4.Id,
                UserId = user1.Id,
                Body = "I organised my office Christmas party at the pagination and it was the most epic night ever. The staff were super efficient and executed my plan to the detail. In addition the venue is so nicely decorated for the Christmas holiday. I'm not a fan of vegan food but they didn't have a wide selection, if you aren't fussy then it's a place to check out.",
                CreatedOn = DateTime.Now
            };
            var barComment11 = new BarComment
            {
                BarId = bar4.Id,
                UserId = user2.Id,
                Body = "One of Canary wharf's more relaxing venues for a breakfast meeting.",
                CreatedOn = DateTime.Now
            };
            var barComment12 = new BarComment
            {
                BarId = bar4.Id,
                UserId = user3.Id,
                Body = "A dog wandering around sniffing my legs on a Friday night in a well designed bar?",
                CreatedOn = DateTime.Now
            };
            var barComment13 = new BarComment
            {
                BarId = bar5.Id,
                UserId = user4.Id,
                Body = "Open space, light, modern place. Cosy. No table cloth, table was a bit dirty. Friendly caring service. Food OK - nice BF omlette, but toast too crunchy and dry.",
                CreatedOn = DateTime.Now
            };
            var barComment14 = new BarComment
            {
                BarId = bar5.Id,
                UserId = user5.Id,
                Body = "1 star because 0 doesn't seem to be an option... I had the misfortune of visiting for lunch and will never make that mistake again.I have been here for drinks before and while service has never been good it's never been noticably terrible before.",
                CreatedOn = DateTime.Now
            };
            var barComment15 = new BarComment
            {
                BarId = bar5.Id,
                UserId = user1.Id,
                Body = "Highly recommended to anyone looking for breakfast in the Canary Wharf district. Full veggie breakfast and buttermilk pancakes perfectly executed, with punctual service and a cozy atmosphere. Would go again.",
                CreatedOn = DateTime.Now
            };
            var barComment16 = new BarComment
            {
                BarId = bar6.Id,
                UserId = user6.Id,
                Body = "Get there early if you want to have a table or stool as it gets full very fast and most tables are pre reserved.",
                CreatedOn = DateTime.Now
            };
            var barComment17 = new BarComment
            {
                BarId = bar6.Id,
                UserId = user7.Id,
                Body = "I been there few times and I can say that everything is fantastic. Especially Tony is a very good guy... I will defenitly come back.",
                CreatedOn = DateTime.Now
            };
            var barComment18 = new BarComment
            {
                BarId = bar7.Id,
                UserId = user1.Id,
                Body = "Nice seating area outside. Great place for a happy hour. Very friendly staff. Just didn't like the burgers we've ordered. Not really tasty. May try something else next time.",
                CreatedOn = DateTime.Now
            };
            var barComment19 = new BarComment
            {
                BarId = bar7.Id,
                UserId = user2.Id,
                Body = "Everything is good with this venue - except the food.",
                CreatedOn = DateTime.Now
            };
            var barComment20 = new BarComment
            {
                BarId = bar7.Id,
                UserId = user3.Id,
                Body = "We instantly felt welcome as soon as we walked in the door. The food was amazing and the staff were very warm and friendly and nothing was  too much trouble. The decor is stunning and I thoroughly recommend this bar/restaurant. We will definitely be going back.",
                CreatedOn = DateTime.Now
            };
            var barComment21 = new BarComment
            {
                BarId = bar8.Id,
                UserId = user4.Id,
                Body = "Quite good.",
                CreatedOn = DateTime.Now
            };
            var barComment22 = new BarComment
            {
                BarId = bar8.Id,
                UserId = user5.Id,
                Body = "Good lunchtime menu.. tasty meal, good price, great service and environment. Separate area for food or just drinks",
                CreatedOn = DateTime.Now
            };
            var barComment23 = new BarComment
            {
                BarId = bar9.Id,
                UserId = user6.Id,
                Body = "I really like this place and had my birthday drinks here after work.  Staff are accommodating, reasonable priced and the outside seating is great",
                CreatedOn = DateTime.Now
            };
            var barComment24 = new BarComment
            {
                BarId = bar9.Id,
                UserId = user7.Id,
                Body = "One of my favourite bars!",
                CreatedOn = DateTime.Now
            };
            var barComment25 = new BarComment
            {
                BarId = bar10.Id,
                UserId = user1.Id,
                Body = "Good place, a bit expensive though!",
                CreatedOn = DateTime.Now
            };
            var barComment26 = new BarComment
            {
                BarId = bar10.Id,
                UserId = user2.Id,
                Body = "Nice inside with a reel modern feel. Cocktails were great!",
                CreatedOn = DateTime.Now
            };
            var barComment27 = new BarComment
            {
                BarId = bar10.Id,
                UserId = user3.Id,
                Body = "Stylish and great outdoor areas. Expensive.",
                CreatedOn = DateTime.Now
            };

            builder.Entity<BarComment>().HasData(barComment1, barComment2, barComment3, barComment4, barComment5, barComment6, barComment7, barComment8, barComment9, barComment10, barComment11, barComment12, barComment13, barComment14, barComment15, barComment16, barComment17, barComment18, barComment19, barComment20, barComment21, barComment22, barComment23, barComment24, barComment25, barComment26, barComment27);

            //SEEDING COCKTAILCOMMENTS
            //SEEDING COCKTAILCOMMENTS
            //SEEDING COCKTAILCOMMENTS
            var cocktailComment1 = new CocktailComment
            {
                CocktailId = whiteSangria.Id,
                UserId = user1.Id,
                Body = "This drink is summer in a bottle! It tastes amazing on a hot summer day. I have been drinking this for a few years now and it's always as good as the first time. It has such a perfect long island flavor. I would buy it again, for sure. I would also recommend it for someone who doesn't like a strong tasting drink, it's very mild.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment2 = new CocktailComment
            {
                CocktailId = whiteSangria.Id,
                UserId = user2.Id,
                Body = "I honestly wasnt a fan of this. My husband and friend at the time of trial actually enjoyed it. We brought it out of Walmart- it was something cheap a spur of the moment purchase. It tasted artificial. Like the lemonade could have been better along with the tea. It wasnt a good fit for me. I wouldn't buy my again. I would rather just make my own from scratch that way I know what exactly is in it.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment3 = new CocktailComment
            {
                CocktailId = michelada.Id,
                UserId = user3.Id,
                Body = "loved!!!! loved!!!! loved!!!! judt a little loud on smell and strong spice taste. i do t drink anymore but I use to and j mean alot. price is affordable on pretty much any salary depends on how badly you want it I suppose. changes would be have more specials or do some drawing prices ect.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment4 = new CocktailComment
            {
                CocktailId = michelada.Id,
                UserId = user4.Id,
                Body = "I drink this when I'm in the mood for long island ice tea. I love the flavor of the mix. It goes down smooth and taste great. This product compares well to similar products. I would definitely recommend this product to others. I will be purchasing this product again."
            };
            var cocktailComment5 = new CocktailComment
            {
                CocktailId = pinkGinIcedTea.Id,
                UserId = user5.Id,
                Body = "My sister had made some long island iced tea for us. It tastes really good. Almost like an iced tea but with the alcohol. It is pretty expensive but you can get alternatives ones at the liquor store for cheap. I recommend to get it when you or anyone parties.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment6 = new CocktailComment
            {
                CocktailId = pinkGinIcedTea.Id,
                UserId = user6.Id,
                Body = "It's very smooth. Mostly, it is affordable, easy to find and tastes great. I think this is a popular item all around, especially the brand.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment7 = new CocktailComment
            {
                CocktailId = longIsland.Id,
                UserId = user7.Id,
                Body = "AWESOME!",
                CreatedOn = DateTime.Now
            };
            var cocktailComment8 = new CocktailComment
            {
                CocktailId = longIsland.Id,
                UserId = user1.Id,
                Body = "My sister had made some long island iced tea for us. It tastes really good. Almost like an iced tea but with the alcohol. It is pretty expensive but you can get alternatives ones at the liquor store for cheap. I recommend to get it when you or anyone parties.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment9 = new CocktailComment
            {
                CocktailId = cranberryVodka.Id,
                UserId = user2.Id,
                Body = "Awsome drink.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment10 = new CocktailComment
            {
                CocktailId = cranberryVodka.Id,
                UserId = user1.Id,
                Body = "I think this is great for people that like really strong alcohol I’m not really a fan of it I tried it once at a gathering but I don’t think it will be my cuppa tea to actually purchase this at anytime.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment11 = new CocktailComment
            {
                CocktailId = bucksFizz.Id,
                UserId = user2.Id,
                Body = "This is a really good summer drink to have on the rocks or with Coke I love to have it sitting outside by a fire price of this is well worth the bottle of rum I highly recommend anybody trying this.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment12 = new CocktailComment
            {
                CocktailId = bucksFizz.Id,
                UserId = user3.Id,
                Body = "I actually like this drink. You can drink it alone. It's inexpensive. It's pretty strong. It's not too bitter but it does have a bit too much sweet & sour taste. I've bought it several times.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment13 = new CocktailComment
            {
                CocktailId = sexOnTheBeach.Id,
                UserId = user4.Id,
                Body = "This will always be one of my favorite drinks.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment14 = new CocktailComment
            {
                CocktailId = sexOnTheBeach.Id,
                UserId = user5.Id,
                Body = "This is by far one of my favourites! It is sweet and you don't think it's going to get you drunk but you can end up crawling if you don't drink it responsibly ;) , also it is super cheap and perfect for parties. I totally recommend it straight or with some ice.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment15 = new CocktailComment
            {
                CocktailId = raspberryGin.Id,
                UserId = user1.Id,
                Body = "perfect for those tea drinking friends you may have coming over to an event you are hosting at your house. very affordable and lasts a really long time compared to some other brands",
                CreatedOn = DateTime.Now
            };
            var cocktailComment16 = new CocktailComment
            {
                CocktailId = raspberryGin.Id,
                UserId = user6.Id,
                Body = "OH MY GOODNESS!!! I LOVE THIS STUFF!",
                CreatedOn = DateTime.Now
            };
            var cocktailComment17 = new CocktailComment
            {
                CocktailId = passionFruit.Id,
                UserId = user7.Id,
                Body = "One of my favorites to drink!",
                CreatedOn = DateTime.Now
            };
            var cocktailComment18 = new CocktailComment
            {
                CocktailId = passionFruit.Id,
                UserId = user1.Id,
                Body = "It is ok..just ok. I wouldn't necessarily recommend. ",
                CreatedOn = DateTime.Now
            };
            var cocktailComment19 = new CocktailComment
            {
                CocktailId = cosmopolitan.Id,
                UserId = user2.Id,
                Body = "Tried this once at a party. I'm not a big fan of it, but this was good. It was better than expected. I would try again.",
                CreatedOn = DateTime.Now
            };
            var cocktailComment20 = new CocktailComment
            {
                CocktailId = cosmopolitan.Id,
                UserId = user3.Id,
                Body = "Always a good one! Mix it with anything you like and it will be delicious. The flavour on its own is too strong for me, but if that's what you like than go for it babes! The bottle can last for ages so I think u get good value for what you've pay for. I would buy again",
                CreatedOn = DateTime.Now
            };

            builder.Entity<CocktailComment>().HasData(cocktailComment1, cocktailComment2, cocktailComment3, cocktailComment4, cocktailComment5, cocktailComment6, cocktailComment7, cocktailComment8, cocktailComment9, cocktailComment10, cocktailComment11, cocktailComment12, cocktailComment13, cocktailComment14, cocktailComment15, cocktailComment16, cocktailComment17, cocktailComment18, cocktailComment19, cocktailComment20);

            //SEEDING BARRATINGS
            //SEEDING BARRATINGS
            //SEEDING BARRATINGS
            var barRating1 = new BarRating
            {
                UserId = user1.Id,
                BarId = bar1.Id,
                Value = 4.00,
                CreatedOn = DateTime.Now
            };
            var barRating2 = new BarRating
            {
                UserId = user2.Id,
                BarId = bar2.Id,
                Value = 5.00,
                CreatedOn = DateTime.Now
            };
            var barRating3 = new BarRating
            {
                UserId = user3.Id,
                BarId = bar3.Id,
                Value = 4.70,
                CreatedOn = DateTime.Now
            };
            var barRating4 = new BarRating
            {
                UserId = user4.Id,
                BarId = bar4.Id,
                Value = 4.20,
                CreatedOn = DateTime.Now
            };
            var barRating5 = new BarRating
            {
                UserId = user5.Id,
                BarId = bar5.Id,
                Value = 4.50,
                CreatedOn = DateTime.Now
            };
            var barRating6 = new BarRating
            {
                UserId = user6.Id,
                BarId = bar6.Id,
                Value = 2.60,
                CreatedOn = DateTime.Now
            };
            var barRating7 = new BarRating
            {
                UserId = user7.Id,
                BarId = bar7.Id,
                Value = 4.30,
                CreatedOn = DateTime.Now
            };
            var barRating8 = new BarRating
            {
                UserId = user1.Id,
                BarId = bar8.Id,
                Value = 4.30,
                CreatedOn = DateTime.Now
            };
            var barRating9 = new BarRating
            {
                UserId = user2.Id,
                BarId = bar9.Id,
                Value = 3.90,
                CreatedOn = DateTime.Now
            };

            builder.Entity<BarRating>().HasData(barRating1, barRating2, barRating3, barRating4, barRating5, barRating6, barRating7, barRating8, barRating9);

            //SEEDING COCKTAILRATINGS
            //SEEDING COCKTAILRATINGS
            //SEEDING COCKTAILRATINGS
            var cocktailRating1 = new CocktailRating
            {
                UserId = user1.Id,
                CocktailId = whiteSangria.Id,
                Value = 4.00,
                CreatedOn = DateTime.Now
            };
            var cocktailRating2 = new CocktailRating
            {
                UserId = user2.Id,
                CocktailId = michelada.Id,
                Value = 5.00,
                CreatedOn = DateTime.Now
            };
            var cocktailRating3 = new CocktailRating
            {
                UserId = user3.Id,
                CocktailId = pinkGinIcedTea.Id,
                Value = 4.70,
                CreatedOn = DateTime.Now
            };
            var cocktailRating4 = new CocktailRating
            {
                UserId = user4.Id,
                CocktailId = longIsland.Id,
                Value = 4.20,
                CreatedOn = DateTime.Now
            };
            var cocktailRating5 = new CocktailRating
            {
                UserId = user5.Id,
                CocktailId = cranberryVodka.Id,
                Value = 4.50,
                CreatedOn = DateTime.Now
            };
            var cocktailRating6 = new CocktailRating
            {
                UserId = user6.Id,
                CocktailId = bucksFizz.Id,
                Value = 2.60,
                CreatedOn = DateTime.Now
            };
            var cocktailRating7 = new CocktailRating
            {
                UserId = user7.Id,
                CocktailId = sexOnTheBeach.Id,
                Value = 4.30,
                CreatedOn = DateTime.Now
            };
            var cocktailRating8 = new CocktailRating
            {
                UserId = user1.Id,
                CocktailId = raspberryGin.Id,
                Value = 4.30,
                CreatedOn = DateTime.Now
            };
            var cocktailRating9 = new CocktailRating
            {
                UserId = user2.Id,
                CocktailId = passionFruit.Id,
                Value = 3.90,
                CreatedOn = DateTime.Now
            };

            builder.Entity<CocktailRating>().HasData(cocktailRating1, cocktailRating2, cocktailRating3, cocktailRating4, cocktailRating5, cocktailRating6, cocktailRating7, cocktailRating8, cocktailRating9);
        }
    }
}
