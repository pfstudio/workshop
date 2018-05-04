using Microsoft.EntityFrameworkCore;
using workshop.Models;

namespace workshop.Data
{
    public class RecordDbContext: DbContext
    {
        // 调用父类构造
        public RecordDbContext(DbContextOptions<RecordDbContext> options)
            :base(options)
        {
        }
        // 建立数据集
        public DbSet<Record> Records { get; set; }
    }
}