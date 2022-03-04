using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  class CategoryManager
    {
        private readonly AgencyContext _agencyContext;

        public CategoryManager(AgencyContext agencyContext)
        {
            _agencyContext = agencyContext;
        }

        public List<Category> GetAll(Func<Category,bool> filter = null)
        {
            return _agencyContext.Categories
                .Include(c=>c.CategoryRecords)
                .Where(filter).ToList();
        }
    }
}
