/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace NSMBe4.Patcher
{
    class PatchCompiler
    {

        public static int compilePatch(uint destAddr, DirectoryInfo romDir)
        {
            return runProcess("make CODEADDR=0x" + destAddr.ToString("X8"), romDir.FullName);
        }

        public static int cleanPatch(DirectoryInfo romDir)
        {
            return runProcess("make clean", romDir.FullName);
        }

        public static int runProcess(string proc, string cwd)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "/C " + proc + " || pause";
            info.CreateNoWindow = false;
            info.UseShellExecute = false;
            info.WorkingDirectory = cwd;

            Process p = Process.Start(info);
            p.WaitForExit();
            return p.ExitCode;
        }
    }
}
