﻿using System.ServiceProcess;

namespace GTPriceImporterService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new GTPriceServiceImport() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
