using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Services
{
    class IGTradeService
    {
        #region Public Properties
        public HttpIGAccountService IGAccountService { get; set; }
        #endregion

        public IGTradeService() { }

        public IGTradeService(HttpIGAccountService httpIGAccountService)
        {
            IGAccountService = httpIGAccountService;
        }
    }
}
