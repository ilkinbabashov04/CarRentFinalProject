﻿using Core.Entity.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class BaseProjectContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server = DESKTOP-V1MHL8O\MSSQLSERVER01;Database=CarRentalDatabase;Integrated Security=True;TrustServerCertificate=True");
		}

        public DbSet<About> Abouts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<CarDescription> CarDescriptions { get; set; }
		public DbSet<CarFeature> CarFeatures { get; set; }
		public DbSet<CarPricing> CarPricings { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Feature> Features { get; set; }
		public DbSet<FooterAddress> FooterAddresses { get; set; }
		public DbSet<Home> Homes { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Pricing> Pricings { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<SocialMedia> SocialMedias { get; set; }
		public DbSet<Testimonial> Testimonials { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Blog> Blogs { get; set; }
        public DbSet<TagCloud> TagClouds { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RentACar> RentACars { get; set; }
        public DbSet<RentACarProcess> RentACarProcesses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
		//public DbSet<User> Users { get; set; }
		//public DbSet<OperationClaims> OperationClaims { get; set; }
		//public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

	}
}
