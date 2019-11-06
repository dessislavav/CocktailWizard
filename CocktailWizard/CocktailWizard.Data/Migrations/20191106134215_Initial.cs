using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailWizard.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.UniqueConstraint("AK_AspNetUsers_CreatedOn", x => x.CreatedOn);
                });

            migrationBuilder.CreateTable(
                name: "Bars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Info = table.Column<string>(maxLength: 1000, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bars", x => x.Id);
                    table.UniqueConstraint("AK_Bars_CreatedOn_Id", x => new { x.CreatedOn, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Info = table.Column<string>(maxLength: 1000, nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.Id);
                    table.UniqueConstraint("AK_Cocktails_CreatedOn_Id", x => new { x.CreatedOn, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.UniqueConstraint("AK_Ingredients_CreatedOn_Id", x => new { x.CreatedOn, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    HasExpired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bans", x => x.Id);
                    table.UniqueConstraint("AK_Bans_CreatedOn_Id", x => new { x.CreatedOn, x.Id });
                    table.ForeignKey(
                        name: "FK_Bans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BarComments",
                columns: table => new
                {
                    BarId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Body = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarComments", x => new { x.BarId, x.UserId });
                    table.UniqueConstraint("AK_BarComments_BarId_CreatedOn", x => new { x.BarId, x.CreatedOn });
                    table.ForeignKey(
                        name: "FK_BarComments_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarRatings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    BarId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarRatings", x => new { x.BarId, x.UserId });
                    table.UniqueConstraint("AK_BarRatings_CreatedOn", x => x.CreatedOn);
                    table.ForeignKey(
                        name: "FK_BarRatings_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarCocktails",
                columns: table => new
                {
                    BarId = table.Column<Guid>(nullable: false),
                    CocktailId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCocktails", x => new { x.CocktailId, x.BarId });
                    table.UniqueConstraint("AK_BarCocktails_CreatedOn", x => x.CreatedOn);
                    table.ForeignKey(
                        name: "FK_BarCocktails_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BarCocktails_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CocktailComments",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Body = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailComments", x => new { x.CocktailId, x.UserId });
                    table.UniqueConstraint("AK_CocktailComments_CreatedOn", x => x.CreatedOn);
                    table.ForeignKey(
                        name: "FK_CocktailComments_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailRatings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CocktailId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailRatings", x => new { x.CocktailId, x.UserId });
                    table.UniqueConstraint("AK_CocktailRatings_CreatedOn", x => x.CreatedOn);
                    table.ForeignKey(
                        name: "FK_CocktailRatings_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredient",
                columns: table => new
                {
                    CocktailId = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredient", x => new { x.IngredientId, x.CocktailId });
                    table.UniqueConstraint("AK_CocktailIngredient_CreatedOn", x => x.CreatedOn);
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("297d06e6-c058-486f-a18a-06a971ebfcd7"), "2b5be8ab-3f39-430f-aaec-bf5b97f8ec6b", "Manager", "MANAGER" },
                    { new Guid("6c8fcd7e-62f6-4f3e-a73d-acbfd60b97ab"), "d9b320dc-ec0a-484d-a5c6-b65c5eb32189", "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7bd06fe6-79ca-43a1-862b-446a1466bb93"), 0, "0a7c4668-10e8-49bf-9a08-ffe8a1c2397e", new DateTime(2019, 11, 6, 13, 42, 14, 775, DateTimeKind.Utc).AddTicks(2474), null, "manager@cw.com", false, false, true, null, null, "MANAGER@CW.COM", "MANAGER@CW.COM", "AQAAAAEAACcQAAAAEFeAb/YN/Ei7nDHWMKS90E76r4KqFxOxGtbALR93lYsv96pML0zbV5ZwI3Q/R8A8zg==", null, false, "7I5VNHIJTSZNOT3KDWKNFUV5PVYBHGXN", false, "manager@cw.com" });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "Id", "Address", "CreatedOn", "DeletedOn", "ImagePath", "Info", "IsDeleted", "ModifiedOn", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("11f5779b-49b1-43ac-9153-5fa72d810b34"), "16 Hertsmere Road, London, E14 4AX", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8608), null, "https://www.drakeandmorgan.co.uk/the-sipping-room/wp-content/uploads/sites/29/2018/06/sipping-room-e1528980994126.jpg", "An escape from the everyday, The Sipping Room specialises in thoughtful, inspired menus, locally sourced ingredients, and innovative, handcrafted cocktails. Retreat from the world while you enjoy our unrivalled service in the most welcoming environment. Our stylish outdoor terrace provides the perfect alfresco respite throughout the seasons.", false, null, "The Sipping Room", "020 3907 0320" },
                    { new Guid("363ea8fb-60f3-4aa2-8041-394520e3fb41"), "5-16 Gerrard Street, London, W1D 6JE", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8599), null, "https://static.designmynight.com/uploads/2018/01/0033-optimised.jpg", "Opium Cocktail Bar & Dim Sum Parlour is a chic, hidden venue in the heart of Chinatown. Run by experienced London bar moguls Dre Masso and Eric Yu, Opium certainly has some pedigree behind it. The decor is oriental themed but again keeps an element of freshness with a twist that makes it modern and current; metal finishes on miss-matched Chinese furniture gives Opium, a contemporary London feel that is very welcome. Expect 3 bars of amazing Asian cocktails and a selection of dim sum - just a little teaser to get your appetite going.", false, null, "Opium", "020 7734 7276" },
                    { new Guid("69ce843b-97c4-4164-8ba0-c8ca4ef02cf4"), "The Park Pavilion, London, E14 5FW", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8591), null, "https://i.pinimg.com/originals/6e/27/09/6e27095874a8a710ddb2a93adfa8de4e.jpg", "Located in the Park Pavilion on Canada Square, Canary Wharf, The Parlour is a striking & innovative all-day bar with style, substance & seasonally tempting drinks and food. A secret garden-inspired lounge with timber panelling is a must for cocktail lovers & perfect for pre- or post-dinner drinks, whilst the mixology table is ideal for those who want to mix & muddle for themselves. A stunning alfresco terrace, complete with its own bar provides the perfect playpen for those wanting to soak up the sun.", false, null, "The Parlour", "0207 715 9551" },
                    { new Guid("19fb565f-df92-4f81-ac42-bc256d10469a"), "1a Principal Place, Worship Street, London, EC2A 2BA", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(3533), null, "https://static.designmynight.com/uploads/2017/10/TheAllegory054-optimised.jpg", "In the heart of Shoreditch's happening hub, Principal Place, The Allegory is an everyday escape in London’s buzzing unsquare mile. Linger over a long brunch before finishing with an espresso martini.Enjoy quick catch-ups over pastries and freshly ground coffee, wholesome sharing platters and creative cocktails with colleagues; these one-of-a-kind experiences will be found at The Allegory. With a beautiful alfresco terrace, large open plan bar and cosy candlelit corners, this is a destination you'll want to return to again and again.", false, null, "The Allegory", "020 3948 9810" },
                    { new Guid("64c6592d-2c7b-42c4-b18d-5ac34bdc39d1"), "6 Pancras Square, London, N1C 4AG", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8575), null, "https://static.designmynight.com/uploads/2015/09/DM-KX-bar-area-optimised.jpg", "Our flagship bar & restaurant located in the heart of London's most exciting dining destination with two floors, two show stopping bars, an open kitchen and an extensive year-round outside space. Perfect for alfresco dining and drinks in the sun. Drake & Morgan at King's Cross offers a relaxed drinking and dining space in a beautiful setting. Open from an early morning until late evening, it's perfect for every occasion - from a business meeting and working lunch to a romantic dinner or after work drinks.", false, null, "Drake & Morgan", "0203 826 4870" },
                    { new Guid("84552d70-e670-4391-bd7b-1b54b0282b61"), "9 Cabot Square, London, E14 4EB", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8583), null, "https://static.designmynight.com/uploads/2016/12/DM-Pagin_0016-copy-optimised.jpg", "Perfectly positioned on the riverside in Canary Wharf, next to the bridge leading over to West India Quay, The Pagination is the perfect antidote to busy London life. With industrial inspired details, exposed metals, and soft handwoven textures, it offers a sanctuary, day or night and the expansive terrace offers alfresco drinking and dining in both the warmer months and the colder due to the abundance of blankets and hot water bottles to keep you snug.", false, null, "The Pagination", "020 7512 0397" },
                    { new Guid("6c296d75-bcd2-4711-9a28-6d0b6bcaa34b"), "52 Holborn Viaduct, London, EC1A 2FD", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8545), null, "https://s3.amazonaws.com/VenueFixer/venue_images/images/000/002/460/original/The-Fable0028.jpg?1412868965", "Inspired by the fantasy world of fairy tales and Aesop's fables, The Fable near Holborn Viaduct in central London, is anything but ordinary. From the vintage typewriter, to the leather bound books, every detail tells a story. Whether you visit for crafted cocktails, a morning latte & eggs Benedict or dinner at dusk, expect to be entranced, enthralled and enchanted.", false, null, "The Fable", "0207 651 4940" },
                    { new Guid("49b52950-a806-4075-8260-9ba0bdc1ab06"), "1 Ropemaker Street, London, EC2Y 9HT", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8497), null, "https://static.designmynight.com/uploads/2016/12/DM006-min-optimised.jpg", "Statement wallpapers and furniture are complemented by soft lighting and cosy faux fur to create your uber chic, contemporary bar - The Refinery CityPoint. The all-day dining bar & restaurant features a private dining room, sunken lounge and alfresco terrace with a pizza oven in the summer. It suits all occasions from early morning breakfasts right through to late night drinks. Make the most of our set menus for larger groups, or pre order packages when you want a selection of nibbles to eat!", false, null, "The Refinery", "020 7382 0606" },
                    { new Guid("4b6b8336-28df-48b2-a1fd-bade70b94eb6"), "Devonshire Terrace, Devonshire Square, London, EC2M 4WY", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8556), null, "https://hirespace.imgix.net/spaces/7065/rdpcg53tvbj.jpg?h=1080&w=1920&auto=format&fit=crop&q=40", "In the heart of the peaceful Devonshire Square, moments from Liverpool Street Station, Devonshire Terrace is your everyday escape from the hustle and bustle of City life. From quick catch-ups over freshly ground coffee to relaxing after work cocktails in one of our many gorgeous spaces, sit back and relax and we'll take care of the rest. No need to wait for the warmer months to drink and dine alfresco, enjoy our all year round terrace with its beautiful glass domed roof to protect you from the elements.", false, null, "Devonshire Terrace", "020 7256 3233" },
                    { new Guid("93532afb-fe74-43be-88a4-1c6948c634b8"), "58 Gresham Street, London, EC2V 7BB", new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(8566), null, "https://hirespace.imgix.net/spaces/4317/dmtrdvkicpx.jpg?h=1080&w=1920&auto=format&fit=crop&q=40", "Located right in the heart of the City, The Anthologist is the ideal backdrop for all your drink and food needs, from breakfast meetings to client updates over lunch, after work drinks or dinner with friends. Sample new wines or vintages from across the globe, a unique range of innovative cocktails and relaxed all - day dining fare.", false, null, "The Anthologist", "0207 726 8711" }
                });

            migrationBuilder.InsertData(
                table: "Cocktails",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ImagePath", "Info", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("e9b10c8b-46ed-45a3-9c66-a2c92d74e112"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1718), null, "https://i2-prod.mirror.co.uk/incoming/article14984511.ece/ALTERNATES/s1200b/0_JAJ_TEM_100419Martini_002JPG.jpg", "This easy passion fruit cocktail is bursting with zingy flavours and is perfect for celebrating with friends. Top with prosecco for a special tipple", false, null, "Passion fruit martini" },
                    { new Guid("3ebcd248-c91b-45e4-8900-90748828cc67"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1766), null, "http://d1fm27ee7pjs8v.cloudfront.net/images/detailed/68/Raspberry_Gin.jpg", "Preserve the taste of summer in a bottle with this raspberry gin, perfect topped up with tonic. The gin will keep its lovely pink hue for a few months", false, null, "Raspberry gin" },
                    { new Guid("c2acdffb-a172-4b52-9316-8a5bbe02c16d"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1777), null, "https://keyassets-p2.timeincuk.net/wp/prod/wp-content/uploads/sites/53/2018/06/sex-on-the-beach-recipe.jpg", "Combine vodka with peach schnapps and cranberry juice to make a classic sex on the beach cocktail. Garnish with cocktail cherries and orange slices.", false, null, "Sex on the beach cocktail" },
                    { new Guid("1686f69d-baaf-4423-a857-4b4d1684496f"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1847), null, "https://assets.epicurious.com/photos/5d1a68edbd7b210008b3e752/5:4/w_3700,h_2960,c_limit/IcedTea_HERO_062719_105.jpg", "Blend pink gin with iced tea and you have this unique cocktail, made with spiced rum, elderflower and pink grapefruit. Serve in a jug for a sharing cocktail.", false, null, "Pink gin iced tea" },
                    { new Guid("1dc069f0-0caa-4b64-82c0-66a744607ab1"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1858), null, "https://www.simplyrecipes.com/wp-content/uploads/2017/07/long-island-ice-tea-horiz-a-1800.jpg", "Mix a jug of this classic cocktail for a summer party. It's made with equal parts of vodka, gin, tequila, rum and triple sec, plus lime, cola and plenty of ice.", false, null, "Long Island iced tea" },
                    { new Guid("2b113d19-305b-43f0-b149-44e86e7f8308"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1867), null, "https://d1doqjmisr497k.cloudfront.net/-/media/mccormick-us/recipes/old-bay/o/2000/old_bay_summer_michelada_2000x1125.jpg?vd=20181011T160350Z&hash=50FB5D8483A2C3F0E67C89EFEAC4025B423348E9", "Cold lager, chilli powder, pepper and lime: spice up your lager with this Mexican cocktail, popular throughout Latin America and great for a summer party.", false, null, "Michelada" },
                    { new Guid("323bc1dd-c842-4689-bc7b-2953787a3129"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1875), null, "https://101recipes.com/wp-content/uploads/2019/04/Easy-White-Wine-Sangria-Recipe-2.jpg", "Try this refreshing twist on a traditional sangria and use white wine instead of red with elderflower to complement the fruit. Perfect for summer parties.", false, null, "White wine sangria" },
                    { new Guid("b725576e-25cb-4d3e-b661-68e6df37e2f7"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1883), null, "http://toast.life/media/767804/Bucks-Fizz.jpg", "The simple and classic combination of orange juice and champagne makes a perfect cocktail for a celebratory brunch or party", false, null, "Bucks fizz" },
                    { new Guid("86a1c72c-4daf-4e26-a483-67b569b5271f"), new DateTime(2019, 11, 6, 13, 42, 14, 791, DateTimeKind.Utc).AddTicks(1892), null, "https://cdn.bmstores.co.uk/images/hpcRecipe/imgMain/blog-festivecocktails-051217-2.jpg", "This bittersweet fruity vodka is best served well chilled in shot glasses. It can also be made with other berries like blackcurrants or strawberries.", false, null, "Cranberry vodka" },
                    { new Guid("24e8a669-799a-48b7-bc6f-b6884a33d0c7"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(6180), null, "https://i1.wp.com/gigglesgobblesandgulps.com/wp-content/uploads/2018/06/cosmopolitan-cocktail-recipe.jpg?ssl=1", "Lipsmackingly sweet-and-sour, the Cosmopolitan cocktail of vodka, cranberry, orange liqueur and citrus is a good time in a glass. Perfect for a party.", false, null, "Cosmopolitan cocktail" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "IsDeleted", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { new Guid("e05c419e-2f82-425c-b769-2842179d7e85"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3635), null, false, null, "Prosecco" },
                    { new Guid("12b1f952-2e1f-4758-9e80-1c1efba96cc5"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3641), null, false, null, "Raspberries" },
                    { new Guid("7982b0ce-b116-44dd-85a8-06e66df516cc"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3648), null, false, null, "Strawberries" },
                    { new Guid("145fd313-ef00-4003-b95a-a5e6558f255d"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3654), null, false, null, "Peach schnapps" },
                    { new Guid("2d5dd44c-41d7-43d0-9139-81aa791089a3"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3681), null, false, null, "Apples" },
                    { new Guid("30743785-290f-460c-b16e-6fc992b44366"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3668), null, false, null, "Cherries" },
                    { new Guid("cce2c9a4-e14a-4cf9-83ad-111fa4a19eaf"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3674), null, false, null, "Olives" },
                    { new Guid("ad6430e6-0b4b-49aa-b412-7d6f6ddbe5b0"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3687), null, false, null, "Kiwis" },
                    { new Guid("5495d66c-52ff-43f3-b26d-1b5279e07767"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3629), null, false, null, "Sugar syrup" },
                    { new Guid("47cac5ab-7071-46c9-bfbb-0f6ffaff0f0b"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3661), null, false, null, "Oranges" },
                    { new Guid("b988748e-cd6a-48da-aa36-20773102e426"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3622), null, false, null, "Passoa" },
                    { new Guid("7f5402b0-2136-4abb-b809-86c1cb502f62"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3578), null, false, null, "Ginger ale" },
                    { new Guid("99667755-0b3c-4746-89da-9d8dd7a6169a"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3608), null, false, null, "Cranberry juice" },
                    { new Guid("2d403bd0-6656-48a3-80a5-1910f2526d94"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3603), null, false, null, "Triple sec" },
                    { new Guid("59bd8032-57f2-4193-8d40-c805ec7d6122"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3597), null, false, null, "Cinnamon Syrup" },
                    { new Guid("dd3b4dcd-1e23-4b02-bdf1-859d892a7d89"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3591), null, false, null, "Lemon Sour" },
                    { new Guid("f9d9ac89-7c03-4a41-8a1a-b69262f89e16"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3584), null, false, null, "Club soda" },
                    { new Guid("730bcb1e-ed31-4600-9e42-7019898154b5"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3571), null, false, null, "Cola" },
                    { new Guid("0303b014-79b5-4044-9994-85ac83f293fc"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3564), null, false, null, "Cointreau" },
                    { new Guid("4f036905-92af-4b1b-8879-41b0fa8f1020"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3555), null, false, null, "Tequila" },
                    { new Guid("8b4157a7-49f0-4487-b800-c569c9ec7dd6"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3537), null, false, null, "Vodka" },
                    { new Guid("af31c27c-d4e5-4d19-8304-2c649adb2f49"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3507), null, false, null, "Gin" },
                    { new Guid("91767830-fb0e-4e77-a93a-d01eb2520553"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(1020), null, false, null, "Whisky" },
                    { new Guid("5998858c-01e8-41d2-8116-798649a2763f"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3615), null, false, null, "Lime wedge" },
                    { new Guid("f97a5f83-f9da-43a3-bef9-67091533ccc9"), new DateTime(2019, 11, 6, 13, 42, 14, 790, DateTimeKind.Utc).AddTicks(3547), null, false, null, "Rum" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("7bd06fe6-79ca-43a1-862b-446a1466bb93"), new Guid("297d06e6-c058-486f-a18a-06a971ebfcd7") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bans_UserId",
                table: "Bans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BarCocktails_BarId",
                table: "BarCocktails",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_BarComments_UserId",
                table: "BarComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BarRatings_UserId",
                table: "BarRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComments_UserId",
                table: "CocktailComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredient_CocktailId",
                table: "CocktailIngredient",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailRatings_UserId",
                table: "CocktailRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bans");

            migrationBuilder.DropTable(
                name: "BarCocktails");

            migrationBuilder.DropTable(
                name: "BarComments");

            migrationBuilder.DropTable(
                name: "BarRatings");

            migrationBuilder.DropTable(
                name: "CocktailComments");

            migrationBuilder.DropTable(
                name: "CocktailIngredient");

            migrationBuilder.DropTable(
                name: "CocktailRatings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bars");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
