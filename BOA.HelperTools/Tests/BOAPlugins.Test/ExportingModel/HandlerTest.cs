﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BOAPlugins.ExportingModel
{
    [TestClass]
    public class HandlerTest
    {
        #region Public Methods
        [TestMethod]
        public void Handle()
        {
            var path   = @"D:\work\BOA.BusinessModules\Dev\BOA.CardGeneral.DebitCard\BOA.CardGeneral.DebitCard.sln";
            var result = Handler.Handle(path);

            Assert.IsNull(result.ErrorMessage);


            var result2 = MessagingExporter.ExportAsTypeScriptCode(path);
            Assert.IsNull(result2.ErrorMessage);

            var data = new ExportAsCSharpCodeData
            {
                solutionFilePath       = path,
                RemoveUnusedProperties = true
            };

            result2 = MessagingExporter.ExportAsCSharpCode(data);
            Assert.IsNull(result2.ErrorMessage);
        }
        #endregion
    }
}