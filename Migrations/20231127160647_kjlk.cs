using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Nompilo_Healthcare_system.Migrations
{
    public partial class kjlk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alert_v",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastView = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntendedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleController = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert_v", x => x.AlertID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_AccountCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Get_Time",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Get_Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Get_Time", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "In_Touch_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Names = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_In_Touch_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Information",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contraceptives = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pre_Natal_Care_Alert",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastView = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntendedUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pre_Natal_Care_Alert", x => x.AlertID);
                });

            migrationBuilder.CreateTable(
                name: "Resourse",
                columns: table => new
                {
                    InformationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContraceptiveMethods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Effectiveness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthBenefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reversibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailabilityandAccess = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resourse", x => x.InformationID);
                });

            migrationBuilder.CreateTable(
                name: "Resourses",
                columns: table => new
                {
                    InformationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContraceptiveMethods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Effectiveness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthBenefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SideEffects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reversibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailabilityandAccess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resourses", x => x.InformationID);
                });

            migrationBuilder.CreateTable(
                name: "selfDiagnos",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selfDiagnos", x => x.DiagnosisId);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine_Information",
                columns: table => new
                {
                    InformationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose_Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine_Information", x => x.InformationID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "add_Medications",
                columns: table => new
                {
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveIngredient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    DosageForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_add_Medications", x => x.MedicationId);
                    table.ForeignKey(
                        name: "FK_add_Medications_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecStatus = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    DateofAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultingPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfAppointment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstTimeVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "ContraceptivesRefill",
                columns: table => new
                {
                    RefillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraceptiveType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRefillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactNumber = table.Column<int>(type: "int", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContraceptivesRefill", x => x.RefillID);
                    table.ForeignKey(
                        name: "FK_ContraceptivesRefill_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Counselling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counselling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counselling_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fam_Screening",
                columns: table => new
                {
                    ScreeningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question1 = table.Column<int>(type: "int", nullable: false),
                    Question2 = table.Column<int>(type: "int", nullable: false),
                    Question3 = table.Column<int>(type: "int", nullable: false),
                    Question4 = table.Column<int>(type: "int", nullable: false),
                    Question5 = table.Column<int>(type: "int", nullable: false),
                    Question6 = table.Column<int>(type: "int", nullable: false),
                    Question7 = table.Column<int>(type: "int", nullable: false),
                    Question8 = table.Column<int>(type: "int", nullable: false),
                    Question9 = table.Column<int>(type: "int", nullable: false),
                    Question10 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fam_Screening", x => x.ScreeningID);
                    table.ForeignKey(
                        name: "FK_Fam_Screening_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fetal_Monitoring",
                columns: table => new
                {
                    FetalMonitorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question1 = table.Column<int>(type: "int", nullable: false),
                    Question2 = table.Column<int>(type: "int", nullable: false),
                    Question3 = table.Column<int>(type: "int", nullable: false),
                    Question4 = table.Column<int>(type: "int", nullable: false),
                    Question5 = table.Column<int>(type: "int", nullable: false),
                    Question6 = table.Column<int>(type: "int", nullable: false),
                    Question7 = table.Column<int>(type: "int", nullable: false),
                    Question8 = table.Column<int>(type: "int", nullable: false),
                    Question9 = table.Column<int>(type: "int", nullable: false),
                    Question10 = table.Column<int>(type: "int", nullable: false),
                    Question11 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fetal_Monitoring", x => x.FetalMonitorID);
                    table.ForeignKey(
                        name: "FK_Fetal_Monitoring_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Get_Vaccinateds",
                columns: table => new
                {
                    VaccineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RecStatus = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    VaccineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Get_Vaccinateds", x => x.VaccineId);
                    table.ForeignKey(
                        name: "FK_Get_Vaccinateds_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "medFile",
                columns: table => new
                {
                    PatientMedicalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinRelationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinPostalcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakingMed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalInfor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HealthcareSystemUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medFile", x => x.PatientMedicalId);
                    table.ForeignKey(
                        name: "FK_medFile_AspNetUsers_HealthcareSystemUserId",
                        column: x => x.HealthcareSystemUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_medFile_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pre_NatalCareNutritionTracking",
                columns: table => new
                {
                    NutritionTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question1 = table.Column<int>(type: "int", nullable: false),
                    Question2 = table.Column<int>(type: "int", nullable: false),
                    Question3 = table.Column<int>(type: "int", nullable: false),
                    Question4 = table.Column<int>(type: "int", nullable: false),
                    Question5 = table.Column<int>(type: "int", nullable: false),
                    Question6 = table.Column<int>(type: "int", nullable: false),
                    Question7 = table.Column<int>(type: "int", nullable: false),
                    Question8 = table.Column<int>(type: "int", nullable: false),
                    Question9 = table.Column<int>(type: "int", nullable: false),
                    Question10 = table.Column<int>(type: "int", nullable: false),
                    Question11 = table.Column<int>(type: "int", nullable: false),
                    Question12 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pre_NatalCareNutritionTracking", x => x.NutritionTrackingID);
                    table.ForeignKey(
                        name: "FK_Pre_NatalCareNutritionTracking_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "referrals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificConcerns = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referrals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_referrals_AspNetUsers_Patient",
                        column: x => x.Patient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refillrequests",
                columns: table => new
                {
                    RefillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosageForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Statuss = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refillrequests", x => x.RefillId);
                    table.ForeignKey(
                        name: "FK_refillrequests_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestContraceptivesInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contraceptivetype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestContraceptivesInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestContraceptivesInfo_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "requestPrescrips",
                columns: table => new
                {
                    PrescripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentSymptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChronicConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requestPrescrips", x => x.PrescripId);
                    table.ForeignKey(
                        name: "FK_requestPrescrips_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reviews = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ultra_Sound_Image",
                columns: table => new
                {
                    NutritionTrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question1 = table.Column<int>(type: "int", nullable: false),
                    Question2 = table.Column<int>(type: "int", nullable: false),
                    Question3 = table.Column<int>(type: "int", nullable: false),
                    Question4 = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ultra_Sound_Image", x => x.NutritionTrackingID);
                    table.ForeignKey(
                        name: "FK_Ultra_Sound_Image_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "vaccine_Reportings",
                columns: table => new
                {
                    Vaccination_FeddbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    VaccineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccinationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientFeedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccine_Reportings", x => x.Vaccination_FeddbackId);
                    table.ForeignKey(
                        name: "FK_vaccine_Reportings_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VaccineScreening",
                columns: table => new
                {
                    ScreeningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cough = table.Column<bool>(type: "bit", nullable: false),
                    Dizzines = table.Column<bool>(type: "bit", nullable: false),
                    Abdominalpain = table.Column<bool>(type: "bit", nullable: false),
                    Headache = table.Column<bool>(type: "bit", nullable: false),
                    legswelling = table.Column<bool>(type: "bit", nullable: false),
                    snezzing = table.Column<bool>(type: "bit", nullable: false),
                    ChestPressure = table.Column<bool>(type: "bit", nullable: false),
                    Allergy = table.Column<bool>(type: "bit", nullable: false),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakenVac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GetDateSurvay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineScreening", x => x.ScreeningID);
                    table.ForeignKey(
                        name: "FK_VaccineScreening_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "illnessTreatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Illness_Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_illnessTreatments", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_illnessTreatments_selfDiagnos_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "selfDiagnos",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "selfHistories",
                columns: table => new
                {
                    SelfDigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GetDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DiagnosisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selfHistories", x => x.SelfDigId);
                    table.ForeignKey(
                        name: "FK_selfHistories_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_selfHistories_selfDiagnos_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "selfDiagnos",
                        principalColumn: "DiagnosisId");
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosageamount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePrescribed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrescriptionExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescripId = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_prescriptions_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prescriptions_requestPrescrips_PrescripId",
                        column: x => x.PrescripId,
                        principalTable: "requestPrescrips",
                        principalColumn: "PrescripId");
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations_Comments",
                columns: table => new
                {
                    VaccinationCommentsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vaccination_FeddbackId = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations_Comments", x => x.VaccinationCommentsId);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Comments_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vaccinations_Comments_vaccine_Reportings_Vaccination_FeddbackId",
                        column: x => x.Vaccination_FeddbackId,
                        principalTable: "vaccine_Reportings",
                        principalColumn: "Vaccination_FeddbackId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_add_Medications_Id",
                table: "add_Medications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Id",
                table: "Appointments",
                column: "Id");

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
                name: "IX_ContraceptivesRefill_PatientID",
                table: "ContraceptivesRefill",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Counselling_PatientID",
                table: "Counselling",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Fam_Screening_PatientID",
                table: "Fam_Screening",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Fetal_Monitoring_PatientID",
                table: "Fetal_Monitoring",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Get_Vaccinateds_Id",
                table: "Get_Vaccinateds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_illnessTreatments_DiagnosisId",
                table: "illnessTreatments",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_medFile_HealthcareSystemUserId",
                table: "medFile",
                column: "HealthcareSystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_medFile_Id",
                table: "medFile",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pre_NatalCareNutritionTracking_PatientID",
                table: "Pre_NatalCareNutritionTracking",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_Id",
                table: "prescriptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_PrescripId",
                table: "prescriptions",
                column: "PrescripId");

            migrationBuilder.CreateIndex(
                name: "IX_referrals_Patient",
                table: "referrals",
                column: "Patient");

            migrationBuilder.CreateIndex(
                name: "IX_refillrequests_Id",
                table: "refillrequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestContraceptivesInfo_PatientID",
                table: "RequestContraceptivesInfo",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_requestPrescrips_Id",
                table: "requestPrescrips",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_PatientID",
                table: "Review",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_selfHistories_DiagnosisId",
                table: "selfHistories",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_selfHistories_Id",
                table: "selfHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ultra_Sound_Image_PatientID",
                table: "Ultra_Sound_Image",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_Comments_Id",
                table: "Vaccinations_Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_Comments_Vaccination_FeddbackId",
                table: "Vaccinations_Comments",
                column: "Vaccination_FeddbackId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccine_Reportings_Id",
                table: "vaccine_Reportings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineScreening_Id",
                table: "VaccineScreening",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "add_Medications");

            migrationBuilder.DropTable(
                name: "Alert_v");

            migrationBuilder.DropTable(
                name: "Appointments");

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
                name: "ContraceptivesRefill");

            migrationBuilder.DropTable(
                name: "Counselling");

            migrationBuilder.DropTable(
                name: "Fam_Screening");

            migrationBuilder.DropTable(
                name: "Fetal_Monitoring");

            migrationBuilder.DropTable(
                name: "Get_Time");

            migrationBuilder.DropTable(
                name: "Get_Vaccinateds");

            migrationBuilder.DropTable(
                name: "illnessTreatments");

            migrationBuilder.DropTable(
                name: "In_Touch_Contacts");

            migrationBuilder.DropTable(
                name: "Information");

            migrationBuilder.DropTable(
                name: "medFile");

            migrationBuilder.DropTable(
                name: "Pre_Natal_Care_Alert");

            migrationBuilder.DropTable(
                name: "Pre_NatalCareNutritionTracking");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "referrals");

            migrationBuilder.DropTable(
                name: "refillrequests");

            migrationBuilder.DropTable(
                name: "RequestContraceptivesInfo");

            migrationBuilder.DropTable(
                name: "Resourse");

            migrationBuilder.DropTable(
                name: "Resourses");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "selfHistories");

            migrationBuilder.DropTable(
                name: "Ultra_Sound_Image");

            migrationBuilder.DropTable(
                name: "Vaccinations_Comments");

            migrationBuilder.DropTable(
                name: "Vaccine_Information");

            migrationBuilder.DropTable(
                name: "VaccineScreening");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "requestPrescrips");

            migrationBuilder.DropTable(
                name: "selfDiagnos");

            migrationBuilder.DropTable(
                name: "vaccine_Reportings");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
