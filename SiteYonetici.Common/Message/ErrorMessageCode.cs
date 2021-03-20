using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.Common.Message
{
    public enum ErrorMessageCode
    {
        CheckYourEposta = 101,
        EpostaOrPasswordError = 102,
        PasswordChangeOccurredError = 103,
        UserNotFound = 104,
        CouldNotSendPassword = 105,
    }
}
