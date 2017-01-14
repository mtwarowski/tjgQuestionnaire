using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PairingTest.Web.Helpers
{
    public interface IAppConfiguration
    {
        string QuestionnaireUrl { get; }
    }
}