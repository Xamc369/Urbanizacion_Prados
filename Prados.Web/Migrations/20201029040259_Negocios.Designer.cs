// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prados.Web.Data;

namespace Prados.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201029040259_Negocios")]
    partial class Negocios
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Aniostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ani_Descripcion");

                    b.Property<int?>("ContabilidadtblId");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.ToTable("Aniostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Contabilidadtbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnioId");

                    b.Property<int?>("EgrId");

                    b.Property<int?>("MessId");

                    b.Property<int?>("PuntId");

                    b.Property<int?>("TipId");

                    b.Property<int?>("ValoId");

                    b.HasKey("Id");

                    b.HasIndex("AnioId");

                    b.HasIndex("EgrId");

                    b.HasIndex("MessId");

                    b.HasIndex("PuntId");

                    b.HasIndex("TipId");

                    b.HasIndex("ValoId");

                    b.ToTable("Contabilidadtbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Egresostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContabilidadtblId");

                    b.Property<string>("Egr_Descripcion");

                    b.Property<DateTime>("Egr_FechadePago");

                    b.Property<string>("Egr_Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.ToTable("Egresostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Ingresostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Ing_FechadePago");

                    b.Property<int>("Ing_IngId");

                    b.Property<double>("Ing_Sede");

                    b.Property<int?>("PagosContId");

                    b.Property<int?>("TipId");

                    b.Property<int?>("ValId");

                    b.HasKey("Id");

                    b.HasIndex("PagosContId");

                    b.HasIndex("TipId");

                    b.HasIndex("ValId");

                    b.ToTable("Ingresostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Managerstbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Managerstbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.MarcasAutostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Mar_Descripcion")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("MarcasAutostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Mesestbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContabilidadtblId");

                    b.Property<string>("Mes_Descripcion");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.ToTable("Mesestbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Negociostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Neg_Descripcion");

                    b.Property<string>("Neg_Direccion");

                    b.Property<string>("Neg_Estado")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("Neg_FechaCreacion");

                    b.Property<string>("Neg_Nombre");

                    b.Property<string>("Neg_Telefono");

                    b.Property<int?>("PropietariosId");

                    b.HasKey("Id");

                    b.HasIndex("PropietariosId");

                    b.ToTable("Negociostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Noticiastbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Not_Autor");

                    b.Property<string>("Not_Descripcion");

                    b.Property<string>("Not_Estado")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("Not_Fecha");

                    b.Property<string>("Not_Titulo");

                    b.HasKey("Id");

                    b.ToTable("Noticiastbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Pagostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnioId");

                    b.Property<int?>("MesId");

                    b.Property<string>("PAG_ESTADO")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("PAG_FECHACREACION");

                    b.Property<DateTime>("PAG_FECHAPAGADO");

                    b.Property<int?>("PropietarioId");

                    b.Property<int?>("PuntodePagoId");

                    b.Property<int?>("TiposId");

                    b.Property<int?>("ValId");

                    b.HasKey("Id");

                    b.HasIndex("AnioId");

                    b.HasIndex("MesId");

                    b.HasIndex("PropietarioId");

                    b.HasIndex("PuntodePagoId");

                    b.HasIndex("TiposId");

                    b.HasIndex("ValId");

                    b.ToTable("Pagostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Productostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NegocioId");

                    b.Property<string>("Pro_Estado")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("Pro_FechaCreacion");

                    b.Property<string>("Pro_Nombre");

                    b.Property<string>("Pro_Precio");

                    b.HasKey("Id");

                    b.HasIndex("NegocioId");

                    b.ToTable("Productostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Propietariostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TiposViviendatblId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TiposViviendatblId");

                    b.HasIndex("UserId");

                    b.ToTable("Propietariostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.PuntosPagotbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContabilidadtblId");

                    b.Property<string>("Pun_Descripcion");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.ToTable("PuntosPagotbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Saldostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Saldostbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.TiposPagotbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContabilidadtblId");

                    b.Property<int?>("IngresostblId");

                    b.Property<string>("Tip_Descripcion");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.HasIndex("IngresostblId");

                    b.ToTable("TiposPagotbl");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.TiposViviendatbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tip_Descripcion");

                    b.HasKey("Id");

                    b.ToTable("TiposViviendatbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Userstbl", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Pro_Apellidos")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Pro_Identificacion")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Pro_Lote")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("Pro_Nombres")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Pro_Observaciones");

                    b.Property<string>("Pro_Telefono")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Pro_TipoIdentificacion")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<int?>("Pro_TipoViviendaId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("Pro_TipoViviendaId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Valorestbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContabilidadtblId");

                    b.Property<int?>("IngresostblId");

                    b.Property<string>("Val_Estado")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("Val_FechaCreacion");

                    b.Property<string>("Val_Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContabilidadtblId");

                    b.HasIndex("IngresostblId");

                    b.ToTable("Valorestbls");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Vehiculostbl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("IngresostblId");

                    b.Property<int?>("MarcasAutostblId");

                    b.Property<int?>("PropietarioId");

                    b.Property<DateTime>("Veh_Born");

                    b.Property<string>("Veh_Codigo")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Veh_Detalles");

                    b.Property<string>("Veh_Estado");

                    b.Property<string>("Veh_Placa")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.HasIndex("IngresostblId");

                    b.HasIndex("MarcasAutostblId");

                    b.HasIndex("PropietarioId");

                    b.ToTable("Vehiculostbls");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Userstbl")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Userstbl")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Prados.Web.Data.Entities.Userstbl")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Userstbl")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Aniostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Anios")
                        .HasForeignKey("ContabilidadtblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Contabilidadtbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Aniostbl", "Anio")
                        .WithMany()
                        .HasForeignKey("AnioId");

                    b.HasOne("Prados.Web.Data.Entities.Egresostbl", "Egr")
                        .WithMany()
                        .HasForeignKey("EgrId");

                    b.HasOne("Prados.Web.Data.Entities.Mesestbl", "Mess")
                        .WithMany()
                        .HasForeignKey("MessId");

                    b.HasOne("Prados.Web.Data.Entities.PuntosPagotbl", "Punt")
                        .WithMany()
                        .HasForeignKey("PuntId");

                    b.HasOne("Prados.Web.Data.Entities.TiposPagotbl", "Tip")
                        .WithMany()
                        .HasForeignKey("TipId");

                    b.HasOne("Prados.Web.Data.Entities.Valorestbl", "Valo")
                        .WithMany()
                        .HasForeignKey("ValoId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Egresostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Egresos")
                        .HasForeignKey("ContabilidadtblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Ingresostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Pagostbl", "PagosCont")
                        .WithMany()
                        .HasForeignKey("PagosContId");

                    b.HasOne("Prados.Web.Data.Entities.TiposPagotbl", "Tip")
                        .WithMany()
                        .HasForeignKey("TipId");

                    b.HasOne("Prados.Web.Data.Entities.Valorestbl", "Val")
                        .WithMany()
                        .HasForeignKey("ValId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Managerstbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Userstbl", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Mesestbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Meses")
                        .HasForeignKey("ContabilidadtblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Negociostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Propietariostbl", "Propietarios")
                        .WithMany("Negocio")
                        .HasForeignKey("PropietariosId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Pagostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Aniostbl", "Anio")
                        .WithMany("Pagos")
                        .HasForeignKey("AnioId");

                    b.HasOne("Prados.Web.Data.Entities.Mesestbl", "Mes")
                        .WithMany("Pagos")
                        .HasForeignKey("MesId");

                    b.HasOne("Prados.Web.Data.Entities.Propietariostbl", "Propietario")
                        .WithMany("Pagos")
                        .HasForeignKey("PropietarioId");

                    b.HasOne("Prados.Web.Data.Entities.PuntosPagotbl", "PuntodePago")
                        .WithMany("Pagos")
                        .HasForeignKey("PuntodePagoId");

                    b.HasOne("Prados.Web.Data.Entities.TiposPagotbl", "Tipos")
                        .WithMany("Pagos")
                        .HasForeignKey("TiposId");

                    b.HasOne("Prados.Web.Data.Entities.Valorestbl", "Val")
                        .WithMany("Pagos")
                        .HasForeignKey("ValId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Productostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Negociostbl", "Negocio")
                        .WithMany("Producto")
                        .HasForeignKey("NegocioId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Propietariostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.TiposViviendatbl")
                        .WithMany("Propietarios")
                        .HasForeignKey("TiposViviendatblId");

                    b.HasOne("Prados.Web.Data.Entities.Userstbl", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.PuntosPagotbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Puntos")
                        .HasForeignKey("ContabilidadtblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.TiposPagotbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Tipos")
                        .HasForeignKey("ContabilidadtblId");

                    b.HasOne("Prados.Web.Data.Entities.Ingresostbl")
                        .WithMany("Ing_Tipos")
                        .HasForeignKey("IngresostblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Userstbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.TiposViviendatbl", "Pro_TipoVivienda")
                        .WithMany()
                        .HasForeignKey("Pro_TipoViviendaId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Valorestbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Contabilidadtbl")
                        .WithMany("Con_Valores")
                        .HasForeignKey("ContabilidadtblId");

                    b.HasOne("Prados.Web.Data.Entities.Ingresostbl")
                        .WithMany("Ing_Valores")
                        .HasForeignKey("IngresostblId");
                });

            modelBuilder.Entity("Prados.Web.Data.Entities.Vehiculostbl", b =>
                {
                    b.HasOne("Prados.Web.Data.Entities.Ingresostbl")
                        .WithMany("Veh_Ing")
                        .HasForeignKey("IngresostblId");

                    b.HasOne("Prados.Web.Data.Entities.MarcasAutostbl")
                        .WithMany("Vehiculos")
                        .HasForeignKey("MarcasAutostblId");

                    b.HasOne("Prados.Web.Data.Entities.Propietariostbl", "Propietario")
                        .WithMany("Vehiculos")
                        .HasForeignKey("PropietarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
