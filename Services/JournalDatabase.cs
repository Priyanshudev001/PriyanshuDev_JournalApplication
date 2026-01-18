using SQLite;
using Journal_Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Journal_Application.Services
{
    public class JournalDatabase
    {
        private SQLiteAsyncConnection database;

        private async Task Init()
        {
            if (database != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "journal.db3");
            database = new SQLiteAsyncConnection(dbPath);
            await database.CreateTableAsync<JournalEntry>();
        }

        public async Task<JournalEntry> GetEntryAsync(string dateKey)
        {
            await Init();
            return await database
                .Table<JournalEntry>()
                .Where(i => i.DateKey == dateKey)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveEntryAsync(JournalEntry entry)
        {
            await Init();
            var existing = await GetEntryAsync(entry.DateKey);

            if (existing == null)
                return await database.InsertAsync(entry);
            else
                return await database.UpdateAsync(entry);
        }

        public async Task<int> DeleteEntryAsync(string dateKey)
        {
            await Init();
            var entry = await GetEntryAsync(dateKey);
            if (entry != null)
                return await database.DeleteAsync(entry);
            return 0;
        }


        // ✅ THIS IS THE MISSING METHOD (VERY IMPORTANT)
        public async Task<List<JournalEntry>> GetAllEntriesAsync()
        {
            await Init();
            return await database
                .Table<JournalEntry>()
                .OrderByDescending(e => e.DateKey)
                .ToListAsync();
        }

    }
}
