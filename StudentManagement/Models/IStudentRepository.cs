using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 透過 Id 來取得學生資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        /// <summary>
        /// 取的所有學生資料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudents();

        /// <summary>
        /// 添加一名學生資料
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新一名學生資料
        /// </summary>
        /// <param name="updateStudent"></param>
        /// <returns></returns>
        Student Update(Student updateStudent);

        /// <summary>
        /// 透過 Id 來刪除一筆學生資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Delete(int id);

    }
}
