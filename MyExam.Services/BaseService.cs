using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExam.Services
{
    class BaseService<T> where T : BaseEntity
    {
        private MyDbContext ctx;
        public BaseService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(e => e.IsDeleted == false).OrderByDescending(e => e.CreateDateTime);
        }

        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            return GetAll().Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 查找id=id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }
        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }

    }
}
