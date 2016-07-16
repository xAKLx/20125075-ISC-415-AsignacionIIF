using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using _20125075_ISC_415_AsignacionIIF.Infrastructure;

namespace _20125075ISC415AsignacionIIF.Migrations
{
    [DbContext(typeof(MessageContext))]
    partial class MessageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_20125075_ISC_415_AsignacionIIF.Core.Message", b =>
                {
                    b.Property<DateTime>("Date");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Receiver");

                    b.Property<string>("Sender");

                    b.HasKey("Date");

                    b.ToTable("Messages");
                });
        }
    }
}
