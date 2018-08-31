using DapperExtension.Core;
using Model;

namespace Repository
{
    public class FileRepo : BaseRepo<File>
    {
        public FileRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public void DeleteFilesOfProduct(int productId)
        {
            string sql = @" DELETE FROM dbo.[file] where productId =@productId ";

            DbManage.Execute(sql, new { productId });
        }
    }
}