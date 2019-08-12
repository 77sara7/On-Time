using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class RunImacro
    {

        private string content;
        public RunImacro(string content)
        {
            this.content = content;
        }
        public iMacros.Status run()
        {
            File.WriteAllText("mac.iim", content);
            int timeout = 60;
            iMacros.Status status;
            iMacros.AppClass app = new iMacros.AppClass();

            String result = "";

            status = app.iimInit("-V7", true, "", "", "", timeout);
            if (status != iMacros.Status.sOk) return status;
            result = result + "init " + Convert.ToString(status) + "; ";
            string macro = Directory.GetCurrentDirectory() + "\\mac.iim";
            status = app.iimDisplay("Interface version =\n" + app.iimGetInterfaceVersion().ToString(), timeout);
            if (status != iMacros.Status.sOk) return status;
            result = result + "display " + Convert.ToString(status) + "; ";

            status = app.iimPlay(macro, timeout);
            if (status != iMacros.Status.sOk) return status;
            result = result + "play " + Convert.ToString(status) + "; ";

            status = app.iimExit(timeout);
            if (status != iMacros.Status.sOk) return status;
            result = result + "exit " + Convert.ToString(status);
            return status;
        }

    }
}
