using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NoMansSkyRecipies.Data;
using Serilog;

namespace NmsRecipes.DAL.Repositories
{
    public static class ServiceMethods
    {
        public static void DoInTransaction(RecipiesDbContext db, Action<RecipiesDbContext> expression)
        {
            using var transaction = db.Database.BeginTransaction();
            try
            {
                expression(db);
                transaction.Commit();
                db.SaveChanges();
            }
            catch (DbUpdateException updException)
            {
                transaction.Rollback();
                Log.Error(updException, "Exception occured while updating");
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Log.Error(e, "Exception occured while running transaction");
                throw;
            }
        }

    }
}
