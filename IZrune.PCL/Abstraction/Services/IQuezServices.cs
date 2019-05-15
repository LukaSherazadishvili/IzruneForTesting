using IZrune.PCL.Abstraction.Models;
using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.Abstraction.Services
{
   public interface IQuezServices
    {

        Task<IEnumerable<IQuestion>> GetQuestionsAsync(int studentsId, QuezCategory TestType);

        Task GetQuezResultAsync( int Duration, List<IQuestion> QuestionList);
       
    }
}
