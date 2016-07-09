using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
