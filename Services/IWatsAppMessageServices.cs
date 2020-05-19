using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Services
{
 public interface IWatsAppMessageServices
 {
     void SendMessage(string message, string toNumber);
 }
}
