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

        Task<IEnumerable<IQuestion>> GetQuestionsAsync( QuezCategory TestType);

        Task GetQuezResultAsync(IQuezQuestion contrl);

        Task<TimeSpan> GetExamDate(QuezCategory TestType);

        Task<string> GetDiploma();

        Task<IQuisResultInfo> GetQuisResult();

        Task<int> GetSmsCodeAsync(int ParrentId);

        Task<string> GetEgmuAsync(int TestId);

        Task<bool> CheckSmsCode(string SmsCode);
    }
}
