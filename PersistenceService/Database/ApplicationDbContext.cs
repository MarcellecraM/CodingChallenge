using ParticipantService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace PersistenceService.Database
{

    public partial class ApplicationDbContext : DbContext
    {
        private const string DB_NAME = "MilitaryTrainigSimulator";
        static string dbServer = "localhost";
        static int dbPort = 1433;
        static string dataSource = $"{dbServer},{dbPort}";

        static SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
        {

            IntegratedSecurity = true,
            InitialCatalog = DB_NAME,
            DataSource = dataSource,
            Pooling = true
        };

        public ApplicationDbContext()
            : base(connectionStringBuilder.ToString())
        {

        }

        public DbSet<TrackedObjectSample> TrackedObjectSamples { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure an index
            //modelBuilder.Entity<TrackedObjectDAL>()
            //    .HasIndex(e => e.Timestamp);             
        }
    }
}
