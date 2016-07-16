using _20125075_ISC_415_AsignacionIIF.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Infrastructure
{
    public class DataContext: DbContext
    {
        public DataContext() : base()
        {

        }

        public DbSet<Message> Messages { get; set; }
    }
} 
